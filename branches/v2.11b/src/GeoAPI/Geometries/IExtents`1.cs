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
using System.Collections.Generic;
using GeoAPI.Coordinates;
using GeoAPI.CoordinateSystems;
using GeoAPI.DataStructures;
using NPack.Interfaces;

namespace GeoAPI.Geometries
{
    /// <summary>
    /// Represents an axis-aligned, orthogonal region which can be used to describe the 
    /// furthest extents of a spatial object (such as an <see cref="IGeometry{TCoordinate}"/>
    /// instance) in its coordinate system.
    /// </summary>
    /// <typeparam name="TCoordinate">The type of coordinate to use.</typeparam>
    public interface IExtents<TCoordinate> : IExtents, IComparable<IExtents<TCoordinate>>,
                                             IEquatable<IExtents<TCoordinate>>, 
                                             IContainable<IExtents<TCoordinate>>, 
                                             IIntersectable<IExtents<TCoordinate>>
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>, 
                            IComparable<TCoordinate>, IConvertible, 
                            IComputable<Double, TCoordinate>
    {
        Boolean Borders(TCoordinate coordinate);
        Boolean Borders(IExtents<TCoordinate> other);
        Boolean Borders(TCoordinate coordinate, Tolerance tolerance);
        Boolean Borders(IExtents<TCoordinate> other, Tolerance tolerance);
        /// <summary>
        /// Computes the coordinate of the center of the <see cref="IExtents{TCoordinate}"/> 
        /// (as long as it is non-null).
        /// </summary>
        /// <returns>
        /// The center coordinate of this envelope, 
        /// or <see langword="null" /> if the envelope is null.
        /// </returns>.
        new TCoordinate Center { get; }

        /// <summary>  
        /// Returns <see langword="true"/> if the given point lies in or on the extent.
        /// </summary>
        /// <param name="coordinate"> the point which this <see cref="IExtents{TCoordinate}"/> is
        /// being checked for containing.</param>
        /// <returns>    
        /// <see langword="true"/> if the point lies in the interior or
        /// on the boundary of this <see cref="IExtents{TCoordinate}"/>.
        /// </returns>
        Boolean Contains(TCoordinate coordinate);
        Boolean Contains(TCoordinate coordinate, Tolerance tolerance);
        Boolean Contains(IExtents<TCoordinate> other, Tolerance tolerance);
        Double Distance(IExtents<TCoordinate> extents);
        void ExpandToInclude(TCoordinate coordinate);
        void ExpandToInclude(params TCoordinate[] coordinate);
        void ExpandToInclude(IEnumerable<TCoordinate> coordinates);
        /// <summary>
        /// Enlarges the boundary of the <see cref="IExtents{TCoordinate}"/> so that it contains
        /// <paramref name="other"/>. Does nothing if <paramref name="other"/> is empty, is null, or
        /// wholly on or within the boundaries.
        /// </summary>
        /// <param name="other">The <see cref="IExtents{TCoordinate}"/> to merge with.</param>
        void ExpandToInclude(IExtents<TCoordinate> other);
        void ExpandToInclude(IGeometry<TCoordinate> geometry);
        Double GetIntersectingArea(IGeometry<TCoordinate> geometry);
        IExtents<TCoordinate> Intersection(IExtents<TCoordinate> extents);
        IExtents<TCoordinate> Intersection(IGeometry<TCoordinate> geometry);

        /// <summary>  
        /// Check if the <paramref name="coordinate"/> overlaps (lies inside) 
        /// the region of the <see cref="IExtents{TCoordinate}"/>.
        /// </summary>
        /// <param name="coordinate">The <typeparamref name="TCoordinate"/> to be tested.</param>
        /// <returns>
        /// <see langword="true"/> if the point overlaps the <see cref="IExtents{TCoordinate}"/>.
        /// </returns>
        Boolean Intersects(TCoordinate coordinate);
        Boolean Intersects(TCoordinate coordinate, Tolerance tolerance);
        Boolean Intersects(IExtents<TCoordinate> other, Tolerance tolerance);

        /// <summary>
        /// Gets the maximum coordinate of this <see cref="IExtents{TCoordinate}"/>.
        /// </summary>
        new TCoordinate Max { get; }

        /// <summary>
        /// Gets the minimum coordinate of this <see cref="IExtents{TCoordinate}"/>.
        /// </summary>
        new TCoordinate Min { get; }

        /// <summary>
        /// Computes whether this <see cref="IExtents{TCoordinate}"/>
        /// intersects a coordinate.
        /// </summary>
        /// <param name="coordinate">
        /// A <typeparamref name="TCoordinate"/> to test for overlap.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if the <typeparamref name="TCoordinate"/> 
        /// is contained within the extents, <see langword="false"/> otherwise.
        /// </returns>
        Boolean Overlaps(TCoordinate coordinate);

        /// <summary>
        /// Computes whether this <see cref="IExtents{TCoordinate}"/>
        /// intersects another <see cref="IExtents{TCoordinate}"/>.
        /// </summary>
        /// <param name="other">
        /// Another <see cref="IExtents{TCoordinate}"/> to test for overlap.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if there is any overlap,
        /// <see langword="false"/> otherwise.
        /// </returns>
        Boolean Overlaps(IExtents<TCoordinate> other);
        Boolean Overlaps(TCoordinate coordinate, Tolerance tolerance);
        Boolean Overlaps(IExtents<TCoordinate> other, Tolerance tolerance);
        new ICoordinateSystem<TCoordinate> SpatialReference { get; }
        IEnumerable<IExtents<TCoordinate>> Split(TCoordinate coordinate);
        new IGeometry<TCoordinate> ToGeometry();
        Boolean Touches(TCoordinate coordinate);
        Boolean Touches(IExtents<TCoordinate> other);
        Boolean Touches(TCoordinate coordinate, Tolerance tolerance);
        Boolean Touches(IExtents<TCoordinate> other, Tolerance tolerance);
        IExtents<TCoordinate> Union(TCoordinate coordinate);
        IExtents<TCoordinate> Union(IExtents<TCoordinate> box);
        Boolean Within(TCoordinate coordinate);
        Boolean Within(IExtents<TCoordinate> other);
        Boolean Within(TCoordinate coordinate, Tolerance tolerance);
        Boolean Within(IExtents<TCoordinate> other, Tolerance tolerance);

        /// <summary> 
        /// Gets the factory which contains the context in which this extents was created.
        /// </summary>
        /// <returns>The factory for the <see cref="IExtents{TCoordinate}"/>.</returns>
        new IGeometryFactory<TCoordinate> Factory { get; }

        /*
        void SetCenter(params Double[] dimensions);
        void SetCenter(IPoint<TCoordinate> center, params Double[] dimensions);
        void SetCenter(TCoordinate center);
        void SetCenter(IPoint<TCoordinate> center);
        void SetCenter(TCoordinate center, params Double[] dimensions);
        */

        Boolean Covers(TCoordinate coordinate);
        Boolean Covers(IExtents<TCoordinate> other);
    }
}