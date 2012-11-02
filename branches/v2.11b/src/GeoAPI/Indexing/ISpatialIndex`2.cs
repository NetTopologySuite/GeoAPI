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

namespace GeoAPI.Indexing
{
    /// <summary> 
    /// The basic insertion and query operations supported by classes
    /// implementing spatial index algorithms.
    /// </summary>
    /// <remarks>
    /// A spatial index typically provides a primary filter for range rectangle queries. A
    /// secondary filter is required to test for exact intersection. Of course, this
    /// secondary filter may consist of other tests besides intersection, such as
    /// testing other kinds of spatial relationships.
    /// </remarks>
    public interface ISpatialIndex<TBounds, TItem> : IDisposable, ISpatialIndexNodeFactory<TBounds, TItem>
        where TItem : IBoundable<TBounds>
    {
        /// <summary>
        /// Adds a spatial item with an extent specified by the given
        /// <paramref name="item"/>'s <see cref="IBoundable{TBounds}.Bounds"/> 
        /// to the index. 
        /// </summary>
        void Insert(TItem item);

        /// <summary>
        /// Adds a range of spatial items, each with an extent specified by its
        /// <see cref="IBoundable{TBounds}.Bounds"/> to the index. 
        /// </summary>
        void InsertRange(IEnumerable<TItem> items);

        /// <summary>
        /// Removes a spatial item from the index.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if an item was matched and removed successfully;
        /// <see langword="false"/> otherwise.
        /// </returns>
        Boolean Remove(TItem item);

        //IEnumerable<TItem> Query(SpatialExpression spatialExpression);

        /// <summary> 
        /// Queries the index for all <typeparamref name="TItem"/>s whose 
        /// <see cref="IBoundable{TBounds}.Bounds"/> intersect the given search 
        /// <typeparamref name="TBounds"/>.
        /// </summary>
        /// <param name="bounds">The bounds to query on.</param>
        /// <returns>An enumeration of the items found by the query.</returns>
        /// <remarks>
        /// Note that some kinds of indexes may also return objects which do not in fact
        /// intersect the query bounds.
        /// </remarks>
        IEnumerable<TItem> Query(TBounds bounds);

        /// <summary>
        /// Queries the index for all <typeparamref name="TItem"/>s whose 
        /// <see cref="IBoundable{TBounds}.Bounds"/> intersect the given search 
        /// <typeparamref name="TBounds"/> and which match the given 
        /// <paramref name="predicate"/>.
        /// </summary>
        /// <param name="bounds">The bounds to query on.</param>
        /// <param name="predicate">A predicate delegate to apply to the items found.</param>
        /// <returns>An enumeration of the items found by the query.</returns>
        /// <remarks>
        /// Note that some kinds of indexes may also return objects which do not in fact
        /// intersect the query bounds. 
        /// </remarks>
        IEnumerable<TItem> Query(TBounds bounds, Predicate<TItem> predicate);

        /// <summary>
        /// Queries the index for all <typeparamref name="TItem"/>s whose 
        /// <see cref="IBoundable{TBounds}.Bounds"/> intersect the given search 
        /// <typeparamref name="TBounds"/>, and returns the result  of the 
        /// application of <paramref name="selector"/> over each item.
        /// </summary>
        /// <param name="bounds">The bounds to query on.</param>
        /// <param name="selector">
        /// The selection function to apply to each item which matches the query bounds.
        /// </param>
        /// <typeparam name="TResult">The type of the result to generate.</typeparam>
        /// <returns>
        /// An enumeration of <typeparamref name="TResult"/>s which are generated
        /// by applying <paramref name="selector"/> to each <typeparamref name="TItem"/>
        /// matching the query <paramref name="bounds"/>.
        /// </returns>
        IEnumerable<TResult> Query<TResult>(TBounds bounds, Func<TItem, TResult> selector);

        /// <summary>
        /// Gets the bounds of the index.
        /// </summary>
        TBounds Bounds { get; }
    }
}
