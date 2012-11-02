using System;
using System.Collections.Generic;
using System.Text;

namespace GeoAPI.Indexing
{
    public interface ISpatialIndexNodeFactory<TBounds, TItem> where TItem : IBoundable<TBounds>
    {
        ISpatialIndexNode<TBounds, TItem> CreateNode(Int32 level);
    }
}
