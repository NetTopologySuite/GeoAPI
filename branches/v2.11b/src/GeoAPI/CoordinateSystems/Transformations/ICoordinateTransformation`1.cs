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
using GeoAPI.Geometries;
using NPack.Interfaces;

namespace GeoAPI.CoordinateSystems.Transformations
{
	/// <summary>
	/// Describes a coordinate transformation. This interface only describes a 
	/// coordinate transformation, it does not actually perform the transform 
	/// operation on points. To transform points you must use a math transform.
	/// </summary>
    public interface ICoordinateTransformation<TCoordinate> : ICoordinateTransformation
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>,
                            IComparable<TCoordinate>, IConvertible,
                            IComputable<Double, TCoordinate>
	{
		/// <summary>
		/// Gets math transform.
		/// </summary>
		new IMathTransform<TCoordinate> MathTransform { get; }

		/// <summary>
		/// Source coordinate system.
		/// </summary>
		new ICoordinateSystem<TCoordinate> Source { get; }

		/// <summary>
		/// Target coordinate system.
		/// </summary>
		new ICoordinateSystem<TCoordinate> Target { get; }

        new ICoordinateTransformation<TCoordinate> Inverse { get; }

	    IExtents<TCoordinate> Transform(IExtents<TCoordinate> extents, 
                                        IGeometryFactory<TCoordinate> factory);
        IGeometry<TCoordinate> Transform(IGeometry<TCoordinate> geometry, 
                                         IGeometryFactory<TCoordinate> factory);
        IPoint<TCoordinate> Transform(IPoint<TCoordinate> point,
                                      IGeometryFactory<TCoordinate> factory);

        IExtents<TCoordinate> InverseTransform(IExtents<TCoordinate> extents,
                                               IGeometryFactory<TCoordinate> factory);
        IGeometry<TCoordinate> InverseTransform(IGeometry<TCoordinate> geometry,
                                                IGeometryFactory<TCoordinate> factory);
        IPoint<TCoordinate> InverseTransform(IPoint<TCoordinate> point,
                                             IGeometryFactory<TCoordinate> factory);
	}
}
