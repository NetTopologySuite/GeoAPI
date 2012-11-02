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

namespace GeoAPI.CoordinateSystems.Transformations
{
	/// <summary>
	/// Flags indicating parts of domain covered by a convex hull. 
	/// </summary>
	/// <remarks>
	/// These flags can be combined. For example, the value 3 
	/// corresponds to a combination of <see cref="Inside"/> and <see cref="Outside"/>,
	/// which means that some parts of the convex hull are inside the 
	/// domain, and some parts of the convex hull are outside the domain.
	/// </remarks>
	public enum DomainFlags
	{
		/// <summary>
		/// At least one point in a convex hull is inside the transform's domain.
		/// </summary>
		Inside = 1,

		/// <summary>
		/// At least one point in a convex hull is outside the transform's domain.
		/// </summary>
		Outside = 2,

		/// <summary>
		/// At least one point in a convex hull is not transformed continuously.
		/// </summary>
		/// <remarks>
		/// As an example, consider a "Longitude_Rotation" transform which adjusts 
		/// longitude coordinates to take account of a change in Prime Meridian. If
		/// the rotation is 5 degrees east, then the point (Lat=175,Lon=0) is not 
		/// transformed continuously, since it is on the meridian line which will 
		/// be split at +180/-180 degrees.
		/// </remarks>
		Discontinuous = 4
	}
}
