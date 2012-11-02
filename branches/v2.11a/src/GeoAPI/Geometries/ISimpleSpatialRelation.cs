using System;
using GeoAPI.Coordinates;
using NPack.Interfaces;

namespace GeoAPI.Geometries
{
    public interface ISimpleSpatialRelation<TCoordinate> : ISimpleSpatialRelation
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>,
                            IComparable<TCoordinate>, IComputable<double, TCoordinate>
    {
        Boolean Contains(IGeometry<TCoordinate> g);

        Boolean ContainsProperly(IGeometry<TCoordinate> g);

        Boolean CoveredBy(IGeometry<TCoordinate> g);

        Boolean Covers(IGeometry<TCoordinate> g);

        Boolean Crosses(IGeometry<TCoordinate> g);

        Boolean Disjoint(IGeometry<TCoordinate> g);

        Boolean Intersects(IGeometry<TCoordinate> g);

        Boolean Overlaps(IGeometry<TCoordinate> g);

        Boolean Touches(IGeometry<TCoordinate> g);

        Boolean Within(IGeometry<TCoordinate> g);
    }

    public interface ISimpleSpatialRelation
    {
        Boolean Contains(IGeometry g);

        Boolean ContainsProperly(IGeometry g);

        Boolean CoveredBy(IGeometry g);

        Boolean Covers(IGeometry g);

        Boolean Crosses(IGeometry g);

        Boolean Disjoint(IGeometry g);

        Boolean Intersects(IGeometry g);

        Boolean Overlaps(IGeometry g);

        Boolean Touches(IGeometry g);

        Boolean Within(IGeometry g);
    }
}