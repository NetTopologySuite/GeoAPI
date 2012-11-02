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

using System;

namespace GeoAPI.Coordinates
{
    /// <summary>
    /// Standard ordinate index values.
    /// </summary>
    public enum Ordinates
    {
        /// <summary>
        /// X Ordinate = 0.
        /// </summary>
        X = 0,

        /// <summary>
        /// Y Ordinate = 1.
        /// </summary>
        Y = 1,

        /// <summary>
        /// Z Ordinate = 2.
        /// </summary>
        Z = 2,

        /// <summary>
        /// W Ordinate = 3.
        /// </summary>
        W = 3,

        /// <summary>
        /// M Ordinate = 4.
        /// </summary>
        M = 4,

        /// <summary>
        /// Longitude Ordinate = 0.
        /// </summary>
        Lon = 0,

        /// <summary>
        /// Latitude Ordinate = 1.
        /// </summary>
        Lat = 1
    }

    /// <summary>
    /// Ordinate flags
    /// </summary>
    [Flags]
    public enum OrdinateFlags
    {
        /// <summary>
        /// No ordinates
        /// </summary>
        None = 0,

        /// <summary>
        /// X-ordinate flag
        /// </summary>
        X = 1,

        /// <summary>
        /// Y-ordinate flag
        /// </summary>
        Y = 2,

        /// <summary>
        /// X- and Y-ordinate flag
        /// </summary>
        XY = 3,

        /// <summary>
        /// Z-ordinate flag
        /// </summary>
        Z = 4,

        /// <summary>
        /// X-, Y- and Z-ordinate flag
        /// </summary>
        XYZ = 7,

        /// <summary>
        /// M-ordinate flag (measure)
        /// </summary>
        M = 8,

        /// <summary>
        /// X-, Y- and M-ordinate flag
        /// </summary>
        XYM = 11,

        /// <summary>
        /// X-, Y-, Z- and M-ordinate flag
        /// </summary>
        XYZM = 15,

        [Obsolete]
        W = 16,
        [Obsolete]
        XYW = 19,
        [Obsolete]
        XYZW = 23,
        [Obsolete]
        XYMW = 27,
        [Obsolete]
        XYZMW = 31
    }
}