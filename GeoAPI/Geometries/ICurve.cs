namespace GeoAPI.Geometries
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICurve : IGeometry
    {
        ICoordinateSequence CoordinateSequence { get; }

        IPoint StartPoint { get; }
        
        IPoint EndPoint { get; }

        bool IsClosed { get; }
        
        bool IsRing { get; }        
    }
}
