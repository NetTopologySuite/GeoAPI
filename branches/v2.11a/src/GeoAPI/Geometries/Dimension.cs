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
    /// Constants representing the dimensions of a point, a curve and a surface.
    /// Also, constants representing the dimensions of the empty point and
    /// non-empty geometries, and a wildcard dimension meaning "any dimension".
    /// </summary>
    [TypeConverter(typeof(DimensionTypeConverter))]
    public enum Dimensions
    {
        /// <summary>
        /// Dimension value of a point (0).
        /// </summary>
        Point = 0,

        /// <summary>
        /// Dimension value of a curve (1).
        /// </summary>
        Curve = 1,

        /// <summary>
        /// Dimension value of a surface (2).
        /// </summary>
        Surface = 2,

        /// <summary>
        /// Dimension value of a empty point (-1).
        /// </summary>
        False = -1,

        /// <summary>
        /// Dimension value of non-empty geometries (= {Point,Curve,A}).
        /// </summary>
        True = -2,

        /// <summary>
        /// Dimension value for any dimension (= {False, True}).
        /// </summary>
        DontCare = -3
    }

    /// <summary>
    /// Converts between <see cref="Dimensions"/> values and character types which represent
    /// the dimensional intersection.
    /// </summary>
    public class DimensionTypeConverter : TypeConverter
    {
        public override Boolean CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(String) || sourceType == typeof(Char))
            {
                return true;
            }

            return false;
        }

        public override Boolean CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(String) || destinationType == typeof(Char))
            {
                return true;
            }

            return false;
        }

        public override Object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, Object value)
        {
            if (value == null)
            {
                return null;
            }

            if(value is String)
            {
                String s = value as String;
                
                if(s.Length != 1)
                {
                    return null;
                }

                return ToDimensionValue(s[0]);
            }

            if(value is Char)
            {
                Char c = (Char) value;

                return ToDimensionValue(c);
            }

            return null;
        }

        public override Object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, Object value, Type destinationType)
        {
            if(value == null || !(value is Dimensions))
            {
                return null;
            }

            Dimensions d = (Dimensions) value;


            if (destinationType == typeof(String))
            {
                return Convert.ToString(ToDimensionSymbol(d));
            }

            if (destinationType == typeof(Char))
            {
                return ToDimensionSymbol(d);
            }

            return null;
        }

        /// <summary>
        /// Converts the dimension value to a dimension symbol,
        /// for example, <c>True => 'T'</c>
        /// </summary>
        /// <param name="dimensionValue">Number that can be stored in the <see cref="IntersectionMatrix"/>.
        /// Possible values are <c>True, False, DontCare, 0, 1, 2</c>.</param>
        /// <returns>Character for use in the String representation of an <see cref="IntersectionMatrix"/>.
        /// Possible values are <c>T, F, * , 0, 1, 2</c>.</returns>
        public static Char ToDimensionSymbol(Dimensions dimensionValue)
        {
            switch (dimensionValue)
            {
                case Dimensions.False:
                    return 'F';
                case Dimensions.True:
                    return 'T';
                case Dimensions.DontCare:
                    return '*';
                case Dimensions.Point:
                    return '0';
                case Dimensions.Curve:
                    return '1';
                case Dimensions.Surface:
                    return '2';
                default:
                    throw new ArgumentOutOfRangeException(
                        "Unknown dimension value: " + dimensionValue);
            }
        }

        /// <summary>
        /// Converts the dimension symbol to a dimension value,
        /// for example, <c>'*' => DontCare</c>
        /// </summary>
        /// <param name="dimensionSymbol">Character for use in the String 
        /// representation of an <see cref="IntersectionMatrix"/>.
        /// Possible values are <c>T, F, * , 0, 1, 2</c>.</param>
        /// <returns>Number that can be stored in the <see cref="IntersectionMatrix"/>.
        /// Possible values are <c>True, False, DontCare, 0, 1, 2</c>.</returns>
        public static Dimensions ToDimensionValue(Char dimensionSymbol)
        {
            switch (Char.ToUpper(dimensionSymbol))
            {
                case 'F':
                    return Dimensions.False;
                case 'T':
                    return Dimensions.True;
                case '*':
                    return Dimensions.DontCare;
                case '0':
                    return Dimensions.Point;
                case '1':
                    return Dimensions.Curve;
                case '2':
                    return Dimensions.Surface;
                default:
                    throw new ArgumentOutOfRangeException(
                        "Unknown dimension symbol: " + dimensionSymbol);
            }
        }
    }
}