#pragma warning disable 1591
namespace GeoAPI.Geometries
{
    public interface IMultiCurve : IGeometryCollection
    {
        bool IsClosed { get; }
    }
}