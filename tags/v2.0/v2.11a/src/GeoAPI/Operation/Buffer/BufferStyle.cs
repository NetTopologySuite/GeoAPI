// Portions copyright 2005 - 2007: Diego Guidi
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

namespace GeoAPI.Operations.Buffer
{
    /// <summary>
    /// Buffer style.
    /// </summary>
    public enum BufferStyle
    {
        /// <summary> 
        /// Specifies a round line buffer end cap endCapStyle (Default).
        /// </summary>/
        Round = 1,

        /// <summary> 
        /// Specifies a butt (or flat) line buffer end cap endCapStyle.
        /// </summary>
        Butt = 2,

        /// <summary>
        /// Specifies a square line buffer end cap endCapStyle.
        /// </summary>
        Square = 3,
    }
}
