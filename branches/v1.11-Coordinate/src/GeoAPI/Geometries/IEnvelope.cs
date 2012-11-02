using System;

namespace GeoAPI.Geometries
{
    /// <summary>
    /// Defines a rectangular region of the 2D coordinate plane.
    /// </summary>
    /// <remarks>
    /// <para>
    /// It is often used to represent the bounding box of a <c>Geometry</c>,
    /// e.g. the minimum and maximum x and y values of the <c>Coordinate</c>s.
    /// </para>
    /// <para>
    /// Note that Envelopes support infinite or half-infinite regions, by using the values of
    /// <c>Double.PositiveInfinity</c> and <c>Double.NegativeInfinity</c>.
    /// </para>
    /// <para>
    /// When Envelope objects are created or initialized,
    /// the supplies extent values are automatically sorted into the correct order.    
    /// </para>
    /// </remarks>
    public interface IEnvelope : ICloneable, IComparable, IComparable<IEnvelope>, IEquatable<IEnvelope>
    {
        /// <summary>
        /// Gets the area of the envelope
        /// </summary>
        double Area { get; }

        /// <summary>
        /// Gets the width of the envelope
        /// </summary>
        double Width { get; }

        /// <summary>
        /// Gets the height of the envelope
        /// </summary>
        double Height { get; }

        /// <summary>
        /// Gets the maximum x-ordinate of the envelope
        /// </summary>
        double MaxX { get; }

        /// <summary>
        /// Gets the maximum y-ordinate of the envelope
        /// </summary>
        double MaxY { get; }

        /// <summary>
        /// Gets the minimum x-ordinate of the envelope
        /// </summary>
        double MinX { get; }

        /// <summary>
        /// Gets the mimimum y-ordinate of the envelope
        /// </summary>
        double MinY { get; }

        /// <summary>
        /// Gets the <see cref="Coordinate"/> or the center of the envelope
        /// </summary>
        Coordinate Centre { get; }
        
        /// <summary>
        /// Returns if the point specified by <see paramref="x"/> and <see paramref="y"/> is contained by the envelope.
        /// </summary>
        /// <param name="x">The x-ordinate</param>
        /// <param name="y">The y-ordinate</param>
        /// <returns>True if the point is contained by the envlope</returns>
        bool Contains(double x, double y);

        /// <summary>
        /// Returns if the point specified by <see paramref="p"/> is contained by the envelope.
        /// </summary>
        /// <param name="p">The point</param>
        /// <returns>True if the point is contained by the envlope</returns>
        bool Contains(Coordinate p);
        
        bool Contains(IEnvelope other);

        bool Covers(double x, double y);

        bool Covers(Coordinate p);

        bool Covers(IEnvelope other);

        double Distance(IEnvelope env);
        
        void ExpandBy(double distance);
        
        void ExpandBy(double deltaX, double deltaY);
        
        void ExpandToInclude(Coordinate p);
        
        void ExpandToInclude(double x, double y);
        
        void ExpandToInclude(IEnvelope other);

        void Init();

        void Init(Coordinate p);

        void Init(IEnvelope env);

        void Init(Coordinate p1, Coordinate p2);
        
        void Init(double x1, double x2, double y1, double y2);

        IEnvelope Intersection(IEnvelope env);

        void Translate(double transX, double transY);

        IEnvelope Union(IPoint point);
        
        IEnvelope Union(Coordinate coord);
        
        IEnvelope Union(IEnvelope box);        

        bool Intersects(Coordinate p);
        
        bool Intersects(double x, double y);
        
        bool Intersects(IEnvelope other);
        
        bool IsNull { get; }

        void SetToNull();

        void Zoom(double perCent);
                
        bool Overlaps(IEnvelope other);

        bool Overlaps(Coordinate p);
        
        bool Overlaps(double x, double y);
        
        void SetCentre(double width, double height);
        
        void SetCentre(IPoint centre, double width, double height);
        
        void SetCentre(Coordinate centre);
        
        void SetCentre(IPoint centre);
        
        void SetCentre(Coordinate centre, double width, double height);                
    }
}
