/**
 * Author: Alexander Block
 * Is a part of Calc application
 * developed for C# study goals and is an intelectual
 * property of his author.
*/

using System;
using System.Globalization;

namespace Calc
{

  //························································
  public class CMath
  {
    private static CNumber m_res; // temporary result holder

    private CMath ()
    {
      /** Reason for private constructor is to prevent creating
       * instances of that class cose it has only static methods.
       * */
    }

    #region Trigonomical
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    public static CNumber Tan(CNumber _num)
    {
      m_res = new CNumber("0", _num);

      m_res.Value = Math.Tan(_num.ToRadians);
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    public static CNumber Cos(CNumber _num)
    {
      m_res = new CNumber("0", _num);

      m_res.Value = Math.Cos(_num.ToRadians);
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    public static CNumber Sin(CNumber _num)
    {
      m_res = new CNumber("0", _num);

      // TODO: Check whenever `_num` passed here from some register
      // and TCS from GUI is applied for him.
      m_res.Value = Math.Sin(_num.ToRadians);
      return m_res;
    }
    #endregion Trigonomical

    #region Boolean logic
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    public static CNumber Not(CNumber _num)
    {
      CCalculator.ASSERT(_num.IsInteger
        , "\"NOT\" operation need integer.", "CMath.Not");

      m_res = new CNumber("0", _num);

      m_res.Value = ~((long)_num.Value);
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    public static CNumber And(CNumber _N1, CNumber _N2)
    {
      CCalculator.ASSERT(_N1.IsInteger && _N2.IsInteger
        , "\"AND\" operation need integer.", "CMath.And");

      m_res = new CNumber("0", _N1);

      m_res.Value = (long)_N1.Value & (long)_N2.Value;
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    public static CNumber Or(CNumber _N1, CNumber _N2)
    {
      CCalculator.ASSERT(_N1.IsInteger && _N2.IsInteger
        , "\"OR\" operation need integer.", "CMath.Or");

      m_res = new CNumber("0", _N1);

      m_res.Value = (long)_N1.Value | (long)_N2.Value;
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    public static CNumber Xor(CNumber _N1, CNumber _N2)
    {
      CCalculator.ASSERT(_N1.IsInteger && _N2.IsInteger
        , "\"XOR\" operation need integer.", "CMath.Xor");

      m_res = new CNumber("0", _N1);

      m_res.Value = (long)_N1.Value ^ (long)_N2.Value;
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    public static CNumber ShiftLeft(CNumber _N, CNumber _BitNum)
    {
      CCalculator.ASSERT(_N.IsInteger && _BitNum.IsInteger
        , "\"<<\" operation need integer.", "CMath.ShiftLeft");

      m_res = new CNumber("0", _N);

      try
      {
        int bitNum = Convert.ToInt32((long)_BitNum.Value);
        m_res.Value = (long)_N.Value << bitNum;
      }
      catch (OverflowException)
      {
        CCalculator.ASSERT(false, "\"x << Y\" - Y overflow.", "CMath.ShiftLeft");
      }
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    public static CNumber ShiftRight(CNumber _N, CNumber _BitNum)
    {
      CCalculator.ASSERT(_N.IsInteger && _BitNum.IsInteger
        , "\">>\" operation need integer.", "CMath.ShiftRight");

      m_res = new CNumber("0", _N);

      try
      {
        int bitNum = Convert.ToInt32((long)_BitNum.Value);
        m_res.Value = (long)_N.Value >> bitNum;
      }
      catch (OverflowException)
      {
        CCalculator.ASSERT(false, "\"x >> Y\" - Y overflow.", "CMath.ShiftRight");
      }
      return m_res;
    }
    #endregion Boolean logic

    #region Regular Operations
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    // unarty plus
    public static CNumber Plus(CNumber _num)
    {
      return _num;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    // binary plus
    public static CNumber Plus(CNumber _N1, CNumber _N2)
    {
      m_res = new CNumber("0", _N1);

      m_res.Value = _N1.dValue + _N2.dValue;
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    // unary minus
    public static CNumber Minus(CNumber _num)
    {
      m_res = new CNumber("0", _num);

      m_res.Value = -(_num.dValue);
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    // binary minus
    public static CNumber Minus(CNumber _N1, CNumber _N2)
    {
      m_res = new CNumber("0", _N1);

      m_res.Value = _N1.dValue - _N2.dValue;
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    public static CNumber Pow(CNumber _num, CNumber _NDegree)
    {
      m_res = new CNumber("0", _num);

      m_res.Value = Math.Pow(_num.dValue, _NDegree.dValue);
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    public static CNumber Mul(CNumber _N1, CNumber _N2)
    {
      m_res = new CNumber("0", _N1);

      m_res.Value = _N1.dValue * _N2.dValue;
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>
    /// Regular devision
    /// </summary>
    /// <param name="_NDividend"></param>
    /// <param name="_NDivisor"></param>
    /// <returns></returns>
    public static CNumber Div(CNumber _NDividend, CNumber _NDivisor)
    {
      m_res = new CNumber("0", _NDividend);

      CCalculator.ASSERT(_NDivisor.dValue != 0.0d
        , "Divide by zero.", "CMath.Div");
         
      m_res.Value = _NDividend.dValue / _NDivisor.dValue;
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>
    /// Mod devision
    /// </summary>
    /// <param name="_N"></param>
    /// <param name="_NBase">base of mod devision</param>
    /// <returns></returns>
    public static CNumber Mod(CNumber _N, CNumber _NBase)
    {
      m_res = new CNumber("0", _N);

      m_res.Value = _N.dValue % _NBase.dValue;
      return m_res;
    }

    /// <summary>
    /// Absolute of value `_N`
    /// </summary>
    /// <param name="_N">value to take from absolut</param>
    /// <returns></returns>
    public static CNumber Abs(CNumber _N)
    {
      m_res = new CNumber("0", _N);

      m_res.Value = Math.Abs(_N.dValue);
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>
    /// Square root (degree of 2)</summary>
    /// <param name="_N">value, root of which to be evaluated</param>
    /// <returns>result of root of degree 2</returns>
    public static CNumber Root(CNumber _N)
    {
      CCalculator.ASSERT(_N.dValue > 0.0d
        , "#(x): x - Must be positive.", "CMath.Root unary");

      m_res = new CNumber("0", _N);

      m_res.Value = Math.Sqrt(_N.dValue);
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>
    /// Root of variable degree
    /// </summary>
    /// <param name="_NDegree">root degree</param>
    /// <param name="_N">value, root of which to be taken</param>
    /// <returns>result of variable degree</returns>
    public static CNumber Root(CNumber _NDegree, CNumber _N)
    {
      CCalculator.ASSERT((_NDegree.dValue != 0.0d)
        , "(y)_#(x): `y` - Root degree can't be NULL.", "CMath.Root binary");
      CCalculator.ASSERT((_N.dValue >= 0.0d) || (_NDegree.dValue%2 != 0)
        , "(y)_#(x): `x` - Must be positive and/or `y` is even."
        , "CMath.Root binary");

      m_res = new CNumber("0", _N);
      m_res.Value = _N.Value;

      double X = Math.Abs(_N.dValue);
      try
      {
        m_res.Value = Math.Pow(2, Math.Log(X, 2)/_NDegree.dValue);

        // changing sign of result if root argument was negative and
        // root degree was uneven
        if (_N.dValue < 0)
        {
          m_res.Value = -m_res.dValue;
        }
      }
      catch (OverflowException)
      {
        CCalculator.ASSERT(false, "", "");
      }

      return m_res;
    }
    #endregion Regular Operations

    #region Functions
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    public static CNumber Factorial(CNumber _num)
    {
      CCalculator.ASSERT(_num.IsInteger
        , "\"!x\" operation need integer.", "CMath.Factorial");

      m_res = new CNumber("0", _num);
      if ((long)_num.Value == 0)
      {
        m_res.Value = 1L;
        return m_res;
      }

      try
      {
        if (_num.dValue <= 20)
        {
          long num = (long)_num.Value;
          long fact = num;
          while (num > 1)
          {
            fact *= --num;
          }
          m_res.Value = fact;
        }
        else
        {
          double num = _num.dValue;
          double fact = num;
          while (num > 1)
          {
            fact *= --num;
          }
          m_res.Value = fact;
        }
      }
      catch (OverflowException)
      {
        CCalculator.ASSERT(false
          , "Result is out of range.", "CMath.Factorial");
      }
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>
    /// The Great Common Devisor of to numbers</summary>
    static private long _gcd(long _l1, long _l2)
    {
      if (_l2 == 0)
      {
        return _l1;
      }
      return _gcd(_l2, _l1 % _l2);
    }

    /// <summary>Binary. Find the grat common devisor of two 
    /// integer numbers</summary>
    static public CNumber GCD(CNumber _n1, CNumber _n2)
    {
      CCalculator.ASSERT(_n1.IsInteger && _n2.IsInteger
        , "(x)Gcd(y): x, y - Integers expected.", "CMath.GCD");

      m_res = new CNumber("0", _n1);

      m_res.Value = _gcd(_n1.lValue, _n2.lValue);

      return m_res;
    }
    #endregion Functions

    #region Logarithms
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>Base _E logarithm.
    /// Exponent. Exp(2) == _e² == _e^2</summary>
    /// <param name="_sN1">argument</param>
    /// <returns>Returns the base _E logarithm of a specified number</returns>
    public static CNumber Exp(CNumber _num)
    {
      m_res = new CNumber("0", _num);

      m_res.Value = Math.Exp(_num.dValue);
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>Base 2 logarithm</summary>
    /// <param name="_sN1">argument</param>
    /// <returns>Returns the base 2 logarithm of a specified number</returns>
    public static CNumber Lg(CNumber _num)
    {
      m_res = new CNumber("0", _num);

      m_res.Value = Math.Log(_num.dValue, 2);
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>Base _E logarithm</summary>
    /// <param name="_sN1">argument</param>
    /// <returns>Returns the base _E logarithm of a specified number</returns>
    public static CNumber Ln(CNumber _num)
    {
      m_res = new CNumber("0", _num);

      m_res.Value = Math.Log(_num.dValue);     // ln
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>Unary Logarithm fo base 10</summary>
    /// <param name="_num">argument</param>
    /// <returns>Returns the base 10 logarithm of a specified number</returns>
    public static CNumber Log(CNumber _num)
    {
      m_res = new CNumber("0", _num);

      m_res.Value = Math.Log10(_num.dValue);
      return m_res;
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>Binary Logarithm of variable base</summary>
    /// <param name="_base">base</param>
    /// <param name="_num">argument</param>
    /// <returns>Returns the _sBase logarithm of a specified number</returns>
    public static CNumber Log(CNumber _base, CNumber _num)
    {
      CCalculator.ASSERT(_base.dValue >= 0
        , "(y)Log(x): y - POSITIVE expected."
        , "CMath.Log");

      m_res = new CNumber("0", _num);

      m_res.Value = Math.Log(_num.dValue, _base.dValue);
      return m_res;
    }
    #endregion Logarithms

  };
}

//? EOF
