namespace GeoAPI.Geometries
{
    public interface ICurve : IGeometry
    {
        ICoordinateSequence CoordinateSequence { get; }

        IPoint StartPoint { get; }
        
        IPoint EndPoint { get; }

        bool IsClosed { get; }
        
        bool IsRing { get; }        
    }
}
