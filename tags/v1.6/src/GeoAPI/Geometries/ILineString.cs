using System;

namespace GeoAPI.Geometries
{
    public interface ILineString : ICurve, ILineal
    {
        IPoint GetPointN(int n);

        Coordinate GetCoordinateN(int n);
        
        [Obsolete("Use Geometry.Reverse")]
        new ILineString Reverse();
    }
}
