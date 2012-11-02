// Portions copyright 2005 - 2007: Diego Guidi
// Portions copyright 2006 - 2008: Rory Plaire (codekaizen@gmail.com)
//
// This file is part of GeoAPI.Net.
// GeoAPI.Net is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// GeoAPI.Net is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with GeoAPI.Net; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System;
using System.Collections;
using System.Collections.Generic;

namespace GeoAPI.DataStructures
{
    /// <summary>
    /// Represents a 2-tuple.
    /// </summary>
    /// <typeparam name="TItem">The type of item in the tuple.</typeparam>
    public struct Pair<TItem> : ITuple<TItem>, 
                                IComparable<Pair<TItem>>, 
                                IEquatable<Pair<TItem>>
        where TItem : IEquatable<TItem>, IComparable<TItem>
    {
        private readonly TItem _t0;
        private readonly TItem _t1;
        private readonly Boolean _isValueType;

        /// <summary>
        /// Creates a new <see cref="Pair{TItem}"/> with the given values.
        /// </summary>
        /// <param name="item1">The first item in the pair.</param>
        /// <param name="item2">The second item in the pair.</param>
        public Pair(TItem item1, TItem item2)
        {
            _t0 = item1;
            _t1 = item2;
            _isValueType = item1 is ValueType;
        }

        public override Boolean Equals(Object obj)
        {
            if (obj == null)
	        {
                return false;
	        }

            if (obj is Pair<TItem>)
            {
                return Equals((Pair<TItem>) obj);
            }

            ITuple<TItem> tuple = obj as ITuple<TItem>;

            if (tuple == null)
            {
                return false;
            }

            return tuple.Rank == Rank
                   && Equals(tuple[0], First)
                   && Equals(tuple[1], Second);
        }

        public override Int32 GetHashCode()
        {
            TItem defVal = default(TItem);
            Boolean useHash1 = _isValueType ? First.Equals(defVal) : Equals(First, defVal);
            Boolean useHash2 = _isValueType ? Second.Equals(defVal) : Equals(Second, defVal);

            Int32 hash =
                (useHash1 ? 37 : First.GetHashCode()) ^
                (useHash2 ? 17 : Second.GetHashCode()) ^
                Rank.GetHashCode();

            return hash;
        }

        public override String ToString()
        {
            return String.Format("({0}, {1})", First, Second);
        }

        /// <summary>
        /// Gets the first item in the pair.
        /// </summary>
        public TItem First
        {
            get { return _t0; }
        }

        /// <summary>
        /// Gets the second item in the pair.
        /// </summary>
        public TItem Second
        {
            get { return _t1; }
        }

        #region ITuple<TItem> Members

        public Int32 Rank
        {
            get { return 2; }
        }

        public TItem this[Int32 index]
        {
            get
            {
                if (index < 0 || index > 1)
                {
                    throw new ArgumentOutOfRangeException("index", index,
                                                          "Index must be 0 or 1 for " 
                                                          + GetType());
                }

                return index == 0 ? _t0 : _t1;
            }
        }

        #endregion

        #region IComparable<Pair<TItem>> Members

        public Int32 CompareTo(Pair<TItem> other)
        {
            Int32 comparison = Comparer<TItem>.Default.Compare(First, other.First);

            if (comparison != 0)
            {
                return comparison;
            }

            return Comparer<TItem>.Default.Compare(Second, other.Second);
        }

        #endregion

        #region IEquatable<Pair<TItem>> Members

        public Boolean Equals(Pair<TItem> other)
        {
            return _isValueType
                ? First.Equals(other.First) && Second.Equals(other.Second)
                : Equals(First, other.First) && Equals(Second, other.Second);
        }

        #endregion

        #region IEnumerable<TItem> Members

        public IEnumerator<TItem> GetEnumerator()
        {
            yield return _t0;
            yield return _t1;
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region IComparable<ITuple<TItem>> Members

        public Int32 CompareTo(ITuple<TItem> other)
        {
            Int32 comparison = simpleCompareCheck(other);

            if (comparison != 0)
            {
                return comparison;
            }

            comparison = Comparer<TItem>.Default.Compare(this[0], other[0]);

            if (comparison != 0)
            {
                return comparison;
            }

            return Comparer<TItem>.Default.Compare(this[1], other[1]);
        }

        #endregion

        #region IEquatable<ITuple<TItem>> Members

        public Boolean Equals(ITuple<TItem> other)
        {
            if (!simpleEqualityCheck(other))
            {
                return false;
            }

            return Equals(this[0], other[0]) && Equals(this[1], other[1]);
        }

        #endregion

        #region ITuple Members


        Object ITuple.this[Int32 index]
        {
            get { return this[index]; }
        }

        #endregion

        #region IComparable<ITuple> Members

        Int32 IComparable<ITuple>.CompareTo(ITuple other)
        {
            Int32 comparison = simpleCompareCheck(other);

            if (comparison != 0)
            {
                return comparison;
            }

            comparison = Comparer.Default.Compare(this[0], other[0]);

            if (comparison != 0)
            {
                return comparison;
            }

            comparison = Comparer.Default.Compare(this[1], other[1]);

            if (comparison != 0)
            {
                return comparison;
            }

            return Comparer.Default.Compare(this[2], other[2]);
        }

        #endregion

        #region IEquatable<ITuple> Members

        Boolean IEquatable<ITuple>.Equals(ITuple other)
        {
            if (!simpleEqualityCheck(other))
            {
                return false;
            }

            return Equals(this[0], other[0]) &&
                Equals(this[1], other[1]) &&
                Equals(this[2], other[2]);
        }

        #endregion
        private Int32 simpleCompareCheck(ITuple other)
        {
            if (ReferenceEquals(other, null)) return 1;
            if (other.Rank > Rank) return -1;
            if (other.Rank < Rank) return 1;

            return 0;
        }

        private Boolean simpleEqualityCheck(ITuple other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (other.Rank != Rank) return false;

            return true;
        }
    }
}
