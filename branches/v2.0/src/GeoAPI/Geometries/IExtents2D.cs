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

namespace GeoAPI.Geometries
{
    public interface IExtents2D : IExtents
    {
        /// <summary>
        /// Returns the <see cref="IExtents{TCoordinate}"/>s minimum x-value.
        /// </summary>
        /// <returns>The minimum x-coordinate.</returns>
        /// <remarks>
        /// <see cref="XMin"/> > <see cref="XMax"/> indicates that this is an empty 
        /// <see cref="IExtents{TCoordinate}"/>.
        /// </remarks>
        Double XMin { get; }

        /// <summary>
        /// Returns the <see cref="IExtents{TCoordinate}"/>s maximum x-value. 
        /// </summary>
        /// <returns>The maximum x-coordinate.</returns>
        /// <remarks>
        /// <see cref="XMin"/> > <see cref="XMax"/> indicates that this is an empty 
        /// <see cref="IExtents{TCoordinate}"/>.
        /// </remarks>
        Double XMax { get; }

        /// <summary>
        /// Returns the <see cref="IExtents{TCoordinate}"/>s minimum y-value. 
        /// </summary>
        /// <returns>The minimum y-coordinate.</returns>
        /// <remarks>
        /// <see cref="YMin"/> > <see cref="YMax"/> indicates that this is an empty 
        /// <see cref="IExtents{TCoordinate}"/>.
        /// </remarks>
        Double YMin { get; }

        /// <summary>
        /// Returns the <see cref="IExtents{TCoordinate}"/>s maximum y-value.
        /// </summary>
        /// <returns>The maximum y-coordinate.</returns>
        /// <remarks>
        /// <see cref="YMin"/> > <see cref="YMax"/> indicates that this is an empty 
        /// <see cref="IExtents{TCoordinate}"/>.
        /// </remarks>
        Double YMax { get; }

        Double Width { get; }
        Double Height { get; }
        Double Area { get; }
    }
}
