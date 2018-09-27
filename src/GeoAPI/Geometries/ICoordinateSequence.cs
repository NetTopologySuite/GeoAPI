using System;

namespace GeoAPI.Geometries
{
#if HAS_SYSTEM_ICLONEABLE
    using ICloneable = System.ICloneable;
#else
    using ICloneable = GeoAPI.ICloneable;
#endif

    /// <summary>
    /// The internal representation of a list of coordinates inside a Geometry.
    /// <para>
    /// This allows Geometries to store their
    /// points using something other than the NTS <see cref="Coordinate"/> class. 
    /// For example, a storage-efficient implementation
    /// might store coordinate sequences as an array of x's
    /// and an array of y's. 
    /// Or a custom coordinate class might support extra attributes like M-values.
    /// </para>
    /// <para>
    /// Implementing a custom coordinate storage structure
    /// requires implementing the <see cref="ICoordinateSequence"/> and
    /// <see cref="ICoordinateSequenceFactory"/> interfaces. 
    /// To use the custom CoordinateSequence, create a
    /// new <see cref="IGeometryFactory"/> parameterized by the CoordinateSequenceFactory
    /// The <see cref="IGeometryFactory"/> can then be used to create new <see cref="IGeometry"/>s.
    /// The new Geometries will use the custom CoordinateSequence implementation.
    /// </para>
    /// </summary>
    ///// <remarks>
    ///// For an example see <see cref="ExtendedCoordinateSample"/>
    ///// </remarks>
    ///// <seealso cref="NetTopologySuite.Geometries.Implementation.CoordinateArraySequenceFactory"/>
    ///// <seealso cref="NetTopologySuite.Geometries.Implementation.ExtendedCoordinateExample"/>
    ///// <seealso cref="NetTopologySuite.Geometries.Implementation.PackedCoordinateSequenceFactory"/>
    public interface ICoordinateSequence 
        : ICloneable
    {
        /// <summary>
        /// Returns the dimension (number of ordinates in each coordinate) for this sequence.
        /// <para>
        /// This total includes any measures, indicated by non-zero <see cref="Measures"/>.
        /// </para>
        /// </summary>
        int Dimension { get; }

        /// <summary>
        /// Returns the number of measures included in {@link #getDimension()} for each coordinate for this
        /// sequence.
        /// For a measured coordinate sequence a non-zero value is returned.
        /// <list type="Bullet">
        /// <item>For <see cref="Geometries.Ordinates.XY"/> sequence measures is zero</item>
        /// <item>For <see cref="Geometries.Ordinates.XYM"/> sequence measure is one</item>
        /// <item>For <see cref="Geometries.Ordinates.XYZ"/> sequence measure is zero</item>
        /// <item>For <see cref="Geometries.Ordinates.XYZM"/> sequence measure is one</item>
        /// <item>Values greater than one are supported</item>
        /// </list>
        /// </summary>
        int Measures { get; }

        /// <summary>
        /// Returns the kind of ordinates this sequence supplies.
        /// </summary>
        Ordinates Ordinates { get; }

        /// <summary>
        /// Returns the number of coordinates in this sequence.
        /// </summary>        
        int Count { get; }

        /// <summary>
        /// Creates a coordinate for use in this sequence.
        /// </summary>
        /// <remarks>
        /// The coordinate is created supporting the same number of <see cref="Dimension"/> and <see cref="Measures"/>
        /// as this sequence and is suitable for use with <see cref="GetCoordinate(int, CoordinateXY)"/>.
        /// </remarks>
        /// <returns>A coordinate for use with this sequence</returns>
        CoordinateXY CreateCoordinate();

        /// <summary>
        /// Returns (possibly a copy of) the ith Coordinate in this collection.
        /// Whether or not the Coordinate returned is the actual underlying
        /// Coordinate or merely a copy depends on the implementation.
        /// Note that in the future the semantics of this method may change
        /// to guarantee that the Coordinate returned is always a copy. Callers are
        /// advised not to assume that they can modify a CoordinateSequence by
        /// modifying the Coordinate returned by this method.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        CoordinateXY GetCoordinate(int i);

        /// <summary>
        /// Returns a copy of the i'th coordinate in this sequence.
        /// This method optimizes the situation where the caller is
        /// going to make a copy anyway - if the implementation
        /// has already created a new Coordinate object, no further copy is needed.
        /// </summary>
        /// <param name="i">The index of the coordinate to retrieve.</param>
        /// <returns>A copy of the i'th coordinate in the sequence</returns>
        CoordinateXY GetCoordinateCopy(int i);             

        /// <summary>
        /// Copies the i'th coordinate in the sequence to the supplied Coordinate.  
        /// At least the first two dimensions <b>must</b> be copied.        
        /// </summary>
        /// <param name="index">The index of the coordinate to copy.</param>
        /// <param name="coord">A Coordinate to receive the value.</param>
        void GetCoordinate(int index, CoordinateXY coord);

        /// <summary>
        /// Returns ordinate X (0) of the specified coordinate.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The value of the X ordinate in the index'th coordinate.</returns>
        double GetX(int index);

        /// <summary>
        /// Returns ordinate Y (1) of the specified coordinate.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The value of the Y ordinate in the index'th coordinate.</returns>
        double GetY(int index);

        /// <summary>
        /// Returns ordinate Z of the specified coordinate if available.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>
        /// The value of the Z ordinate in the index'th coordinate, or
        /// <see cref="Coordinate.NullOrdinate"/> if not defined.
        /// </returns>
        /// <remarks>Default implementation (C#)
        /// <code>
        /// double GetZ(int index)
        /// {
        ///     if ((Dimension - Measures) > 2)
        ///     {
        ///         return GetOrdinate(index, 2);
        ///     }
        ///     else
        ///     {
        ///         return Coordinate.NullOrdinate;
        ///     }
        /// }
        /// </code>
        /// </remarks>
        double GetZ(int index);

        /// <summary>
        /// Returns ordinate M of the specified coordinate if available.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>
        /// The value of the Z ordinate in the index'th coordinate, or
        /// <see cref="Coordinate.NullOrdinate"/> if not defined.
        /// </returns>
        /// <remarks>
        /// <code>
        /// double GetM(int index)
        /// {
        ///     if (Dimension > 2 &amp;&amp; Measures > 0)
        ///     {
        ///         int mIndex = Dimension - Measures;
        ///         return getOrdinate(index, mIndex);
        ///     }
        ///     else
        ///     {
        ///         return double.NaN;
        ///     }
        /// }
        /// </code>
        /// </remarks>
        double GetM(int index);

        /// <summary>
        /// Returns the ordinate of a coordinate in this sequence.
        /// Ordinate indices 0 and 1 are assumed to be X and Y.
        /// <para/>
        /// Ordinate indices greater than 1 have user-defined semantics
        /// (for instance, they may contain other dimensions or measure
        /// values as described by <see cref="Dimension"/> and <see cref="Measures"/>.
        /// </summary>
        /// <remarks>
        /// If the sequence does not provide value for the required ordinate, the implementation <b>must not</b> throw an exception, it should return <see cref="Coordinate.NullOrdinate"/>.
        /// </remarks>
        /// <param name="index">The coordinate index in the sequence.</param>
        /// <param name="ordinate">The ordinate index in the coordinate (in range [0, dimension-1]).</param>
        /// <returns>The ordinate value, or <see cref="Coordinate.NullOrdinate"/> if the sequence does not provide values for <paramref name="ordinate"/>"/></returns>       
        double GetOrdinate(int index, Ordinate ordinate);

        /// <summary>
        /// Sets the value for a given ordinate of a coordinate in this sequence.       
        /// </summary>
        /// <remarks>
        /// If the sequence can't store the ordinate value, the implementation <b>must not</b> throw an exception, it should simply ignore the call.
        /// </remarks>
        /// <param name="index">The coordinate index in the sequence.</param>
        /// <param name="ordinate">The ordinate index in the coordinate (in range [0, dimension-1]).</param>
        /// <param name="value">The new ordinate value.</param>       
        void SetOrdinate(int index, Ordinate ordinate, double value);

        /// <summary>
        /// Returns (possibly copies of) the Coordinates in this collection.
        /// Whether or not the Coordinates returned are the actual underlying
        /// Coordinates or merely copies depends on the implementation. Note that
        /// if this implementation does not store its data as an array of Coordinates,
        /// this method will incur a performance penalty because the array needs to
        /// be built from scratch.
        /// </summary>
        /// <returns></returns>
        CoordinateXY[] ToCoordinateArray();

        /// <summary>
        /// Expands the given Envelope to include the coordinates in the sequence.
        /// Allows implementing classes to optimize access to coordinate values.      
        /// </summary>
        /// <param name="env">The envelope to expand.</param>
        /// <returns>A reference to the expanded envelope.</returns>       
        Envelope ExpandEnvelope(Envelope env);

        /// <summary>
        /// Creates a reversed version of this coordinate sequence with cloned <see cref="Coordinate"/>s
        /// </summary>
        /// <returns>A reversed version of this sequence</returns>
        [Obsolete("Not conform with JTS")]
        ICoordinateSequence Reversed();

        /// <summary>
        /// Returns a deep copy of this collection.
        /// </summary>
        /// <returns>A copy of the coordinate sequence containing copies of all points</returns>
        ICoordinateSequence Copy();
    }
}
