// Portions copyright 2005 - 2007: Diego Guidi
// Portions copyright 2006 - 2008: Rory Plaire (codekaizen@gmail.com)
//
// This file is part of GeoAPI.Net.
// GeoAPI.Net is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// GeoAPI.Net is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with GeoAPI.Net; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System;
using GeoAPI.Coordinates;
using NPack.Interfaces;

namespace GeoAPI.Geometries
{
    public interface ISpatialRelation<TCoordinate> : ISpatialRelation, 
                                                     IEquatable<IGeometry<TCoordinate>>
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>, 
                            IComparable<TCoordinate>, IConvertible,
                            IComputable<Double, TCoordinate>
    {
        /// <summary>
        /// Returns <see langword="true"/> if 
        /// <c><paramref name="g"/>.<see cref="Within"/>(this)</c>
        /// returns <see langword="true"/>.
        /// </summary>
        /// <param name="g">
        /// The <see cref="IGeometry{TCoordinate}"/> with 
        /// which to compare this <see cref="IGeometry{TCoordinate}"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if this <see cref="IGeometry{TCoordinate}"/> 
        /// contains <paramref name="g"/>.
        /// </returns>
        Boolean Contains(IGeometry<TCoordinate> g);
        Boolean Contains(IGeometry<TCoordinate> g, Tolerance tolerance);

        /// <summary>
        /// Returns <see langword="true"/> if this geometry is 
        /// covered by the specified geometry.
        /// Note the difference between <c>CoveredBy</c> and <c>Within</c>: 
        /// <c>CoveredBy</c> is a more inclusive relation.
        /// </summary>
        /// <param name="g">
        /// The <see cref="IGeometry{TCoordinate}"/> with which to compare 
        /// this <see cref="IGeometry{TCoordinate}"/>.
        /// </param>
        /// <remarks>
        /// <para>
        /// The <see cref="CoveredBy"/> predicate has the following equivalent 
        /// definitions:
        /// <list type="bullet">
        /// <item>
        /// <description>
        /// Every point of this geometry is a point of the other geometry.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// The DE-9IM Intersection Matrix for the two geometries is 
        /// <c>T*F**F***</c> or <c>*TF**F***</c> or <c>**FT*F***</c> or <c>**F*TF***</c>.
        /// </description>
        /// </item>
        /// <item>
        /// <description>
        /// <c>g.Covers(this)</c> (<c>CoveredBy</c> is the inverse of <c>Covers</c>).
        /// </description>
        /// </item>
        /// </list>
        /// </para>
        /// </remarks>
        /// <returns>
        /// <see langword="true"/> if this <see cref="IGeometry{TCoordinate}"/> 
        /// is covered by <paramref name="g" />.
        /// </returns>
        /// <seealso cref="ISpatialRelation{TCoordinate}.Within(IGeometry{TCoordinate})" />
        /// <seealso cref="ISpatialRelation{TCoordinate}.Covers(IGeometry{TCoordinate})" />
        Boolean CoveredBy(IGeometry<TCoordinate> g);
        Boolean CoveredBy(IGeometry<TCoordinate> g, Tolerance tolerance);

        /// <summary>
        /// Returns <see langword="true"/> if this geometry covers the 
        /// specified geometry.  
        /// </summary>
        /// <remarks>
        /// <para>
        /// The <c>Covers</c> predicate has the following equivalent definitions:
        ///     - Every point of the other geometry is a point of this geometry.
        ///     - The DE-9IM Intersection Matrix for the two geometries is <c>T*****FF*</c> 
        ///       or <c>*T****FF*</c> or <c>***T**FF*</c> or <c>****T*FF*</c>.
        ///     - <c>g.CoveredBy(this)</c> (<c>Covers</c> is the inverse of <c>CoveredBy</c>).
        /// </para>
        /// Note the difference between <c>Covers</c> and <c>Contains</c>: 
        /// <see cref="Covers"/> is a more inclusive relation.
        /// In particular, unlike <c>Contains</c> it does not distinguish between
        /// points in the boundary and in the interior of geometries.      
        /// <para>
        /// For most situations, <c>Covers</c> should be used 
        /// in preference to <c>Contains</c>.
        /// As an added benefit, <c>Covers</c> is more amenable to 
        /// optimization, and hence should be more highly performing.
        /// </para>
        /// </remarks>
        /// <param name="g">
        /// The <see cref="IGeometry{TCoordinate}"/> with which to compare 
        /// this <see cref="IGeometry{TCoordinate}"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if this <see cref="IGeometry{TCoordinate}"/> 
        /// covers <paramref name="g" />.
        /// </returns>
        /// <seealso cref="IGeometry{TCoordinate}.Contains(IGeometry{TCoordinate})" />
        /// <seealso cref="IGeometry{TCoordinate}.Contains(IGeometry{TCoordinate}, Tolerance)" />
        /// <seealso cref="IGeometry{TCoordinate}.Covers(IGeometry{TCoordinate}, Tolerance)" />
        /// <seealso cref="IGeometry{TCoordinate}.CoveredBy" />
        Boolean Covers(IGeometry<TCoordinate> g);
        Boolean Covers(IGeometry<TCoordinate> g, Tolerance tolerance);

        /// <summary>  
        /// Returns <see langword="true"/> if the DE-9IM intersection matrix 
        /// for the two <see cref="IGeometry{TCoordinate}"/>s is
        ///  T*T****** (for a point and a curve, a point and an area or a line
        /// and an area) 0******** (for two curves).
        /// </summary>
        /// <param name="g">
        /// The <see cref="IGeometry{TCoordinate}"/> with which 
        /// to compare this <see cref="IGeometry{TCoordinate}"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the two <see cref="IGeometry{TCoordinate}"/>s 
        /// cross.
        /// For this function to return <see langword="true"/>, 
        /// the <see cref="IGeometry{TCoordinate}"/>s must be a point and a curve; 
        /// a point and a surface; two curves; or a curve and a surface.
        /// </returns>
        Boolean Crosses(IGeometry<TCoordinate> g);
        Boolean Crosses(IGeometry<TCoordinate> g, Tolerance tolerance);

        /// <summary>  
        /// Returns <see langword="true"/> if the DE-9IM intersection matrix 
        /// for the two <see cref="IGeometry{TCoordinate}"/>s is FF*FF****.
        /// </summary>
        /// <param name="g">
        /// The <see cref="IGeometry{TCoordinate}"/> with which 
        /// to compare this <see cref="IGeometry{TCoordinate}"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the two 
        /// <see cref="IGeometry{TCoordinate}"/>s are disjoint.
        /// </returns>
        Boolean Disjoint(IGeometry<TCoordinate> g);
        Boolean Disjoint(IGeometry<TCoordinate> g, Tolerance tolerance);

        /// <summary>
        /// Returns true if the two <see cref="IGeometry{TCoordinate}"/>s are 
        /// exactly equal, up to a specified tolerance.
        /// </summary>
        /// <param name="g">
        /// The <see cref="IGeometry{TCoordinate}"/> with which to compare 
        /// this <see cref="IGeometry{TCoordinate}"/>.
        /// </param>
        /// <param name="tolerance">
        /// Distance at or below which two <typeparamref name="TCoordinate"/>s 
        /// will be considered equal.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if this and the other 
        /// <see cref="IGeometry{TCoordinate}"/>
        /// are of the same class and have equal internal data.
        /// </returns>
        /// <remarks>
        /// Two geometries are exactly within a tolerance equal iff:
        /// <list type="bullet">
        /// <item><description>they have the same class,</description></item>
        /// <item>
        /// <description>
        /// they have the same values of <typeparamref name="TCoordinate"/>s , 
        /// within the given tolerance distance, in their internal coordinate lists, 
        /// in exactly the same order.
        /// </description>
        /// </item>
        /// </list>
        /// </para>
        /// <para>
        /// If this and the other <see cref="IGeometry{TCoordinate}"/>s are
        /// composites and any children are not <see cref="IGeometry{TCoordinate}"/>s, 
        /// returns false.
        /// </para>
        /// </remarks>
        Boolean Equals(IGeometry<TCoordinate> g, Tolerance tolerance);

        Boolean EqualsExact(IGeometry<TCoordinate> g);
        Boolean EqualsExact(IGeometry<TCoordinate> g, Tolerance tolerance);

        /// <summary>  
        /// Returns <see langword="true"/> if 
        /// <see cref="Disjoint(IGeometry{TCoordinate})"/> returns 
        /// <see langword="false"/>.
        /// </summary>
        /// <param name="g">The <see cref="IGeometry{TCoordinate}"/> with 
        /// which to compare this <see cref="IGeometry{TCoordinate}"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the two <see cref="IGeometry{TCoordinate}"/>s 
        /// intersect.
        /// </returns>
        Boolean Intersects(IGeometry<TCoordinate> g);
        Boolean Intersects(IGeometry<TCoordinate> g, Tolerance tolerance);

        /// <summary> 
        /// Tests whether the distance from this <see cref="IGeometry{TCoordinate}"/>
        /// to another is less than or equal to a specified value.
        /// </summary>
        /// <param name="g">the Geometry to check the distance to.</param>
        /// <param name="distance">the distance value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the geometries are less than 
        /// <paramref name="distance"/> apart.
        /// </returns>
        Boolean IsWithinDistance(IGeometry<TCoordinate> g, Double distance);
        Boolean IsWithinDistance(IGeometry<TCoordinate> g, Double distance, Tolerance tolerance);

        /// <summary>
        /// Returns <see langword="true"/> if the DE-9IM intersection matrix for the two
        /// <see cref="IGeometry{TCoordinate}"/>s is
        ///  T*T***T** (for two points or two surfaces)
        ///  1*T***T** (for two curves).
        /// </summary>
        /// <param name="g">The <see cref="IGeometry{TCoordinate}"/> with 
        /// which to compare this <see cref="IGeometry{TCoordinate}"/>.</param>
        /// <returns>
        /// <see langword="true"/> if the two <see cref="IGeometry{TCoordinate}"/>s 
        /// overlap. For this function to return <see langword="true"/>, 
        /// the <see cref="IGeometry{TCoordinate}"/>s must be two points, 
        /// two curves or two surfaces.
        /// </returns>
        Boolean Overlaps(IGeometry<TCoordinate> g);
        Boolean Overlaps(IGeometry<TCoordinate> g, Tolerance tolerance);
        Boolean Relate(IGeometry<TCoordinate> g, IntersectionMatrix intersectionPattern);
        Boolean Relate(IGeometry<TCoordinate> g, IntersectionMatrix intersectionPattern, Tolerance tolerance);

        /// <summary>  
        /// Returns <see langword="true"/> if the elements in the DE-9IM intersection
        /// matrix for the two <see cref="IGeometry{TCoordinate}"/>s match the elements in 
        /// <paramref name="intersectionPattern"/>.
        /// </summary>
        /// <param name="g">
        /// The <see cref="IGeometry{TCoordinate}"/> with which 
        /// to compare this <see cref="IGeometry{TCoordinate}"/>.
        /// </param>
        /// <param name="intersectionPattern">
        /// The pattern against which to check the intersection matrix 
        /// for the two <see cref="IGeometry{TCoordinate}"/>s.
        /// </param>
        /// <returns><see langword="true"/> if the DE-9IM intersection matrix 
        /// for the two <see cref="IGeometry{TCoordinate}"/>s match 
        /// <paramref name="intersectionPattern"/>.
        /// </returns>
        /// <remarks>
        /// The elements in <paramref name="intersectionPattern"/> may be:
        ///  0
        ///  1
        ///  2
        ///  T ( = 0, 1 or 2)
        ///  F ( = -1)
        ///  * ( = -1, 0, 1 or 2)
        /// For more information on the DE-9IM, see the OpenGIS Simple Features
        /// Specification.
        /// </remarks>
        Boolean Relate(IGeometry<TCoordinate> g, String intersectionPattern);
        Boolean Relate(IGeometry<TCoordinate> g, String intersectionPattern, Tolerance tolerance);

        /// <summary>
        /// Returns the DE-9IM intersection matrix for the two 
        /// <see cref="IGeometry{TCoordinate}"/>s.
        /// </summary>
        /// <param name="g">
        /// The <see cref="IGeometry{TCoordinate}"/> with which to 
        /// compare this <see cref="IGeometry{TCoordinate}"/>.
        /// </param>
        /// <returns>
        /// A matrix describing the intersections of the interiors,
        /// boundaries and exteriors of the two <see cref="IGeometry{TCoordinate}"/>s.
        /// </returns>
        IntersectionMatrix Relate(IGeometry<TCoordinate> g);

        /// <summary>  
        /// Returns <see langword="true"/> if the DE-9IM intersection matrix for the two
        /// <see cref="IGeometry{TCoordinate}"/>s is FT*******, F**T***** or F***T****.
        /// </summary>
        /// <param name="g">
        /// The <see cref="IGeometry{TCoordinate}"/> with which to 
        /// compare this <see cref="IGeometry{TCoordinate}"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the two <see cref="IGeometry{TCoordinate}"/>s 
        /// touch; Returns false if both <see cref="IGeometry{TCoordinate}"/>s are points.
        /// </returns>
        Boolean Touches(IGeometry<TCoordinate> g);
        Boolean Touches(IGeometry<TCoordinate> g, Tolerance tolerance);

        /// <summary>
        /// Returns <see langword="true"/> if the DE-9IM intersection matrix for the two
        /// <see cref="IGeometry{TCoordinate}"/>s is T*F**F***.
        /// </summary>
        /// <param name="g">
        /// The <see cref="IGeometry{TCoordinate}"/> with which to compare 
        /// this <see cref="IGeometry{TCoordinate}"/>.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if this <see cref="IGeometry{TCoordinate}"/> 
        /// is within <paramref name="g"/>.
        /// </returns>
        Boolean Within(IGeometry<TCoordinate> g);
        Boolean Within(IGeometry<TCoordinate> g, Tolerance tolerance);
    }
}
