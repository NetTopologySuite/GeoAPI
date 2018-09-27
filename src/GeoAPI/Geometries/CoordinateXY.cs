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
    /// </remarks>
#if HAS_SYSTEM_SERIALIZABLEATTRIBUTE
    [Serializable]
#endif
#pragma warning disable 612,618
    public class CoordinateXY : IComparable<CoordinateXY>, ICoordinate
    {

        ///<summary>
        /// The value used to indicate a null or missing ordinate value.
        /// In particular, used for the value of ordinates for dimensions
        /// greater than the defined dimension of a coordinate.
        ///</summary>
        public const double NullOrdinate = double.NaN;
        /// <summary>
        /// Gets or sets the X-ordinate value.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y-ordinate value.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the Z-ordinate value (<see cref="NullOrdinate"/>).
        /// </summary>
        public virtual double Z
        {
            get => NullOrdinate; 
            set { }
        }

        /// <summary>
        /// Gets the default m-measure (<see cref="NullOrdinate"/>).
        /// </summary>
        public virtual double M
        {
            get => NullOrdinate;
            set { }
        }

        /// <summary>
        /// Constructs a <c>CoordinateXY</c> at (x,y).
        /// </summary>
        /// <param name="x">The X value</param>
        /// <param name="y">The Y value</param>
        public CoordinateXY(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        ///  Constructs a <c>CoordinateXY</c> at (0,0).
        /// </summary>
        public CoordinateXY() : this(0.0, 0.0) { }

        /// <summary>
        /// Constructs a <c>Coordinate</c> having the same (x,y) values as
        /// <paramref name="c"/>.
        /// </summary>
        /// <param name="c"><c>Coordinate</c> to copy.</param>
        [Obsolete]
        public CoordinateXY(ICoordinate c) : this(c.X, c.Y) { }

        /// <summary>
        /// Constructs a <c>Coordinate</c> having the same (x,y,z) values as
        /// <paramref name="c"/>.
        /// </summary>
        /// <param name="c"><c>Coordinate</c> to copy.</param>
        public CoordinateXY(CoordinateXY c) : this(c.X, c.Y) { }


        /// <summary>
        /// Gets or sets the ordinate value for the given index.
        /// </summary>
        /// <remarks>
        /// The base implementation supports <see cref="Ordinate.X"/> and <see cref="Ordinate.Y"/> as values for the index.
        /// </remarks>
        /// <param name="ordinateIndex">The ordinate index</param>
        /// <returns>The ordinate value</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="ordinateIndex"/> is not in the valid range.</exception>
        public virtual double this[Ordinate ordinateIndex]
        {
            get
            {
                switch (ordinateIndex)
                {
                    case Ordinate.X:
                        return X;
                    case Ordinate.Y:
                        return Y;
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
                }
                throw new ArgumentOutOfRangeException(nameof(ordinateIndex));
            }
        }

        /// <summary>
        /// Gets/Sets <c>Coordinate</c>s (x,y,z) values.
        /// </summary>
        public virtual CoordinateXY CoordinateValue
        {
            get
            {
                return this;
            }
            set
            {
                X = value.X;
                Y = value.Y;
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
        public bool Equals2D(CoordinateXY other)
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
        public bool Equals2D(CoordinateXY c, double tolerance)
        {
            if (!EqualsWithTolerance(X, c.X, tolerance))
                return false;
            if (!EqualsWithTolerance(Y, c.Y, tolerance))
                return false;
            return true;
        }

        protected static bool EqualsWithTolerance(double v1, double v2, double tolerance)
        {
            return Math.Abs(v1 - v2) <= tolerance;
        }

        /// <summary>
        /// Returns <c>true</c> if <c>other</c> has the same values for the x and y ordinates.
        /// Since Coordinates are 2.5D, this routine ignores the z value when making the comparison.
        /// </summary>
        /// <param name="o"><c>Coordinate</c> with which to do the comparison.</param>
        /// <returns><c>true</c> if <c>other</c> is a <c>Coordinate</c> with the same values for the x and y ordinates.</returns>
        public override bool Equals(object o)
        {
            if (!(o is CoordinateXY other))
            {
#pragma warning disable 612, 618
                if (!(o is ICoordinate iother))
                    return false;
                return ((ICoordinate)this).Equals(iother);
#pragma warning restore 612,618
            }
            return Equals(other);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public Boolean Equals(CoordinateXY other)
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
            if (o is CoordinateXY other)
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
        public int CompareTo(CoordinateXY other)
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
        /// Returns a <c>string</c> of the form <I>(x,y,z)</I> .
        /// </summary>
        /// <returns><c>string</c> of the form <I>(x,y,z)</I></returns>
        public override string ToString()
        {
            return string.Format(NumberFormatInfo.InvariantInfo, "({0:R}, {1:R})", X, Y);
        }

        /// <summary>
        /// Create a new object as copy of this instance.
        /// </summary>
        /// <returns></returns>
        public virtual CoordinateXY Copy()
        {
            return new CoordinateXY(X, Y);
        }

        /// <summary>
        /// Create a new object as copy of this instance.
        /// </summary>
        /// <returns></returns>
        [Obsolete("Use Copy")]
        public object Clone()
        {
            return MemberwiseClone();
        }

        /// <summary>
        /// Computes the 2-dimensional Euclidean distance to another location.
        /// </summary>
        /// <param name="c">A <see cref="CoordinateXY"/> with which to do the distance comparison.</param>
        /// <returns>the 2-dimensional Euclidean distance between the locations.</returns>
        /// <remarks>The Z-ordinate is ignored.</remarks>
        public double Distance(CoordinateXY c)
        {
            var dx = X - c.X;
            var dy = Y - c.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }
        
        /// <summary>
        /// Gets a hashcode for this coordinate.
        /// </summary>
        /// <returns>A hashcode for this coordinate.</returns>
        public override int GetHashCode()
        {
            var result = 17;
            // ReSharper disable NonReadonlyFieldInGetHashCode
            result = 37 * result + X.GetHashCode();
            result = 37 * result + Y.GetHashCode();
            // ReSharper restore NonReadonlyFieldInGetHashCode
            return result;
        }

        #region ICoordinate

        /// <summary>
        /// X coordinate.
        /// </summary>
        [Obsolete]
        double ICoordinate.X
        {
            get { return X; }
            set { X = value; }
        }

        /// <summary>
        /// Y coordinate.
        /// </summary>
        [Obsolete]
        double ICoordinate.Y
        {
            get { return Y; }
            set { Y = value; }
        }

        /// <summary>
        /// Z coordinate.
        /// </summary>
        [Obsolete]
        double ICoordinate.Z
        {
            get { return Z; }
            set { Z = value; }
        }

        /// <summary>
        /// The measure value
        /// </summary>
        [Obsolete]
        double ICoordinate.M
        {
            get { return M; }
            set { M = value; }
        }

        /// <summary>
        /// Gets/Sets <c>Coordinate</c>s (x,y,z) values.
        /// </summary>
        [Obsolete]
        ICoordinate ICoordinate.CoordinateValue
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
        /// Gets/Sets the ordinate value for a given index
        /// </summary>
        /// <param name="index">The index of the ordinate</param>
        /// <returns>The ordinate value</returns>
        [Obsolete]
        double ICoordinate.this[Ordinate index]
        {
            get
            {
                switch (index)
                {
                    case Ordinate.X:
                        return X;
                    case Ordinate.Y:
                        return Y;
                    case Ordinate.Z:
                        return Z;
                    default:
                        return NullOrdinate;
                }
            }
            set
            {
                switch (index)
                {
                    case Ordinate.X:
                        X = value;
                        break;
                    case Ordinate.Y:
                        Y = value;
                        break;
                    case Ordinate.Z:
                        Z = value;
                        break;
                }
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
        [Obsolete]
        bool ICoordinate.Equals2D(ICoordinate other)
        {
            return X == other.X && Y == other.Y;
        }

        /// <summary>
        /// Compares this object with the specified object for order.
        /// Since Coordinates are 2.5D, this routine ignores the z value when making the comparison.
        /// Returns
        ///   -1  : this.x lowerthan other.x || ((this.x == other.x) AND (this.y lowerthan other.y))
        ///    0  : this.x == other.x AND this.y = other.y
        ///    1  : this.x greaterthan other.x || ((this.x == other.x) AND (this.y greaterthan other.y))
        /// </summary>
        /// <param name="other"><c>Coordinate</c> with which this <c>Coordinate</c> is being compared.</param>
        /// <returns>
        /// A negative integer, zero, or a positive integer as this <c>Coordinate</c>
        ///         is less than, equal to, or greater than the specified <c>Coordinate</c>.
        /// </returns>
        [Obsolete]
        int IComparable<ICoordinate>.CompareTo(ICoordinate other)
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
        /// Compares this object with the specified object for order.
        /// Since Coordinates are 2.5D, this routine ignores the z value when making the comparison.
        /// Returns
        ///   -1  : this.x lowerthan other.x || ((this.x == other.x) AND (this.y lowerthan other.y))
        ///    0  : this.x == other.x AND this.y = other.y
        ///    1  : this.x greaterthan other.x || ((this.x == other.x) AND (this.y greaterthan other.y))
        /// </summary>
        /// <param name="o"><c>Coordinate</c> with which this <c>Coordinate</c> is being compared.</param>
        /// <returns>
        /// A negative integer, zero, or a positive integer as this <c>Coordinate</c>
        ///         is less than, equal to, or greater than the specified <c>Coordinate</c>.
        /// </returns>
        int IComparable.CompareTo(object o)
        {
            if (o is ICoordinate other)
                return ((IComparable<ICoordinate>) this).CompareTo(other);
            return 1;
        }

        /// <summary>
        /// Returns <c>true</c> if <c>other</c> has the same values for x, y and z.
        /// </summary>
        /// <param name="other"><c>Coordinate</c> with which to do the 3D comparison.</param>
        /// <returns><c>true</c> if <c>other</c> is a <c>Coordinate</c> with the same values for x, y and z.</returns>
        [Obsolete]
        bool ICoordinate.Equals3D(ICoordinate other)
        {
            return (X == other.X) && (Y == other.Y) &&
                ((Z == other.Z) || (Double.IsNaN(Z) && Double.IsNaN(other.Z)));
        }

        /// <summary>
        /// Computes the 2-dimensional Euclidean distance to another location.
        /// The Z-ordinate is ignored.
        /// </summary>
        /// <param name="p"><c>Coordinate</c> with which to do the distance comparison.</param>
        /// <returns>the 2-dimensional Euclidean distance between the locations</returns>
        [Obsolete]
        double ICoordinate.Distance(ICoordinate p)
        {
            var dx = X - p.X;
            var dy = Y - p.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        #endregion ICoordinate

    }
}