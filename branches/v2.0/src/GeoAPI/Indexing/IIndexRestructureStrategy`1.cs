
namespace GeoAPI.Indexing
{
    /// <summary>
    /// Interface for a strategy used to restructure a spatial index.
    /// </summary>
    public interface IIndexRestructureStrategy<TBounds>
    {
        /// <summary>
        /// Restructures a node in the index.
        /// </summary>
        /// <param name="node">The spatial index node to restructure.</param>
        void RestructureNode(ISpatialIndexNode<TBounds> node);
    }
}
