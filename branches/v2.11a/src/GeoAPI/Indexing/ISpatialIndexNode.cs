
using System;
using System.Collections.Generic;

namespace GeoAPI.Indexing
{
    /// <summary>
    /// Represents a node in a spatial index.
    /// </summary>
    /// <typeparam name="TBounds">Type of bounds used for index entries.</typeparam>
    /// <typeparam name="TItem">Type of index entry.</typeparam>
    public interface ISpatialIndexNode<TBounds, TItem> : IBoundable<TBounds>
        where TItem : IBoundable<TBounds>
    {
        //void AddSubNode(ISpatialIndexNode<TBounds, TItem> child);
        //void AddSubNodes(IEnumerable<ISpatialIndexNode<TBounds, TItem>> children);
        //void AddItem(TItem item);
        //void AddItems(IEnumerable<TItem> items);
        void Add(IBoundable<TBounds> child);
        void AddRange(IEnumerable<IBoundable<TBounds>> children);
        //TBounds Bounds { get; }

        /// <summary>
        /// Gets the number of subnodes in the <see cref="ISpatialIndexNode{TBounds,TItem}"/>, 
        /// not counting subnodes' subnodes (grandchildren nodes).
        /// </summary>
        Int32 SubNodeCount { get; }

        /// <summary>
        /// Gets the number of items in the <see cref="ISpatialIndexNode{TBounds,TItem}"/>, 
        /// not counting child node items.
        /// </summary>
        Int32 ItemCount { get; }
        IEnumerable<TItem> Items { get; }
        IEnumerable<ISpatialIndexNode<TBounds, TItem>> SubNodes { get; }
        IEnumerable<IBoundable<TBounds>> ChildBoundables { get; }

        Boolean IsEmpty { get; }
        Boolean IsLeaf { get; }
        Boolean IsPrunable { get; }
        Boolean HasItems { get; }
        Boolean HasSubNodes { get; }
        //Boolean RemoveItem(TItem item);
        //Boolean RemoveSubNode(ISpatialIndexNode<TBounds, TItem> child);
        Boolean Remove(IBoundable<TBounds> child);
        Boolean RemoveRange(IEnumerable<IBoundable<TBounds>> children);
        IEnumerable<TItem> Query(TBounds query);
        IEnumerable<TItem> Query(TBounds query, Predicate<TItem> predicate);
        IEnumerable<TResult> Query<TResult>(TBounds query, Func<TItem, TResult> predicate);

        /// <summary>
        /// Gets the count of items in this node and all subnodes.
        /// </summary>
        Int32 TotalItemCount { get; }

        /// <summary>
        /// Gets the count of contained <see cref="ISpatialIndexNode{TBounds,TItem}"/>s 
        /// in this node and all subnodes.
        /// </summary>
        Int32 TotalNodeCount { get; }

        Int32 Level { get; }
        void Clear();
    }
}
