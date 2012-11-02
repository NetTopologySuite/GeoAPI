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

#region Namespace Imports

using System;
using NPack;
using NPack.Interfaces;

#endregion Namespace Imports

namespace GeoAPI.Coordinates
{
    /// <summary>
    /// Interface for coordinates
    /// </summary>
    public interface ICoordinate : IVector<DoubleComponent>, IComparable<ICoordinate>,
                                   IEquatable<ICoordinate>, IConvertible,
                                   IComputable<Double, ICoordinate>
    {
        /// <summary>
        /// Function to evaluate if a coordinate has a value for <paramref name="ordinateIndex"/>
        /// </summary>
        /// <param name="ordinateIndex">The ordinate to test</param>
        /// <returns><c>true</c> if the ordinate is present.</returns>
        Boolean ContainsOrdinate(Ordinates ordinateIndex);

        /// <summary>
        /// Computes the distance to another coordinate
        /// </summary>
        /// <param name="other">The other coordinate</param>
        /// <returns>The distance to the other coordinate.</returns>
        Double Distance(ICoordinate other);

        /// <summary>
        /// Gets the <paramref name="ordinates"/> value
        /// </summary>
        /// <param name="ordinates">The ordinate</param>
        Double this[Ordinates ordinates] { get; }

        /// <summary>
        /// Function to return <see cref="Ordinates.X"/>- and <see cref="Ordinates.Y"/>-ordinate values as array.
        /// </summary>
        Double[] ToArray2D();

        /// <summary>
        /// Function to return the provided values as array.
        /// </summary>
        /// <param name="ordinates"></param>
        /// <returns></returns>
        Double[] ToArray(params Ordinates[] ordinates);

        /// <summary>
        /// Get whether this coordinate does not contain any ordinate values.
        /// </summary>
        Boolean IsEmpty { get; }

        /// <summary>
        /// Gets a coordinate with all ordinate values = 0;
        /// </summary>
        new ICoordinate Zero { get; }

        /// <summary>
        /// Gets the factory that created this coordinate.
        /// </summary>
        [Obsolete]
        ICoordinateFactory Factory { get; }
    }
}