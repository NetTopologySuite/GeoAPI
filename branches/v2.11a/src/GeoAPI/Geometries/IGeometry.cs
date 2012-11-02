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
using GeoAPI.Coordinates;
using GeoAPI.CoordinateSystems;
using GeoAPI.Geometries.Prepared;

namespace GeoAPI.Geometries
{
    public interface IGeometry : ISpatialOperator, ISpatialRelation, ICloneable, IComparable
    {
        /// <summary>
        /// Returns the Well-Known Binary representation of this
        /// <see cref="IGeometry{TCoordinate}"/>.
        /// </summary>
        /// <remarks>
        /// For a definition of the Well-Known Binary format, see the OpenGIS Simple
        /// Features Specification.
        /// </remarks>
        /// <returns>
        /// The Well-Known Binary representation of this
        /// <see cref="IGeometry{TCoordinate}"/>.
        /// </returns>
        Byte[] AsBinary();

        /// <summary>
        /// Returns the Well-Known Text representation of this
        /// <see cref="IGeometry{TCoordinate}"/>.
        /// </summary>
        /// <remarks>
        /// For a definition of the Well-Known Text format, see the OpenGIS Simple
        /// Features Specification.
        /// </remarks>
        /// <returns>
        /// The Well-Known Text representation of this
        /// <see cref="IGeometry{TCoordinate}"/>.
        /// </returns>
        String AsText();

        Dimensions BoundaryDimension { get; }

        IPoint Centroid { get; }

        new IGeometry Clone();

        ICoordinateSequence Coordinates { get; }

        /// <summary>
        /// The inherent dimension of this geometric object, which must be
        /// less than or equal to the coordinate dimension.
        /// </summary>
        Dimensions Dimension { get; }

        /// <summary>
        /// Returns <see langword="true"/> if this geometry is the empty geometry.
        /// If true, then this geometry represents the empty point set, ∅, for the
        /// coordinate space.
        /// </summary>
        Boolean IsEmpty { get; }

        Boolean IsRectangle { get; }

        /// <summary>
        /// Returns <see langword="true"/> if the geometry has no anomalous geometric
        /// points, such as self intersection or self tangency. The description of each
        /// instantiable geometric class will include the specific conditions that cause
        /// an instance of that class to be classified as not simple.
        /// </summary>
        Boolean IsSimple { get; }

        Boolean IsValid { get; }

        Int32 PointCount { get; }

        OgcGeometryType GeometryType { get; }

        String GeometryTypeName { get; }

        void Normalize();

        String Srid { get; }

        Object UserData { get; set; }

        IGeometry Envelope { get; }

        IExtents Extents { get; }

        IGeometryFactory Factory { get; }

        //IPoint<TCoordinate> InteriorPoint { get; }
        IPoint PointOnSurface { get; }

        IPrecisionModel PrecisionModel { get; }

        /// <summary>
        /// Gets the spatial reference system associated with the geometry.
        /// </summary>
        ICoordinateSystem SpatialReference { get; }

        IPreparedGeometry ToPreparedGeometry();
    }
}