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
using GeoAPI.Coordinates;
using NPack.Interfaces;

namespace GeoAPI.CoordinateSystems
{
    /// <summary>
    /// Creates spatial reference objects using codes.
    /// </summary>
    /// <remarks>
    /// The codes are maintained by an external authority. 
    /// A commonly used authority is EPSG.
    /// </remarks>
    public interface ICoordinateSystemAuthorityFactory<TCoordinate> : ICoordinateSystemAuthorityFactory
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>,
                            IComparable<TCoordinate>, IConvertible,
                            IComputable<Double, TCoordinate>
    {
        /// <summary>
        /// Returns a projected coordinate system object corresponding to the given code.
        /// </summary>
        /// <param name="code">The identification code.</param>
        /// <returns>The projected coordinate system object with the given code.</returns>
        new IProjectedCoordinateSystem<TCoordinate> CreateProjectedCoordinateSystem(Int64 code);

        /// <summary>
        /// Returns a geographic coordinate system object corresponding to the given code.
        /// </summary>
        /// <param name="code">The identification code.</param>
        /// <returns>The geographic coordinate system object with the given code.</returns>
        new IGeographicCoordinateSystem<TCoordinate> CreateGeographicCoordinateSystem(Int64 code);

        /// <summary>
        /// Create an <see cref="IVerticalCoordinateSystem{TCoordinate}" /> instace from a code.
        /// </summary>
        /// <param name="code">Authority code.</param>
        /// <returns>The vertical coordinate system for the given code.</returns>
        new IVerticalCoordinateSystem<TCoordinate> CreateVerticalCoordinateSystem(Int64 code);

        /// <summary>
        /// Creates a 3D coordinate system from a code.
        /// </summary>
        /// <param name="code">Authority code.</param>
        /// <returns>Compound coordinate system for the given code</returns>
        new ICompoundCoordinateSystem<TCoordinate> CreateCompoundCoordinateSystem(Int64 code);

        /// <summary>
        /// Creates an <see cref="IHorizontalCoordinateSystem{TCoordinate}" /> instance from a code.
        /// The horizontal coordinate system could be geographic or projected.
        /// </summary>
        /// <param name="code">Authority code.</param>
        /// <returns>Horizontal coordinate system for the given code.</returns>
        new IHorizontalCoordinateSystem<TCoordinate> CreateHorizontalCoordinateSystem(Int64 code);
    }
}
