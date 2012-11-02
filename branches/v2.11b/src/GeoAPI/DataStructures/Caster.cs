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
    /// Casts
    /// </summary>
    public static class Caster
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TT"></typeparam>
        /// <typeparam name="TU"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IEnumerable<TT> Upcast<TT, TU>(IEnumerable<TU> items)
            where TU : TT
        {
            if (items == null)
                yield break;

            foreach (TU item in items)
            {
                yield return item;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TT"></typeparam>
        /// <typeparam name="TU"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IEnumerable<TT> Downcast<TT, TU>(IEnumerable<TU> items)
            where TT : TU
        {
            if (items == null)
                yield break;

            foreach (TT item in items)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Casts all items in an <see cref="IEnumerable"/> to a specific type. If an item is not of type T, <value>null</value> is returned.
        /// </summary>
        /// <typeparam name="T">the Type to cast to</typeparam>
        /// <param name="enumerable"></param>
        /// <returns>a casted enumerable</returns>
        public static IEnumerable<T> Cast<T>(IEnumerable enumerable)
        {

            if (enumerable == null)
                yield break;

            foreach (T item in enumerable)
            {
                yield return item;
            }
        }

        /// <summary>
        /// Casts all items in an <see cref="IEnumerable"/> to a specific type. If an item is not of type T, it is ignored.
        /// </summary>
        /// <typeparam name="T">the Type to cast to</typeparam>
        /// <param name="enumerable"></param>
        /// <returns>a casted enumerable</returns>
        public static IEnumerable<T> CastNoNulls<T>(IEnumerable enumerable)
        {
            if (enumerable == null)
                yield break;

            foreach (Object item in enumerable)
            {
                if (item is T)
                {
                    yield return (T)item;
                }
            }
        }
    }
}
