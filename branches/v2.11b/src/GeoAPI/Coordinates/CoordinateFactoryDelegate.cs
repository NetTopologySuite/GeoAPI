using System;
using NPack.Interfaces;

namespace GeoAPI.Coordinates
{
    public delegate TCoordinate CoordinateFactoryDelegate<TCoordinate>(params Double[] components)
        where TCoordinate : ICoordinate, IEquatable<TCoordinate>, IComparable<TCoordinate>, 
                            IComputable<TCoordinate>, IConvertible;
}
