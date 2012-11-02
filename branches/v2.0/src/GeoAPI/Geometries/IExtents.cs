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
using GeoAPI.DataStructures;
using NPack;
using NPack.Interfaces;

namespace GeoAPI.Geometries
{
    public interface IExtents : ICloneable, IComparable, IEquatable<IExtents>,
                                IContainable<IExtents>, IIntersectable<IExtents>
    {
        ICoordinate Center { get; }
        IGeometryFactory Factory { get; }
        Boolean IsEmpty { get; }
        ICoordinate Max { get; }
        ICoordinate Min { get; }
        ICoordinateSystem SpatialReference { get; }
        Boolean Borders(IExtents other);
        Boolean Borders(IExtents other, Tolerance tolerance);
        Boolean Contains(params Double[] coordinate);
        Boolean Contains(ICoordinate other);
        Boolean Contains(Tolerance tolerance, params Double[] coordinate);
        Boolean Contains(IExtents other, Tolerance tolerance);
        Boolean Contains(ICoordinate other, Tolerance tolerance);
        Double Distance(IExtents extents);
        void ExpandToInclude(ICoordinateSequence coordinates);
        void ExpandToInclude(params Double[] coordinate);
        void ExpandToInclude(IExtents other);
        void ExpandToInclude(IGeometry other);
        IExtents Intersection(IExtents extents);
        Boolean Intersects(params Double[] coordinate);
        Boolean Intersects(Tolerance tolerance, params Double[] coordinate);
        Boolean Intersects(IExtents other, Tolerance tolerance);
        Double GetMax(Ordinates ordinate);
        Double GetMin(Ordinates ordinate);
        Double GetSize(Ordinates axis);
        Double GetSize(Ordinates axis1, Ordinates axis2);
        Double GetSize(Ordinates axis1, Ordinates axis2, Ordinates axis3);
        Double GetSize(params Ordinates[] axes);
        Boolean Overlaps(params Double[] coordinate);
        Boolean Overlaps(ICoordinate other);
        Boolean Overlaps(IExtents other);
        void Scale(params Double[] vector);
        void Scale(Double factor);
        void Scale(Double factor, Ordinates axis);
        void SetToEmpty();
        IGeometry ToGeometry();
        void Translate(params Double[] vector);
        void TranslateRelativeToWidth(params Double[] vector);
        void Transform(ITransformMatrix<DoubleComponent> transformMatrix);
        IExtents Union(IPoint point);
        IExtents Union(IExtents box);

        Boolean Touches(IExtents a);

        Boolean Within(IExtents a);

        Boolean Covers(params Double[] coordinate);
        Boolean Covers(ICoordinate coordinate);
        Boolean Covers(IExtents other);
    }
}
