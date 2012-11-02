using System;
using GeoAPI.Coordinates;
using NPack.Interfaces;

namespace GeoAPI.Geometries
{
    /**
     * Identifies {@link Geometry} subclasses which
     * are 0-dimensional and with components which are {@link Points}. 
     */
    public interface IPuntal : IGeometry
    {
        
    }

    public interface IPuntal<TCoordinate> : IGeometry<TCoordinate>
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>, IComparable<TCoordinate>,
                            IComputable<Double, TCoordinate>, IConvertible
    {

    }

}
