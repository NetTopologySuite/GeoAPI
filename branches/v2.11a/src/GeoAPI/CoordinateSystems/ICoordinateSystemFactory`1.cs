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
using System.Collections.Generic;
using GeoAPI.Coordinates;
using GeoAPI.Geometries;
using NPack.Interfaces;

namespace GeoAPI.CoordinateSystems
{
    /// <summary>
    /// Represents a factory instance for building up complex 
    /// coordinate system instances from simpler instances or values.
    /// </summary>
    /// <remarks>
    /// <para>
    /// A <see cref="ICoordinateSystemFactory{TCoordinate}"/> allows applications 
    /// to make coordinate systems that  cannot be created by a 
    /// <see cref="ICoordinateSystemAuthorityFactory{TCoordinate}"/>. 
    /// This factory is very flexible, whereas the authority factory is easier to use.
    /// </para>
    /// <para>
    /// A <see cref="ICoordinateSystemAuthorityFactory{TCoordinate}"/> can be used 
    /// to make coordinate systems which are 'standard' or well documented by an 
    /// authority, and <see cref="ICoordinateSystemFactory{TCoordinate}"/> can be used 
    /// to make coordinate systems which are suited for a specific use, and not well 
    /// known outside a local geography.
    /// </para>
    /// <para>
    /// For example, the EPSG authority has codes for USA state plane coordinate systems 
    /// using the NAD83 datum, but these coordinate systems always use meters. EPSG does not 
    /// have codes for NAD83 state plane coordinate systems that use feet units. This factory
    /// lets an application create such a hybrid coordinate system.
    /// </para>
    /// </remarks>
    public interface ICoordinateSystemFactory<TCoordinate> : ICoordinateSystemFactory
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>,
                            IComparable<TCoordinate>, IConvertible,
                            IComputable<Double, TCoordinate>
    {
        /// <summary>
        /// Creates a <see cref="ICompoundCoordinateSystem{TCoordinate}"/>.
        /// </summary>
        /// <param name="name">Name of compound coordinate system.</param>
        /// <param name="head">Head coordinate system.</param>
        /// <param name="tail">Tail coordinate system.</param>
        /// <returns>Compound coordinate system.</returns>
        ICompoundCoordinateSystem<TCoordinate> CreateCompoundCoordinateSystem(
                                                    ICoordinateSystem<TCoordinate> head,
                                                    ICoordinateSystem<TCoordinate> tail, 
                                                    String name);

        /// <summary>
        /// Creates a <see cref="ICompoundCoordinateSystem{TCoordinate}"/>.
        /// </summary>
        /// <param name="name">Name of compound coordinate system.</param>
        /// <param name="head">Head coordinate system.</param>
        /// <param name="tail">Tail coordinate system.</param>
        /// <returns>Compound coordinate system.</returns>
        ICompoundCoordinateSystem<TCoordinate> CreateCompoundCoordinateSystem(
                                                    ICoordinateSystem<TCoordinate> head,
                                                    ICoordinateSystem<TCoordinate> tail, 
                                                    String name, String authority,
                                                    String authorityCode, String alias,
                                                    String abbreviation, String remarks);

        /// <summary>
        /// Creates a <see cref="IFittedCoordinateSystem{TCoordinate}"/>.
        /// </summary>
        /// <remarks>
        /// The units of the axes in the fitted coordinate system will be 
        /// inferred from the units of the base coordinate system. If the affine map
        /// performs a rotation, then any mixed axes must have identical units. For
        /// example, a (lat_deg, lon_deg, height_feet) system can be rotated in the 
        /// (lat, lon) plane, since both affected axes are in degrees. But you 
        /// should not rotate this coordinate system in any other plane.
        /// </remarks>
        /// <param name="name">Name of coordinate system.</param>
        /// <param name="baseCoordinateSystem">Base coordinate system.</param>
        /// <returns>An <see cref="IFittedCoordinateSystem{TCoordinate}"/> instance.</returns>
        IFittedCoordinateSystem<TCoordinate> CreateFittedCoordinateSystem(
                                                    ICoordinateSystem<TCoordinate> baseCoordinateSystem,
                                                    String toBaseWkt, 
                                                    IEnumerable<IAxisInfo> axes, 
                                                    String name);

        /// <summary>
        /// Creates a <see cref="IFittedCoordinateSystem{TCoordinate}"/>.
        /// </summary>
        /// <remarks>
        /// The units of the axes in the fitted coordinate system will be 
        /// inferred from the units of the base coordinate system. If the affine map
        /// performs a rotation, then any mixed axes must have identical units. For
        /// example, a (lat_deg, lon_deg, height_feet) system can be rotated in the 
        /// (lat, lon) plane, since both affected axes are in degrees. But you 
        /// should not rotate this coordinate system in any other plane.
        /// </remarks>
        /// <param name="name">Name of coordinate system.</param>
        /// <param name="baseCoordinateSystem">Base coordinate system.</param>
        /// <returns>An <see cref="IFittedCoordinateSystem{TCoordinate}"/> instance.</returns>
        IFittedCoordinateSystem<TCoordinate> CreateFittedCoordinateSystem(
                                                    ICoordinateSystem<TCoordinate> baseCoordinateSystem,
                                                    String toBaseWkt, 
                                                    IEnumerable<IAxisInfo> axes, 
                                                    String name, 
                                                    String authority, 
                                                    String authorityCode, 
                                                    String alias,
                                                    String abbreviation, 
                                                    String remarks);

        /// <summary>
        /// Creates a coordinate system object from an XML String.
        /// </summary>
        /// <param name="xml">XML representation for the spatial reference.</param>
        /// <returns>The resulting spatial reference object.</returns>
        new ICoordinateSystem<TCoordinate> CreateFromXml(String xml);

        /// <summary>
        /// Creates a spatial reference object given its Well-Known Text representation.
        /// The output object may be either a 
        /// <see cref="IGeographicCoordinateSystem{TCoordinate}"/> or
        /// a <see cref="IProjectedCoordinateSystem{TCoordinate}"/>.
        /// </summary>
        /// <param name="wkt">
        /// The Well-Known Text representation for the spatial reference.
        /// </param>
        /// <returns>The resulting spatial reference object.</returns>
        new ICoordinateSystem<TCoordinate> CreateFromWkt(String wkt);

        /// <summary>
        /// Creates a <see cref="IGeographicCoordinateSystem{TCoordinate}"/>, which 
        /// could be Lat / Lon or Lon / Lat.
        /// </summary>
        /// <param name="name">Name of geographical coordinate system.</param>
        /// <param name="angularUnit">Angular units.</param>
        /// <param name="datum">Horizontal datum.</param>
        /// <param name="primeMeridian">Prime meridian.</param>
        /// <param name="axis0">First axis.</param>
        /// <param name="axis1">Second axis.</param>
        /// <returns>Geographic coordinate system.</returns>
        IGeographicCoordinateSystem<TCoordinate> CreateGeographicCoordinateSystem(
                                                        IExtents<TCoordinate> extents, 
                                                        IAngularUnit angularUnit, 
                                                        IHorizontalDatum datum,
                                                        IPrimeMeridian primeMeridian, 
                                                        IAxisInfo axis0, 
                                                        IAxisInfo axis1, 
                                                        String name);

        /// <summary>
        /// Creates a <see cref="IGeographicCoordinateSystem{TCoordinate}"/>, which 
        /// could be Lat / Lon or Lon / Lat.
        /// </summary>
        /// <param name="name">Name of geographical coordinate system.</param>
        /// <param name="angularUnit">Angular units.</param>
        /// <param name="datum">Horizontal datum.</param>
        /// <param name="primeMeridian">Prime meridian.</param>
        /// <param name="axis0">First axis.</param>
        /// <param name="axis1">Second axis.</param>
        /// <returns>Geographic coordinate system.</returns>
        IGeographicCoordinateSystem<TCoordinate> CreateGeographicCoordinateSystem(
                                                        IExtents<TCoordinate> extents, 
                                                        IAngularUnit angularUnit, 
                                                        IHorizontalDatum datum,
                                                        IPrimeMeridian primeMeridian, 
                                                        IAxisInfo axis0, 
                                                        IAxisInfo axis1, 
                                                        String name,
                                                        String authority, 
                                                        String authorityCode, 
                                                        String alias,
                                                        String abbreviation, 
                                                        String remarks);

        /// <summary>
        /// Creates a <see cref="ILocalCoordinateSystem{TCoordinate}">local coordinate 
        /// system</see>.
        /// </summary>
        /// <remarks>
        /// The dimension of the local coordinate system is determined by the size of 
        /// the axis array. All the axes will have the same units. If you want to make 
        /// a coordinate system with mixed units, then you can make a compound 
        /// coordinate system from different local coordinate systems.
        /// </remarks>
        /// <param name="name">Name of local coordinate system.</param>
        /// <param name="datum">Local datum.</param>
        /// <param name="unit">Units.</param>
        /// <param name="axes">Axis info.</param>
        /// <returns>Local coordinate system.</returns>
        new ILocalCoordinateSystem<TCoordinate> CreateLocalCoordinateSystem(
                                                        ILocalDatum datum, 
                                                        IUnit unit, 
                                                        IEnumerable<IAxisInfo> axes, 
                                                        String name);

        /// <summary>
        /// Creates a <see cref="ILocalCoordinateSystem{TCoordinate}">local coordinate 
        /// system</see>.
        /// </summary>
        /// <remarks>
        /// The dimension of the local coordinate system is determined by the size of 
        /// the axis array. All the axes will have the same units. If you want to make 
        /// a coordinate system with mixed units, then you can make a compound 
        /// coordinate system from different local coordinate systems.
        /// </remarks>
        /// <param name="name">Name of local coordinate system.</param>
        /// <param name="datum">Local datum.</param>
        /// <param name="unit">Units.</param>
        /// <param name="axes">Axis info.</param>
        /// <returns>Local coordinate system.</returns>
        new ILocalCoordinateSystem<TCoordinate> CreateLocalCoordinateSystem(
                                                        ILocalDatum datum, 
                                                        IUnit unit, 
                                                        IEnumerable<IAxisInfo> axes, 
                                                        String name,
                                                        String authority, 
                                                        String authorityCode, 
                                                        String alias,
                                                        String abbreviation, 
                                                        String remarks);

        /// <summary>
        /// Creates a <see cref="IProjectedCoordinateSystem{TCoordinate}"/> using a 
        /// projection object.
        /// </summary>
        /// <param name="name">Name of projected coordinate system.</param>
        /// <param name="gcs">Geographic coordinate system.</param>
        /// <param name="projection">Projection.</param>
        /// <param name="linearUnit">Linear unit.</param>
        /// <param name="axis0">Primary axis.</param>
        /// <param name="axis1">Secondary axis.</param>
        /// <returns>Projected coordinate system.</returns>
        IProjectedCoordinateSystem<TCoordinate> CreateProjectedCoordinateSystem(
                                                        IGeographicCoordinateSystem<TCoordinate> gcs,
                                                        IProjection projection, 
                                                        ILinearUnit linearUnit, 
                                                        IAxisInfo axis0, 
                                                        IAxisInfo axis1,
                                                        String name);

        /// <summary>
        /// Creates a <see cref="IProjectedCoordinateSystem{TCoordinate}"/> using a 
        /// projection object.
        /// </summary>
        /// <param name="name">Name of projected coordinate system.</param>
        /// <param name="gcs">Geographic coordinate system.</param>
        /// <param name="projection">Projection.</param>
        /// <param name="linearUnit">Linear unit.</param>
        /// <param name="axis0">Primary axis.</param>
        /// <param name="axis1">Secondary axis.</param>
        /// <returns>Projected coordinate system.</returns>
        IProjectedCoordinateSystem<TCoordinate> CreateProjectedCoordinateSystem(
                                                        IGeographicCoordinateSystem<TCoordinate> gcs,
                                                        IProjection projection, 
                                                        ILinearUnit linearUnit,
                                                        IAxisInfo axis0, 
                                                        IAxisInfo axis1,
                                                        String name, 
                                                        String authority, 
                                                        String authorityCode, 
                                                        String alias,
                                                        String abbreviation, 
                                                        String remarks);

        /// <summary>
        /// Creates a <see cref="IVerticalCoordinateSystem{TCoordinate}"/> from 
        /// a <see cref="IVerticalDatum">datum</see> and 
        /// <see cref="ILinearUnit">linear units</see>.
        /// </summary>
        /// <param name="name">Name of vertical coordinate system.</param>
        /// <param name="datum">Vertical datum.</param>
        /// <param name="verticalUnit">Unit.</param>
        /// <param name="axis">Axis info.</param>
        /// <returns>Vertical coordinate system.</returns>
        new IVerticalCoordinateSystem<TCoordinate> CreateVerticalCoordinateSystem(
                                                        IVerticalDatum datum, 
                                                        ILinearUnit verticalUnit, 
                                                        IAxisInfo axis, 
                                                        String name);

        /// <summary>
        /// Creates a <see cref="IVerticalCoordinateSystem{TCoordinate}"/> from 
        /// a <see cref="IVerticalDatum">datum</see> and 
        /// <see cref="ILinearUnit">linear units</see>.
        /// </summary>
        /// <param name="name">Name of vertical coordinate system.</param>
        /// <param name="datum">Vertical datum.</param>
        /// <param name="verticalUnit">Unit.</param>
        /// <param name="axis">Axis info.</param>
        /// <returns>Vertical coordinate system.</returns>
        new IVerticalCoordinateSystem<TCoordinate> CreateVerticalCoordinateSystem(
                                                        IVerticalDatum datum, 
                                                        ILinearUnit verticalUnit, 
                                                        IAxisInfo axis, 
                                                        String name, 
                                                        String authority, 
                                                        String authorityCode, 
                                                        String alias,
                                                        String abbreviation, 
                                                        String remarks);

        /// <summary>
        /// Creates a geocentric coordinate system based on the WGS84 ellipsoid, 
        /// suitable for GPS measurements.
        /// </summary>
        new IGeocentricCoordinateSystem<TCoordinate> CreateWgs84GeocentricCoordinateSystem();

        /// <summary>
        /// Creates a geographic coordinate system based on the WGS84 ellipsoid, 
        /// suitable for GPS measurements.
        /// </summary>
        new IGeographicCoordinateSystem<TCoordinate> CreateWgs84GeographicCoordinateSystem();
    }
}