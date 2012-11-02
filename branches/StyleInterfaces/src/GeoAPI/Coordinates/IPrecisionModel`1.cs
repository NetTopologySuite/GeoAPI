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
using NPack.Interfaces;

namespace GeoAPI.Coordinates
{
    /// <summary>
    /// Specifies the precision model of the <typeparamref name="TCoordinate" />
    /// in an <see cref="ICoordinateSequence{TCoordinate}"/>. 
    /// In other words, specifies the grid of allowable points for all geometries. 
    /// </summary>
    /// <typeparam name="TCoordinate"></typeparam>
    public interface IPrecisionModel<TCoordinate> : IPrecisionModel, 
                                                    IComparable<IPrecisionModel<TCoordinate>>, 
                                                    IEquatable<IPrecisionModel<TCoordinate>>
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>, 
                            IComparable<TCoordinate>, IConvertible, 
                            IComputable<Double, TCoordinate>
    {
        /// <summary>
        /// Gets the <see cref="ICoordinateFactory{TCoordinate}"/> assigned
        /// to generate new coordinates.
        /// </summary>
        new ICoordinateFactory<TCoordinate> CoordinateFactory { get; }

        /// <summary> 
        /// Rounds a <typeparamref name="TCoordinate"/> to the 
        /// <see cref="IPrecisionModel{TCoordinate}"/> grid.
        /// </summary>
        /// <param name="coord">
        /// The coordinate to make precise according to the precision model.
        /// </param>
        TCoordinate MakePrecise(TCoordinate coord);
    }
}
