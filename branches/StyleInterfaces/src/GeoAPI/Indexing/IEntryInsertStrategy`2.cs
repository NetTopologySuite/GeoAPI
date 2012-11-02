// Copyright 2006 - 2008: Rory Plaire (codekaizen@gmail.com)
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

namespace GeoAPI.Indexing
{
    /// <summary>
    /// Interface for a strategy to insert new entries into an updatable spatial index.
    /// </summary>
    /// <typeparam name="TBounds">Type of bounds used to contain the entries.</typeparam>
    /// <typeparam name="TItem">The type of the index entries.</typeparam>
    public interface IItemInsertStrategy<TBounds, TItem>
        where TItem : IBoundable<TBounds>
    {
        /// <summary>
        /// Inserts a new item into the spatial index.
        /// </summary>
        /// <param name="bounds">The bounds of the entry.</param>
        /// <param name="entry">The entry to insert.</param>
        /// <param name="node">The next node at which to try the insert.</param>
        /// <param name="nodeSplitStrategy">
        /// An <see cref="INodeSplitStrategy{TBounds, TItem}"/> used to split the node if it overflows.
        /// </param>
        /// <param name="heuristic">The heuristic used to balance the insert or compute the node split.</param>
        /// <param name="newSiblingFromSplit">A possible new node from a node-split.</param>
        void Insert(TBounds bounds, 
                    TItem entry, 
                    ISpatialIndexNode<TBounds, TItem> node, 
                    INodeSplitStrategy<TBounds, TItem> nodeSplitStrategy, 
                    IndexBalanceHeuristic heuristic, 
                    out ISpatialIndexNode<TBounds, TItem> newSiblingFromSplit);

        ISpatialIndexNodeFactory<TBounds, TItem> NodeFactory { get; set; }
    }
}
