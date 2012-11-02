// Copyright 2006 - 2008: Rory Plaire (codekaizen@gmail.com)
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

using GeoAPI.DataStructures;

namespace GeoAPI.Indexing
{
    /// <summary>
    /// A bounded object in a spatial index.
    /// </summary>
    /// <typeparam name="TBounds">The type used to describe the bounds.</typeparam>
    public interface IBoundable<TBounds> : IIntersectable<TBounds>
    {
        /// <summary> 
        /// Returns a representation of space that encloses the boundable object,
        /// preferably not much bigger than this <see cref="IBoundable{TBounds}"/>'s 
        /// boundary yet fast to test for intersection
        /// with the bounds of other <see cref="IBoundable{TBounds}"/> instances.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The type of object returned depends on the implementation of 
        /// the client object, such as an <see cref="ISpatialIndex{TBounds, TItem}"/>.
        /// </para>
        /// </remarks>
        /// <returns> 
        /// A <typeparamref name="TBounds"/> which describes the bounds of the boundable object.
        /// </returns>
        TBounds Bounds { get; }
    }
}