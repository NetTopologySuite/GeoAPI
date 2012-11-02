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

using System;

namespace GeoAPI.Indexing
{
    /// <summary>
    /// Heuristics used for tree generation and maintenance.
    /// </summary>
    public abstract class IndexBalanceHeuristic
    {
        private readonly Int32 _nodeItemMin;
        private readonly Int32 _nodeItemMax;
        private readonly UInt32 _maxTreeDepth;

        protected IndexBalanceHeuristic(Int32 nodeItemMinimumCount, 
                                        Int32 nodeItemMaximumCount, 
                                        UInt32 maxTreeDepth)
        {
            _nodeItemMin = nodeItemMinimumCount;
            _nodeItemMax = nodeItemMaximumCount;
            _maxTreeDepth = maxTreeDepth;
        }

        /// <summary>
        /// Minimum number of index entries in a node before it is a candiate for splitting
        /// the node.
        /// </summary>
        public virtual Int32 NodeItemMinimumCount
        {
            get { return _nodeItemMin; }
        }

        /// <summary>
        /// Number of index entries in a node to target. More than this will cause a split
        /// if <see cref="MaxTreeDepth"/> is not reached.
        /// </summary>
        public virtual Int32 NodeItemMaximumCount
        {
            get { return _nodeItemMax; }
        }

        /// <summary>
        /// The maximum depth of the tree including the root.
        /// </summary>
        public virtual UInt32 MaxTreeDepth
        {
            get { return _maxTreeDepth; }
        }

        /// <summary>
        /// The target number of nodes for a split node.
        /// </summary>
        public abstract Int32 TargetNodeCount { get; }
    }
}
