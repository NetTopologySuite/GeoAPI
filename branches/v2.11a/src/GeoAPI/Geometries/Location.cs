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
using System.ComponentModel;
using System.Globalization;

namespace GeoAPI.Geometries
{
    /// <summary>
    /// An enumeration of possible relations any given point can have with 
    /// a geometry.
    /// </summary>
    // TODO: check if uninitialized state can be None instead of Interior
    [TypeConverter(typeof(LocationTypeConverter))]
    public enum Locations : byte
    {
        /// <summary>
        /// DE-9IM row index of the interior of the first point and column index of
        /// the interior of the second point. Location value for the interior of a
        /// point.
        /// </summary>
        /// <value>0</value>
        Interior = 0,

        /// <summary>
        /// DE-9IM row index of the boundary of the first point and column index of
        /// the boundary of the second point. Location value for the boundary of a
        /// point.
        /// </summary>
        /// <value>1</value>
        Boundary = 1,

        /// <summary>
        /// DE-9IM row index of the exterior of the first point and column index of
        /// the exterior of the second point. Location value for the exterior of a
        /// point.
        /// </summary>
        /// <value>2</value>
        Exterior = 2,

        /// <summary>
        /// Used for uninitialized location values.
        /// </summary>
        /// <value>0x80</value>
        None = 0x80,
    }

    /// <summary>
    /// Converts between a <see cref="Locations"/> value and a <see cref="Char"/>
    /// which describes it.
    /// </summary>
    public class LocationTypeConverter : TypeConverter
    {
        public override Boolean CanConvertFrom(ITypeDescriptorContext context,
                                               Type sourceType)
        {
            if (sourceType == typeof(String) || sourceType == typeof(Char))
            {
                return true;
            }

            return false;
        }

        public override Boolean CanConvertTo(ITypeDescriptorContext context,
                                             Type destinationType)
        {
            if (destinationType == typeof(String) || destinationType == typeof(Char))
            {
                return true;
            }

            return false;
        }

        public override Object ConvertFrom(ITypeDescriptorContext context,
                                           CultureInfo culture,
                                           Object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is String)
            {
                String s = value as String;

                if (s.Length != 1)
                {
                    return null;
                }

                return ToLocationValue(s[0]);
            }

            if (value is Char)
            {
                Char c = (Char)value;

                return ToLocationValue(c);
            }

            return null;
        }

        public override Object ConvertTo(ITypeDescriptorContext context,
                                         CultureInfo culture,
                                         Object value,
                                         Type destinationType)
        {
            if (value == null || !(value is Locations))
            {
                return null;
            }

            Locations d = (Locations)value;

            if (destinationType == typeof(String))
            {
                return Convert.ToString(ToLocationSymbol(d));
            }

            if (destinationType == typeof(Char))
            {
                return ToLocationSymbol(d);
            }

            return null;
        }

        /// <summary>
        /// Converts the location value to a location symbol, for example, 
        /// <see cref="Locations.Exterior"/> => 'e'.
        /// </summary>
        /// <param name="locationValue">
        /// The <see cref="Locations"/> value to convert.
        /// </param>
        /// <returns>Either 'e', 'b', 'i' or '-'.</returns>
        public static Char ToLocationSymbol(Locations locationValue)
        {
            switch (locationValue)
            {
                case Locations.Exterior:
                    return 'e';
                case Locations.Boundary:
                    return 'b';
                case Locations.Interior:
                    return 'i';
                case Locations.None:
                    return '-';
                default:
                    throw new ArgumentException("Unknown location value: " +
                                                locationValue);
            }
        }

        /// <summary>
        /// Converts a <see cref="Char"/> value in the set { 'e', 'b', 'i', '-' } or
        /// { 'E', 'B', 'I', '-' } to the values <see cref="Locations.Exterior"/>,
        /// <see cref="Locations.Boundary"/>, <see cref="Locations.Interior"/> or 
        /// <see cref="Locations.None"/>, respectively.
        /// </summary>
        /// <param name="locationSymbol">The <see cref="Char"/> to convert.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="locationSymbol"/> isn't in one of the sets
        /// { 'e', 'b', 'i', '-' } or { 'E', 'B', 'I', '-' }.
        /// </exception>
        public static Locations ToLocationValue(Char locationSymbol)
        {
            switch (locationSymbol)
            {
                case 'E':
                case 'e':
                    return Locations.Exterior;
                case 'B':
                case 'b':
                    return Locations.Boundary;
                case 'I':
                case 'i':
                    return Locations.Interior;
                case '-':
                    return Locations.None;
                default:
                    throw new ArgumentOutOfRangeException("locationSymbol",
                                                          locationSymbol,
                                                          "Value must be in one of the " +
                                                          "sets { 'e', 'b', 'i', '-' } " +
                                                          "or { 'E', 'B', 'I', '-' }.");
            }
        }
    }
}
