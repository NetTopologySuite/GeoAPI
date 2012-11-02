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
using System.Collections.Generic;
#if !DOTNET40
using GeoAPI.DataStructures.Collections.Generic;
#endif
using GeoAPI.Geometries;
using NPack.Interfaces;
using GeoAPI.DataStructures;

namespace GeoAPI.Coordinates
{
    /// <summary>
    /// A representation of an ordered sequence of coordinates, such as would represent the 
    /// consecutive vertexes of an <see cref="IGeometry{TCoordinate}"/> instance.
    /// </summary>
    /// <remarks>
    /// <para>
    /// An <see cref="ICoordinateSequence{TCoordinate}"/> allows for various methods of 
    /// storing vertex data. Since a given set of <see cref="IGeometry{TCoordinate}"/> 
    /// instances will likely have many vertexes, it is possible that storage of this 
    /// data can become a challenge, and varying storage strategies can be employed to 
    /// handle this data.
    /// </para>
    /// You can implement <see cref="ICoordinateSequence{TCoordinate}"/> and
    /// <see cref="ICoordinateSequenceFactory{TCoordinate}"/> interfaces to take control 
    /// of how vertex data is created and stored. 
    /// You would then create an <see cref="IGeometryFactory{TCoordinate}"/> parameterized 
    /// by your <see cref="ICoordinateSequenceFactory{TCoordinate}"/>, and use
    /// this IGeometryFactory instance to create new <see cref="IGeometry{TCoordinate}"/> 
    /// instances. All of these new <see cref="IGeometry{TCoordinate}"/> instances 
    /// will use your <see cref="ICoordinateSequence{TCoordinate}"/> implementation.
    /// </para>
    /// </remarks>
    public interface ICoordinateSequence<TCoordinate> : IList<TCoordinate>,
                                                        IComparable<ICoordinateSequence<TCoordinate>>,
                                                        IEquatable<ICoordinateSequence<TCoordinate>>,
                                                        ICoordinateSequence
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>,
                            IComparable<TCoordinate>, IConvertible,
                            IComputable<Double, TCoordinate> 
    {
        /// <summary>
        /// Adds a range of coordinates to the sequence.
        /// </summary>
        /// <param name="coordinates">The coordinates to add.</param>
        /// <param name="allowRepeated">
        /// Value to determine whether repeated coordinates should be added (<see langword="true"/>)
        /// or skipped (<see langword="false"/>).
        /// </param>
        /// <param name="reverse">
        /// Value to determine whether coordinates are added in the reverse order of which they are 
        /// specified in <paramref name="coordinates"/>.
        /// </param>
        /// <returns>
        /// The same instance of the <see cref="ICoordinateSequence{TCoordinate}"/>, with the 
        /// <paramref name="coordinates"/> added.
        /// </returns>
        ICoordinateSequence<TCoordinate> AddRange(IEnumerable<TCoordinate> coordinates,
                                                  Boolean allowRepeated,
                                                  Boolean reverse);

        /// <summary>
        /// Adds a range of coordinates to the sequence.
        /// </summary>
        /// <param name="coordinates">The coordinates to add.</param>
        /// <returns>
        /// The same instance of the <see cref="ICoordinateSequence{TCoordinate}"/>, with the 
        /// <paramref name="coordinates"/> added.
        /// </returns>
        ICoordinateSequence<TCoordinate> AddRange(IEnumerable<TCoordinate> coordinates);
        ICoordinateSequence<TCoordinate> AddSequence(ICoordinateSequence<TCoordinate> sequence);
        ICoordinateSequence<TCoordinate> Append(TCoordinate coordinate);
        ICoordinateSequence<TCoordinate> Append(IEnumerable<TCoordinate> coordinates);
        ICoordinateSequence<TCoordinate> Append(ICoordinateSequence<TCoordinate> coordinates);
#if !DOTNET40
        ISet<TCoordinate> AsSet();
#endif
        new ICoordinateSequence<TCoordinate> Clear();
        ICoordinateSequence<TCoordinate> CloseRing();
        new ICoordinateSequenceFactory<TCoordinate> CoordinateSequenceFactory { get; }
        ICoordinateFactory<TCoordinate> CoordinateFactory { get; }
        new Int32 Count { get; }
        new ICoordinateSequence<TCoordinate> Clone();

        Boolean Equals(ICoordinateSequence<TCoordinate> other, Tolerance tolerance);

        /// <summary>
        /// Expands the given Envelope to include the coordinates in the sequence.
        /// </summary>
        /// <remarks>
        /// Allows implementing classes to optimize access to coordinate values.
        /// </remarks>
        /// <param name="extents">
        /// The <see cref="IExtents{TCoordinate}"/> instance to expand.
        /// </param>
        /// <returns>
        /// An envelope which minimally encompasses all the coordinates in this 
        /// <see cref="ICoordinateSequence{TCoordinate}"/>.
        /// </returns>
        IExtents<TCoordinate> ExpandExtents(IExtents<TCoordinate> extents);
        IExtents<TCoordinate> GetExtents(IGeometryFactory<TCoordinate> geometryFactory);

        new TCoordinate First { get; }
        new ICoordinateSequence<TCoordinate> Freeze();

        Boolean HasRepeatedCoordinates { get; }

        /// <summary>
        /// Determines which orientation of the 
        /// <see cref="ICoordinateSequence{TCoordinate}" /> sequence is (overall) increasing.
        /// In other words, determines which end of the array is "smaller"
        /// (using the standard ordering on <typeparamref name="TCoordinate" />).
        /// Returns an integer indicating the increasing direction.
        /// If the sequence is a palindrome, it is defined to be
        /// oriented in a positive direction.
        /// </summary>
        /// <returns>
        /// <value>1</value> if the sequence is smaller at the start or is a palindrome,
        /// <value>-1</value> if smaller at the end.
        /// </returns>
        Int32 IncreasingDirection { get; }
        new ICoordinateSequence<TCoordinate> Insert(Int32 index, TCoordinate item);
        new Boolean IsReadOnly { get; }
        new TCoordinate this[Int32 index] { get; set; }
        new TCoordinate Last { get; }
        new TCoordinate Maximum { get; }
        ICoordinateSequence<TCoordinate> Merge(ICoordinateSequence<TCoordinate> other);
        new TCoordinate Minimum { get; }
        ICoordinateSequence<TCoordinate> Prepend(TCoordinate coordinate);
        ICoordinateSequence<TCoordinate> Prepend(IEnumerable<TCoordinate> coordinates);
        ICoordinateSequence<TCoordinate> Prepend(ICoordinateSequence<TCoordinate> coordinates);
        new ICoordinateSequence<TCoordinate> RemoveAt(Int32 index);
        new ICoordinateSequence<TCoordinate> Reverse();
        new ICoordinateSequence<TCoordinate> Reversed { get; }
        ICoordinateSequence<TCoordinate> Scroll(TCoordinate coordinateToBecomeFirst);
        ICoordinateSequence<TCoordinate> Scroll(Int32 indexToBecomeFirst);
        ICoordinateSequence<TCoordinate> Slice(Int32 startIndex, Int32 endIndex);
        ICoordinateSequence<TCoordinate> Sort();
        ICoordinateSequence<TCoordinate> Sort(Int32 startIndex, Int32 endIndex);
        ICoordinateSequence<TCoordinate> Sort(Int32 startIndex, Int32 endIndex, IComparer<TCoordinate> comparer);


        ICoordinateSequence<TCoordinate> Splice(IEnumerable<TCoordinate> coordinates,
                                                Int32 startIndex,
                                                Int32 endIndex);

        ICoordinateSequence<TCoordinate> Splice(TCoordinate coordinate,
                                                Int32 startIndex,
                                                Int32 endIndex);

        ICoordinateSequence<TCoordinate> Splice(TCoordinate startCoordinate,
                                                Int32 startIndex,
                                                Int32 endIndex,
                                                TCoordinate endCoordinate);

        ICoordinateSequence<TCoordinate> Splice(IEnumerable<TCoordinate> startCoordinates,
                                                Int32 startIndex,
                                                Int32 endIndex,
                                                TCoordinate endCoordinate);

        ICoordinateSequence<TCoordinate> Splice(TCoordinate startCoordinate,
                                                Int32 startIndex,
                                                Int32 endIndex,
                                                IEnumerable<TCoordinate> endCoordinates);

        ICoordinateSequence<TCoordinate> Splice(IEnumerable<TCoordinate> startCoordinates,
                                                Int32 startIndex,
                                                Int32 endIndex,
                                                IEnumerable<TCoordinate> endCoordinates);

        ICoordinateSequence<TCoordinate> Splice(Int32 startIndex,
                                                Int32 endIndex,
                                                TCoordinate coordinate);

        ICoordinateSequence<TCoordinate> Splice(Int32 startIndex,
                                                Int32 endIndex,
                                                IEnumerable<TCoordinate> coordinates);
        /// <summary>
        /// Creates an array of all the <typeparamref name="TCoordinate"/>
        /// instances in this <see cref="ICoordinateSequence{TCoordinate}"/>.
        /// </summary>
        /// <remarks>
        /// Returns (possibly copies of) the Coordinates in this collection.
        /// Whether or not the Coordinates returned are the actual underlying
        /// Coordinates or merely copies depends on the implementation.
        /// <para>
        /// Note that if the implementation does not store its data as an array of 
        /// <typeparamref name="TCoordinate"/> instances,
        /// this method may incur a significant performance penalty 
        /// because the array needs to be built from scratch. This may or may
        /// not be an issue - measure to determine if it affects your situation.
        /// </para>
        /// </remarks>
        /// <returns>
        /// An array of <typeparamref name="TCoordinate"/> instances.
        /// </returns>
        new TCoordinate[] ToArray();

        ICoordinateSequence<TCoordinate> WithoutRepeatedPoints();
        ICoordinateSequence<TCoordinate> WithoutDuplicatePoints();

        new Pair<TCoordinate> SegmentAt(Int32 index);
    }
}