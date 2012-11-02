namespace GeoAPI.Operations.Buffer
{
    /// <summary>
    /// An interface for classes that control the parameters for the buffer building process
    /// </summary>
    public interface IBufferParameters
    {
        ///<summary>
        /// Gets/Sets the number of quadrant segments which will be used
        ///</summary>
        /// <remarks>
        /// QuadrantSegments is the number of line segments used to approximate an angle fillet.
        /// <list type="Table">
        /// <item>qs &gt;>= 1</item><description>joins are round, and qs indicates the number of segments to use to approximate a quarter-circle.</description>
        /// <item>qs = 0</item><description>joins are beveled</description>
        /// <item>qs &lt; 0</item><description>joins are mitred, and the value of qs indicates the mitre ration limit as <c>mitreLimit = |qs|</c></description>
        /// </list>
        /// </remarks>
        int QuadrantSegments { get; set; }

        ///<summary>
        /// Gets/Sets the end cap style of the generated buffer.
        ///</summary>
        /// <remarks>
        /// <para>
        /// The styles supported are <see cref="EndCapStyle.Round"/>, <see cref="EndCapStyle.Flat"/>, and <see cref="EndCapStyle.Square"/>.
        /// </para>
        /// <para>The default is <see cref="EndCapStyle.Round"/>.</para>
        /// </remarks>
        EndCapStyle EndCapStyle { get; set; }

        ///<summary>
        /// Gets/Sets the join style for outside (reflex) corners between line segments.
        ///</summary>
        /// <remarks>
        /// <para>Allowable values are <see cref="JoinStyle.Round"/> (which is the default), <see cref="JoinStyle.Mitre"/> and <see cref="JoinStyle.Bevel"/></para>
        /// </remarks>
        JoinStyle JoinStyle { get; set; }

        ///<summary>
        /// Sets the limit on the mitre ratio used for very sharp corners.
        ///</summary>
        /// <remarks>
        /// <para>
        /// The mitre ratio is the ratio of the distance from the corner
        /// to the end of the mitred offset corner.
        /// When two line segments meet at a sharp angle, 
        /// a miter join will extend far beyond the original geometry.
        /// (and in the extreme case will be infinitely far.)
        /// To prevent unreasonable geometry, the mitre limit 
        /// allows controlling the maximum length of the join corner.
        /// Corners with a ratio which exceed the limit will be beveled.
        /// </para>
        /// </remarks>
        double MitreLimit { get; set; }
    }
}