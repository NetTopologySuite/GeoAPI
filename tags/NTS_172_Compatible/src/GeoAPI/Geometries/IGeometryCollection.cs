using System.Collections;

namespace GeoAPI.Geometries
{
    public interface IGeometryCollection : IGeometry, IEnumerable
    {        
        int Count { get; }

        IGeometry[] Geometries { get; }

        IGeometry this[int i] { get; }

        bool IsHomogeneous { get; }                
    }
}
