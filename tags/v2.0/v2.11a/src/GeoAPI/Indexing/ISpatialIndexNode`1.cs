
using System;
using System.Collections.Generic;

namespace GeoAPI.Indexing
{
    public interface ISpatialIndexNode<TBounds> : IBoundable<TBounds>
    {
        TBounds Bounds { get; }
        void Add(IBoundable<TBounds> item);
        void AddRange(IEnumerable<IBoundable<TBounds>> items);
        ICollection<IBoundable<TBounds>> Children { get; }
        Boolean IsLeaf { get; }
        Boolean Remove(IBoundable<TBounds> item);
    }
}
