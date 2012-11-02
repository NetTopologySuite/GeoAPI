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

namespace GeoAPI.CoordinateSystems.Transformations
{
    public interface ICoordinateTransformation
    {
        /// <summary>
        /// Human readable description of domain in source coordinate system.
        /// </summary>
        String AreaOfUse { get; }

        /// <summary>
        /// Code used by authority to identify transformation. 
        /// <see langword="null"/> is used for no code.
        /// </summary>
        /// <remarks>
        /// The authority code is a <see cref="String"/> 
        /// defined by an authority to reference a particular spatial 
        /// reference object. For example, the European Survey Group (EPSG) 
        /// authority uses 32 bit integers to reference coordinate systems, 
        /// so all their code strings will consist of a few digits. 
        /// The EPSG code for WGS84 Lat/Lon is "4326".
        /// </remarks>
        String AuthorityCode { get; }

        /// <summary>
        /// Gets math transform.
        /// </summary>
        IMathTransform MathTransform { get; }

        /// <summary>
        /// Source coordinate system.
        /// </summary>
        ICoordinateSystem Source { get; }

        /// <summary>
        /// Target coordinate system.
        /// </summary>
        ICoordinateSystem Target { get; }

        /// <summary>
        /// Name of transformation.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Gets the provider-supplied remarks.
        /// </summary>
        String Remarks { get; }

        /// <summary>
        /// Semantic type of transform. For example, a datum 
        /// transformation or a coordinate conversion.
        /// </summary>
        TransformType TransformType { get; }

        ICoordinateTransformation Inverse { get; }

        IExtents Transform(IExtents extents, IGeometryFactory factory);
        IGeometry Transform(IGeometry geometry, IGeometryFactory factory);
        IPoint Transform(IPoint point, IGeometryFactory factory);
        IExtents InverseTransform(IExtents extents, IGeometryFactory factory);
        IGeometry InverseTransform(IGeometry geometry, IGeometryFactory factory);
        IPoint InverseTransform(IPoint point, IGeometryFactory factory);
    }
}
