using System;
using GeoAPI.Coordinates;
using NPack.Interfaces;

namespace GeoAPI.Geometries
{
    /**
     * Identifies {@link Geometry} subclasses which
     * are 2-dimensional 
     * and have components which have {@link Lineal} boundaries. 
     */
    public interface IPolygonal : IGeometry
    {
        
    }

    public interface IPolygonal<TCoordinate> : IGeometry<TCoordinate>
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>, IComparable<TCoordinate>,
                            IComputable<Double, TCoordinate>, IConvertible
    {

    }

}
