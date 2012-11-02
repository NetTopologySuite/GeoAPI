// Portions copyright 2005 - 2007: Diego Guidi
// Portions copyright 2006 - 2008: Rory Plaire (codekaizen@gmail.com)
//
// This file is part of GeoAPI.Net.
// GeoAPI.Net is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// GeoAPI.Net is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with GeoAPI.Net; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System;
using System.Collections;
using GeoAPI.DataStructures;
using GeoAPI.Geometries;

namespace GeoAPI.Coordinates
{
    public interface ICoordinateSequence : IList, ICloneable
    {
        /// <summary>
        /// Returns the dimension (number of ordinates in each coordinate) 
        /// for this sequence.
        /// </summary>
        CoordinateDimensions Dimension { get; }

        /// <summary>
        /// Gets or sets the ordinate of a coordinate in this sequence.      
        /// </summary>
        /// <param name="index">The coordinate index in the sequence.</param>
        /// <param name="ordinate">
        /// The ordinate index in the coordinate.
        /// </param>
        /// <returns></returns>       
        Double this[Int32 index, Ordinates ordinate] { get; set; }

        new ICoordinate this[Int32 index] { get; set; }

        /// <summary>
        /// Creates an array of all the <see name="ICoordinate"/>
        /// instances in this <see cref="ICoordinateSequence{TCoordinate}"/>.
        /// </summary>
        /// <remarks>
        /// Returns (possibly copies of) the Coordinates in this collection.
        /// Whether or not the Coordinates returned are the actual underlying
        /// Coordinates or merely copies depends on the implementation.
        /// <para>
        /// Note that if the implementation does not store its data as an array of 
        /// <see name="ICoordinate"/> instances,
        /// this method may incur a significant performance penalty 
        /// because the array needs to be built from scratch. This may or may
        /// not be an issue - measure to determine if it affects your situation.
        /// </para>
        /// </remarks>
        /// <returns>An array of <see name="ICoordinate"/> instances.</returns>
        ICoordinate[] ToArray();

        /// <summary>
        /// Expands the given <see cref="IExtents"/> to include the coordinates 
        /// in the sequence.
        /// </summary>
        /// <remarks>
        /// Allows implementing classes to optimize access to coordinate values.
        /// </remarks>
        /// <param name="extents">The <see cref="IExtents"/> instance to expand.</param>
        /// <returns>
        /// An envelope which minimally encompasses all the coordinates in this 
        /// <see cref="ICoordinateSequence{TCoordinate}"/>.
        /// </returns>       
        IExtents ExpandExtents(IExtents extents);

        ICoordinateSequence Merge(ICoordinateSequence other);

        new ICoordinateSequence Clone();

        Boolean Equals(ICoordinateSequence other, Tolerance tolerance);
        IExtents GetExtents(IGeometryFactory geometryFactory);

        ICoordinateSequence Freeze();
        ICoordinate First { get; }
        ICoordinate Last { get; }
        Boolean IsFrozen { get; }
        Int32 LastIndex { get; }
        ICoordinate Maximum { get; }
        ICoordinate Minimum { get; }
        ICoordinateSequence Reverse();
        ICoordinateSequence Reversed { get; }
        ICoordinateSequenceFactory CoordinateSequenceFactory { get; }

        Pair<ICoordinate> SegmentAt(Int32 index);
        event EventHandler SequenceChanged;
    }
}
