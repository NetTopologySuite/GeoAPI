using System;
using GeoAPI.Coordinates;
using NPack.Interfaces;

namespace GeoAPI.Geometries
{
    ///<summary>Identifies <see cref="IGeometry"/> subclasses which are 1-dimensional and have components which are <see cref="ILineString"/></summary>
    public interface ILineal : IGeometry
    {
        
    }

    ///<summary>Identifies <see cref="IGeometry{TCoordinate}"/> subclasses which are 1-dimensional and have components which are <see cref="ILineString{TCoordinate}"/></summary>
    public interface ILineal<TCoordinate> : IGeometry<TCoordinate>
    where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>, IComparable<TCoordinate>,
                        IComputable<Double, TCoordinate>, IConvertible
    {

    }


}
