// Copyright 2006 - 2008: Rory Plaire (codekaizen@gmail.com)
//
// This file is part of SharpMap.
// SharpMap is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// SharpMap is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with SharpMap; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System;
using System.Collections.Generic;
using System.Globalization;

namespace GeoAPI.DataStructures
{
    public class StringEqualityComparer : IEqualityComparer<String>
    {
        private readonly StringComparison _comparison;
        private static readonly Dictionary<StringComparison, StringEqualityComparer> _comparerMap 
            = new Dictionary<StringComparison, StringEqualityComparer>();

        private static readonly Object _compareInitSync = new Object();

// ReSharper disable EmptyConstructor
        static StringEqualityComparer() {}
// ReSharper restore EmptyConstructor

        public static StringEqualityComparer GetComparer(StringComparison comparison)
        {
            StringEqualityComparer comparer;

            if (!_comparerMap.TryGetValue(comparison, out comparer))
            {
                lock (_compareInitSync)
                {
                    if (!_comparerMap.TryGetValue(comparison, out comparer))
                    {
                        comparer = new StringEqualityComparer(comparison);
                        _comparerMap[comparison] = comparer;
                    }
                }
            }

            return comparer;
        }

        public StringEqualityComparer(StringComparison comparison) 
        {
            _comparison = comparison;
        }

        public StringComparison Comparison
        {
            get { return _comparison; }
        }

        #region IEqualityComparer<string> Members

        public Boolean Equals(String x, String y)
        {
            return String.Compare(x, y, Comparison) == 0;
        }

        public Int32 GetHashCode(String obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(obj);
            }

            switch (_comparison)
            {
                case StringComparison.CurrentCultureIgnoreCase:
                    return obj.ToLower(CultureInfo.CurrentCulture).GetHashCode();
                case StringComparison.InvariantCultureIgnoreCase:
                    return obj.ToLowerInvariant().GetHashCode();
                case StringComparison.OrdinalIgnoreCase:
                    return obj.ToLower().GetHashCode();
                case StringComparison.Ordinal:
                case StringComparison.InvariantCulture:
                case StringComparison.CurrentCulture:
                    return obj.GetHashCode();
                default:
                    throw new InvalidOperationException("Unknown 'Comparison' value: " + Comparison);
            }
        }

        #endregion
    }
}
