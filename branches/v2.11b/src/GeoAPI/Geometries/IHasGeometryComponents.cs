using System;
using System.Collections.Generic;
using GeoAPI.Coordinates;
using NPack.Interfaces;

namespace GeoAPI.Geometries
{
    public interface IHasGeometryComponents<TCoordinate>
         where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>, 
                             IComparable<TCoordinate>, IConvertible, 
                             IComputable<Double, TCoordinate>
    {
        /// <summary>
        /// Performs an operation with or on this Geometry and its
        /// component Geometry's. Only GeometryCollections and
        /// Polygons have component Geometry's; for Polygons they are the LinearRings
        /// of the shell and holes.
        /// </summary>
        IEnumerable<IGeometry<TCoordinate>> Components { get; }
    }
}
