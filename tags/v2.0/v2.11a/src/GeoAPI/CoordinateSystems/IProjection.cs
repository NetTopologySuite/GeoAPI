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
using System.Collections.Generic;

namespace GeoAPI.CoordinateSystems
{
	/// <summary>
	/// The IProjection interface defines the standard information stored with projection
	/// objects. 
	/// </summary>
	/// <remarks>
    /// A projection object implements a coordinate transformation from a geographic
    /// coordinate system to a projected coordinate system, given the ellipsoid for the
    /// geographic coordinate system. Each coordinate transformation of
    /// interest, (e.g., Transverse Mercator, Lambert, etc.) should be implemented as a subclass of
    /// some common base class (e.g., Projection) which implements the IProjection interface.
    /// </remarks>
	public interface IProjection : IInfo, IEnumerable<ProjectionParameter>
	{
		/// <summary>
		/// Gets number of parameters of the projection.
		/// </summary>
		Int32 ParameterCount { get; }

		/// <summary>
		/// Gets the projection classification name (e.g. 'Transverse_Mercator').
		/// </summary>
		String ProjectionClassName { get; }

		/// <summary>
		/// Gets an indexed parameter of the projection.
		/// </summary>
		/// <param name="index">Index of parameter</param>
		/// <returns>
		/// The <see cref="ProjectionParameter"/> instance stored at the given <paramref name="index"/>.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// Thrown if <paramref name="index"/> is less than 0 or greater than 
		/// or equal to <see cref="ParameterCount"/>.
		/// </exception>
        ProjectionParameter this[Int32 index] { get; }

		/// <summary>
		/// Gets a named parameter of the projection.
		/// </summary>
		/// <remarks>The parameter name is case insensitive.</remarks>
		/// <param name="name">Name of parameter.</param>
		/// <returns>
		/// The <see cref="ProjectionParameter"/> with the given <paramref name="name"/> 
        /// or <see langword="null"/> if not found.
		/// </returns>
		ProjectionParameter this[String name] { get; }
	}
}
