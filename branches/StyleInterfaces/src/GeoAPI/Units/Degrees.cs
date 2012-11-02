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

namespace GeoAPI.Units
{
    public struct Degrees : IEquatable<Degrees>, IComparable<Degrees>,
                            IEquatable<Radians>, IComparable<Radians>,
                            IFormattable
    {
        private const Double RadianPerDegrees = 0.0174532925199432958;

        public static explicit operator Radians(Degrees degrees)
        {
            return degrees.ToRadians();
        }

        public static implicit operator Double(Degrees degrees)
        {
            return degrees._value;
        }

        public static explicit operator Degrees(Double degrees)
        {
            return new Degrees(degrees);
        }

        public static Degrees operator + (Degrees left, Degrees right)
        {
            return (Degrees)(left._value + right._value);
        }

        public static Degrees operator -(Degrees left, Degrees right)
        {
            return (Degrees)(left._value - right._value);
        }

        public static Degrees operator -(Degrees value)
        {
            return (Degrees)(-value._value);
        }

        private readonly Double _value;

        public Degrees(Double degrees)
        {
            _value = degrees;
        }

        public override Boolean Equals(Object obj)
        {
            if (obj is Degrees)
            {
                return Equals((Degrees)obj);
            }

            if (obj is Radians)
            {
                return Equals((Radians)obj);
            }

            return false;
        }

        public override Int32 GetHashCode()
        {
            return _value.GetHashCode() ^ 31;
        }

        public override string ToString()
        {
            return _value + "\xB0";
        }

        public Radians ToRadians()
        {
            return new Radians(RadianPerDegrees * _value);
        }

        public String ToString(String format)
        {
            return _value.ToString(format);
        }

        #region IFormattable Members

        public string ToString(String format, IFormatProvider formatProvider)
        {
            return _value.ToString(format, formatProvider) + "\xB0";
        }

        #endregion

        #region IEquatable<Degrees> Members

        public Boolean Equals(Degrees other)
        {
            return _value == other._value;
        }

        #endregion

        #region IComparable<Degrees> Members

        public Int32 CompareTo(Degrees other)
        {
            return _value.CompareTo(other._value);
        }

        #endregion

        #region IEquatable<Radians> Members

        public Boolean Equals(Radians other)
        {
            return Equals((Degrees)other);
        }

        #endregion

        #region IComparable<Radians> Members

        public Int32 CompareTo(Radians other)
        {
            return CompareTo((Degrees)other);
        }

        #endregion
    }
}
