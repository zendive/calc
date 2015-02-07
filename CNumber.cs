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
  /// <summary>
  /// Count System for number conversions. This take affect in methods
  /// FromString - before calculation, and ToString - before number presentation.
  /// The calculation itself performed in decimal count system.
  /// i_* prefix means its position in GUI control.
  /// </summary>
  public enum T_CS
  {
    // by index in combo-box
    i_HEX = 0,
    i_DEC = 1,
    i_OCT = 2,
    i_BIN = 3,
    // by value
    HEX = 116,
    DEC = 110,
    OCT = 18,
    BIN = 12
  };

  /// <summary>
  /// Trigonomic Count System format.
  /// This kind of count system takes affect only while trigonomic functions calculation
  /// and valuable only in Input (conversion starts if needed in trigonomic functions).
  /// i_* prefix means its position in GUI control.
  /// </summary>
  public enum T_TCS
  {
    // by enum
    DEGREES = 10,
    RADIANS = 11,
    GRADS = 12,
    // by index in combo-box
    i_DEGREES = 0,
    i_RADIANS = 1,
    i_GRADS   = 2
  };

  /// <summary>
  /// Type of number precision while converting to string.
  /// i_* prefix means its position in GUI control.
  /// </summary>
  public enum T_PREC
  {// by value      by index in combo-box
    DEFAULT = -12,    i_DEFAULT = 0,
    ROUNDTRIP = -11,  i_ROUNDTRIP = 1,
    _0    = 10,        i_0      = 2,
    _1    = 11,        i_1      = 3,
    _2    = 12,        i_2      = 4,
    _3    = 13,        i_3      = 5,
    _5    = 15,        i_5      = 6,
    _8    = 18,        i_8      = 7,
    _13   = 113,       i_13     = 8,
  };

  /// <summary>
  /// The Number, the main unit of all calculations and operations referenced to a number
  /// instance as it self. This class implements methods for conversions to and
  /// from HEX, DEC, OCT, BIN count-systems; Also before passing to any
  /// trigonomic function his value will be converted to Radians as a default
  /// for trigonomic calculations.</summary>
  public class CNumber
  {
    /// <summary>
    /// presentation count system for this number,
    /// takes affect in FromString, and ToString.</summary>
    private T_CS m_cs;

    /// <summary>
    /// trigonomic convesion for this number, takes affect
    /// in CMath_* trigonomic functions.</summary>
    private T_TCS m_tcs;

    /// <summary>
    /// number presentation precision.</summary>
    private T_PREC m_prec;

    /// <summary>
    /// Number value. Can be double, to check for use IsReal();
    /// -or- can be long (Int64), to check for use IsInteger().
    /// Design to took long, is becouse of that only him can be converted to and from
    /// different count-systems, and that make influence on accuracy of some operations.
    /// </summary>
    private object m_value;

    /// <summary>
    /// format provider.</summary>
    private NumberFormatInfo m_fp = new CultureInfo("en-US", false).NumberFormat;

    /// <summary>
    /// number style - real (floating-point).</summary>
    private NumberStyles m_RealStyle;

    /// <summary>
    /// number style - integer (no floating point).</summary>
    private NumberStyles m_IntegerStyle;

    /// <summary>
    /// Count-System property. By default Decimal.
    /// To convert string of number in CS different from default - set this
    /// before calling FromString() and/or ToString()</summary>
    /// <remarks>assume that assigned enumerator have `i_*` prefix</remarks>
    public T_CS CS
    {
      get { return m_cs; }
      set
      {
        switch (value)
        {
          case T_CS.i_HEX: m_cs = T_CS.HEX; break;
          case T_CS.i_DEC: m_cs = T_CS.DEC; break;
          case T_CS.i_OCT: m_cs = T_CS.OCT; break;
          case T_CS.i_BIN: m_cs = T_CS.BIN; break;
        }
      }
    }

    /// <summary>
    /// Trigonomic-Count-System property. While taking a part as a trigonomic
    /// function argument (where by default calculation going with radians)
    /// you may need to make conversion to radians corresponding to this type
    /// of T_TCS.</summary>
    /// <remarks>assume that assigned enumerator have `i_*` prefix</remarks>
    public T_TCS TCS
    {
      get { return m_tcs; }
      set 
      {
        switch (value)
        {
          case T_TCS.i_DEGREES: m_tcs = T_TCS.DEGREES; break;
          case T_TCS.i_RADIANS: m_tcs = T_TCS.RADIANS; break;
          case T_TCS.i_GRADS: m_tcs = T_TCS.GRADS; break;
        }
      }
    }

    /// <summary>
    /// Display Precision property. Number of signs after floating point.
    /// Takes affects on stringed value look after calling ToString().</summary>
    /// <remarks>assume that assigned enumerator have `i_*` prefix</remarks>
    public T_PREC PREC
    {
      get { return m_prec; }
      set 
      {
        switch (value)
        {
          case T_PREC.i_DEFAULT:   m_prec = T_PREC.DEFAULT; break;
          case T_PREC.i_ROUNDTRIP: m_prec = T_PREC.ROUNDTRIP; break;
          case T_PREC.i_0:  m_prec = T_PREC._0; break;
          case T_PREC.i_1:  m_prec = T_PREC._1; break;
          case T_PREC.i_2:  m_prec = T_PREC._2; break;
          case T_PREC.i_3:  m_prec = T_PREC._3; break;
          case T_PREC.i_5:  m_prec = T_PREC._5; break;
          case T_PREC.i_8:  m_prec = T_PREC._8; break;
          case T_PREC.i_13: m_prec = T_PREC._13; break;
        }
      }
    }

    /// <summary>
    /// Performing conversion on CNumber value to radians for trigonomic
    /// operations. The return value depends on T_TCS type giving before
    /// assignment.
    /// Can throw exception ServiceException on not supported conversion
    /// </summary>
    public double ToRadians
    {
      get
      {
        if (T_TCS.RADIANS == m_tcs)
        {
          return Convert.ToDouble(m_value);
        }
        else if (T_TCS.DEGREES == m_tcs)
        {
          return (Convert.ToDouble(m_value)*Math.PI/180);
        }
        else if (T_TCS.GRADS == m_tcs)
        {
          return (Convert.ToDouble(m_value)*Math.PI/200);
        }
        else
        {
          throw new ServiceException(
            "Unsupported type of trigonomic convertion.");
        }
      }
    }

    /// <summary>
    /// Getting type of inner value.
    /// It's Real (with floating point) if while FromString() convertion was
    /// detected that this number can't be converted to integer therefore it is real.
    /// </summary>
    /// <returns>true == Real</returns>
    public bool IsReal
    {
      get { return ((m_value is double)? true : false); }
    }

    /// <summary>
    /// Getting type of inner value.
    /// It's Integer (without floating point) if while FromString() was ENOUTH
    /// to convert it to Int64 (long) (if not - double), and in another case
    /// if while assigening a new value by property Value was POSSIBLE to
    /// convert it to an integer.
    /// </summary>
    /// <returns>true == Integer</returns>
    public bool IsInteger
    {
      get { return ((m_value is long)? true : false); }
    }

    /// <summary>
    /// Init number properties with default characteristics.</summary>
    public CNumber()
    {
      InitDefaults();
    }

    public CNumber(string _sNum)
    {
      InitDefaults();
      this.FromString(_sNum);
    }

    public CNumber(string _sNum, CNumber _Etalon)
    {
      InitDefaults();

      m_cs = _Etalon.m_cs;
      m_tcs = _Etalon.m_tcs;
      m_prec = _Etalon.m_prec;

      this.FromString(_sNum);
    }

    private void InitDefaults()
    {
      // default values
      m_cs = T_CS.DEC;
      m_tcs = T_TCS.RADIANS;
      m_prec = T_PREC.DEFAULT;

      // set separator, its important
      m_fp.NumberDecimalSeparator = ".";
      m_fp.NumberGroupSeparator = "'";
      m_fp.NumberGroupSizes = new int[] {3};

      m_RealStyle = NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent
        | NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite
        | NumberStyles.AllowTrailingWhite;

      m_IntegerStyle = NumberStyles.AllowDecimalPoint | NumberStyles.AllowExponent
        | NumberStyles.AllowLeadingSign | NumberStyles.AllowLeadingWhite
        | NumberStyles.AllowTrailingWhite;
    }

    /// <summary>
    /// Copy count-systems characteristics.
    /// </summary>
    /// <param name="_num">get from reference</param>
    public void GetProperties(CNumber _num)
    {
      m_cs = _num.m_cs;
      m_tcs = _num.m_tcs;
      m_prec = _num.m_prec;
    }

    /// <summary>
    /// Return double instance of a number.
    /// Use this instead of convertion: "(double)N.Value"</summary>
    public double dValue
    {
      get
      {
        if (m_value is double)
        {
          return (double)m_value;
        }
        else
        {
          return Convert.ToDouble(m_value);
        }
      }
    }

    /// <summary>
    /// Return long value of a number when definetly known
    /// that it is long. Use this instead of convertion: "(long)N.Value"
    /// </summary>
    public long lValue
    {
      get
      {
        CCalculator.STUB(m_value is long
          , "Invalid usage of type conversion inside CNumber.lValue");
        return (long)m_value;
      }
    }

    /// <summary>
    /// Accessor to CNumber value. Checks if after assigment it is
    /// posible to convert it into an Integer, if it not, remains double.
    /// To directly assign a new value use: n.Value = 3.1d or 234L
    /// </summary>
    public object Value
    {
      get
      {
        return m_value;
      }
      set
      {
        m_value = value;

        if (m_value is double)
        {
          if (CanBeInteger((double)m_value))
          {
            if ((double)m_value < long.MaxValue)
            {
              try
              {
                m_value = Convert.ToInt64(m_value);
              }
              catch (System.OverflowException)
              {
                // number to big to be integer
                // still double
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// Detects if a number dont have float-point tail
    /// and can be convertable into long type.
    /// </summary>
    /// <param name="_d">tested double value</param>
    /// <returns></returns>
    private bool CanBeInteger(double _d)
    {
      if (_d ==  Math.Ceiling(_d))
      {// it without floating point tail
        if (_d >= Int64.MinValue || _d <= Int64.MaxValue)
        {// it in bounds of long type
          return true;
        }
      }
      return false;
    }

    /// <summary>
    /// Convert given `_sNum` string representation of number to its digital
    /// equivalent, basing on culture format provider and specified
    /// Count-System (CS).
    /// Can throw ServiceException on trouble of conversion input string.
    /// </summary>
    /// <param name="_sNum"></param>
    public void FromString(string _sNum)
    {
      try
      {
        switch (m_cs)
        {
          case T_CS.DEC:
            m_value = long.Parse(_sNum, m_IntegerStyle, m_fp);
            break;

          case T_CS.HEX:
            m_value = (long) Convert.ToInt64(_sNum, 16);
            break;

          case T_CS.OCT:
            m_value = (long) Convert.ToInt64(_sNum, 8);
            break;

          case T_CS.BIN:
            m_value = (long) Convert.ToInt64(_sNum, 2);
            break;
        }
        // here long in any case
      }
      catch (System.FormatException)
      {
        if (m_cs == T_CS.DEC)
        {
          double dout = 0;
          if (!double.TryParse(_sNum, m_RealStyle, m_fp, out dout))
          {
            throw new ServiceException(
              "That >" + _sNum + "< number must be in \"" + m_cs.ToString() + "\" count system.");
          }
          m_value = dout;
          // here parsed to double
        }
        else
        {
          throw new ServiceException(
            "That >" + _sNum + "< number must be in \"" + m_cs.ToString() + "\" count system.");
        }
        // ...still double
      }
      catch (System.OverflowException)
      {
        if (m_cs == T_CS.DEC)
        {
          double dout = 0;
          if (!double.TryParse(_sNum, m_RealStyle, m_fp, out dout))
          {
            throw new ServiceException(
              '>' + _sNum + "< Number exceed 64bit floating point.");
          }
          m_value = dout;
          // here double
        }
        else
        {
          throw new ServiceException("Number exceed 64bits.");
        }
        // ...still double
      }
    }

    /// <summary>
    /// Convert digital substance of the CNumber value to its string equivalent,
    /// based on culture format provider, Count-System (CS) and precision.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      if (m_prec != T_PREC.DEFAULT && m_prec != T_PREC.ROUNDTRIP)
      {
        switch (m_prec)
        {
          case T_PREC._0:
            m_fp.NumberDecimalDigits = 0;
            break;

          case T_PREC._1:
            m_fp.NumberDecimalDigits = 1;
            break;

          case T_PREC._2:
            m_fp.NumberDecimalDigits = 2;
            break;

          case T_PREC._3:
            m_fp.NumberDecimalDigits = 3;
            break;

          case T_PREC._5:
            m_fp.NumberDecimalDigits = 5;
            break;

          case T_PREC._8:
            m_fp.NumberDecimalDigits = 8;
            break;

          case T_PREC._13:
            m_fp.NumberDecimalDigits = 13;
            break;
        }
      }

      string str = "";

      // if it decimal there is the sence to make precision fitting
      if (m_cs == T_CS.DEC)
      {
        // convert to default representation
        if (m_prec == T_PREC.DEFAULT)
        {
          str = ( ((m_value is double)?
            (double)m_value
            :(long)m_value) ).ToString();
        }
          // this precision gives recalculable result
        else if (m_prec == T_PREC.ROUNDTRIP)
        {
          str = ( ((m_value is double)?
            (double)m_value
            :(long)m_value) ).ToString("R");
        }
          // other precision
        else
        {
          str = ( ((m_value is double)?
            (double)m_value
            :(long)m_value) ).ToString("N", m_fp);
        }
      }
        // in case of other Count System the result will be `long` converted,
        // in case of double-type-banking that operation may lead to Overflow.
      else
      {
        try
        {
          switch (m_cs)
          {
            case T_CS.HEX:
              str = Convert.ToString( Convert.ToInt64(m_value), 16);
              break;

            case T_CS.OCT:
              str = Convert.ToString( Convert.ToInt64(m_value), 8);
              break;

            case T_CS.BIN:
              str = Convert.ToString( Convert.ToInt64(m_value), 2);
              break;
          }
        }
        catch (System.OverflowException)
        {
          CCalculator.ASSERT(false
            , "Result exceed 64bits in \"" + m_cs.ToString()
            + "\" count system.", "CNumber.ToString");
        }

        if (m_cs == T_CS.HEX)
        {
          str = "0x" + GroupBy(8, str);
        }
        else if (m_cs == T_CS.OCT)
        {
          str = GroupBy(3, str) + ".oct";
        }
        else if (m_cs == T_CS.BIN)
        {
          str = GroupBy(4, str) + ".bin";
        }
      }

      return str;
    }

    private string GroupBy(int _iGroup, string _str)
    {
      int iRemainder = 0;
      string strHead = "";
      if (_str.Length % _iGroup > 0)
      {
        iRemainder = _iGroup - _str.Length % _iGroup;
        while (iRemainder-- != 0)
        {
          strHead += "0";
        }
        return strHead + _str;
      }
      return _str;
    }

  };
}

//? EOF
