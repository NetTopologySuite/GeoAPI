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

namespace GeoAPI.CoordinateSystems
{
    // DESIGN_NOTE: Split into 3 enums and make the DatumType parameter on IDatum an Int32?
	/// <summary>
	/// Specifies a type of datum.
	/// </summary>
	public enum DatumType
	{
		/// <summary>
		/// Lowest possible value for horizontal datum types
		/// </summary>
		HorizontalMinimum = 1000,

		/// <summary>
		/// Unspecified horizontal datum type. Horizontal datums with this type should never
		/// supply a conversion to WGS84 using Bursa Wolf parameters.
		/// </summary>
		HorizontalOther = 1000,

		/// <summary>
		/// These datums, such as ED50 and NAD27, have been designed to support
		/// horizontal positions on the ellipsoid as opposed to positions in 3-D space. 
		/// These datums were designed mainly to support a horizontal component of a position 
		/// in a domain of limited extent, such as a country, a region or a continent.
		/// </summary>
		HorizontalClassic = 1001,

		/// <summary>
		/// A geocentric datum is a "satellite age" modern geodetic datum mainly of global
        /// extent, such as WGS84 (used in GPS), NAD83, PZ90 (used in GLONASS) and ITRF. These
		/// datums were designed to support both a horizontal component of position and 
		/// a vertical component of position (through ellipsoidal heights). The regional
		/// realizations of ITRF, such as ETRF, are also included in this category.
		/// </summary>
		HorizontalGeocentric = 1002,

		/// <summary>
		/// Highest possible value for horizontal datum types.
		/// </summary>
		HorizontalMaximum = 1999,

		/// <summary>
		/// Lowest possible value for vertical datum types.
		/// </summary>
		VerticalMinimum = 2000,

		/// <summary>
		/// Unspecified vertical datum type.
		/// </summary>
		VerticalOther = 2000,

		/// <summary>
		/// A vertical datum for orthometric heights that are measured along the plumb line.
		/// </summary>
		VerticalOrthometric = 2001,

		/// <summary>
		/// A vertical datum for ellipsoidal heights that are measured along the normal to
		/// the ellipsoid used in the definition of horizontal datum.
		/// </summary>
		VerticalEllipsoidal = 2002,

		/// <summary>
		/// The vertical datum of altitudes or heights in the atmosphere. These are
		/// approximations of orthometric heights obtained with the help of a barometer or
		/// a barometric altimeter. These values are usually expressed in one of the
		/// following units: meters, feet, millibars (used to measure pressure levels), or
		/// theta value (units used to measure geopotential height).
		/// </summary>
		VerticalAltitudeBarometric = 2003,

		/// <summary>
		/// A normal height system.
		/// </summary>
		VerticalNormal = 2004,

		/// <summary>
		/// A vertical datum of geoid model derived heights, also called GPS-derived heights.
		/// These heights are approximations of orthometric heights (H), constructed from the
		/// ellipsoidal heights (h) by the use of the given geoid undulation model (N) 
		/// through the equation: H=h-N.
		/// </summary>
		VerticalGeoidModelDerived = 2005,

		/// <summary>
		/// This attribute is used to support the set of datums generated for hydrographic
		/// engineering projects where depth measurements below sea level are needed. It is
		/// often called a hydrographic or a marine datum. Depths are measured in the 
		/// direction perpendicular (approximately) to the actual equipotential surfaces of
		/// the earth's gravity field, using such procedures as echo-sounding.
		/// </summary>
		VerticalDepth = 2006,

		/// <summary>
		/// Highest possible value for vertical datum types.
		/// </summary>
		VerticalMax = 2999,

		/// <summary>
		/// Lowest possible value for local datum types.
		/// </summary>
		LocalMinimum = 10000,

		/// <summary>
		/// Highest possible value for local datum types.
		/// </summary>
		LocalMaximum = 32767
	}
}
