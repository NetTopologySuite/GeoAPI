using System;
using GeoAPI.Operations.Buffer;

namespace GeoAPI.Geometries
{
    public interface ISpatialOperator
    {
        IGeometry Boundary { get; }
        Double Distance(IGeometry g);
        IGeometry Buffer(Double distance);
        IGeometry Buffer(Double distance, Int32 quadrantSegments);
        IGeometry Buffer(Double distance, BufferStyle endCapStyle);
        IGeometry Buffer(Double distance, Int32 quadrantSegments, BufferStyle endCapStyle);
        IGeometry Intersection(IGeometry other);
        IGeometry Union(IGeometry other);
        IGeometry Difference(IGeometry other);
        IGeometry SymmetricDifference(IGeometry other);
        IGeometry ConvexHull();
    }
}
