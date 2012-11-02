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
using GeoAPI.CoordinateSystems;
using GeoAPI.Coordinates;
using GeoAPI.Indexing;
using NPack.Interfaces;
using GeoAPI.IO.WellKnownBinary;
using GeoAPI.IO.WellKnownText;

namespace GeoAPI.Geometries
{
    /// <summary>
    /// Interface to a factory object which creates instances of
    /// <see cref="IGeometry{TCoordinate}"/> and descendents.
    /// </summary>
    /// <typeparam name="TCoordinate">Type of coordinate to use.</typeparam>
    public interface IGeometryFactory<TCoordinate> : IGeometryFactory, IBoundsFactory<IExtents<TCoordinate>>
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>, 
                            IComparable<TCoordinate>, IConvertible, 
                            IComputable<Double, TCoordinate>
    {
        /// <summary>
        /// Gets the <see cref="ICoordinateFactory{TCoordinate}"/> assigned to the
        /// <see cref="IGeometryFactory{TCoordinate}"/>.
        /// </summary>
        new ICoordinateFactory<TCoordinate> CoordinateFactory { get; }

        /// <summary>
        /// Gets the <see cref="ICoordinateSequenceFactory{TCoordinate}"/> assigned to the
        /// <see cref="IGeometryFactory{TCoordinate}"/>.
        /// </summary>
        new ICoordinateSequenceFactory<TCoordinate> CoordinateSequenceFactory { get; }
       
        /// <summary>
        /// Gets or sets the spatial reference system to associate with the geometry.
        /// </summary>
        new ICoordinateSystem<TCoordinate> SpatialReference { get; set; }
        new IPrecisionModel<TCoordinate> PrecisionModel { get; }
        IGeometry<TCoordinate> BuildGeometry(IEnumerable<IGeometry<TCoordinate>> geomList);
        new IExtents<TCoordinate> CreateExtents();
        new IExtents<TCoordinate> CreateExtents(ICoordinate min, ICoordinate max);
        IExtents<TCoordinate> CreateExtents(TCoordinate min, TCoordinate max);
        IExtents<TCoordinate> CreateExtents(IExtents extents);
        IExtents<TCoordinate> CreateExtents(IExtents<TCoordinate> extents);
        IGeometry<TCoordinate> CreateGeometry(IGeometry<TCoordinate> g);
        IGeometry<TCoordinate> CreateGeometry(ICoordinateSequence<TCoordinate> coordinates, OgcGeometryType type);
        new IPoint<TCoordinate> CreatePoint();
        IPoint<TCoordinate> CreatePoint(TCoordinate coordinate);
        IPoint<TCoordinate> CreatePoint(IEnumerable<TCoordinate> coordinates);
        IPoint<TCoordinate> CreatePoint(ICoordinateSequence<TCoordinate> coordinates);
        //IPoint2D CreatePoint2D();
        //IPoint2D CreatePoint2D(Double x, Double y);
        //IPoint2DM CreatePoint2DM(Double x, Double y, Double m);
        //IPoint3D CreatePoint3D();
        //IPoint3D CreatePoint3D(Double x, Double y, Double z);
        //IPoint3D CreatePoint3D(IPoint2D point2D, Double z);
        //IPoint3DM CreatePoint3DM(Double x, Double y, Double z, Double m);
        ILineString<TCoordinate> CreateLineString(params TCoordinate[] coordinates);
        ILineString<TCoordinate> CreateLineString(IEnumerable<TCoordinate> coordinates);
        ILineString<TCoordinate> CreateLineString(ICoordinateSequence<TCoordinate> coordinates);
        new ILinearRing<TCoordinate> CreateLinearRing();
        ILinearRing<TCoordinate> CreateLinearRing(IEnumerable<TCoordinate> coordinates);
        ILinearRing<TCoordinate> CreateLinearRing(ICoordinateSequence<TCoordinate> coordinates);
        new IPolygon<TCoordinate> CreatePolygon();
        IPolygon<TCoordinate> CreatePolygon(ICoordinateSequence<TCoordinate> coordinates);
        IPolygon<TCoordinate> CreatePolygon(ILinearRing<TCoordinate> shell);
        IPolygon<TCoordinate> CreatePolygon(ILinearRing<TCoordinate> shell, IEnumerable<ILinearRing<TCoordinate>> holes);
        new IMultiPoint<TCoordinate> CreateMultiPoint();
        IMultiPoint<TCoordinate> CreateMultiPoint(IEnumerable<TCoordinate> coordinates);
        IMultiPoint<TCoordinate> CreateMultiPoint(IEnumerable<IPoint<TCoordinate>> point);
        IMultiPoint<TCoordinate> CreateMultiPoint(params IPoint<TCoordinate>[] points);
        IMultiPoint<TCoordinate> CreateMultiPoint(ICoordinateSequence<TCoordinate> coordinates);
        new IMultiLineString<TCoordinate> CreateMultiLineString();
        IMultiLineString<TCoordinate> CreateMultiLineString(IEnumerable<ILineString<TCoordinate>> lineStrings);
        new IMultiPolygon<TCoordinate> CreateMultiPolygon();
        IMultiPolygon<TCoordinate> CreateMultiPolygon(ICoordinateSequence<TCoordinate> coordinates);
        IMultiPolygon<TCoordinate> CreateMultiPolygon(IEnumerable<IPolygon<TCoordinate>> polygons);
        new IGeometryCollection<TCoordinate> CreateGeometryCollection();
        IGeometryCollection<TCoordinate> CreateGeometryCollection(IEnumerable<IGeometry<TCoordinate>> geometries);
        IGeometryCollection<TCoordinate> CreateGeometryCollection(IGeometry<TCoordinate> a, IGeometry<TCoordinate> b);
        IGeometry<TCoordinate> ToGeometry(IExtents<TCoordinate> envelopeInternal);
        new IWktGeometryWriter<TCoordinate> WktWriter { get; set; }
        new IWktGeometryReader<TCoordinate> WktReader { get; set; }
        new IWkbWriter<TCoordinate> WkbWriter { get; set; }
        new IWkbReader<TCoordinate> WkbReader { get; set; }
    }
}
