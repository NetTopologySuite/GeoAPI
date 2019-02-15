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
    /// Implementations may optionally support Z-ordinate and M-measure values
    /// as appropriate for a <see cref="ICoordinateSequence"/>. Use of <see cref="Z"/>
    /// and <see cref="M"/> setters or <see cref="P:GeoAPI.Geometries.Coordinate.this[Ordinate]" /> indexer are recommended.
    /// </remarks>
#if HAS_SYSTEM_SERIALIZABLEATTRIBUTE
    [Serializable]
#endif
#pragma warning disable 612,618
    public class Coordinate : IComparable, IComparable<Coordinate>, ICloneable
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
        /// <remarks>The Z-ordinate is not supported</remarks>
        /// <exception cref="InvalidOperationException">
        /// Thrown if an attempt is made to <b>set</b> the Z-ordinate value
        /// </exception>
        public virtual double Z
        {
            get => NullOrdinate; 
            set { throw new InvalidOperationException($"{GetType().Name} does not support setting Z-ordinate");}
        }

        /// <summary>
        /// Gets or sets the default m-measure (<see cref="NullOrdinate"/>) if supported.
        /// </summary>
        /// <remarks>The M-measure is not supported</remarks>
        /// <exception cref="InvalidOperationException">
        /// Thrown if an attempt is made to <b>set</b> the M-Measure value
        /// </exception>
        public virtual double M
        {
            get => NullOrdinate;
            set { throw new InvalidOperationException($"{GetType().Name} does not support setting M-measure"); }
        }

        /// <summary>
        /// Constructs a <c>Coordinate</c> at (x,y).
        /// </summary>
        /// <param name="x">The X value</param>
        /// <param name="y">The Y value</param>
        public Coordinate(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        ///  Constructs a <c>Coordinate</c> at (0,0).
        /// </summary>
        public Coordinate() : this(0.0, 0.0) { }

        /// <summary>
        /// Constructs a <c>Coordinate</c> having the same (x,y,z) values as
        /// <paramref name="c"/>.
        /// </summary>
        /// <param name="c"><c>Coordinate</c> to copy.</param>
        public Coordinate(Coordinate c) : this(c.X, c.Y) { }


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
        public virtual Coordinate CoordinateValue
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

        protected static bool EqualsWithTolerance(double v1, double v2, double tolerance)
        {
            return Math.Abs(v1 - v2) <= tolerance;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Coordinate other)
        {
            return Equals2D(other);
        }

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
            //Like in JTS
            //return CompareTo((Coordinate) o);

            if (ReferenceEquals(o, null))
                throw new ArgumentNullException(nameof(o));
            if (!(o is Coordinate oc))
                throw new ArgumentException($"Invalid type: '{o.GetType()}'", nameof(o));

            return CompareTo(oc);
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
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (X < other.X)
                return -1;
            if (X > other.X)
                return 1;
            if (Y < other.Y)
                return -1;
            return Y > other.Y ? 1 : 0;
        }

        /// <summary>
        /// Create a new object as copy of this instance.
        /// </summary>
        /// <returns></returns>
        public /*virtual*/ Coordinate Copy()
        {
            return (Coordinate)MemberwiseClone();
        }

        object ICloneable.Clone()
        {
            return Copy();
        }

        /// <summary>
        /// Computes the 2-dimensional Euclidean distance to another location.
        /// </summary>
        /// <param name="c">A <see cref="Coordinate"/> with which to do the distance comparison.</param>
        /// <returns>the 2-dimensional Euclidean distance between the locations.</returns>
        /// <remarks>The Z-ordinate is ignored.</remarks>
        public double Distance(Coordinate c)
        {
            double dx = X - c.X;
            double dy = Y - c.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

#region System.Object overrides
        /// <summary>
        /// Returns <c>true</c> if <c>other</c> has the same values for the x and y ordinates.
        /// Since Coordinates are 2.5D, this routine ignores the z value when making the comparison.
        /// </summary>
        /// <param name="o"><c>Coordinate</c> with which to do the comparison.</param>
        /// <returns><c>true</c> if <c>other</c> is a <c>Coordinate</c> with the same values for the x and y ordinates.</returns>
        public sealed override bool Equals(object o)
        {
            if (o is Coordinate other)
                return Equals(other);
            return false;
        }

        /// <summary>
        /// Gets a hashcode for this coordinate.
        /// </summary>
        /// <returns>A hashcode for this coordinate.</returns>
        public sealed override int GetHashCode()
        {
            var result = 17;
            // ReSharper disable NonReadonlyFieldInGetHashCode
            result = 37 * result + X.GetHashCode();
            result = 37 * result + Y.GetHashCode();
            // ReSharper restore NonReadonlyFieldInGetHashCode
            return result;
        }

        /// <summary>
        /// Returns a <c>string</c> of the form <I>(x,y,z)</I> .
        /// </summary>
        /// <returns><c>string</c> of the form <I>(x,y,z)</I></returns>
        public override string ToString()
        {
            return string.Format(NumberFormatInfo.InvariantInfo, "({0:R}, {1:R})", X, Y);
        }
#endregion
    }
}