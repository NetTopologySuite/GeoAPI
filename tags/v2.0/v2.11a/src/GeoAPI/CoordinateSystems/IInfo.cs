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
    /// The <see cref="IInfo"/> interface defines the standard 
    /// information stored with spatial reference objects. This
    /// interface is reused for many of the spatial reference
    /// objects in the system.
    /// </summary>
    public interface IInfo
    {
        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        String Name { get; }

        /// <summary>
        /// Gets the authority which defined transformation and parameter values.
        /// </summary>
        /// <remarks>
        /// An authority is an organization that maintains definitions of 
        /// spatial reference objects. For example the European Petroleum 
        /// Survey Group (EPSG) maintains a database of coordinate systems, 
        /// and other spatial referencing objects.
        /// </remarks>
        String Authority { get; }

        /// <summary>
        /// Code used by an <see cref="Authority"/> to identify a
        /// spatial reference object. <see langword="null"/> is used for no code.
        /// </summary>
        /// <remarks>
        /// The authority code is a <see cref="String"/> 
        /// defined by an authority to reference a particular spatial 
        /// reference object. For example, the European Survey Group (EPSG) 
        /// authority uses 32 bit integers to reference coordinate systems, 
        /// so all their code strings will consist of a few digits. 
        /// The EPSG code for WGS84 Lat/Lon is "4326".
        /// </remarks>
        String AuthorityCode { get; }

        /// <summary>
        /// Gets the alias of the object.
        /// </summary>
        String Alias { get; }

        /// <summary>
        /// Gets the abbreviation of the object.
        /// </summary>
        String Abbreviation { get; }

        /// <summary>
        /// Gets the provider-supplied remarks for the object.
        /// </summary>
        String Remarks { get; }

        /// <summary>
        /// Gets the Well-Known Text for this spatial reference object
        /// as defined in the OGC Simple Features for SQL and
        /// Coordinate Transformation Services specifications.
        /// </summary>
        String Wkt { get; }

        /// <summary>
        /// Gets an XML representation of this object.
        /// </summary>
        String Xml { get; }

        /// <summary>
        /// Checks whether the values of this instance is equal to the values 
        /// of another instance. Only parameters used for coordinate system 
        /// are used for comparison. Name, abbreviation, authority, alias and 
        /// remarks are ignored in the comparison.
        /// </summary>
        /// <param name="other">The info object to compare parameters with.</param>
        /// <returns>
        /// <see langword="true"/> if the parameters are equal between the 
        /// <see cref="IInfo"/> objects, <see langword="false"/> otherwise.
        /// </returns>
        Boolean EqualParams(IInfo other);
    }
}