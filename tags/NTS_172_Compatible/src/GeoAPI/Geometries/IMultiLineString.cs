namespace GeoAPI.Geometries
{
    public interface IMultiLineString : IMultiCurve
    {
        IMultiLineString Reverse();
    }
}
