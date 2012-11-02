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
	/// The IParameterInfo interface provides an interface through which clients of a
	/// Projected Coordinate System or of a Projection can set the parameters of the
	/// projection.
	/// </summary>
	/// <remarks>
    /// <see cref="IParameterInfo"/> provides a generic interface for discovering the names and default
    /// values of parameters, and for setting and getting parameter values. Subclasses of
    /// this interface may provide projection specific parameter access methods.
    /// </remarks>
	public interface IParameterInfo
	{
		/// <summary>
		/// Gets the number of parameters expected.
		/// </summary>
		Int32 ParameterCount { get; }

		/// <summary>
		/// Returns the default parameters for this projection.
		/// </summary>
		/// <returns></returns>
		Parameter[] DefaultParameters();

		/// <summary>
		/// Gets or sets the parameters set for this projection.
		/// </summary>
		IList<Parameter> Parameters { get; }

		/// <summary>
		/// Gets the parameter by its name.
		/// </summary>
		/// <param name="name">The name of the parameter to access.</param>
		/// <returns>
		/// The parameter with the given <paramref name="name"/> or <see langword="null"/>
		/// if no Parameter with the name could be found.
		/// </returns>
		Parameter this[String name] { get; }
	}
}
