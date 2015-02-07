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
   public class CBlockClaster
   {
      private string m_sOperator;
      private CNumber m_Number;

      public CBlockClaster()
      {
         m_sOperator = null;
         m_Number = null;
      }

      public string Operator
      {
         get { return m_sOperator; }
         set { m_sOperator = value; }
      }
      public CNumber Number
      {
         get { return m_Number; }
         set { m_Number = value; }
      }

      public bool IsNumber
      {
         get { return (m_Number != null); }
      }
      public bool IsOperator
      {
         get { return (m_Number == null); }
      }
   }

   //························································
   public class CBlock: NameObjectCollectionBase
   {
      private CBlockClaster m_claster = new CBlockClaster();
      private CNumber m_NumEtalon = new CNumber();

      /// <summary>
      /// Create an empty collection</summary>
      public CBlock()
      {
      }

      /// <summary>
      /// Clears all the elements in the collection.</summary>
      public void Init(CNumber _Template)
      {
         this.BaseClear();

         m_NumEtalon.GetProperties(_Template);
      }

      /// <summary>
      /// Gets a key-and-value pair (DictionaryEntry) using an index.</summary>
      public CBlockClaster this[int indx]
      {
         get
         {
            //m_claster.Operator = this.BaseGetKey(indx);
            //m_claster.Number = (CNumber)this.BaseGet(indx);
            if (this.BaseGet(indx) is string)
            {
               m_claster.Operator = (string)this.BaseGet(indx);
               m_claster.Number = null;
            }
            else if (this.BaseGet(indx) is CNumber)
            {
               m_claster.Operator = null;
               m_claster.Number = (CNumber)this.BaseGet(indx);
            }
            return m_claster;
         }
      }

      public void AddOperator(string _strOper)
      {
         this.BaseAdd(null, _strOper);
      }
      public void AddNumber(CNumber _Number)
      {
         this.BaseAdd(null, _Number);
      }
      public void AddNumber(string _strNumber)
      {
         this.BaseAdd(null, new CNumber(_strNumber, m_NumEtalon));
      }

      /// <summary>
      /// Removes an entry in the specified index from the collection.</summary>
      /// <param name="_indx">index to remove</param>
      public void Remove(int _indx)
      {
         this.BaseRemoveAt(_indx);
      }

      /// <summary>
      /// Create and return the sub-copy of the current object</summary>
      /// <param name="_from">from index in current object</param>
      /// <param name="_to">to index in current object</param>
      public CBlock Projection(int _from, int _to)
      {
         CBlock block = new CBlock();
         block.Init(m_NumEtalon);
         for (int i = _from; i <= _to; i++)
         {
            if (this[i].IsOperator)
            {
               block.BaseAdd(null, this[i].Operator);
            }
            else if (this[i].IsNumber)
            {
               block.BaseAdd(null, this[i].Number);
            }
         }
         return block;
      }

      /// <summary>
      /// Replace sub-section of the current block with _Res</summary>
      /// <param name="_Res">replace by this CNumber</param>
      /// <param name="_from">from index (included)</param>
      /// <param name="_to">to index (included)</param>
      public void ReplaceWith(CNumber _Res, int _from, int _to)
      {
         // remove next
         for (int i = _from; i < _to; i++)
         {
            this.BaseRemoveAt(_from);
         }
         /** insertion in this collection is impossible,
          * use that fact what last item to remove is always the number
          * so replace that number with new result
          * (cant do it at the `_from` becose there may be an operator,
          * but to assign a number need to reset the key to null and this
          * impossible without rebuilding all collection)
          * */
         this.BaseSet(_from, _Res);
      }
   }
}

//? EOF
