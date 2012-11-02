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

namespace GeoAPI.Coordinates
{
    /// <summary>
    /// The types of Precision Model.
    /// </summary>
    public enum PrecisionModelType
    {
        /// <summary> 
        /// The double floating model corresponds to the 
        /// double-precision floating-point representation specified by the
        /// IEEE-754 standard.
        /// </summary>
        DoubleFloating = 0,

        /// <summary>
        /// The single floating model corresponds to the 
        /// single-precision floating-point representation specified by the
        /// IEEE-754 standard.
        /// </summary>
        SingleFloating = 1,

        /// <summary> 
        /// Fixed precision indicates that coordinates have a fixed number of decimal places.
        /// The number of decimal places is determined by the log10 of the scale factor.
        /// </summary>
        Fixed = 2,

        /// <summary>
        /// The quadruple precision floating model corresponds to the 
        /// quadruple-precision floating-point representation specified by the
        /// draft IEEE-754r standard (binary128).
        /// </summary>
        QuadFloating = 3,
        
        /// <summary>
        /// The double extended floating model corresponds to the 
        /// double-extended-precision floating-point representation specified by the
        /// IEEE-754 standard.
        /// </summary>
        DoubleExtendedFloating = 4
    }
}
