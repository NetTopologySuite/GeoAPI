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

namespace GeoAPI.Coordinates
{
    ///<summary>
    ///</summary>
    public interface ICoordinateSequenceFactory
    {
        /// <summary>
        /// Returns a <see cref="ICoordinateSequence" /> 
        /// based on the given array; whether or not the array is copied is 
        /// implementation-dependent.
        /// </summary>
        /// <param name="coordinates">
        /// A coordinates array, which may not be null nor contain null elements.
        /// </param>
        ICoordinateSequence Create(IEnumerable<ICoordinate> coordinates);

        /// <summary>
        /// Creates a <see cref="ICoordinateSequence" /> which is a copy
        /// of the given <see cref="ICoordinateSequence" />.
        /// This method must handle null arguments by creating an empty sequence.
        /// </summary>
        ICoordinateSequence Create(ICoordinateSequence coordSeq);

        /// <summary>
        /// Creates a <see cref="ICoordinateSequence" /> of the 
        /// specified size and dimension. For this to be useful, the 
        /// <see cref="ICoordinateSequence" /> implementation must be mutable.
        /// </summary>
        /// <param name="size">
        /// The initial capacity of the <see cref="ICoordinateSequence"/>.
        /// </param>
        /// <param name="dimension">
        /// The dimension of the coordinates in the sequence 
        /// (if the implementation allows it to be user-specifiable, otherwise ignored).
        /// </param>
        ICoordinateSequence Create(Int32 size, CoordinateDimensions dimension);

        ///<summary>
				///Creates a <see cref="ICoordinateSequence" /> of the 
        ///specified size and dimension. For this to be useful, the 
        ///<see cref="ICoordinateSequence" /> implementation must be mutable.
        ///</summary>
        ///<param name="dimension">
        /// The dimension of the coordinates in the sequence 
        /// (if the implemntation allows it to be user-specifiable, otherwise ignored)</param>
        ///<returns></returns>
        ICoordinateSequence Create(CoordinateDimensions dimension);

        ///<summary>
        /// Gets the <see cref="IPrecisionModel"/> of the coordinate sequence.
        ///</summary>
        IPrecisionModel PrecisionModel { get; }
    }
}
