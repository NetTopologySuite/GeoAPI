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
    public struct Radians
    {
        private const Double DegreesPerRadian = 57.29577951308232;

        public static implicit operator Double(Radians radians)
        {
            return radians._value;
        }

        public static explicit operator Degrees(Radians radians)
        {
            return radians.ToDegrees();
        }

        public static explicit operator Radians(Double degrees)
        {
            return new Radians(degrees);
        }

        public static Radians operator +(Radians left, Radians right)
        {
            return (Radians)(left._value + right._value);
        }

        public static Radians operator -(Radians left, Radians right)
        {
            return (Radians)(left._value - right._value);
        }

        public static Radians operator -(Radians value)
        {
            return (Radians)(-value._value);
        }

        private readonly Double _value;

        public Radians(Double radians)
        {
            _value = radians;
        }

        public override Boolean Equals(Object obj)
        {
            if (obj is Radians)
            {
                return Equals((Radians)obj);
            }

            if (obj is Degrees)
            {
                return Equals((Degrees)obj);
            }

            return false;
        }

        public override Int32 GetHashCode()
        {
            return _value.GetHashCode() ^ 31;
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public Degrees ToDegrees()
        {
            return new Degrees(DegreesPerRadian * _value);
        }

        public String ToString(String format)
        {
            return _value.ToString(format);
        }

        #region IFormattable Members

        public string ToString(String format, IFormatProvider formatProvider)
        {
            return _value.ToString(format, formatProvider);
        }

        #endregion

        #region IEquatable<Radians> Members

        public Boolean Equals(Radians other)
        {
            return _value == other._value;
        }

        #endregion

        #region IComparable<Radians> Members

        public Int32 CompareTo(Radians other)
        {
            return _value.CompareTo(other._value);
        }

        #endregion

        #region IEquatable<Degrees> Members

        public Boolean Equals(Degrees other)
        {
            return Equals((Radians)other);
        }

        #endregion

        #region IComparable<Degrees> Members

        public Int32 CompareTo(Degrees other)
        {
            return CompareTo((Radians)other);
        }

        #endregion
    }
}
