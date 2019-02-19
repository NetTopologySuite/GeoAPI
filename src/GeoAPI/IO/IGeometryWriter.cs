using System.IO;
using GeoAPI.Geometries;

namespace GeoAPI.IO
{
    /// <summary>
    /// Base interface for geometry reader or writer interfaces.
    /// </summary>
    public interface IGeometryIOSettings
    {
        /// <summary>
        /// Gets or sets whether the SpatialReference ID must be handled.
        /// </summary>
        bool HandleSRID { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not Z can possibly be handled.
        /// <para>
        /// If <see langword="false"/>, then setting <see cref="HandleZ"/> is a no-op.
        /// </para>
        /// </summary>
        bool AllowsZ { get; }

        /// <summary>
        /// Gets a value indicating whether or not M can possibly be handled.
        /// <para>
        /// If <see langword="false"/>, then setting <see cref="HandleM"/> is a no-op.
        /// </para>
        /// </summary>
        bool AllowsM { get; }

        /// <summary>
        /// Gets or sets a value indicating whether or not Z shall be handled.
        /// </summary>
        bool HandleZ { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not M shall be handled.
        /// </summary>
        bool HandleM { get; set; }
    }
    
    /// <summary>
    /// Interface for binary output of <see cref="IGeometry"/> instances.
    /// </summary>
    /// <typeparam name="TSink">The type of the output to produce.</typeparam>
    public interface IGeometryWriter<TSink> : IGeometryIOSettings
    {
        /// <summary>
        /// Writes a binary representation of a given geometry.
        /// </summary>
        /// <param name="geometry">The geometry</param>
        /// <returns>The binary representation of <paramref name="geometry"/></returns>
        TSink Write(IGeometry geometry);

        /// <summary>
        /// Writes a binary representation of a given geometry.
        /// </summary>
        /// <param name="geometry"></param>
        /// <param name="stream"></param>
        void Write(IGeometry geometry, Stream stream);
    }

    /// <summary>
    /// Interface for binary output of <see cref="IGeometry"/> instances.
    /// </summary>
    public interface IBinaryGeometryWriter : IGeometryWriter<byte[]>
    {
        /// <summary>
        /// Gets or sets the desired <see cref="ByteOrder"/>
        /// </summary>
        ByteOrder ByteOrder { get; set; }
    }
    
    /// <summary>
    /// Interface for textual output of <see cref="IGeometry"/> instances.
    /// </summary>
    public interface ITextGeometryWriter : IGeometryWriter<string>
    {
    }

}