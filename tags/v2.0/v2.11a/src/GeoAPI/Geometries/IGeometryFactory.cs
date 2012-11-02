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
using GeoAPI.Coordinates;
using GeoAPI.CoordinateSystems;
using GeoAPI.DataStructures;
using GeoAPI.Indexing;
using GeoAPI.IO.WellKnownBinary;
using GeoAPI.IO.WellKnownText;

namespace GeoAPI.Geometries
{
    /// <summary>
    /// Interface to a factory object which creates instances of
    /// <see cref="IGeometry"/> and descendents.
    /// </summary>
    public interface IGeometryFactory: IBoundsFactory<IExtents> 
    {
        /// <summary>
        /// Gets the <see cref="ICoordinateFactory"/> assigned to the
        /// <see cref="IGeometryFactory"/>.
        /// </summary>
        ICoordinateFactory CoordinateFactory { get; }

        /// <summary>
        /// Gets the <see cref="ICoordinateSequenceFactory"/> assigned to the
        /// <see cref="IGeometryFactory"/>.
        /// </summary>
        ICoordinateSequenceFactory CoordinateSequenceFactory { get; }

        String Srid { get; set; }

        /// <summary>
        /// Gets or sets the spatial reference system to associate with the geometry.
        /// </summary>
        ICoordinateSystem SpatialReference { get; set; }

        IPrecisionModel PrecisionModel { get; }
        IGeometry BuildGeometry(IEnumerable<IGeometry> geometryList);

        IGeometryFactory Clone();

        IExtents CreateExtents();
        IExtents CreateExtents(ICoordinate min, ICoordinate max);
        IGeometry CreateGeometry(IGeometry g);
        IGeometry CreateGeometry(ICoordinateSequence coordinates, OgcGeometryType type);
        IPoint CreatePoint();
        IPoint CreatePoint(ICoordinate coordinate);
        IPoint CreatePoint(ICoordinateSequence coordinates);
        IPoint2D CreatePoint2D();
        IPoint2D CreatePoint2D(Double x, Double y);
        IPoint2DM CreatePoint2DM(Double x, Double y, Double m);
        IPoint3D CreatePoint3D();
        IPoint3D CreatePoint3D(Double x, Double y, Double z);
        IPoint3D CreatePoint3D(IPoint2D point2D, Double z);
        IPoint3DM CreatePoint3DM(Double x, Double y, Double z, Double m);
        ILineString CreateLineString();
        ILineString CreateLineString(IEnumerable<ICoordinate> coordinates);
        ILineString CreateLineString(ICoordinateSequence coordinates);
        ILinearRing CreateLinearRing();
        ILinearRing CreateLinearRing(IEnumerable<ICoordinate> coordinates);
        ILinearRing CreateLinearRing(ICoordinateSequence coordinates);
        IPolygon CreatePolygon();
        IPolygon CreatePolygon(ICoordinateSequence coordinates);
        IPolygon CreatePolygon(IEnumerable<ICoordinate> shell);
        IPolygon CreatePolygon(ILinearRing shell);
        IPolygon CreatePolygon(ILinearRing shell, IEnumerable<ILinearRing> holes);
        IMultiPoint CreateMultiPoint();
        IMultiPoint CreateMultiPoint(IEnumerable<ICoordinate> coordinates);
        IMultiPoint CreateMultiPoint(IEnumerable<IPoint> point);
        IMultiPoint CreateMultiPoint(ICoordinateSequence coordinates);
        IMultiLineString CreateMultiLineString();
        IMultiLineString CreateMultiLineString(IEnumerable<ILineString> lineStrings);
        IMultiPolygon CreateMultiPolygon();
        IMultiPolygon CreateMultiPolygon(ICoordinateSequence coordinates);
        IMultiPolygon CreateMultiPolygon(IEnumerable<IPolygon> polygons);
        IGeometryCollection CreateGeometryCollection();
        IGeometryCollection CreateGeometryCollection(IGeometry a, IGeometry b);
        IGeometryCollection CreateGeometryCollection(IEnumerable<IGeometry> geometries);
        IGeometry ToGeometry(IExtents envelopeInternal);

        IExtents CreateExtents(IExtents first, IExtents second);
        IExtents CreateExtents(IExtents first, IExtents second, IExtents third);
        IExtents CreateExtents(params IExtents[] extents);
        IExtents2D CreateExtents2D(Double xMin, Double yMin, Double xMax, Double yMax);
        IExtents2D CreateExtents2D(Pair<Double> min, Pair<Double> max);
        // TODO: determine if we need to use a RH system or a LH system and adjust parameters correctly
        IExtents3D CreateExtents3D(Double xMin, Double yMin, Double zMin, Double xMax, Double yMax, Double zMax);
        IExtents3D CreateExtents3D(Triple<Double> min, Triple<Double> max);
        IWktGeometryWriter WktWriter { get; set; }
        IWktGeometryReader WktReader { get; set; }
        IWkbWriter WkbWriter { get; set; }
        IWkbReader WkbReader { get; set; }
    }
}
