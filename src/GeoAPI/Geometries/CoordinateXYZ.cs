using System;
using System.Globalization;

namespace GeoAPI.Geometries
{
    /// <summary>
    /// A lightweight class used to store coordinates on the 2-dimensional Cartesian plane
    /// and an additional z-ordinate (<see cref="Z"/>) value.
    /// </summary><remarks>
    /// It is distinct from <see cref="IPoint"/>, which is a subclass of <see cref="IGeometry"/>.
    /// Unlike objects of type <see cref="IPoint"/> (which contain additional
    /// information such as an envelope, a precision model, and spatial reference
    /// system information), a <c>CoordinateXYZ</c> only contains ordinate values
    /// and properties.
    /// <para/>
    /// <c>CoordinateXYZ</c>s are two-dimensional points, with an additional Z-ordinate.    
    /// If an Z-ordinate value is not specified or not defined,
    /// constructed coordinates have a Z-ordinate of <code>NaN</code>
    /// (which is also the value of <see cref="CoordinateXY.NullOrdinate"/>).
    /// <para/>
    /// Apart from the basic accessor functions, NTS supports
    /// only specific operations involving the Z-ordinate.
    /// <para/>
    /// Implementations may optionally support Z-ordinate and M-measure values
    /// as appropriate for a <see cref="ICoordinateSequence"/>. Use of <see cref="Z"/>
    /// and <see cref="CoordinateXY.M"/> setters or <see cref="P:GeoAPI.Geometries.CoordinateXYZ.this[Ordinate]" /> indexer are recommended.
    /// </remarks>
#if HAS_SYSTEM_SERIALIZABLEATTRIBUTE
    [Serializable]
#endif
#pragma warning disable 612,618
    public class CoordinateXYZ : CoordinateXY
    {
        /// <summary>
        /// Gets or sets the Z-ordinate value.
        /// </summary>
        public sealed override double Z { get; set; }

        /// <summary>
        /// Constructs a <c>CoordinateXYZ</c> at (x,y,z).
        /// </summary>
        /// <param name="x">The X value</param>
        /// <param name="y">The Y value</param>
        /// <param name="z">The Z value</param>
        public CoordinateXYZ(double x, double y, double z) : base(x, y)
        {
            Z = z;
        }

        /// <summary>
        ///  Constructs a <c>CoordinateXYZ</c> at (0,0,NaN).
        /// </summary>
        public CoordinateXYZ() : this(0.0, 0.0, NullOrdinate) { }

        /// <summary>
        /// Constructs a <c>CoordinateXYZ</c> having the same (x,y) values as
        /// <paramref name="c"/>.
        /// </summary>
        /// <param name="c"><c>CoordinateXY</c> to copy.</param>
        public CoordinateXYZ(CoordinateXY c) : this(c.X, c.Y, c.Z) { }

        /// <summary>
        /// Constructs a <c>CoordinateXYZ</c> at (x,y,NaN).
        /// </summary>
        /// <param name="x">X value.</param>
        /// <param name="y">Y value.</param>
        public CoordinateXYZ(double x, double y) : this(x, y, NullOrdinate) { }


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
                }
                throw new ArgumentOutOfRangeException(nameof(ordinateIndex));
            }
        }

        /// <summary>
        /// Gets/Sets <c>CoordinateXYZ</c>s (x,y,z) values.
        /// </summary>
        public override CoordinateXY CoordinateValue
        {
            get { return this; }
            set
            {
                X = value.X;
                Y = value.Y;
                Z = value.Z;
            }
        }

        /// <summary>
        /// Returns <c>true</c> if <paramref name="other"/> 
        /// has the same values for X, Y and Z.
        /// </summary>
        /// <param name="other">A <see cref="CoordinateXYZ"/> with which to do the 3D comparison.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="other"/> is a <see cref="CoordinateXYZ"/> 
        /// with the same values for X, Y and Z.
        /// </returns>
        public bool Equals3D(CoordinateXYZ other)
        {
            return (X == other.X) && (Y == other.Y) &&
                ((Z == other.Z) || (double.IsNaN(Z) && double.IsNaN(other.Z)));
        }

        /// <summary>
        /// Tests if another CoordinateXYZ has the same value for Z, within a tolerance.
        /// </summary>
        /// <param name="c">A <see cref="CoordinateXYZ"/>.</param>
        /// <param name="tolerance">The tolerance value.</param>
        /// <returns><c>true</c> if the Z ordinates are within the given tolerance.</returns>
        public bool EqualInZ(CoordinateXYZ c, double tolerance)
        {
            return EqualsWithTolerance(this.Z, c.Z, tolerance);
        }

        /// <summary>
        /// Returns a <c>string</c> of the form <i>(x, y, z)</i> .
        /// </summary>
        /// <returns><c>string</c> of the form <i>(x, y, z)</i></returns>
        public override string ToString()
        {
            return string.Format(NumberFormatInfo.InvariantInfo, "({0:R}, {1:R}, {2:R})", X, Y, Z);
        }

        ///// <summary>
        ///// Create a new object as copy of this instance.
        ///// </summary>
        //public override CoordinateXY Copy()
        //{
        //    return new CoordinateXYZ(X, Y, Z);
        //}

        /// <summary>
        /// Computes the 3-dimensional Euclidean distance to another location.
        /// </summary>
        /// <param name="c">A <see cref="CoordinateXYZ"/> with which to do the distance comparison.</param>
        /// <returns>the 3-dimensional Euclidean distance between the locations.</returns>
        public double Distance3D(CoordinateXYZ c)
        {
            double dx = X - c.X;
            double dy = Y - c.Y;
            double dz = Z - c.Z;
            if (double.IsNaN(dz)) dz = 0;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
    }
}