
namespace GeoAPI.Indexing
{
    /// <summary>
    /// Interface for a strategy to insert new entries into an updatable spatial index.
    /// </summary>
    public interface IItemInsertStrategy<TBounds>
    {
        /// <summary>
        /// Inserts a new item into the spatial index.
        /// </summary>
        /// <param name="item">The boundable item to insert.</param>
        /// <param name="node">The next node at which to try the insert.</param>
        /// <param name="nodeSplitStrategy">
        /// An <see cref="INodeSplitStrategy{TBounds}"/> used to split the node if it overflows.
        /// </param>
        /// <param name="heuristic">The heuristic used to balance the insert or compute the node split.</param>
        /// <param name="newSiblingFromSplit">A possible new node from a node-split.</param>
        void Insert(IBoundable<TBounds> item, ISpatialIndexNode<TBounds> node, 
            INodeSplitStrategy<TBounds> nodeSplitStrategy, 
            IndexBalanceHeuristic heuristic, 
            out ISpatialIndexNode<TBounds> newSiblingFromSplit);
    }
}
