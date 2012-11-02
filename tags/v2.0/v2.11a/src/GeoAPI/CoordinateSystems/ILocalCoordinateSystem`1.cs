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
	/// A local coordinate system, with uncertain relationship to the world.
	/// </summary>
	/// <remarks>
	/// In general, a local coordinate system cannot be related to other coordinate 
	/// systems. However, if two objects supporting this interface have the same dimension, 
	/// axes, units and datum then client code is permitted to assume that the two coordinate
	/// systems are identical. This allows several datasets from a common source (e.g. a CAD
	/// system) to be overlaid. In addition, some implementations of the Coordinate 
	/// Transformation (CT) package may have a mechanism for correlating local datums. (E.g. 
	/// from a database of transformations, which is created and maintained from real-world 
	/// measurements.)
	/// </remarks>
    public interface ILocalCoordinateSystem<TCoordinate> : ILocalCoordinateSystem, 
                                                           ICoordinateSystem<TCoordinate>
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>,
                            IComparable<TCoordinate>, IConvertible,
                            IComputable<Double, TCoordinate>
	{
	}
}
