// Portions copyright 2005 - 2006: Morten Nielsen (www.iter.dk)
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
using GeoAPI.Coordinates;
using NPack.Interfaces;

namespace GeoAPI.CoordinateSystems
{
	/// <summary>
	/// A coordinate system which sits inside another coordinate system. The fitted 
	/// coordinate system can be rotated and shifted, or use any other math transform
	/// to inject itself into the base coordinate system.
	/// </summary>
	/// <remarks>
    /// The dimension of this fitted coordinate system is determined by the source 
    /// dimension of the math transform. The transform should be one-to-one within 
    /// this coordinate system's domain, and the base coordinate system dimension 
    /// must be at least as big as the dimension of this coordinate system.
    /// </remarks>
    public interface IFittedCoordinateSystem<TCoordinate> : IFittedCoordinateSystem,
                                                            ICoordinateSystem<TCoordinate>
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>,
                            IComparable<TCoordinate>, IConvertible,
                            IComputable<Double, TCoordinate>
	{
		/// <summary>
		/// Gets underlying coordinate system.
		/// </summary>
		new ICoordinateSystem<TCoordinate> BaseCoordinateSystem { get; }
	}
}
