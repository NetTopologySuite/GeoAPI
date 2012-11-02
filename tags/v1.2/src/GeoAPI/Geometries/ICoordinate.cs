using System;

namespace GeoAPI.Geometries
{    
    public interface ICoordinate : ICloneable, IComparable, IComparable<ICoordinate>, IEquatable<ICoordinate>
    {
        /// <summary>
        /// The x-ordinate value
        /// </summary>
        double X { get; set; }

        /// <summary>
        /// The y-ordinate value
        /// </summary>
        double Y { get; set; }
        
        /// <summary>
        /// The z-ordinate value
        /// </summary>
        double Z { get; set; }

        /// <summary>
        /// The measure value
        /// </summary>
        double M { get; set; }

        /// <summary>
        /// Gets/Sets all ordinate values
        /// </summary>
        ICoordinate CoordinateValue { get; set; }

        double this[Ordinates index] { get; set; }

        /// <summary>
        /// Computes the 2-dimensional distance to the <see cref="other"/> coordiante.
        /// </summary>
        /// <param name="other">The other coordinate</param>
        /// <returns>The 2-dimensional distance to other</returns>
        double Distance(ICoordinate other);
        
        bool Equals2D(ICoordinate other);
        
        bool Equals3D(ICoordinate other);        
    }
}
