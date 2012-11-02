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

#if NETCF
using System;

namespace GeoAPI.DataStructures
{
    /// <summary>
    /// A supoort class: the purpose is to integrate System.BitConverter 
    /// methods not presents in .NET Compact Framework.
    /// </summary>
    public class BitConverter
    {
        public static Int64 DoubleToInt64Bits(Double x)
        {
#if UNSAFE
            unsafe
            {
                return *(Int64*)&x;
            }
#else
            Byte[] bytes = System.BitConverter.GetBytes(x);
            Int64 value = System.BitConverter.ToInt64(bytes, 0);
            return value;
#endif
        }

        public static Double Int64BitsToDouble(Int64 x)
        {
#if UNSAFE
            unsafe
            {
                return *(Double*)&x;
            }
#else
            Byte[] bytes = System.BitConverter.GetBytes(x);
            Double value = System.BitConverter.ToDouble(bytes, 0);
            return value;
#endif
        }
    }
}
#endif