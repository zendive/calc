/**
 * Author: Alexander Block
 * Is a part of Calc application
 * developed for C# study goals and is an intelectual
 * property of his author.
*/

using System;
using System.Collections.Specialized;

namespace Calc
{
  public class CRegister: NameObjectCollectionBase
  {
    /// <summary>
    /// Create an collection of default registers</summary>
    public CRegister()
    {
      InitDefault();
    }

    /// <summary>
    /// Gets or sets the value associated with the specified key.</summary>
    public CNumber this[string indx]
    {
      get { return (CNumber)this.BaseGet(indx); }
      set { this.BaseSet(indx, value); }
    }

    /// <summary>
    /// Adds an entry to the collection.</summary>
    /// <param name="_sKey">string key</param>
    /// <param name="_o">object value</param>
    public void Add(string _sName, object _oValue)
    {
      this.BaseAdd(_sName, _oValue);
    }

    /// <summary>
    /// Removes an entry with the specified key from the collection.</summary>
    /// <param name="_sKey">_sKey</param>
    public void Remove(string _sKey)
    {
      this.BaseRemove(_sKey);
    }

    /// <summary>
    /// Removes an entry in the specified index from the collection.</summary>
    /// <param name="_indx">index to remove</param>
    public void Remove(int _indx)
    {
      this.BaseRemoveAt(_indx);
    }

    /// <summary>
    /// Clears all the elements in the collection.</summary>
    public void Clear()
    {
      this.BaseClear();
    }

    #region DATA TYPES
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>
    /// reserved registers enumeration</summary>
    public enum T_REG: int
    {
      _FIRST = 0,
      ANS = 0, PI, _E, GS, TRUE, FALSE,
      X, Y, Z = 8,
      _LAST = 8
    };
    /// <summary>
    /// array of default registers
    /// </summary>
    public readonly string[] m_sREG =
      {
        "ans", "pi", "_e", "@", "true", "false",
        "x", "y", "z"
      };
    /// <summary>
    /// Register array accessor
    /// </summary>
    /// <param name="_reg">register enumerator</param>
    /// <returns>string witch is represent a register</returns>
    public string the(T_REG _reg)
    {
      return m_sREG[(int)_reg];
    }
    #endregion DATA TYPES
      
    // consructor with user registers
    public CRegister(string[] m_Def)
    {
      InitDefault();
    }

    /// <summary>
    /// Init reserved registers and constants, order of declaration are
    /// extreamly important: by register's string length descending!</summary>
    private void InitDefault()
    {
      this.BaseAdd(the(T_REG.FALSE), new CNumber("0"));
      this.BaseAdd(the(T_REG.TRUE), new CNumber("-1"));
      this.BaseAdd(the(T_REG.ANS), new CNumber("0"));
      this.BaseAdd(the(T_REG.PI)
        , new CNumber("3.1415926535897932384626433832795028841972"));
      // e number is defined as "_e" and after any register name
      // with e character in name, to avoid accidentally replacement
      this.BaseAdd(the(T_REG._E)
        , new CNumber("2.7182818284590452353602874713526624977572"));
      // Gold section: (1 + #(5))/2 == @
      this.BaseAdd(the(T_REG.GS)
        , new CNumber("1.6180339887498948482045868343656381177203"));
      this.BaseAdd(the(T_REG.X), new CNumber("0"));
      this.BaseAdd(the(T_REG.Y), new CNumber("0"));
      this.BaseAdd(the(T_REG.Z), new CNumber("0"));
    }

    /// <summary>
    /// Answer register.</summary>
    public CNumber ANSWER
    {
      get { return this[the(T_REG.ANS)]; }
      set { this[the(T_REG.ANS)] = value; }
    }

    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>
    /// Detects if given string _reg is one of registers.
    /// </summary>
    /// <param name="_reg">tested string</param>
    /// <returns>true if _reg is register</returns>
    public bool IsRegister(string _reg)
    {
      for (int i = 0; i < this.Count; i++)
      {
        if (_reg == this.BaseGetKey(i))
        {
          return true;
        }
      }
      return false;
    }
      
    //••••••••••••••••••••••••••••••••••••••••••••••••••••••••
    /// <summary>
    /// Detects if given string is a start of any register name
    /// </summary>
    /// <param name="_reg">tested string</param>
    /// <returns>true if is may be a register</returns>
    public bool IsMaybeRegister(string _reg)
    {
      string baseReg;
      for (int i = 0; i < this.Count; i++)
      {
        baseReg = this.BaseGetKey(i);
        if (baseReg.StartsWith(_reg)
          && (_reg.Length <= baseReg.Length))
        {
          return true;
        }
      }
      return false;
    }
  }
}

//? EOF
