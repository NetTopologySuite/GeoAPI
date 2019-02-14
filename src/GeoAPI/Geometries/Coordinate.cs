using System;
using System.Globalization;

namespace GeoAPI.Geometries
{
    /// <summary>
    /// A lightweight class used to store coordinates on the 2-dimensional Cartesian plane.
    /// </summary>
    /// <remarks>
    /// It is distinct from <see cref="IPoint"/>, which is a subclass of <see cref="IGeometry"/>.
    /// Unlike objects of type <see cref="IPoint"/> (which contain additional
    /// information such as an envelope, a precision model, and spatial reference
    /// system information), a <c>Coordinate</c> only contains ordinate values
    /// and properties.
    /// <para/>
    /// <c>Coordinate</c>s are two-dimensional points, with an additional Z-ordinate.    
    /// If an Z-ordinate value is not specified or not defined,
    /// constructed coordinates have a Z-ordinate of <code>NaN</code>
    /// (which is also the value of <see cref="NullOrdinate"/>).
    /// <para/>
    /// Apart from the basic accessor functions, NTS supports
    /// only specific operations involving the Z-ordinate.
    /// <para/>
    /// Implementations may optionally support Z-ordinate and M-measure values
    /// as appropriate for a <see cref="ICoordinateSequence"/>. Use of <see cref="Z"/>
    /// and <see cref="M"/> setters or <see cref="P:GeoAPI.Geometries.Coordinate.this[Ordinate]" /> indexer are recommended.
    /// </remarks>
#if HAS_SYSTEM_SERIALIZABLEATTRIBUTE
    [Serializable]
#endif
    [Obsolete("Use concrete classes like CoordinateXY, CoordinateXYM, CoordinateXYZ or CoordinateXYZM")]
#pragma warning disable 612,618
    public class Coordinate : IComparable<Coordinate>
    {

        ///<summary>
        /// The value used to indicate a null or missing ordinate value.
        /// In particular, used for the value of ordinates for dimensions
        /// greater than the defined dimension of a coordinate.
        ///</summary>
        public const double NullOrdinate = Double.NaN;

        /// <summary>
        /// Gets or sets the X-ordinate value.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y-ordinate value.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the Z-ordinate value.
        /// </summary>
        public virtual double Z { get; set; }

        /// <summary>
        /// Gets the default m-measure (<see cref="NullOrdinate"/>).
        /// </summary>
        public virtual double M
        {
            get => NullOrdinate;
            set { throw new InvalidOperationException($"{nameof(Coordinate)} does not support setting M-measure"); }
        }

        /// <summary>
        /// Constructs a <c>Coordinate</c> at (x,y,z).
        /// </summary>
        /// <param name="x">The X value</param>
        /// <param name="y">The Y value</param>
        /// <param name="z">The Z value</param>
        public Coordinate(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        ///  Constructs a <c>Coordinate</c> at (0,0,NaN).
        /// </summary>
        public Coordinate() : this(0.0, 0.0, NullOrdinate) { }

        /// <summary>
        /// Constructs a <c>Coordinate</c> having the same (x,y,z) values as
        /// <c>other</c>.
        /// </summary>
        /// <param name="c"><c>Coordinate</c> to copy.</param>
        public Coordinate(Coordinate c) : this(c.X, c.Y, c.Z) { }

        /// <summary>
        /// Constructs a <c>Coordinate</c> at (x,y,NaN).
        /// </summary>
        /// <param name="x">X value.</param>
        /// <param name="y">Y value.</param>
        public Coordinate(double x, double y) : this(x, y, NullOrdinate) { }


        /// <summary>
        /// Gets or sets the ordinate value for the given index.
        /// </summary>
        /// <remarks>
        /// The base implementation supports  <see cref="Ordinate.X"/>, <see cref="Ordinate.Y"/> and <see cref="Ordinate.Z"/> as values for the index.
        /// </remarks>
        /// <param name="ordinateIndex">The ordinate index</param>
        /// <returns>The ordinate value</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="ordinateIndex"/> is not in the valid range.</exception>
        public double this[Ordinate ordinateIndex]
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
        /// Gets/Sets <c>Coordinate</c>s (x,y,z) values.
        /// </summary>
        public Coordinate CoordinateValue
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
        /// Returns whether the planar projections of the two <c>Coordinate</c>s are equal.
        ///</summary>
        /// <param name="other"><c>Coordinate</c> with which to do the 2D comparison.</param>
        /// <returns>
        /// <c>true</c> if the x- and y-coordinates are equal;
        /// the Z coordinates do not have to be equal.
        /// </returns>
        public bool Equals2D(Coordinate other)
        {
            return X == other.X && Y == other.Y;
        }

        /// <summary>
        /// Tests if another coordinate has the same value for X and Y, within a tolerance.
        /// </summary>
        /// <param name="c">A <see cref="Coordinate"/>.</param>
        /// <param name="tolerance">The tolerance value.</param>
        /// <returns><c>true</c> if the X and Y ordinates are within the given tolerance.</returns>
        /// <remarks>The Z ordinate is ignored.</remarks>
        public bool Equals2D(Coordinate c, double tolerance)
        {
            if (!EqualsWithTolerance(X, c.X, tolerance))
                return false;
            if (!EqualsWithTolerance(Y, c.Y, tolerance))
                return false;
            return true;
        }

        private static bool EqualsWithTolerance(double v1, double v2, double tolerance)
        {
            return Math.Abs(v1 - v2) <= tolerance;
        }

        /// <summary>
        /// Returns <c>true</c> if <c>other</c> has the same values for the x and y ordinates.
        /// Since Coordinates are 2.5D, this routine ignores the z value when making the comparison.
        /// </summary>
        /// <param name="other"><c>Coordinate</c> with which to do the comparison.</param>
        /// <returns><c>true</c> if <c>other</c> is a <c>Coordinate</c> with the same values for the x and y ordinates.</returns>
        public override bool Equals(object o)
        {
            if (o is Coordinate other)
                return Equals(other);
            if (o is CoordinateXY otherXY)
                return ((CoordinateXY) this).Equals(otherXY);

            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Boolean Equals(Coordinate other)
        {
            return Equals2D(other);
        }

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="obj1"></param>
        ///// <param name="obj2"></param>
        ///// <returns></returns>
        //public static bool operator ==(Coordinate obj1, ICoordinate obj2)
        //{
        //    return Equals(obj1, obj2);
        //}

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="obj1"></param>
        ///// <param name="obj2"></param>
        ///// <returns></returns>
        //public static bool operator !=(Coordinate obj1, ICoordinate obj2)
        //{
        //    return !(obj1 == obj2);
        //}

        /// <summary>
        /// Compares this object with the specified object for order.
        /// Since Coordinates are 2.5D, this routine ignores the z value when making the comparison.
        /// Returns
        ///   -1  : this.x &lt; other.x || ((this.x == other.x) AND (this.y &lt; other.y))
        ///    0  : this.x == other.x AND this.y = other.y
        ///    1  : this.x &gt; other.x || ((this.x == other.x) AND (this.y &gt; other.y))
        /// </summary>
        /// <param name="o"><c>Coordinate</c> with which this <c>Coordinate</c> is being compared.</param>
        /// <returns>
        /// A negative integer, zero, or a positive integer as this <c>Coordinate</c>
        ///         is less than, equal to, or greater than the specified <c>Coordinate</c>.
        /// </returns>
        public int CompareTo(object o)
        {
            if (o is Coordinate other)
                return CompareTo(other);
            return 1;
        }

        /// <summary>
        /// Compares this object with the specified object for order.
        /// Since Coordinates are 2.5D, this routine ignores the z value when making the comparison.
        /// Returns
        ///   -1  : this.x &lt; other.x || ((this.x == other.x) AND (this.y &lt; other.y))
        ///    0  : this.x == other.x AND this.y = other.y
        ///    1  : this.x &gt; other.x || ((this.x == other.x) AND (this.y &gt; other.y))
        /// </summary>
        /// <param name="other"><c>Coordinate</c> with which this <c>Coordinate</c> is being compared.</param>
        /// <returns>
        /// A negative integer, zero, or a positive integer as this <c>Coordinate</c>
        ///         is less than, equal to, or greater than the specified <c>Coordinate</c>.
        /// </returns>
        public int CompareTo(Coordinate other)
        {
            if (X < other.X)
                return -1;
            if (X > other.X)
                return 1;
            if (Y < other.Y)
                return -1;
            return Y > other.Y ? 1 : 0;
        }

        /// <summary>
        /// Returns <c>true</c> if <paramref name="other"/> 
        /// has the same values for X, Y and Z.
        /// </summary>
        /// <param name="other">A <see cref="Coordinate"/> with which to do the 3D comparison.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="other"/> is a <see cref="Coordinate"/> 
        /// with the same values for X, Y and Z.
        /// </returns>
        public bool Equals3D(Coordinate other)
        {
            return (X == other.X) && (Y == other.Y) &&
                ((Z == other.Z) || (Double.IsNaN(Z) && Double.IsNaN(other.Z)));
        }

        /// <summary>
        /// Tests if another coordinate has the same value for Z, within a tolerance.
        /// </summary>
        /// <param name="c">A <see cref="Coordinate"/>.</param>
        /// <param name="tolerance">The tolerance value.</param>
        /// <returns><c>true</c> if the Z ordinates are within the given tolerance.</returns>
        public bool EqualInZ(Coordinate c, double tolerance)
        {
            return EqualsWithTolerance(this.Z, c.Z, tolerance);
        }

        /// <summary>
        /// Returns a <c>string</c> of the form <I>(x,y,z)</I> .
        /// </summary>
        /// <returns><c>string</c> of the form <I>(x,y,z)</I></returns>
        public override string ToString()
        {
            return "(" + X.ToString("R", NumberFormatInfo.InvariantInfo) + ", " +
                         Y.ToString("R", NumberFormatInfo.InvariantInfo) + ", " +
                         Z.ToString("R", NumberFormatInfo.InvariantInfo) + ")";
        }

        /// <summary>
        /// Create a new object as copy of this instance.
        /// </summary>
        /// <returns></returns>
        public virtual Coordinate Copy()
        {
            return new Coordinate(X, Y, Z);
        }

        /// <summary>
        /// Computes the 2-dimensional Euclidean distance to another location.
        /// </summary>
        /// <param name="c">A <see cref="Coordinate"/> with which to do the distance comparison.</param>
        /// <returns>the 2-dimensional Euclidean distance between the locations.</returns>
        /// <remarks>The Z-ordinate is ignored.</remarks>
        public double Distance(Coordinate c)
        {
            var dx = X - c.X;
            var dy = Y - c.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        /// <summary>
        /// Computes the 3-dimensional Euclidean distance to another location.
        /// </summary>
        /// <param name="c">A <see cref="Coordinate"/> with which to do the distance comparison.</param>
        /// <returns>the 3-dimensional Euclidean distance between the locations.</returns>
        public double Distance3D(Coordinate c)
        {
            double dx = X - c.X;
            double dy = Y - c.Y;
            double dz = Z - c.Z;
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }

        /// <summary>
        /// Gets a hashcode for this coordinate.
        /// </summary>
        /// <returns>A hashcode for this coordinate.</returns>
        public override int GetHashCode()
        {
            int result = 17;
            // ReSharper disable NonReadonlyFieldInGetHashCode
            result = 37 * result + GetHashCode(X);
            result = 37 * result + GetHashCode(Y);
            // ReSharper restore NonReadonlyFieldInGetHashCode
            return result;
        }

        /// <summary>
        /// Computes a hash code for a double value, using the algorithm from
        /// Joshua Bloch's book <i>Effective Java"</i>
        /// </summary>
        /// <param name="value">A hashcode for the double value</param>
        public static int GetHashCode(double value)
        {
            return value.GetHashCode();

            // This was implemented as follows, but that's actually equivalent:
            /*
            var f = BitConverter.DoubleToInt64Bits(value);
            return (int)(f ^ (f >> 32));
            */
        }

        /// <summary>
        /// Implicit conversion operator to get a <see cref="CoordinateXY"/> from a <see cref="Coordinate"/>.
        /// </summary>
        /// <param name="c">The coordinate</param>
        public static implicit operator CoordinateXY(Coordinate c)
        {
            return new CoordinateXY(c.X, c.Y);
        }

        /// <summary>
        /// Implicit conversion operator to get a <see cref="Coordinate"/> from a <see cref="CoordinateXY"/>.
        /// </summary>
        /// <param name="c">The coordinate</param>
        public static implicit operator Coordinate(CoordinateXY c)
        {
            return new Coordinate(c.X, c.Y);
        }

        /// <summary>
        /// Explicit conversion operator to get a <see cref="CoordinateXYZ"/> from a <see cref="Coordinate"/>.
        /// </summary>
        /// <param name="c">The coordinate</param>
        public static explicit operator CoordinateXYZ(Coordinate c)
        {
            return new CoordinateXYZ(c.X, c.Y, c.Z);
        }

        /// <summary>
        /// Implicit conversion operator to get a <see cref="Coordinate"/> from a <see cref="CoordinateXYZ"/>.
        /// </summary>
        /// <param name="c">The coordinate</param>
        public static explicit operator Coordinate(CoordinateXYZ c)
        {
            return new Coordinate(c.X, c.Y, c.Z);
        }

    }
}