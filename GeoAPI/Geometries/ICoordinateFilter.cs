namespace GeoAPI.Geometries
{
    /// <summary>
    /// <c>Geometry</c> classes support the concept of applying a
    /// coordinate filter to every coordinate in the <c>Geometry</c>. A
    /// coordinate filter can be used to record information about each coordinate. 
    /// Coordinate filters can be
    /// used to implement centroid and
    /// envelope computation, and many other functions.<para/>
    /// <c>ICoordinateFilter</c> is
    /// an example of the Gang-of-Four Visitor pattern. 
    /// <para/>
    /// <b>Note</b>: it is not recommended to use filters to mutate the coordinates.
    /// In particular, modified values may not be preserved if the target Geometry uses a non-default <see cref="ICoordinateSequence"/>.
    /// If in-place mutation is required, use <see cref="ICoordinateSequenceFilter"/>.
    /// In general, Geometrys should be treated as immutable, 
    /// and mutation should be performed by creating a new Geometry object (see <see cref="T:NetTopologySuite.Geometries.Utilities.GeometryEditor"/> 
    /// and <see cref="T:NetTopologySuite.Geometries.Utilities.GeometryTransformer"/> for convenient ways to do this).
    /// </summary>
    /// <seealso cref="IGeometry.Apply(ICoordinateFilter)"/>
    /// <seealso cref="IGeometry.Apply(ICoordinateSequenceFilter)"/>
    /// <seealso cref="T:NetTopologySuite.Geometries.Utilities.GeometryTransformer"/> 
    /// <see cref="T:NetTopologySuite.Geometries.Utilities.GeometryEditor"/> 
    public interface ICoordinateFilter
    {
        /// <summary>
	    /// Performs an operation with or on <c>coord</c>.
    	/// </summary>
        /// <param name="coord"><c>Coordinate</c> to which the filter is applied.</param>
    	void Filter(Coordinate coord);
    }

}
