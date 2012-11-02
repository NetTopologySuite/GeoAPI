using System;
using GeoAPI.Coordinates;
using NPack.Interfaces;

namespace GeoAPI.Geometries.Prepared
{
    /// <summary>
    ///
    /// </summary>
    public interface IPreparedGeometry : ISimpleSpatialRelation
    {
        /// <summary>
        /// Gets the original <see cref="IGeometry"/> which has been prepared
        /// </summary>
        IGeometry Geometry { get; }
    }
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TCoordinate"></typeparam>
    public interface IPreparedGeometry<TCoordinate> : IPreparedGeometry, ISimpleSpatialRelation<TCoordinate>
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>, IComparable<TCoordinate>,
            IComputable<Double, TCoordinate>, IConvertible
    {
        /// <summary>
        /// Gets the original <see cref="IGeometry{TCoordinate}"/> which has been prepared
        /// </summary>
        new IGeometry<TCoordinate> Geometry { get; }
    }
}