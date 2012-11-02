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
using System.Diagnostics;
using System.Text;

#if NETCF
using BitConverter = GeoAPI.DataStructures.BitConverter;
#endif

namespace GeoAPI.DataStructures
{
    /// <summary>
    /// <see cref="DoubleBits"/> manipulates <see cref="Double"/> numbers
    /// by using bit manipulation and bit-field extraction.
    /// For some operations (such as determining the exponent)
    /// this is more accurate than using mathematical operations
    /// (which suffer from round-off error).
    /// The algorithms and constants in this class
    /// apply only to IEEE-754 double-precision floating point format.
    /// </summary>
    public struct DoubleBits
    {
        public enum Fields : ulong
        {
            Sign = 0x8000000000000000,
            Exponent = 0x7FF0000000000000,
            Significand = 0xFFFFFFFFFFFFF
        }

        /// <summary>
        /// Value to add to the exponent in order to make it positive. This
        /// avoids using using two's-complement in the exponent, which inverts the 
        /// values of the double value's exponents bits for negative exponents.
        /// </summary>
        public const Int32 ExponentBias = 1023;

        public const Int32 ExponentMaxBiasValue = 0x7FF;

        /// <summary>
        /// The number of bits in the floating point's significand.
        /// </summary>
        public const Int32 SignificandBitCount = 52;

        /// <summary>
        /// The number of bits in the floating point's exponent.
        /// </summary>
        public const Int32 ExponentBitCount = 11;

        public const Int32 SignificandStartBit = 0;
        public const Int32 ExponentStartBit = 52;
        public const Int32 SignStartBit = 63;

        /// <summary>
        /// Generates a <see cref="Double"/> having the value
        /// of 2 raised to the power of <paramref name="exp"/>.
        /// </summary>
        /// <param name="exp">The power to raise 2 to.</param>
        /// <returns>The value of 2 raised to the given power.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when <paramref name="exp"/> is greater than <value>1023</value>
        /// or less than <value>-1022</value>.
        /// </exception>
        public static Double PowerOf2(Int32 exp)
        {
            if (exp > 1023 || exp < -1022)
            {
                throw new ArgumentOutOfRangeException("exp", exp, "Exponent out of bounds");
            }

            Int64 expBias = exp + ExponentBias;

            // expBias is now raised (biased) by 1023, meaning it will be between
            // 1 and 2046.
            Debug.Assert(expBias >= 1 && expBias <= 2046);

            // Move the biased value to the exponent bits of the IEEE double 
            // (bits 52 - 62)
            Int64 bits = expBias << 52;
            return BitConverter.Int64BitsToDouble(bits);
        }

        /// <summary>
        /// Computes the exponent of a double floating point 
        /// value.
        /// </summary>
        public static Int32 GetExponent(Double d)
        {
            DoubleBits db = new DoubleBits(d);
            return db.Exponent;
        }

        public static Double TruncateToPowerOf2(Double d)
        {
            DoubleBits db = new DoubleBits(d);
            db = db.ZeroLowerBits(SignificandBitCount);
            return db.Double;
        }

        public static String ToBinaryString(Double d)
        {
            DoubleBits db = new DoubleBits(d);
            return db.ToString();
        }

        public static Double MaximumCommonSignificand(Double d1, Double d2)
        {
            if (d1 == 0.0 || d2 == 0.0)
            {
                return 0.0;
            }

            DoubleBits db1 = new DoubleBits(d1);
            DoubleBits db2 = new DoubleBits(d2);

            if (db1.Exponent != db2.Exponent)
            {
                return 0.0;
            }

            Int32 maxCommon = db1.GetCommonSignificandBitsCount(db2);
            db1.ZeroLowerBits(64 - (12 + maxCommon));
            return db1.Double;
        }

        public static Double FindNextHigherPowerOf2(Double d)
        {
            Double exp = TruncateToPowerOf2(d);
            DoubleBits expBits = new DoubleBits(exp);

            // we're as big as we can get
            return expBits.BiasedExponent == ExponentMaxBiasValue
                       ? Double.PositiveInfinity
                       : PowerOf2(expBits.Exponent + 1);
        }

        private readonly Double _x;
        private readonly Int64 _xBits;

        public DoubleBits(Double x)
        {
            _x = x;
            _xBits = BitConverter.DoubleToInt64Bits(x);
        }

        private DoubleBits(Double x, Int64 xBits)
        {
            _x = x;
            _xBits = xBits;
        }

        public Double Double
        {
            get { return BitConverter.Int64BitsToDouble(_xBits); }
        }

        /// <summary>
        /// Gets the raw exponent value for the double-floating point value. 
        /// The raw exponent value is the exponent biased by 
        /// <see cref="ExponentBias"/> in order to remain positive.
        /// </summary>
        public Int32 BiasedExponent
        {
            get
            {
                Int64 signExp = _xBits & (Int64) Fields.Exponent;
                Int32 exp = (Int32)(signExp >> ExponentStartBit);
                return exp;
            }
        }

        /// <summary>
        /// Gets the exponent for the double value.
        /// </summary>
        public Int32 Exponent
        {
            get { return BiasedExponent - ExponentBias; }
        }

        public Int32 Sign
        {
            get
            {
                return unchecked((_xBits & (Int64) Fields.Sign) > 0 ? 1 : 0);
            }
        }

        /// <summary>
        /// Creates a new <see cref="DoubleBits"/> value
        /// with the lower <paramref name="bitCount"/> bits
        /// set to 0.
        /// </summary>
        /// <param name="bitCount">The number of bits to set to 0.</param>
        /// <returns>
        /// A <see cref="DoubleBits"/> structure with the <paramref name="bitCount"/>
        /// lower bits set to 0.
        /// </returns>
        public DoubleBits ZeroLowerBits(Int32 bitCount)
        {
            Int64 invMask = (1L << bitCount) - 1L;
            Int64 mask = ~invMask;
            return new DoubleBits(_x, _xBits & mask);
        }

        /// <summary>
        /// Gets the bit at the given <paramref name="index"/>.
        /// </summary>
        /// <param name="index">Index of the bit to retrieve.</param>
        /// <returns>The value of the bit at <paramref name="index"/>.</returns>
        public Int32 this[Int32 index]
        {
            get
            {
                if(index < 0 || index > 63)
                {
                    throw new IndexOutOfRangeException();
                }

                Int64 mask = (1L << index);
                return (_xBits & mask) > 0 ? 1 : 0;
            }
        }

        public Int32 this[Fields field, Int32 index]
        {
            get
            {
                Int32 shift = 1;
                switch (field)
                {
                    case Fields.Sign:
                        if(index != 0) throw new IndexOutOfRangeException();
                        shift = SignStartBit;
                        break;
                    case Fields.Exponent:
                        if (index < 0 || index > ExponentBitCount - 1)
                        {
                            throw new IndexOutOfRangeException();
                        }
                        shift = ExponentStartBit;
                        break;
                    case Fields.Significand:
                        if (index < 0 || index > SignificandBitCount - 1)
                        {
                            throw new IndexOutOfRangeException();
                        }
                        shift = SignificandStartBit;
                        break;
                    default:
                        break;
                }

                Int64 mask = 1L << shift + index;

                unchecked
                {
                    return ((_xBits & (Int64)field) & mask) > 0 ? 1 : 0;
                }
            }
        }

        /// <summary> 
        /// This computes the number of common most-significant bits in the significand.
        /// It does not count the hidden bit, which is always 1.
        /// It does not determine whether the numbers have the same exponent - if they do
        /// not, the value computed by this function is meaningless.
        /// </summary>
        /// <returns> The number of common most-significant significand bits.</returns>
        public Int32 GetCommonSignificandBitsCount(DoubleBits db)
        {
            for (Int32 i = 0; i < SignificandBitCount; i++)
            {
                if (this[i] != db[i])
                {
                    return i;
                }
            }

            return SignificandBitCount;
        }

        /// <summary>
        /// A representation of the double bits formatted for easy readability.
        /// </summary>
        public override String ToString()
        {
            // convert to a binary string
            String numStr = NumberBaseConverter.ConvertBase10ToAny(_xBits, 2);

            // pad a buffer with zeros, add the binary string, and keep the last 64 chars
            StringBuilder buffer = new StringBuilder(new String('0', 64), 128);
            buffer.Append(numStr);
            buffer.Remove(0, buffer.Length - 64);

            buffer.Append(_x.ToString("[ 0 ]"));
            buffer.Insert(64 - ExponentStartBit, Exponent.ToString("(0) "));
            buffer.Insert(64 - SignStartBit, Sign == 0 ? "(+) " : "(-) ");

            //String str = bitStr.Substring(0, 1) + "  "
            //             + bitStr.Substring(1, 12) + "(" + Exponent + ") "
            //             + bitStr.Substring(12)
            //             + " [ " + _x + " ]";
            return buffer.ToString();
        }
    }
}