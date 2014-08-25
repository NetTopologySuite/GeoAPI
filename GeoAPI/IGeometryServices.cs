using GeoAPI.Geometries;

namespace GeoAPI
{
    /// <summary>
    /// Delegate function to get a coordinate system from a given initialization string
    /// </summary>
    /// <param name="init">The initialization string</param>
    /// <typeparam name="TCoordinateSystem">The type of the coordinate sytem.</typeparam>
    public delegate TCoordinateSystem GetCoordinateSystemDelegate<TCoordinateSystem>(string init);
    
    /// <summary>
    /// An interface for classes that offer access to geometry creating facillities.
    /// </summary>
    public interface IGeometryServices
    {
        /// <summary>
        /// Gets the default spatial reference id
        /// </summary>
        int DefaultSRID { get; }
        
        /// <summary>
        /// Gets or sets the coordiate sequence factory to use
        /// </summary>
        ICoordinateSequenceFactory DefaultCoordinateSequenceFactory { get; }

        /// <summary>
        /// Gets or sets the default precision model
        /// </summary>
        IPrecisionModel DefaultPrecisionModel { get; }

        /// <summary>
        /// Creates a precision model based on given precision model type
        /// </summary>
        /// <returns>The precision model type</returns>
        IPrecisionModel CreatePrecisionModel(PrecisionModels modelType);

        /// <summary>
        /// Creates a precision model based on given precision model.
        /// </summary>
        /// <returns>The precision model</returns>
        IPrecisionModel CreatePrecisionModel(IPrecisionModel modelType);

        /// <summary>
        /// Creates a precision model based on the given scale factor.
        /// </summary>
        /// <param name="scale">The scale factor</param>
        /// <returns>The precision model.</returns>
        IPrecisionModel CreatePrecisionModel(double scale);

        /// <summary>
        /// Creates a new geometry factory, using <see cref="DefaultPrecisionModel"/>, <see cref="DefaultSRID"/> and <see cref="DefaultCoordinateSequenceFactory"/>.
        /// </summary>
        /// <returns>The geometry factory</returns>
        IGeometryFactory CreateGeometryFactory();
        
        /// <summary>
        /// Creates a geometry fractory using <see cref="DefaultPrecisionModel"/> and <see cref="DefaultCoordinateSequenceFactory"/>.
        /// </summary>
        /// <param name="srid"></param>
        /// <returns>The geometry factory</returns>
        IGeometryFactory CreateGeometryFactory(int srid);

        /// <summary>
        /// Creates a geometry factory using the given <paramref name="coordinateSequenceFactory"/> along with <see cref="DefaultPrecisionModel"/> and <see cref="DefaultSRID"/>.
        /// </summary>
        /// <param name="coordinateSequenceFactory">The coordinate sequence factory to use.</param>
        /// <returns>The geometry factory.</returns>
        IGeometryFactory CreateGeometryFactory(ICoordinateSequenceFactory coordinateSequenceFactory);

        /// <summary>
        /// Creates a geometry factory using the given <paramref name="precisionModel"/> along with <see cref="DefaultCoordinateSequenceFactory"/> and <see cref="DefaultSRID"/>.
        /// </summary>
        /// <param name="precisionModel">The coordinate sequence factory to use.</param>
        /// <returns>The geometry factory.</returns>
        IGeometryFactory CreateGeometryFactory(IPrecisionModel precisionModel);

        /// <summary>
        /// Creates a geometry factory using the given <paramref name="precisionModel"/> along with <see cref="DefaultCoordinateSequenceFactory"/> and <see cref="DefaultSRID"/>.
        /// </summary>
        /// <param name="precisionModel">The coordinate sequence factory to use.</param>
        /// <param name="srid">The spatial reference id.</param>
        /// <returns>The geometry factory.</returns>
        IGeometryFactory CreateGeometryFactory(IPrecisionModel precisionModel, int srid);

        /// <summary>
        /// Creates a geometry factory using the given <paramref name="precisionModel"/>,
        /// <paramref name="srid"/> and <paramref name="coordinateSequenceFactory"/>.
        /// </summary>
        /// <param name="precisionModel">The coordinate sequence factory to use.</param>
        /// <param name="srid">The spatial reference id.</param>
        /// <param name="coordinateSequenceFactory">The coordinate sequence factory.</param>
        /// <returns>The geometry factory.</returns>
        IGeometryFactory CreateGeometryFactory(IPrecisionModel precisionModel, int srid,
                                               ICoordinateSequenceFactory coordinateSequenceFactory);

        /// <summary>
        /// Reads the configuration from the stream
        /// </summary>
        void ReadConfiguration();

        /// <summary>
        /// Writes the current configuration to the stream
        /// </summary>
        void WriteConfiguration();

    }

    /// <summary>
    /// An interface for classes that offer access to coordinate system creating facillities.
    /// </summary>
    public interface ICoordinateSystemServices<TCoordinateSystem>
    {
        /// <summary>
        /// Gets or sets the default Authority
        /// </summary>
        string DefaultAuthority { get; set; }

        /// <summary>
        /// Gets the initialization string for the coordinate system defined by <paramref name="srid"/>.
        /// </summary>
        /// <param name="srid">The spatial reference id</param>
        /// <returns>The initialization string</returns>
        string GetCoordinateSystemInitializationString(int srid);

        /// <summary>
        /// Gets the initialization string for the coordinate system defined by <paramref name="authorityCode"/> and <paramref name="authority"/>.
        /// </summary>
        /// <param name="authority">The authority name</param>
        /// <param name="authorityCode">The code assigned by <paramref name="authority"/></param>
        /// <returns>The initialization string</returns>
        string GetCoordinateSystemInitializationString(string authority, int authorityCode);

        /// <summary>
        /// Returns the coordinate system defined by <paramref name="init"/>
        /// </summary>
        /// <param name="init">The initialization for the coordinate system</param>
        /// <returns>The coordinate system.</returns>
        TCoordinateSystem GetCoordinateSytem(string init);
    }
}