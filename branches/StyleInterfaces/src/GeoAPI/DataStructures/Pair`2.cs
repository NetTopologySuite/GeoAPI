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
    /// <typeparam name="TItem1">The type of first item in the tuple.</typeparam>
    /// <typeparam name="TItem2">The type of second item in the tuple.</typeparam>
    public struct Pair<TItem1, TItem2> : ITuple, IComparable<Pair<TItem1, TItem2>>, IEquatable<Pair<TItem1, TItem2>>
        where TItem1 : IEquatable<TItem1>, IComparable<TItem1>
        where TItem2 : IEquatable<TItem2>, IComparable<TItem2>
    {
        private readonly TItem1 _t0;
        private readonly TItem2 _t1;
        private readonly Boolean _isT0ValueType;
        private readonly Boolean _isT1ValueType;

        /// <summary>
        /// Creates a new <see cref="Pair{TItem}"/> with the given values.
        /// </summary>
        /// <param name="item1">The first item in the pair.</param>
        /// <param name="item2">The second item in the pair.</param>
        public Pair(TItem1 item1, TItem2 item2)
        {
            _t0 = item1;
            _t1 = item2;
            _isT0ValueType = typeof(TItem1).IsValueType;
            _isT1ValueType = typeof(TItem2).IsValueType;
        }

        public override Boolean Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is Pair<TItem1, TItem2>)
            {
                return Equals((Pair<TItem1, TItem2>)obj);
            }

            ITuple tuple = obj as ITuple;

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
            TItem1 defValT0 = default(TItem1);
            TItem2 defValT1 = default(TItem2);
            Boolean useHash1 = _isT0ValueType ? First.Equals(defValT0) : Equals(First, defValT0);
            Boolean useHash2 = _isT1ValueType ? Second.Equals(defValT1) : Equals(Second, defValT1);

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
        public TItem1 First
        {
            get { return _t0; }
        }

        /// <summary>
        /// Gets the second item in the pair.
        /// </summary>
        public TItem2 Second
        {
            get { return _t1; }
        }

        #region ITuple<TItem> Members

        public Int32 Rank
        {
            get { return 2; }
        }

        public Object this[Int32 index]
        {
            get
            {
                if (index < 0 || index > 1)
                {
                    throw new ArgumentOutOfRangeException("index", index,
                                                          "Index must be 0 or 1 for " + GetType());
                }

                return index == 0 ? _t0 : (Object)_t1;
            }
        }

        #endregion

        #region IComparable<Pair<TItem>> Members

        public Int32 CompareTo(Pair<TItem1, TItem2> other)
        {
            Int32 compare = _isT0ValueType 
                ? First.CompareTo(other.First)
                : Comparer<TItem1>.Default.Compare(First, other.First);

            return compare != 0
                ? compare
                :  _isT1ValueType
                    ? Second.CompareTo(other.Second)
                    : Comparer<TItem2>.Default.Compare(Second, other.Second);
        }

        #endregion

        #region IEquatable<Pair<TItem>> Members

        public Boolean Equals(Pair<TItem1, TItem2> other)
        {
            return (_isT0ValueType ? First.Equals(other.First) : Equals(First, other.First))
                   ? (_isT1ValueType ? Second.Equals(other.Second) : Equals(Second, other.Second))
                   : false;
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            yield return _t0;
            yield return _t1;
        }

        #endregion

        #region IComparable<ITuple<TItem>> Members

        public Int32 CompareTo(ITuple other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            if (other.Rank > Rank)
            {
                return -1;
            }

            if (other.Rank < Rank)
            {
                return 1;
            }

            Int32 comparison = Comparer.Default.Compare(this[0], other[0]);

            if (comparison != 0)
            {
                return comparison;
            }

            return Comparer.Default.Compare(this[1], other[1]);
        }

        #endregion

        #region IEquatable<ITuple<TItem>> Members

        public Boolean Equals(ITuple other)
        {
            if (ReferenceEquals(other, null))
            {
                return false;
            }

            if (other.Rank != Rank)
            {
                return false;
            }

            return Equals(this[0], other[0]) && Equals(this[1], other[1]);
        }

        #endregion
    }
}
