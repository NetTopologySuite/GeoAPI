/*
 * The JTS Topology Suite is a collection of Java classes that
 * implement the fundamental operations required to validate a given
 * geo-spatial data set to a known topological specification.
 *
 * Copyright (C) 2001 Vivid Solutions
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 2.1 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 *
 * For more information, contact:
 *
 *     Vivid Solutions
 *     Suite #1A
 *     2328 Government Street
 *     Victoria BC  V8T 5G5
 *     Canada
 *
 *     (250)385-6040
 *     www.vividsolutions.com
 */
using System;

namespace GeoAPI.Operations.Buffer
{
    ///<summary>
    /// Contains the parameters which describe how a buffer should be constructed.
    ///</summary>
    public class BufferParameters
    {
        ///<summary>
        /// Specifies the EndCap Style for the buffer operation
        ///</summary>
        public enum BufferEndCapStyle
        {
            ///<summary>Specifies a round line buffer end cap style.
            ///</summary>
            CapRound = 1,
            ///<summary>Specifies a flat line buffer end cap style.
            ///</summary>
            CapFlat = 2,
            ///<summary>Specifies a square line buffer end cap style.
            ///</summary>
            CapSquare = 3
        }

        ///<summary>
        /// Specifies the Join Style for the buffer operation
        ///</summary>
        public enum BufferJoinStyle
        {
            ///<summary>
            /// Specifies a round join style.
            ///</summary>
            JoinRound=1,
            ///<summary>
            /// Specifies a mitre join style.
            ///</summary>
            JoinMitre=2,
            ///<summary>
            /// Specifies a bevel join style.
            ///</summary>
            JoinBevel=3
        }

        ///<summary>
        /// The default number of facets into which to divide a fillet of 90 degrees.
        /// A value of 8 gives less than 2% max error in the buffer distance.
        /// For a max error of &lt; 1%, use QS = 12.
        /// For a max error of &lt; 0.1%, use QS = 18.
        ///</summary>
        public const int DefaultQuadrantSegments = 8;

        ///<summary>
        /// The default mitre limit Allows fairly pointy mitres.
        ///</summary>
        public const double DefaultMitreLimit = 5.0;

        private Int32 _quadrantSegments = DefaultQuadrantSegments;
        private BufferEndCapStyle _endCapStyle = BufferEndCapStyle.CapRound;
        private BufferJoinStyle _joinStyle = BufferJoinStyle.JoinRound;
        private Double _mitreLimit = DefaultMitreLimit;
        private Boolean _isSingleSided;

        ///<summary>
        /// Creates a set of parameters with the given quadrantSegments value.
        ///</summary>
        ///<param name="quadrantSegments">the number of quadrant segments to use</param>
        public BufferParameters(Int32 quadrantSegments) 
        {
            QuadrantSegments = quadrantSegments;
        }

        ///<summary>
        /// Creates a set of parameters with the given quadrantSegments and endCapStyle values.
        ///</summary>
        ///<param name="quadrantSegments">the number of quadrant segments to use</param>
        ///<param name="endCapStyle">the end cap style to use</param>
        public BufferParameters(Int32 quadrantSegments,
          BufferEndCapStyle endCapStyle) 
        {
            QuadrantSegments = quadrantSegments;
            EndCapStyle = endCapStyle;
        }

        ///<summary>
        /// Creates a set of parameters with the given parameter values.
        ///</summary>
        ///<param name="quadrantSegments">the number of quadrant segments to use</param>
        ///<param name="endCapStyle">the end cap style to use</param>
        ///<param name="joinStyle">the join style to use</param>
        ///<param name="mitreLimit">the mitre limit to use</param>
        public BufferParameters(
            Int32 quadrantSegments,
            BufferEndCapStyle endCapStyle,
            BufferJoinStyle joinStyle,
            Double mitreLimit)
        {
            QuadrantSegments = quadrantSegments;
            EndCapStyle = endCapStyle;
            JoinStyle = joinStyle;
            MitreLimit = mitreLimit;
        }

        ///<summary>
        /// Gets/Sets the number of quadrant segments which will be used
        /// <list type="Bullet">
   /* <ul>
   * <li>If <tt>quadSegs</tt> >= 1, joins are round, and <tt>quadSegs</tt> indicates the number of 
   * segments to use to approximate a quarter-circle.
   * <li>If <tt>quadSegs</tt> = 0, joins are bevelled (flat)
   * <li>If <tt>quadSegs</tt> < 0, joins are mitred, and the value of qs
   * indicates the mitre ration limit as
   * <pre>
   * mitreLimit = |<tt>quadSegs</tt>|
   * </pre>
   * </ul>
   * For round joins, <tt>quadSegs</tt> determines the maximum
   * error in the approximation to the true buffer curve.
   * The default value of 8 gives less than 2% max error in the buffer distance.
   * For a max error of < 1%, use QS = 12.
   * For a max error of < 0.1%, use QS = 18.
   * The error is always less than the buffer distance 
   * (in other words, the computed buffer curve is always inside the true
   * curve).
    */    /// <item></item>
        /// </list>
        ///</summary>
        public Int32 QuadrantSegments
        {
            get { return _quadrantSegments; }
            set
            {
                _quadrantSegments = value;
                /** 
                 * Indicates how to construct fillets.
                 * If qs >= 1, fillet is round, and qs indicates number of 
                 * segments to use to approximate a quarter-circle.
                 * If qs = 0, fillet is bevelled flat (i.e. no filleting is performed)
                 * If qs < 0, fillet is mitred, and absolute value of qs
                 * indicates maximum length of mitre according to
                 * 
                 * mitreLimit = |qs|
                 */
                if (_quadrantSegments == 0)
                    _joinStyle = BufferJoinStyle.JoinBevel;

                if (_quadrantSegments < 0)
                {
                    _joinStyle = BufferJoinStyle.JoinMitre;
                    _mitreLimit = Math.Abs(_quadrantSegments);
                }

                if (value <= 0)
                {
                    _quadrantSegments = 1;
                }
                /**
                 * If join style was set by the quadSegs value,
                 * use the default for the actual quadrantSegments value.
                 */
                if (_joinStyle != BufferJoinStyle.JoinRound)
                {
                    _quadrantSegments = DefaultQuadrantSegments;
                }

            }
        }

        ///<summary>
        /// Computes the maximum distance error due to a given level
        /// of approximation to a true arc.
        ///</summary>
        ///<param name="quadSegs">the number of segments used to approximate a quarter-circle</param>
        ///<returns>the error of approximation</returns>
        public static Double BufferDistanceError(Int32 quadSegs)
        {
            Double alpha = Math.PI / 2.0 / quadSegs;
            return 1 - Math.Cos(alpha / 2.0);
        }

        ///<summary>
        /// Gets/Sets the end cap style of the generated buffer.
        /// The styles supported are <see cref="BufferEndCapStyle.CapRound"/>, <see cref="BufferEndCapStyle.CapFlat"/>, <see cref="BufferEndCapStyle.CapSquare"/>.
        /// The default is <see cref="BufferEndCapStyle.CapRound"/>.
        ///</summary>
        public BufferEndCapStyle EndCapStyle
        {
            get { return _endCapStyle; }
            set { _endCapStyle = value; }
        }

        ///<summary>
        /// Gets/Sets the join style for outside (reflex) corners between line segments.
        /// Allowable values are <see cref="BufferJoinStyle.JoinRound"/> (which is default), <see cref="BufferJoinStyle.JoinMitre"/>, <see cref="BufferJoinStyle.JoinBevel"/>
        ///</summary>
        public BufferJoinStyle JoinStyle
        {
            get { return _joinStyle;}
            set { _joinStyle = value; }
        }
  
        ///<summary>
        /// Gets/Sets the limit on the mitre ratio used for very sharp corners.
        /// to the end of the mitred offset corner.
        /// When two line segments meet at a sharp angle, 
        /// a miter join will extend far beyond the original geometry.
        /// (and in the extreme case will be infinitely far.)
        /// To prevent unreasonable geometry, the mitre limit 
        /// allows controlling the maximum length of the join corner.
        /// Corners with a ratio which exceed the limit will be beveled.
        ///</summary>
        public Double MitreLimit
        {
            get { return _mitreLimit; }
            set { _mitreLimit = value; }
        }

        ///<value>
        /// Defines whether the buffer is to be generated on a single side only.
        ///</value>
        ///<remarks>
        /// A single-sided buffer is constructed on only one side of each input line.
        /// The side used is determined by the sign of the buffer distance:
        /// <list type="Bullet">
        /// <item>A positive distance indicates the right-hand side</item>
        /// <item>A negative distance indicates the left-hand side</item>
        /// </list>
        /// The single-sided buffer of point geometries is 
        /// the same as the regular buffer.
        ///</remarks>
        public Boolean IsSingleSided
        {
            get { return _isSingleSided; }
            set { _isSingleSided = value; }
       }
    }
}
