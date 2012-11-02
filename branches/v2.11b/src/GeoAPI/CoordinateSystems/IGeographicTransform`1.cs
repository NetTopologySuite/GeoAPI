// Portions copyright 2005 - 2006: Morten Nielsen (www.iter.dk)
// Portions copyright 2006 - 2008: Rory Plaire (codekaizen@gmail.com)
//
// This file is part of GeoAPI.
// GeoAPI is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// GeoAPI is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with GeoAPI; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System;
using System.Collections.Generic;
using GeoAPI.Coordinates;
using NPack.Interfaces;

namespace GeoAPI.CoordinateSystems
{
	/// <summary>
	/// The IGeographicTransform interface is implemented on geographic transformation
	/// objects and implements datum transformations between geographic coordinate systems.
	/// </summary>
	public interface IGeographicTransform<TCoordinate> : IInfo
        where TCoordinate : ICoordinate, IEquatable<TCoordinate>, IComparable<TCoordinate>, IComputable<Double, TCoordinate>, IConvertible
	{
		/// <summary>
		/// Gets or sets source geographic coordinate system for the transformation.
		/// </summary>
		IGeographicCoordinateSystem<TCoordinate> Source { get; }

		/// <summary>
		/// Gets or sets the target geographic coordinate system for the transformation.
		/// </summary>
		IGeographicCoordinateSystem<TCoordinate> Target { get; }

		/// <summary>
		/// Returns an accessor interface to the parameters for this geographic transformation.
		/// </summary>
		IParameterInfo ParameterInfo { get; }

		/// <summary>
		/// Transforms an array of points from the source geographic coordinate system
		/// to the target geographic coordinate system.
		/// </summary>
		/// <param name="points">Points in the source geographic coordinate system</param>
		/// <returns>Points in the target geographic coordinate system</returns>
        IEnumerable<TCoordinate> Forward(IEnumerable<TCoordinate> points);

		/// <summary>
		/// Transforms an array of points from the target geographic coordinate system
		/// to the source geographic coordinate system.
		/// </summary>
		/// <param name="points">Points in the target geographic coordinate system</param>
		/// <returns>Points in the source geographic coordinate system</returns>
        IEnumerable<TCoordinate> Inverse(IEnumerable<TCoordinate> points);
	}
}
