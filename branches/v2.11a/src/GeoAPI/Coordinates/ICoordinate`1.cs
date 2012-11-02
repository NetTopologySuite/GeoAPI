using System;
using NPack;
using NPack.Interfaces;

namespace GeoAPI.Coordinates
{
    /// <summary>
    /// Interface for coordinates
    /// </summary>
    /// <typeparam name="TCoordinate">The type of the coordinate</typeparam>
    public interface ICoordinate<TCoordinate> : ICoordinate,
                                                IVector<DoubleComponent, TCoordinate>
        where TCoordinate : ICoordinate, IVector<DoubleComponent, TCoordinate>
    {
        Double Distance(TCoordinate other);

        new TCoordinate Zero { get; }

        new Int32 ComponentCount { get; }

        new DoubleComponent this[Int32 index] { get; }

        new TCoordinate Clone();

        new TCoordinate Multiply(Double factor);

        /*
        new void GetComponents(out DoubleComponent a, out DoubleComponent b);
        new void GetComponents(out DoubleComponent a, out DoubleComponent b, out DoubleComponent c);
        new void GetComponents(out DoubleComponent a, out DoubleComponent b, out DoubleComponent c, out DoubleComponent d);
         */
    }
}