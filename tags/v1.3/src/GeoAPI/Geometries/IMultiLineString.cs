namespace GeoAPI.Geometries
{
    public interface IMultiLineString : IMultiCurve, ILineal
    {
        IMultiLineString Reverse();
    }
}
