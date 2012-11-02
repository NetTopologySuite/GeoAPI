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
using NPack.Interfaces;

namespace GeoAPI.Coordinates
{
    /// <summary>
    /// An object that knows how to build a particular implementation of
    /// <see cref="ICoordinateSequence"/> from an array of Coordinates.
    /// </summary>
    /// <seealso cref="ICoordinateSequence{TCoordinate}" />
    public interface ICoordinateSequenceFactory<TCoordinate> : ICoordinateSequenceFactory
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>, 
                            IComparable<TCoordinate>, IConvertible, 
                            IComputable<Double, TCoordinate>
    {
        ICoordinateFactory<TCoordinate> CoordinateFactory { get; }

        //ICoordinateSequence<TCoordinate> Create(Int32 initialCapacity);
        ICoordinateSequence<TCoordinate> Create(params TCoordinate[] coordinates);

        /// <summary>
        /// Returns a <see cref="ICoordinateSequence{TCoordinate}" /> based on the given array; 
        /// whether or not the array is copied is implementation-dependent.
        /// </summary>
        /// <param name="coordinates">
        /// An enumeration of coordinates, which may not be null nor contain null elements.
        /// </param>
        ICoordinateSequence<TCoordinate> Create(IEnumerable<TCoordinate> coordinates);
        ICoordinateSequence<TCoordinate> Create(Func<Double, Double> componentTransform, IEnumerable<TCoordinate> coordinates);
        ICoordinateSequence<TCoordinate> Create(IEnumerable<TCoordinate> coordinates, Boolean allowRepeated);
        ICoordinateSequence<TCoordinate> Create(Func<Double, Double> componentTransform, IEnumerable<TCoordinate> coordinates, Boolean allowRepeated);
        ICoordinateSequence<TCoordinate> Create(IEnumerable<TCoordinate> coordinates, Boolean allowRepeated, Boolean direction);
        ICoordinateSequence<TCoordinate> Create(Func<Double, Double> componentTransform, IEnumerable<TCoordinate> coordinates, Boolean allowRepeated, Boolean direction);

        /// <summary>
        /// Creates a <see cref="ICoordinateSequence{TCoordinate}" /> which is a copy
        /// of the given <see cref="ICoordinateSequence{TCoordinate}" />.
        /// This method must handle null arguments by creating an empty sequence.
        /// </summary>
        ICoordinateSequence<TCoordinate> Create(ICoordinateSequence<TCoordinate> coordSeq);

        new ICoordinateSequence<TCoordinate> Create(ICoordinateSequence coordSeq);

        /// <summary>
        /// Creates a <see cref="ICoordinateSequence{TCoordinate}" /> of the specified size and dimension.
        /// For this to be useful, the <see cref="ICoordinateSequence{TCoordinate}" /> implementation must be mutable.
        /// </summary>
        /// <param name="dimension">the dimension of the coordinates in the sequence 
        /// (if user-specifiable, otherwise ignored)</param>
        new ICoordinateSequence<TCoordinate> Create(Int32 size, CoordinateDimensions dimension);

        //new ICoordinateSequence<TCoordinate> Create();

        new ICoordinateSequence<TCoordinate> Create(CoordinateDimensions dimension);
        new IPrecisionModel<TCoordinate> PrecisionModel { get; }
    }
}
