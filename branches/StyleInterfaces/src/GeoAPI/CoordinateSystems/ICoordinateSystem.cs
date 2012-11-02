// Portions copyright 2005 - 2006: Morten Nielsen (www.iter.dk)
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
using GeoAPI.Geometries;

namespace GeoAPI.CoordinateSystems
{
    public interface ICoordinateSystem : IInfo
    {
        /// <summary>
        /// Dimension of the coordinate system.
        /// </summary>
        Int32 Dimension { get; }

        /// <summary>
        /// Gets axis details for dimension within coordinate system.
        /// </summary>
        /// <param name="dimension">Dimension</param>
        /// <returns>Axis info</returns>
        IAxisInfo GetAxis(Int32 dimension);

        /// <summary>
        /// Gets units for dimension within coordinate system.
        /// </summary>
        IUnit GetUnits(Int32 dimension);

        /// <summary>
        /// Gets default envelope of coordinate system.
        /// </summary>
        /// <remarks>
        /// Gets default envelope of coordinate system. Coordinate systems 
        /// which are bounded should return the minimum bounding box of their 
        /// domain. Unbounded coordinate systems should return a box which is 
        /// as large as is likely to be used. For example, a (lon, lat) 
        /// geographic coordinate system in degrees should return a box from 
        /// (-180, -90) to (180, 90), and a geocentric coordinate system could 
        /// return a box from (-r, -r, -r) to (+r, +r, +r) where r is the 
        /// approximate radius of the Earth.
        /// </remarks>
        IExtents DefaultEnvelope { get; }
    }
}
