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

namespace GeoAPI.DataStructures
{
    /// <summary>
    /// Describes an object which can contain an item of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of item to determine the contain status with.</typeparam>
#if DOTNET40
    public interface IContainable<in T>
#else
    public interface IContainable<T>
#endif
    {
        /// <summary> 
        /// Check if the region defined by <paramref name="other"/>
        /// is contained by the region of the <see cref="IContainable{T}"/>.
        /// </summary>
        /// <param name="other">
        /// The object which the <see cref="IContainable{T}"/> is
        /// being checked for containment.
        /// </param>
        /// <returns>        
        /// <see langword="true"/> if <see cref="IContainable{T}"/> contains <paramref name="other"/>.
        /// </returns>
        Boolean Contains(T other);
    }
}
