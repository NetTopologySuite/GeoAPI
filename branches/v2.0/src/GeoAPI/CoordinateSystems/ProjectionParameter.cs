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
using System.Globalization;

namespace GeoAPI.CoordinateSystems
{
    /// <summary>
    /// A named projection parameter value.
    /// </summary>
    /// <remarks>
    /// The linear units of parameters' values match the linear units of the containing 
    /// projected coordinate system. The angular units of parameter values match the 
    /// angular units of the geographic coordinate system that the projected coordinate 
    /// system is based on. (Notice that this is different from <see cref="Parameter"/>,
    /// where the units are always meters and degrees.)
    /// </remarks>
    public class ProjectionParameter : Parameter
    {
        /// <summary>
        /// Initializes an instance of a ProjectionParameter
        /// </summary>
        /// <param name="name">Name of parameter</param>
        /// <param name="value">Parameter value</param>
        public ProjectionParameter(String name, Double value)
            : base(name, value) {}


        /// <summary>
        /// Returns the Well-Known Text for this object
        /// as defined in the simple features specification.
        /// </summary>
        public String Wkt
        {
            get { return String.Format(CultureInfo.InvariantCulture.NumberFormat, "PARAMETER[\"{0}\", {1}]", Name, Value); }
        }

        /// <summary>
        /// Gets an XML representation of this object.
        /// </summary>
        public String Xml
        {
            get
            {
                return
                    String.Format(CultureInfo.InvariantCulture.NumberFormat,
                                  "<CS_ProjectionParameter Name=\"{0}\" Value=\"{1}\"/>", Name, Value);
            }
        }
    }
}