using System;
using GeoAPI.Operations.Buffer;

namespace GeoAPI.Geometries
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGeometry : ICloneable, IComparable, IComparable<IGeometry>, IEquatable<IGeometry>
    {
        IGeometryFactory Factory { get; }
        IPrecisionModel PrecisionModel { get; }

        /// <summary>
        /// The spatial reference id
        /// </summary>
        int SRID { get; set; }

        /// <summary>
        /// The geometry type
        /// </summary>
        string GeometryType { get; } 

        /// <summary>
        /// A ISurface method moved in IGeometry 
        /// </summary>
        double Area { get; }

        /// <summary>
        /// A ICurve method moved in IGeometry
        /// </summary>
        double Length { get; }        

        /// <summary>
        /// A IGeometryCollection method moved in IGeometry
        /// </summary>
        int NumGeometries { get; }

        /// <summary>
        /// A ILineString method moved to IGeometry
        /// </summary>
        int NumPoints { get; }        

        IGeometry Boundary { get; }

        Dimensions BoundaryDimension { get; }

        /// <summary>
        /// A ISurface method moved in IGeometry 
        /// </summary>
        IPoint Centroid { get; }                        
        
        ICoordinate Coordinate { get; }
        
        ICoordinate[] Coordinates { get; }       
                        
        Dimensions Dimension { get; set; }
                
        IGeometry Envelope { get; }

        IEnvelope EnvelopeInternal { get; }

        IPoint InteriorPoint { get; }

        /// <summary>
        /// A ISurface method moved in IGeometry 
        /// </summary>        
        IPoint PointOnSurface { get; }        

        /// <summary>
        /// A IGeometryCollection method moved in IGeometry
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        IGeometry GetGeometryN(int n);   
                               
        void Normalize();

        byte[] AsBinary();
        
        string AsText();
        
        object UserData { get; set; }

        IGeometry ConvexHull();

        IntersectionMatrix Relate(IGeometry g);

        IGeometry Difference(IGeometry other);

        IGeometry SymmetricDifference(IGeometry other);

        IGeometry Buffer(double distance);

        IGeometry Buffer(double distance, int quadrantSegments);

        IGeometry Buffer(double distance, BufferStyle endCapStyle);

        IGeometry Buffer(double distance, int quadrantSegments, BufferStyle endCapStyle);

        IGeometry Intersection(IGeometry other);

        IGeometry Union(IGeometry other);

        bool EqualsExact(IGeometry other);

        bool EqualsExact(IGeometry other, double tolerance);

        bool IsEmpty { get; }

        bool IsRectangle { get; }

        bool IsSimple { get; }

        bool IsValid { get; }

        bool Within(IGeometry g);

        bool Contains(IGeometry g);

        bool IsWithinDistance(IGeometry geom, double distance);

        bool CoveredBy(IGeometry g);

        bool Covers(IGeometry g);

        bool Crosses(IGeometry g);

        bool Intersects(IGeometry g);

        bool Overlaps(IGeometry g);

        bool Relate(IGeometry g, string intersectionPattern);

        bool Touches(IGeometry g);

        bool Disjoint(IGeometry g);

        /// <summary>  
        /// Returns the minimum distance between this <c>Geometry</c>
        /// and the <c>Geometry</c> g.
        /// </summary>
        /// <param name="g">The <c>Geometry</c> from which to compute the distance.</param>
        double Distance(IGeometry g);

        /// <summary>
        /// Performs an operation with or on this <c>Geometry</c>'s
        /// coordinates. If you are using this method to modify the point, be sure
        /// to call <see cref="GeometryChanged()"/> afterwards.
        /// Note that you cannot use this  method to modify this Geometry 
        /// if its underlying <see cref="ICoordinateSequence"/>'s Get method
        /// returns a copy of the <see cref="ICoordinate"/>, rather than the actual
        /// Coordinate stored (if it even stores Coordinates at all).
        /// </summary>
        /// <param name="filter">The filter to apply to this <c>Geometry</c>'s coordinates</param>
        void Apply(ICoordinateFilter filter);

        ///<summary>
        /// Performs an operation on the coordinates in this <c>Geometry</c>'s <see cref="ICoordinateSequence"/>s. 
        /// If this method modifies any coordinate values, <see cref="GeometryChanged()"/> must be called to update the geometry state.
        ///</summary>
        /// <param name="filter">The filter to apply</param>
        void Apply(ICoordinateSequenceFilter filter);

        /// <summary>
        /// Performs an operation with or on this <c>Geometry</c> and its
        /// subelement <c>Geometry</c>s (if any).
        /// Only GeometryCollections and subclasses
        /// have subelement Geometry's.
        /// </summary>
        /// <param name="filter">
        /// The filter to apply to this <c>Geometry</c> (and
        /// its children, if it is a <c>GeometryCollection</c>).
        /// </param>
        void Apply(IGeometryFilter filter);

        /// <summary>
        /// Performs an operation with or on this Geometry and its
        /// component Geometry's. Only GeometryCollections and
        /// Polygons have component Geometry's; for Polygons they are the LinearRings
        /// of the shell and holes.
        /// </summary>
        /// <param name="filter">The filter to apply to this <c>Geometry</c>.</param>
        void Apply(IGeometryComponentFilter filter);

        /// <summary>
        /// Notifies this Geometry that its Coordinates have been changed by an external
        /// party (using a CoordinateFilter, for example). The Geometry will flush
        /// and/or update any information it has cached (such as its Envelope).
        /// </summary>
        void GeometryChanged();

        /// <summary>
        /// Notifies this Geometry that its Coordinates have been changed by an external
        /// party. When #geometryChanged is called, this method will be called for
        /// this Geometry and its component Geometries.
        /// </summary>
        void GeometryChangedAction();
    }
}
