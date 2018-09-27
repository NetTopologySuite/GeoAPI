namespace GeoAPI.Geometries
{
    public interface ILineString : ICurve, ILineal
    {
        IPoint GetPointN(int n);

        CoordinateXY GetCoordinateN(int n);
    }
}
