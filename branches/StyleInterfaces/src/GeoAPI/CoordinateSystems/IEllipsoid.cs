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

namespace GeoAPI.CoordinateSystems
{
	/// <summary>
	/// The IEllipsoid interface defines the standard information stored with ellipsoid objects.
	/// </summary>
	public interface IEllipsoid : IInfo
	{
		/// <summary>
		/// Gets or sets the value of the semi-major axis.
		/// </summary>
		Double SemiMajorAxis { get; }

		/// <summary>
		/// Gets or sets the value of the semi-minor axis.
		/// </summary>
		Double SemiMinorAxis { get; }

		/// <summary>
		/// Gets or sets the value of the inverse of the flattening constant of the ellipsoid.
		/// </summary>
		Double InverseFlattening { get; }

		/// <summary>
		/// Gets or sets the value of the axis unit.
		/// </summary>
		ILinearUnit AxisUnit { get; }

		/// <summary>
		/// Gets or sets a value indicating whether the inverse flattening 
		/// is definitive for this ellipsoid.
		/// </summary>
		/// <remarks>
        /// Some ellipsoids use the IVF as the defining value, and calculate 
        /// the polar radius whenever asked. Other ellipsoids use the polar radius 
        /// to calculate the IVF whenever asked. This distinction can be important 
        /// to avoid floating-point rounding errors.
        /// </remarks>
		Boolean IsIvfDefinitive { get; }
	}
}
