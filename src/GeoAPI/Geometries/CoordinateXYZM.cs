using System;
using System.Globalization;

namespace GeoAPI.Geometries
{
    /// <summary>
    /// A lightweight class used to store coordinates on the 2-dimensional Cartesian plane
    /// and additional z- and m-ordinate values (<see cref="CoordinateXYZ.Z"/>, <see cref="M"/>).
    /// </summary>
    /// <remarks>
    /// It is distinct from <see cref="IPoint"/>, which is a subclass of <see cref="IGeometry"/>.
    /// Unlike objects of type <see cref="IPoint"/> (which contain additional
    /// information such as an envelope, a precision model, and spatial reference
    /// system information), a <c>CoordinateXYZM</c> only contains ordinate values
    /// and properties.
    /// <para/>
    /// <c>CoordinateXYZM</c>s are two-dimensional points, with an additional Z-ordinate.    
    /// If an Z-ordinate value is not specified or not defined,
    /// constructed coordinates have a Z-ordinate of <code>NaN</code>
    /// (which is also the value of <see cref="CoordinateXY.NullOrdinate"/>).
    /// <para/>
    /// Apart from the basic accessor functions, NTS supports
    /// only specific operations involving the Z- and/or M-ordinate.
    /// <para/>
    /// Implementations may optionally support Z-ordinate and M-measure values
    /// as appropriate for a <see cref="ICoordinateSequence"/>. Use of <see cref="CoordinateXYZ.Z"/>
    /// and <see cref="M"/> setters or <see cref="P:GeoAPI.Geometries.CoordinateXYZM.this[Ordinate]" /> indexer are recommended.
    /// </remarks>
#if HAS_SYSTEM_SERIALIZABLEATTRIBUTE
    [Serializable]
#endif
#pragma warning disable 612,618
    public sealed class CoordinateXYZM : CoordinateXYZ
    {
        /// <summary>
        /// Gets or sets the measure-ordinate value.
        /// </summary>
        public override double M { get; set; }

        /// <summary>
        /// Constructs a <c>CoordinateXYZM</c> at (x,y,z).
        /// </summary>
        /// <param name="x">The X value</param>
        /// <param name="y">The Y value</param>
        /// <param name="z">The Z value</param>
        /// <param name="m">The Measure value</param>
        public CoordinateXYZM(double x, double y, double z, double m) : base(x, y, z)
        {
            M = m;
        }

        /// <summary>
        ///  Constructs a <c>CoordinateXYZM</c> at (0,0,NaN,NaN).
        /// </summary>
        public CoordinateXYZM() : this(0.0, 0.0, NullOrdinate, NullOrdinate) { }

        /// <summary>
        /// Constructs a <c>CoordinateXYZM</c> having the same (x,y) values as
        /// <paramref name="c"/>.
        /// </summary>
        /// <param name="c"><c>CoordinateXY</c> to copy.</param>
        public CoordinateXYZM(CoordinateXY c) : this(c.X, c.Y, c.Z, c.M) { }

        /// <summary>
        /// Constructs a <c>CoordinateXYZM</c> at (x,y,NaN).
        /// </summary>
        /// <param name="x">X value.</param>
        /// <param name="y">Y value.</param>
        public CoordinateXYZM(double x, double y) : this(x, y, NullOrdinate, NullOrdinate) { }


        /// <summary>
        /// Gets or sets the ordinate value for the given index.
        /// </summary>
        /// <remarks>
        /// The base implementation supports  <see cref="Ordinate.X"/>, <see cref="Ordinate.Y"/> and <see cref="Ordinate.Z"/> as values for the index.
        /// </remarks>
        /// <param name="ordinateIndex">The ordinate index</param>
        /// <returns>The ordinate value</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="ordinateIndex"/> is not in the valid range.</exception>
        public override double this[Ordinate ordinateIndex]
        {
            get
            {
                switch (ordinateIndex)
                {
                    case Ordinate.X:
                        return X;
                    case Ordinate.Y:
                        return Y;
                    case Ordinate.Z:
                        return Z;
                    case Ordinate.M:
                        return M;
                }
                throw new ArgumentOutOfRangeException(nameof(ordinateIndex));
            }
            set
            {
                switch (ordinateIndex)
                {
                    case Ordinate.X:
                        X = value;
                        return;
                    case Ordinate.Y:
                        Y = value;
                        return;
                    case Ordinate.Z:
                        Z = value;
                        return;
                    case Ordinate.M:
                        M = value;
                        return;
                }
                throw new ArgumentOutOfRangeException(nameof(ordinateIndex));
            }
        }

        /// <summary>
        /// Gets/Sets <c>CoordinateXYZM</c>s (x,y,z) values.
        /// </summary>
        public override CoordinateXY CoordinateValue
        {
            get { return this; }
            set
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
                M = value.M;
            }
        }

        /// <summary>
        /// Returns a <c>string</c> of the form <i>(x, y, z, m=m)</i> .
        /// </summary>
        /// <returns><c>string</c> of the form <i>(x, y, z, m=m)</i></returns>
        public override string ToString()
        {
            return string.Format(NumberFormatInfo.InvariantInfo, "({0:R}, {1:R}, {2:R}, m={3:R})", X, Y, Z, M);
        }

        ///// <summary>
        ///// Create a new object as copy of this instance.
        ///// </summary>
        ///// <returns></returns>
        //public override CoordinateXY Copy()
        //{
        //    return new CoordinateXYZM(X, Y, Z, M);
        //}
    }
}