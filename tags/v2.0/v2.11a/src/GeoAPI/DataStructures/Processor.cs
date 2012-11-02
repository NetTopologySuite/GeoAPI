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
using System.Collections.Generic;

namespace GeoAPI.DataStructures
{
    public static class Processor
    {
        /// <summary>
        /// Executes a function on each item in a <see cref="IEnumerable{T}" />
        /// and returns the results in a new <see cref="IEnumerable{T}" />.
        /// </summary>
        public static IEnumerable<TItem> Transform<TItem>(IEnumerable<TItem> items,
                                                          Func<TItem, TItem> func)
        {
            foreach (TItem item in items)
            {
                yield return func(item);
            }
        }

        /// <summary>
        /// Executes a function on each item in a <see cref="IEnumerable{TItem}" />
        /// and returns the results in a new <see cref="IEnumerable{TResult}" />.
        /// </summary>
        /// <typeparam name="TItem">The type of the input enumeration members.</typeparam>
        /// <typeparam name="TResult">The type of the output enumeration members.</typeparam>
        public static IEnumerable<TResult> Transform<TItem, TResult>(IEnumerable<TItem> items,
                                                                     Func<TItem, TResult> func)
        {
            foreach (TItem item in items)
            {
                yield return func(item);
            }
        }

        public static IEnumerable<TResult> Select<TItem, TResult>(IEnumerable<TItem> items,
                                                                     Func<TItem, TResult> func)
        {
            return Transform(items, func);
        }


        public static IEnumerable<TItem> Where<TItem>(IEnumerable<TItem> items, Predicate<TItem> clause)
        {
            foreach (TItem item in items)
                if (clause(item))
                    yield return item;
        }


    }
}
