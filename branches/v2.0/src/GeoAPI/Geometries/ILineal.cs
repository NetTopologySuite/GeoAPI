using System;
using GeoAPI.Coordinates;
using NPack.Interfaces;

namespace GeoAPI.Geometries
{
    /**
     * Identifies {@link Geometry} subclasses which
     * are 1-dimensional and have components which are {@link LineStrings}.
     */
    public interface ILineal : IGeometry
    {
        
    }

    public interface ILineal<TCoordinate> : IGeometry<TCoordinate>
    where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>, IComparable<TCoordinate>,
                        IComputable<Double, TCoordinate>, IConvertible
    {

    }


}
