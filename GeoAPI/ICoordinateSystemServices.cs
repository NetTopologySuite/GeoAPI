using System;
using GeoAPI.CoordinateSystems;
using GeoAPI.CoordinateSystems.Transformations;

namespace GeoAPI
{
    /// <summary>
    /// Interface for classes that provide access to coordinate system and tranformation facilities.
    /// </summary>
    public interface ICoordinateSystemServices
    {
        /// <summary>
        /// Returns the coordinate system by <paramref name="srid"/> identifier
        /// </summary>
        /// <param name="srid">The initialization for the coordinate system</param>
        /// <returns>The coordinate system.</returns>
        ICoordinateSystem GetCoordinateSystem(int srid);

        /// <summary>
        /// Returns the coordinate system by <paramref name="authority"/> and <paramref name="code"/>.
        /// </summary>
        /// <param name="authority">The authority for the coordinate system</param>
        /// <param name="code">The code assigned to the coordinate system by <paramref name="authority"/>.</param>
        /// <returns>The coordinate system.</returns>
        ICoordinateSystem GetCoordinateSystem(string authority, long code);

        /// <summary>
        /// Method to get the identifier, by which this coordinate system can be accessed.
        /// </summary>
        /// <param name="authority">The authority name</param>
        /// <param name="authorityCode">The code assigned by <paramref name="authority"/></param>
        /// <returns>The identifier or <value>null</value></returns>
        int? GetSRID(string authority, long authorityCode);

        /// <summary>
        /// Method to create a coordinate tranformation between two spatial reference systems, defined by their identifiers
        /// </summary>
        /// <remarks>This is a convenience function for <see cref="CreateTransformation(ICoordinateSystem,ICoordinateSystem)"/>.</remarks>
        /// <param name="sourceSrid">The identifier for the source spatial reference system.</param>
        /// <param name="targetSrid">The identifier for the target spatial reference system.</param>
        /// <returns>A coordinate transformation, <value>null</value> if no transformation could be created.</returns>
        ICoordinateTransformation CreateTransformation(int sourceSrid, int targetSrid);

        /// <summary>
        /// Method to create a coordinate tranformation between two spatial reference systems
        /// </summary>
        /// <param name="source">The source spatial reference system.</param>
        /// <param name="target">The target spatial reference system.</param>
        /// <returns>A coordinate transformation, <value>null</value> if no transformation could be created.</returns>
        ICoordinateTransformation CreateTransformation(ICoordinateSystem source, ICoordinateSystem target);
        
        /// <summary>
        /// Gets the initialization string for the coordinate system defined by <paramref name="srid"/>.
        /// </summary>
        /// <param name="srid">The spatial reference id</param>
        /// <returns>The initialization string</returns>
        [Obsolete("Not used", true)]
        string GetCoordinateSystemInitializationString(int srid);


        /// <summary>
        /// A factory that can create <see cref="ICoordinateSystem"/>s.
        /// </summary>
        [Obsolete("Not used", true)]
        ICoordinateSystemFactory CoordinateSystemFactory { get; }

        /// <summary>
        /// A factory that can create <see cref="ICoordinateTransformation"/>s.
        /// </summary>
        [Obsolete("Not used", true)]
        ICoordinateTransformationFactory CoordinateTransformationFactory { get; }
    }

    /// <summary>
    /// An interface for classes that offer access to coordinate system creating facillities.
    /// </summary>
    [Obsolete("Unused", true)]
    public interface ICoordinateSystemServices<TCoordinateSystem> : ICoordinateSystemServices
    {
        /// <summary>
        /// Gets or sets the default Authority
        /// </summary>
        [Obsolete]
        string DefaultAuthority { get; set; }

        /// <summary>
        /// Gets the initialization string for the coordinate system defined by <paramref name="srid"/>.
        /// </summary>
        /// <param name="srid">The spatial reference id</param>
        /// <returns>The initialization string</returns>
        [Obsolete]
        new string GetCoordinateSystemInitializationString(int srid);

        /// <summary>
        /// Gets the initialization string for the coordinate system defined by <paramref name="authorityCode"/> and <paramref name="authority"/>.
        /// </summary>
        /// <param name="authority">The authority name</param>
        /// <param name="authorityCode">The code assigned by <paramref name="authority"/></param>
        /// <returns>The initialization string</returns>
        [Obsolete]
        string GetCoordinateSystemInitializationString(string authority, int authorityCode);

        /// <summary>
        /// Returns the coordinate system defined by <paramref name="init"/>
        /// </summary>
        /// <param name="init">The initialization for the coordinate system</param>
        /// <returns>The coordinate system.</returns>
        [Obsolete("Misspelled, use GetCoordinateSystem")]
        TCoordinateSystem GetCoordinateSytem(string init);

        /// <summary>
        /// Returns the coordinate system defined by <paramref name="init"/>
        /// </summary>
        /// <param name="init">The initialization for the coordinate system</param>
        /// <returns>The coordinate system.</returns>
        TCoordinateSystem GetCoordinateSystem(string init);
}