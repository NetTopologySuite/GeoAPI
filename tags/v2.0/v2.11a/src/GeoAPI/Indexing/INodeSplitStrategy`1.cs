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
    /// Interface for a node splitting strategy used by an updateable spatial index.
    /// </summary>
    public interface INodeSplitStrategy<TBounds, TItem>
        where TItem : IBoundable<TBounds>
    {

        ISpatialIndexNodeFactory<TBounds, TItem> NodeFactory { get; set; }

        /// <summary>
        /// Splits a given node based on the given <paramref name="heuristic"/>.
        /// </summary>
        /// <param name="node">The node to split.</param>
        /// <param name="heuristic">The heuristic used to compute the node split.</param>
        /// <returns>The new node split off from <paramref name="node"/>.</returns>
        ISpatialIndexNode<TBounds, TItem> SplitNode(ISpatialIndexNode<TBounds, TItem> node, 
                                                    IndexBalanceHeuristic heuristic);
    }
}
