namespace GeoAPI.Geometries
{
    public interface IPoint : IGeometry
    {
        double X { get; set; }

        double Y { get; set; }

        double Z { get; set; }

        ICoordinateSequence CoordinateSequence { get; }
    }
}
