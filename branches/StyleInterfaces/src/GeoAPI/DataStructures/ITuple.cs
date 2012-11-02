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

namespace GeoAPI.DataStructures
{
    /// <summary>
    /// Represents an ordered collection of homogeneous items.
    /// </summary>
    public interface ITuple : IEnumerable, IComparable<ITuple>, IEquatable<ITuple>
    {
        /// <summary>
        /// Gets the number of items in the tuple.
        /// </summary>
        Int32 Rank { get; }

        /// <summary>
        /// Gets the item at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The position of the item in the tuple.</param>
        /// <returns>The item at the given position in the tuple.</returns>
        Object this[Int32 index] { get; }
    }
}
