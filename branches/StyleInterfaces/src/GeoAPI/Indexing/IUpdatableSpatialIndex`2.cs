using System;

namespace GeoAPI.Indexing
{
    public interface IUpdatableSpatialIndex<TBounds, TItem> : ISpatialIndex<TBounds, TItem>
        where TItem : IBoundable<TBounds>
    {
        /// <summary> 
        /// Removes a single item from the tree.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns><see langword="true"/> if the item was found.</returns>
        //Boolean Remove(TItem item);

        void Clear();
    }
}
