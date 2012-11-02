using System;
using GeoAPI.Coordinates;

namespace GeoAPI.Geometries
{
    public interface ISpatialRelation : ISimpleSpatialRelation, IEquatable<IGeometry>
    {
        Boolean Contains(IGeometry g, Tolerance tolerance);

        Boolean ContainsProperly(IGeometry g, Tolerance tolerance);

        Boolean CoveredBy(IGeometry g, Tolerance tolerance);

        Boolean Covers(IGeometry g, Tolerance tolerance);

        Boolean Crosses(IGeometry g, Tolerance tolerance);

        Boolean Disjoint(IGeometry g, Tolerance tolerance);

        Boolean Equals(IGeometry g, Tolerance tolerance);

        /// <summary>
        /// Returns true if the two <see cref="IGeometry{TCoordinate}"/> instances
        /// are exactly equal.
        /// Two <see cref="IGeometry{TCoordinate}"/> instances are exactly equal iff:
        /// <list type="bullet">
        /// <item><description>they have the same class,</description></item>
        /// <item><description>
        /// they have the same values of coordinates in their internal
        /// coordinate sequences, in exactly the same order.
        /// </description></item>
        /// </list>
        /// If this and the other <see cref="IGeometry{TCoordinate}"/>s are
        /// composites and any children are not <see cref="IGeometry{TCoordinate}"/>
        /// instances, returns <see langword="false"/>.
        /// This provides a stricter test of equality than <see cref="Equals"/>.
        /// </summary>
        /// <param name="g">
        /// The <see cref="IGeometry{TCoordinate}"/> with which to
        /// compare this <see cref="IGeometry{TCoordinate}"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if this and the other
        /// <see cref="IGeometry{TCoordinate}"/>
        /// are of the same class and have equal internal data.
        /// </returns>
        Boolean EqualsExact(IGeometry g);

        Boolean EqualsExact(IGeometry g, Tolerance tolerance);

        Boolean Intersects(IGeometry g, Tolerance tolerance);

        Boolean IsWithinDistance(IGeometry g, Double distance);

        Boolean IsWithinDistance(IGeometry g, Double distance, Tolerance tolerance);

        Boolean Overlaps(IGeometry g, Tolerance tolerance);

        Boolean Relate(IGeometry g, IntersectionMatrix intersectionPattern);

        Boolean Relate(IGeometry g, IntersectionMatrix intersectionPattern, Tolerance tolerance);

        Boolean Relate(IGeometry g, String intersectionPattern);

        Boolean Relate(IGeometry g, String intersectionPattern, Tolerance tolerance);

        IntersectionMatrix Relate(IGeometry g);

        Boolean Touches(IGeometry g);

        Boolean Touches(IGeometry g, Tolerance tolerance);

        Boolean Within(IGeometry g, Tolerance tolerance);
    }
}