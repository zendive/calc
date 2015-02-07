/**
 * Author: Alexander Block
 * Is a part of Calc application
 * developed for C# study goals and is an intelectual
 * property of his author.
*/

using System;
using System.Globalization;
using System.Collections.Specialized;

namespace Calc
{
  /// Commands like Clear-screen, passed to application level
  /// via throwing Calc.CommandException  with command and optional argument
  public enum AppCommand: int
  {
    Standby = 0,
    ClearResultScreen,
    ExitClientApplication,
    AboutCalcEngine,
    ChangeInputCS,
    ChangeResultCS
  };


  /// <summary>Calculation logic implementation</summary>
  public class CCalculator
  {
    /// <summary>
    /// CCalculator version string</summary>
    public readonly string m_sVersion = "0.1.0.0";

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    // Get Engine specific information
    public string AboutEngine
    {
      get
      {
        string str;
        int iBreakLine = 1;
        string nl = Environment.NewLine;

        str = "Algorithm engine version: " + m_sVersion + nl + nl;
        str += "(" + ((int)CMD._LAST + 1) + ") Commands:" + nl;
        for (CMD cmd = CMD._FIRST; cmd <= CMD._LAST; cmd++)
        {
          str += the(cmd) + "; ";

          if ((iBreakLine++ % 6) == 0)
          {
            str += nl;
          }
        }

        iBreakLine = 1;
        str += nl + nl + "(" + ((int)AO._LAST + 1) + ") Operations:" + nl;
        for (AO ao = AO._LAST; ao >= AO._FIRST; ao--)
        {
          str += the(ao) + "; ";

          if ((iBreakLine++ % 6) == 0)
          {
            str += nl;
          }
        }
        str += nl + nl + "Read Manual for more details...";

        return str;
      }
    }

    /**
    * User Arithmetic expression is a Set of blocks with only one meaning at a time.
    *                              `m_block`
    * After parsing user expression, each block have a single sence, one of below:
    * * IsOperator. His literal name storred like value of string type
    * -or-
    * * IsNumber, his literal instance converted on the fly in Parse_to_Blocks to
    *   CNumber value type.
    */
    /// <summary>Partitionised expression holder</summary>
    private CBlock m_block = new CBlock();

    /// <summary>
    /// Set of default and user registers</summary>
    private CRegister m_reg = new CRegister();

    /// <summary>
    /// result of calculations while solving whole expression, has properties of
    /// user answer holder.</summary>
    private CNumber m_result;

    #region ***** INTERCHANGE COMMANDS *****
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>
    /// enumerator of reserved commands</summary>
    private enum CMD: int
    {
      _FIRST = 0,
      ABOUT_ENGINE1 = 0, ABOUT_ENGINE2,
      EXIT_APP1, EXIT_APP2,
      CLEAR_SCREEN, CLEAR_RESULT, CLEAR_ALL,
      IHEX, RHEX,
      IDEC, RDEC,
      IOCT, ROCT,
      IBIN, RBIN = 14,
      _LAST = 14
    };
    /// <summary>
    /// string array of commands</summary>
    private readonly string[] m_sCMD =
         {
           "?", "help",
           "exit", "by",
           "cls", "clr", "clear",
           "ihex", "rhex",
           "idec", "rdec",
           "ioct", "roct",
           "ibin", "rbin"
         };

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>Accessor for array of commands;</summary>
    /// <param name="_cmd">Command enumerator;</param>
    /// <returns>String equivalent to the passed enumerator;</returns>
    private string the(CMD _cmd)
    {
      return m_sCMD[(int)_cmd];
    }
    #endregion ***** INTERCHANGE COMMANDS *****

    #region ***** ARITHMETIC OPERATORS *****
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>enumerator of Arithmetic Operations. Enumeration
    /// number grows from low priority witch is 0 to higher</summary>
    private enum AO: int
    {
      _FIRST = 0,       //-- the lowest priority
      /**0*/  PLS = 0,
      /**1*/  MIN,
      /**2*/  XOR,
      /**3*/  OR,
      /**4*/  AND,
      /**5*/  NOT,
      /**6*/  SH_LEFT,
      /**7*/  SH_RIGHT,
      /**8*/  MUL,
      /**9*/  MOD,
      /**10*/ DIV,
      /**11*/ POW,
      /**12*/ ABS,
      /**13*/ ROOT,
      /**14*/ TAN,      ///-- FUNCTION
      /**15*/ SIN,
      /**16*/ COS,
      /**17*/ EXP,
      /**18*/ LG,
      /**19*/ LN,
      /**20*/ LOG,
      /**21*/ FACT,
      /**22*/ GCD,
      /**23*/ BREND,    ///-- PRIORITY OVERRIDE
      /**24*/ BRBEG = 24,
      _LAST = 24        ///-- the highest priotity
    };

    /// <summary>array of reserved arithmetic operations, see AO</summary>
    private readonly string[] m_sAO =
    {
      /// the lowest priority
      ///--- ARITHMETIC
      /// - and + haven't priority dependency
      ///      [ENUMERATOR]   [OPERATOR TYPE]
      "+",     // PLS         - unary/binary
      "-",     // MIN         - unary/binary
      ///--- LOGIC
      "xor",   // XOR         - binary
      "or",    // OR          - binary
      "and",   // AND         - binary
      "not",   // NOT         - unary
      ///--- ARITHMETIC
      "<<",    // SH_LEFT     - binary
      ">>",    // SH_RIGHT    - binary
      "*",     // MUL         - binary
      "\\",    // MOD         - binary
      "/",     // DIV         - binary
      "^",     // POW         - binary (degree required on right)
      "abs",   // ABS         - unary
      "#",     // ROOT         - unary/binary (degree required on left)
      ///--- TRIGONOMETRY
      "tan",   // TAN         - unary
      "sin",   // SIN         - unary
      "cos",   // COS         - unary
      ///--- FUNCTION
      "exp",   // EXP         - unary
      "lg",    // LG          - unary
      "ln",    // LN          - unary
      "log",   // LOG         - unary/binary (base required on left)
      "!",     // FACT        - unary
      "gcd",   // GCD         - binary
      ///--- PRIORITY OVERRIDE
      ")",     // BREND (never taken like priority operation)
      "("      // BRBEG relies upon the BREND
      /// the highest priotity
    };

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>accessor of array of operations</summary>
    /// <param name="_AO">enumerator of operation</param>
    /// <returns>string equivalent to enumerator</returns>
    private string the(AO _ao)
    {
      return m_sAO[(int) _ao];
    }
    #endregion ***** ARITHMETIC OPERATORS *****

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>New object initialization</summary>
    public CCalculator() { }

    /// <summary>
    /// Return to a caller reference to a register holder</summary>
    /// <returns>register holder</returns>
    public CRegister GetRegReference()
    {
      return m_reg;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>
    /// Solve user expression, may throw Calc.SyntaxException,
    /// Calc.CommandException, Calc.EngineException.</summary>
    /// <param name="_sExpr">user expression</param>
    /// <param name="_Answer">user answer holder, where
    /// solved expression result is placed</param>
    public void Solve(string _sExpr, ref CNumber _Answer)
    {// void expression equals to void result
      if (_sExpr == null || _sExpr == "")
      {
        return;
      }
      _sExpr = _sExpr.Replace(" ", "").ToLower();   // simplify

      CatchCommand(_sExpr);   // if command - execute

      // reinit & assimilate template number properties
      m_block.Init(_Answer);

      // `m_result` will like the caller answer holder and will take
      // a part in temporary calculations through `Parts_Solve` and
      // Parse_to_Blocks;
      m_result = new CNumber("0", _Answer);
      try
      {
        Parse_to_Blocks(_sExpr);
      }
      catch (ServiceException xcp)
      {
        ASSERT(false, xcp.Message, "Solve.conversion");
      }

      Parts_Solve(m_block);

      // set user answer holder and common answer registry to
      // a newly solved value
      _Answer = m_block[0].Number;
      m_reg.ANSWER.GetProperties(_Answer);
      m_reg.ANSWER = _Answer;
      return;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>
    /// Solve partitionized expression.
    /// Assume what:
    ///  - created valid partition (Parse_to_Blocks)
    ///  - any registers replaced by value (..)
    ///  - no braces conflicts (..)
    /// Parentheses solved in recursion call, end-condition is
    /// when one part is left, and its a number.</summary>
    /// <param name="_scPart">valid partition</param>
    /// <returns>answer</returns>
    private void Parts_Solve(CBlock _blocks)
    {
      int hipos = 0;    // hi priority index
      int brEnd = 0;    // index of found `AO.BREND`

      while (1 < _blocks.Count)
      {
        // find hi priority operator index
        if (GetHiPriorityPos(_blocks, ref hipos))
        {
          // engage braces by self-recursion
          if (the(AO.BRBEG) == _blocks[hipos].Operator)
          {
            // here `hipos` is index of `AO.BRBEG`
            brEnd = FindEndBrace(_blocks, hipos);
            // copy sub-expression parts
            CBlock subBlocks = _blocks.Projection((hipos + 1), (brEnd - 1));

            Parts_Solve(subBlocks); // enter recursion

            // RETURN AFTER RECURSIVE CALL WITH ONLY ONE PART
            ASSERT(1 == subBlocks.Count
              , "Invalid recursion return"
              , "Parts_Solve: ret from recursion");

            _blocks.ReplaceWith(subBlocks[0].Number, hipos, brEnd);
          }
            /// engage unary or binary operators by priority
            /// Check if current operator is a double-sence operator:
            /// Unary have number from right, binary from both sides.
          else
          {
            bool unary = true;      // is unray by default
            if ( IsDoubleSenseAO(_blocks[hipos].Operator) )
            {
              // check boundaries before accesing by indexes
              if ((hipos > 0) && ((hipos + 1) < _blocks.Count))
              {
                // define from context...
                unary = (_blocks[hipos - 1].IsOperator
                  || _blocks[hipos + 1].IsOperator);
              }
            }
            else
            {
              // define from table of unary operators...
              unary = IsUnaryAO(_blocks[hipos].Operator);
            }

            if (unary)
            {
              m_result = this.SolveUnary(_blocks, hipos);
              _blocks.ReplaceWith(m_result, hipos, hipos + 1);
            }
            else // binary
            {
              m_result = SolveBinary(_blocks, hipos);
              _blocks.ReplaceWith(m_result, hipos - 1, hipos + 1);
            }
          }
        }
        else // priority operator not found
        {
          ASSERT(false
            , "Omitted operator found after >" + _blocks[0].Number + '<'
            , "Parts_Solve: operator not found");
        }
      }

      // End solving with single number
      ASSERT(_blocks.Count == 1 , "Nothing to solve", "Parts_Solve: 1-part,1");
      ASSERT(_blocks[0].IsNumber
        , "Unrecovered >" + _blocks[0].Operator + "< operator"
        , "Parts_Solve: 1-part,2");
      return;
    }

    #region BINARY-UNARY SOLVING
    /// <summary>
    /// Solve binary operator, where operands taken from right and left
    /// of operator position</summary>
    /// <param name="_blocks">in blocks</param>
    /// <param name="_iHipos">at operator position</param>
    /// <returns>calculation result</returns>
    private CNumber SolveBinary(CBlock _blocks, int _iHipos)
    {
      ASSERT((_blocks.Count >= 3) && (_iHipos > 0)
        && ((_iHipos + 1) < _blocks.Count)
        , "Unrecovered binary operator >" + _blocks[_iHipos].Operator + '<'
        , "SolveBinary1");
      ASSERT(_blocks[_iHipos - 1].IsNumber && _blocks[_iHipos + 1].IsNumber
        , "Missing argument for binary operator >" + _blocks[_iHipos].Operator + '<'
        , "SolveBinary2");

      CNumber nLeft = _blocks[_iHipos - 1].Number;
      string strOperator = _blocks[_iHipos].Operator;
      CNumber nRight = _blocks[_iHipos + 1].Number;
      CNumber res;

      STUB((nLeft != null) && (nRight != null), "Bad _nLeft in SolveBinary.");
      STUB((strOperator != null) && (strOperator != ""), "Bad _sOper in SolveBinary.");

      ///-- ARITHMETIC
      if (the(AO.POW) == strOperator)
      {
        res = CMath.Pow(nLeft, nRight);
      }
      else if (the(AO.MUL) == strOperator)
      {
        res = CMath.Mul(nLeft, nRight);
      }
      else if (the(AO.DIV) == strOperator)
      {
        res = CMath.Div(nLeft, nRight);
      }
      else if (the(AO.MOD) == strOperator)
      {
        res = CMath.Mod(nLeft, nRight);
      }
      else if (the(AO.PLS) == strOperator)
      {
        res = CMath.Plus(nLeft, nRight);
      }
      else if (the(AO.MIN) == strOperator)
      {
        res = CMath.Minus(nLeft, nRight);
      }
      else if (the(AO.ROOT) == strOperator)
      {
        res = CMath.Root(nLeft, nRight);
      }
        ///-- FUNCTION
      else if (the(AO.LOG) == strOperator)
      {
        res = CMath.Log(nLeft, nRight);
      }
        ///-- LOGIC
      else if (the(AO.AND) == strOperator)
      {
        res = CMath.Log(nLeft, nRight);
      }
      else if (the(AO.OR) == strOperator)
      {
        res = CMath.Or(nLeft, nRight);
      }
      else if (the(AO.XOR) == strOperator)
      {
        res = CMath.Xor(nLeft, nRight);
      }
        ///-- BITWISE
      else if (the(AO.SH_LEFT) == strOperator)
      {
        res = CMath.ShiftLeft(nLeft, nRight);
      }
      else if (the(AO.SH_RIGHT) == strOperator)
      {
        res = CMath.ShiftRight(nLeft, nRight);
      }
      else if (the(AO.GCD) == strOperator)
      {
        res = CMath.GCD(nLeft, nRight);
      }
        //-- not implemented
      else
      {
        STUB(false, "[" + strOperator
          + "] not implemented binary operation!");
        return null;
      }

      ASSERT(!(double.IsInfinity(res.dValue))
        , "Result is ±Infinity", "SolveBinary");
      ASSERT(!(double.IsNaN(res.dValue))
        , "Result is Not a number", "SolveBinary");

      return res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>
    /// Solve unray operator, where operand taken from right of operator
    /// position</summary>
    /// <param name="_blocks">in blocks</param>
    /// <param name="_iHipos">at operator position</param>
    /// <returns>calculation result</returns>
    private CNumber SolveUnary(CBlock _blocks, int _iHipos)
    {
      ASSERT(((_iHipos + 1) < _blocks.Count) && (_blocks[_iHipos + 1].IsNumber)
        , "Missing argument for >" + _blocks[_iHipos].Operator + '<'
        , "SolveUnary");

      string strOperator = _blocks[_iHipos].Operator;
      CNumber number =  _blocks[_iHipos + 1].Number;
      CNumber res;

      STUB(strOperator != null && strOperator != "", "Bad _sOper in SolveUnary");
      STUB(number != null, "Bad _Num in SolveUnary");

      ///-- LOGIC
      if (the(AO.NOT) == strOperator)
      {
        res = CMath.Not(number);
      }
        ///-- ARITHMETIC
      else if (the(AO.MIN) == strOperator)
      {
        res = CMath.Minus(number);
      }
      else if (the(AO.PLS) == strOperator)
      {
        res = CMath.Plus(number);
      }
      else if (the(AO.ROOT) == strOperator)
      {  // square root
        res = CMath.Root(number);
      }
      else if (the(AO.ABS) == strOperator)
      {
        res = CMath.Abs(number);
      }
        ///-- THRIGONOMETRY
      else if (the(AO.SIN) == strOperator)
      {
        res = CMath.Sin(number);
      }
      else if (the(AO.COS) == strOperator)
      {
        res = CMath.Cos(number);
      }
      else if (the(AO.EXP) == strOperator)
      {
        res = CMath.Exp(number);
      }
      else if (the(AO.TAN) == strOperator)
      {
        res = CMath.Tan(number);
      }
        ///-- FUNCTION
      else if (the(AO.FACT) == strOperator)
      {
        res = CMath.Factorial(number);
      }
      else if (the(AO.LG) == strOperator)
      {
        res = CMath.Lg(number);
      }
      else if (the(AO.LN) == strOperator)
      {
        res = CMath.Ln(number);
      }
      else if (the(AO.LOG) == strOperator)
      {
        res = CMath.Log(number);
      }
      else
      {
        STUB(false, "[ " + strOperator.ToUpper()
          + " ] not implemented unary operation!");
        return null;
      }

      ASSERT(!(double.IsInfinity(res.dValue))
        , "Result is ±Infinity", "SolveUnary");
      ASSERT(!(double.IsNaN(res.dValue))
        , "Result is Not a number", "SolveUnary");

      return res;
    }
    #endregion BINARY-UNARY SOLVING

    #region PRIORITY DETERMINATION
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    // Find next operator to solve by its priority.
    // And, if available, return it in `_hipos` with valid status,
    // false otherwise
    private bool GetHiPriorityPos(CBlock _blocks, ref int _hipos)
    {
      bool bvalid = false;
      int priority = 0;
      int testpriority = 0;

      for (int i = 0; i < _blocks.Count; i++)
      {
        if (_blocks[i].IsNumber)
        {
          continue;
        }
        // here is operator

        testpriority = GetPriority(_blocks[i].Operator);

        // no priority is assigned
        if (!bvalid)
        {
          priority = testpriority;
          _hipos = i;
          bvalid = true;
          continue;
        }
          /** Assign new priority position.
                * operator (>) or (>=) affects in order of solving operators
                * with same priority:
                * (>) - the first operator from the left
                * (>=)- the last operator from the left
                * */
        else if (testpriority > priority)
        {
          priority = testpriority;
          _hipos = i;
        }
      }

      return bvalid;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    // priority of operation gived by name (`_sAO`)
    // witch in fact is index in `m_sAO`
    private int GetPriority(string _sAO)
    {
      for (int i = (int)AO._FIRST; i <= (int)AO._LAST; i++)
      {
        if (m_sAO[i] == _sAO)
        {
          return i;
        }
      }
      ASSERT(false, "Missing priority for >" + _sAO + "<", "GetPriority");
      return -1;
    }
    #endregion PRIORITY DETERMINATION

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>Find end of breace-scope specified by _iStart</summary>
    /// <param name="_blocks">in blocks</param>
    /// <param name="_iStart">at start position</param>
    /// <returns>position of end-brace</returns>
    private int FindEndBrace(CBlock _blocks, int _iStart)
    {
      STUB(_iStart < (_blocks.Count - 1)
        , "Bad FindEndBrace, index exceed blocks count.");
      STUB((_blocks[_iStart].IsOperator)
        && (the(AO.BRBEG) == _blocks[_iStart].Operator)
        , "Bad FindEndBrace, need operator.");

      int innerBrace = 0;

      for (int i = (_iStart + 1); i < _blocks.Count; i++)
      {
        if (_blocks[i].IsNumber)
        {
          continue;
        }

        if (the(AO.BRBEG) == _blocks[i].Operator)
        {
          ++innerBrace;
        }
        else if (the(AO.BREND) == _blocks[i].Operator)
        {
          if (innerBrace == 0)
          {
            return i;
          }
          else
          {
            --innerBrace;
          }
        }
      }
      STUB(false, "End brace not found.");
      return -1;  // unreached code
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>separate _sExpr to numbers and knowed arithmetic
    /// operators and functions. result stored in StringCollection m_scPart.
    /// any other unknown symbol will throw an CCalcException.</summary>
    /// <param name="_sExpr">string to engage</param>
    private void Parse_to_Blocks(string _sExpr)
    {
      string ss = "";   // contain only valid sequence (number/operator)
      string st;        // temp
      int sLen = _sExpr.Length;
      int len = 1;      // lenght of the substring
      int i = 0;        // expression string index
      int brace = 0;    // brace balance == 0

      while (i < _sExpr.Length)
      {
        len = 1;
        ss = _sExpr.Substring(i, len);

        // `ss` is a number
        if (IsMaybeNumber(ss))
        {
          for (len = 2; ((i + len - 1) < sLen)
            && (i < (_sExpr.Length - 1)); len++)
          {
            if (IsMaybeNumber((st = _sExpr.Substring(i, len))))
            {
              ss = st;
            }
            else
            {
              break;
            }
          }
          m_block.AddNumber(ss);
          // jump over passed sub-string
          i += (len - 1);
        }
          // `ss` is an operator or register
        else if (IsMaybeAO(ss)
          || m_reg.IsMaybeRegister(ss))
        {
          for (len = 2; ((i + len - 1) < sLen)
            && (i < (_sExpr.Length - 1)); len++)
          {
            if (IsMaybeAO(st = _sExpr.Substring(i, len))
              || m_reg.IsMaybeRegister(st = _sExpr.Substring(i, len)))
            {
              ss = st;
            }
            else
            {
              break;
            }
          }
          // at the end there may be one of register or operator
          if (IsAO(ss))
          {
            m_block.AddOperator(ss);
          }
          else if (m_reg.IsRegister(ss))
          {
            m_block.AddNumber(m_reg[ss]);
          }
          else
          {
            ASSERT(false, "Unrecognized >" + ss + '<'
              , "Parse_to_Blocks.operators & registers");
          }

          // track braces changes
          if (the(AO.BRBEG) == ss)
          {
            ++brace;
          }
          else if (the(AO.BREND) == ss)
          {
            --brace;
          }
          // jump over passed sub-string
          i += (len - 1);
        }
        else
        {
          ASSERT(false, "Unrecognized >" + ss + '<', "Parse_to_Blocks.other");
        }
      }
      ASSERT(brace == 0, "Lack for parenthes", "Parts_BracesValidation");
    }// TEST_POINT: end of partition

    #region AFFIRMATION, STATEMENT
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>Define if given string is one of an integer,
    /// float or exponent number. dot '.' is treats as part
    /// of the number (may throw Calc.SyntaxException if
    /// multiple dot found).
    ///   Exponent known as 1e2 or 1e-2 or 1e+2
    ///   Hexadecimal number known as number witch has "0x" prefix.
    /// </summary>
    /// <remarks>This is a part of Parse_to_Blocks algorythm!
    /// Nature of algorithm is to determine if passed string
    /// may be a number or not; string passed as a parameter may be
    /// a sub string of an integer/real/hexadecimal number, in that case
    /// method returns true in hope that next (larger) substring will be
    /// its continue.</remarks>
    /// <param name="_num">string to engage</param>
    /// <returns>true if _num is seems to be a number, false otherwise</returns>
    private bool IsMaybeNumber(string _num)
    {
      bool bExpFirst = true;  // exponent is first simbol
      bool bWasExp = false;   // exp not previous
      bool bWasDot = false;   // dot not encountered
      bool bWasSign = false;  // sign symbol wasn't encountered

      bool bWas0 = false;
      bool bWas0x = false;    // hex prefix 0x wasn't

      for (int i = 0; i < _num.Length; i++)
      {
        // it's a digit?
        if ((_num[i] >= '0') && (_num[i] <= '9'))
        {
          bExpFirst = false;
          bWasExp = false;
          bWas0 = ((_num[i] == '0')? true : false);
          continue;
        }
          // it's a floating point delimiter?
        else if (_num[i] == ',' || _num[i] == '.')
        {
          // if dot is encoutered not once, - so HALT
          ASSERT(bWasDot == false, "Bad second decimal point", "IsNumber");
          bWasDot = true;
          continue;
        }
          // was prefix and this is a hexadecimal digits
        else if ((_num[i] >= 'a') && (_num[i] <= 'f') && bWas0x)
        {
          continue;
        }
          // was 0 and this is an end of hexadecimal prefix
        else if (_num[i] == 'x' && bWas0 && (m_result.CS == T_CS.HEX))
        {
          if (bWas0x) { return false; }
          bWas0x = true;
          continue;
        }
          // exponent symbol not first
        else if ((_num[i] == 'e') && !bExpFirst)
        {
          bWasExp = true;
          if (i < (_num.Length - 1))
          {
            if ( ((_num[i + 1] >= '0') && (_num[i + 1] <= '9'))
              || (_num[i + 1] == '+')
              || (_num[i + 1] == '-') )
            {// 'e' symbol folows by positive by default
              //  or signed exponent number
              if ((i + 2) == _num.Length)
              {
                return true;
              }
              else
              {
                continue;
              }
            }
            else
            {// denial 'e' symbol as an exponent symbol
              return false;
            }
          }
          // continue if on this iteration exponent symbol
          //  is the last in string
          continue;
        }
          // was sign after exponent?
        else if ( ((_num[i] == '-') || (_num[i] == '+')) && bWasExp )
        {
          if (bWasSign)
          {
            return false;
          }
          bWasSign = true;
          continue;
        }
        return false;
      }
      return true;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>check if expression is one of arithmetic operations
    /// predefined in m_sAO array</summary>
    /// <param name="_sAO">string to engage</param>
    /// <returns>true if `_sAO` is almost an operator or indeed
    /// the operator, false otherwise</returns>
    private bool IsMaybeAO(string _sAO)
    {
      for (AO ao = AO._FIRST; ao <= AO._LAST; ao++)
      {
        if (the(ao).StartsWith(_sAO)
          && (_sAO.Length <= the(ao).Length))
        {
          return true;
        }
      }
      return false;
    }
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    private bool IsAO(string _sAO)
    {
      for (AO ao = AO._FIRST; ao <= AO._LAST; ao++)
      {
        if (the(ao) == _sAO)
        {
          return true;
        }
      }
      return false;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    // ret true if `_sAO` is unary operator
    private bool IsUnaryAO(string _sAO)
    {
      if ( the(AO.NOT)    == _sAO
        || the(AO.FACT)   == _sAO
        || the(AO.SIN)    == _sAO
        || the(AO.COS)    == _sAO
        || the(AO.TAN)    == _sAO
        || the(AO.LN)     == _sAO
        || the(AO.ABS)    == _sAO
        || the(AO.EXP)    == _sAO
        || the(AO.LG)     == _sAO)
      {
        return true;
      }
      return false;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    // ret true if `_sAO` is can be or unary or binary operator
    private bool IsDoubleSenseAO(string _sAO)
    {
      if ( the(AO.PLS)  == _sAO
        || the(AO.MIN)  == _sAO
        || the(AO.ROOT) == _sAO
        || the(AO.LOG)  == _sAO)
      {
        return true;
      }
      return false;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>check if expression is a command as predefined
    /// in `m_sCMD`</summary>
    /// <param name="_sCmd">user expression here as a command</param>
    /// <returns>true if _sCmd is a known command, false otherwise</returns>
    private bool IsCommand(string _sCmd)
    {
      for (CMD cmd = CMD._FIRST; cmd <= CMD._LAST; cmd++)
      {
        if (the(cmd) == _sCmd)
        {
          return true;
        }
      }
      return false;
    }
    #endregion AFFIRMATION, STATEMENT

    #region RUN COMMANDS
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>execute predefined command on Application--Calculator
    /// level. Command specified in `m_sCMD`</summary>
    /// <param name="_sCmd">command name</param>
    /// <returns>empty string on application level command or
    /// result of the specific command</returns>
    private void CatchCommand(string _sCmd)
    {
      // is not a command - continue parsing expression
      if (!IsCommand(_sCmd))
      {
        return;
      }

      // display help on usage
      if (the(CMD.ABOUT_ENGINE1) == _sCmd || the(CMD.ABOUT_ENGINE2) == _sCmd)
      {
        throw new Calc.CommandException(AppCommand.AboutCalcEngine);
      }
        // EXIT application
      else if (the(CMD.EXIT_APP1) == _sCmd || the(CMD.EXIT_APP2) == _sCmd)
      {//"exit", "by"
        throw new Calc.CommandException(AppCommand.ExitClientApplication);
      }
        // clear history pane only
      else if (the(CMD.CLEAR_SCREEN) == _sCmd)
      {//"cls"
        throw new Calc.CommandException(AppCommand.ClearResultScreen);
      }
        // clear answer register
      else if (the(CMD.CLEAR_RESULT) == _sCmd)
      {//"clr"
        m_reg.ANSWER = new CNumber("0");
        throw new Calc.CommandException(AppCommand.Standby);
      }
        // clear history pane & result register
      else if (the(CMD.CLEAR_ALL) == _sCmd)
      {//"clear"
        m_reg.ANSWER = new CNumber("0");
        throw new Calc.CommandException(AppCommand.ClearResultScreen);
      }
        // change input count-system
      else if (the(CMD.IHEX) == _sCmd)
      {
        throw new Calc.CommandException(AppCommand.ChangeInputCS, (int)T_CS.i_HEX);
      }
      else if (the(CMD.IDEC) == _sCmd)
      {
        throw new Calc.CommandException(AppCommand.ChangeInputCS, (int)T_CS.i_DEC);
      }
      else if (the(CMD.IOCT) == _sCmd)
      {
        throw new Calc.CommandException(AppCommand.ChangeInputCS, (int)T_CS.i_OCT);
      }
      else if (the(CMD.IBIN) == _sCmd)
      {
        throw new Calc.CommandException(AppCommand.ChangeInputCS, (int)T_CS.i_BIN);
      }
        // change result count-system
      else if (the(CMD.RHEX) == _sCmd)
      {
        throw new Calc.CommandException(AppCommand.ChangeResultCS, (int)T_CS.i_HEX);
      }
      else if (the(CMD.RDEC) == _sCmd)
      {
        throw new Calc.CommandException(AppCommand.ChangeResultCS, (int)T_CS.i_DEC);
      }
      else if (the(CMD.ROCT) == _sCmd)
      {
        throw new Calc.CommandException(AppCommand.ChangeResultCS, (int)T_CS.i_OCT);
      }
      else if (the(CMD.RBIN) == _sCmd)
      {
        throw new Calc.CommandException(AppCommand.ChangeResultCS, (int)T_CS.i_BIN);
      }
        // unreacheble code (@if all done right)
      else
      {// otherwise here we are...
        STUB(false, "Not implemented command: \"" + _sCmd + "\""
          + "\nAt: CCalculator.RunCommand");
      }
    }
    #endregion RUN COMMANDS

    #region ASSERTION
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>Assertion validator</summary>
    /// <param name="_true">Boolean expression, Assertion will fail if it false;</param>
    /// <param name="_sErrMsg">User friendly message for Reason of Fail;</param>
    /// <param name="_sDebug">Additional information about the Reason of Fail
    /// which is apended only in DEBUG solution;</param>
    public static void ASSERT(bool _true, string _sErrMsg
      , string _sDebug)
    {
      if (!_true)
      {  // assertion failure with message
#if DEBUG
        // on debug only show also debug message
        throw new Calc.SyntaxException(_sErrMsg + ": " + _sDebug);
#else
        throw new Calc.SyntaxException(_sErrMsg);
#endif
      }
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>Assertion validator for Internal Code missusages
    /// and NOT implemented features of CCalculator engine;</summary>
    /// <param name="_true">Boolean expression, Assertion will fail if it false;</param>
    /// <param name="_sMsg">Programmer-friendly message;</param>
    /// <exception cref="Calc.EngineException"></exception>
    public static void STUB(bool _true, string _sMsg)
    {
      if (!_true)
      {
        throw new Calc.EngineException(_sMsg);
      }
    }
    #endregion
  }
}

//? EOF
