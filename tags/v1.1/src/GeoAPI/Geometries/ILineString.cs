namespace GeoAPI.Geometries
{
    public interface ILineString : ICurve
    {
        IPoint GetPointN(int n);

        ICoordinate GetCoordinateN(int n);

        ILineString Reverse();
    }
}
