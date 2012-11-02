// Portions copyright 2012 -     : Felix Obermaier
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

using System.Collections.Generic;

namespace GeoAPI.Coordinates
{
    /// <summary>
    /// Conversion utility between <see cref="Ordinates"/> and <see cref="OrdinateFlags"/>
    /// </summary>
    public static class OrdinateUtility
    {
        /// <summary>
        /// Function to transform an ordinate flags value to an array of ordinate indices.
        /// </summary>
        /// <param name="flags">The ordinate flags</param>
        /// <returns>The array of ordinate indices</returns>
        public static Ordinates[] ToOrdinates(OrdinateFlags flags)
        {
            var tmp = new List<Ordinates>(5);
            for (var i = 0; i < 5; i++)
            {
                var tmpFlag = (OrdinateFlags)(1 << i);
                if ((flags & tmpFlag) != OrdinateFlags.None)
                    tmp.Add((Ordinates)i);
            }
            return tmp.ToArray();
        }

        /// <summary>
        /// Function to transform a series of ordinate indices to a <see cref="OrdinateFlags"/>
        /// </summary>
        /// <param name="ordinates">The ordinate indices</param>
        /// <returns>The ordinate flags</returns>
        public static OrdinateFlags ToOrdinateFlags(params Ordinates[] ordinates)
        {
            var res = OrdinateFlags.None;
            foreach (var ordinate in ordinates)
                res |= (OrdinateFlags)(1 << (int)ordinate);
            return res;
        }
    }
}