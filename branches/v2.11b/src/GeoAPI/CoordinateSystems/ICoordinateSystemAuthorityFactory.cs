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

namespace GeoAPI.CoordinateSystems
{
    /// <summary>
    /// Creates spatial reference objects using codes.
    /// </summary>
    /// <remarks>
    /// The codes are maintained by an external authority. 
    /// A commonly used authority is EPSG.
    /// </remarks>
    public interface ICoordinateSystemAuthorityFactory
    {
        /// <summary>
        /// Returns the authority name for this factory (e.g., "EPSG" or "POSC").
        /// </summary>
        String Authority { get; }

        /// <summary>
        /// Returns a projected coordinate system object corresponding to the given code.
        /// </summary>
        /// <param name="code">The identification code.</param>
        /// <returns>The projected coordinate system object with the given code.</returns>
        IProjectedCoordinateSystem CreateProjectedCoordinateSystem(Int64 code);

        /// <summary>
        /// Returns a geographic coordinate system object corresponding to the given code.
        /// </summary>
        /// <param name="code">The identification code.</param>
        /// <returns>The geographic coordinate system object with the given code.</returns>
        IGeographicCoordinateSystem CreateGeographicCoordinateSystem(Int64 code);

        /// <summary>
        /// Returns a horizontal datum object corresponding to the given code.
        /// </summary>
        /// <param name="code">The identification code.</param>
        /// <returns>The horizontal datum object with the given code.</returns>
        IHorizontalDatum CreateHorizontalDatum(Int64 code);

        /// <summary>
        /// Returns an ellipsoid object corresponding to the given code.
        /// </summary>
        /// <param name="code">The identification code.</param>
        /// <returns>The ellipsoid object with the given code.</returns>
        IEllipsoid CreateEllipsoid(Int64 code);

        /// <summary>
        /// Returns a prime meridian object corresponding to the given code.
        /// </summary>
        /// <param name="code">The identification code.</param>
        /// <returns>The prime meridian object with the given code.</returns>
        IPrimeMeridian CreatePrimeMeridian(Int64 code);

        /// <summary>
        /// Returns a linear unit object corresponding to the given code.
        /// </summary>
        /// <param name="code">The identification code.</param>
        /// <returns>The linear unit object with the given code.</returns>
        ILinearUnit CreateLinearUnit(Int64 code);

        /// <summary>
        /// Returns an <see cref="IAngularUnit" /> instance corresponding to the given code.
        /// </summary>
        /// <param name="code">The identification code.</param>
        /// <returns>The angular unit object for the given code.</returns>
        IAngularUnit CreateAngularUnit(Int64 code);

        /// <summary>
        /// Creates an <see cref="IVerticalDatum"/> instance from a code.
        /// </summary>
        /// <param name="code">Authority code.</param>
        /// <returns>Vertical datum for the given code.</returns>
        IVerticalDatum CreateVerticalDatum(Int64 code);

        /// <summary>
        /// Create an <see cref="IVerticalCoordinateSystem{TCoordinate}" /> instace from a code.
        /// </summary>
        /// <param name="code">Authority code.</param>
        /// <returns>The vertical coordinate system for the given code.</returns>
        IVerticalCoordinateSystem CreateVerticalCoordinateSystem(Int64 code);

        /// <summary>
        /// Creates a 3D coordinate system from a code.
        /// </summary>
        /// <param name="code">Authority code.</param>
        /// <returns>Compound coordinate system for the given code</returns>
        ICompoundCoordinateSystem CreateCompoundCoordinateSystem(Int64 code);

        /// <summary>
        /// Creates an <see cref="IHorizontalCoordinateSystem{TCoordinate}" /> instance from a code.
        /// The horizontal coordinate system could be geographic or projected.
        /// </summary>
        /// <param name="code">Authority code.</param>
        /// <returns>Horizontal coordinate system for the given code.</returns>
        IHorizontalCoordinateSystem CreateHorizontalCoordinateSystem(Int64 code);

        /// <summary>
        /// Gets a description of the object corresponding to a code.
        /// </summary>
        String DescriptionText { get; }

        /// <summary>
        /// Gets the Geoid code from a WKT name.
        /// </summary>
        /// <remarks>
        /// In the OGC definition of WKT horizontal datums, the geoid is referenced 
        /// by a quoted String, which is used as a key value. This method converts 
        /// the key value String into a code recognized by this authority.
        /// </remarks>
        /// <param name="wkt"></param>
        /// <returns></returns>
        String GeoidFromWktName(String wkt);

        /// <summary>
        /// Gets the WKT name of a Geoid.
        /// </summary>
        /// <remarks>
        /// In the OGC definition of WKT horizontal datums, the geoid is referenced by 
        /// a quoted String, which is used as a key value. This method gets the OGC WKT 
        /// key value from a geoid code.
        /// </remarks>
        /// <param name="geoid"></param>
        /// <returns></returns>
        String WktGeoidName(String geoid);
    }
}
