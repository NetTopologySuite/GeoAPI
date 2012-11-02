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
using System.Collections.Generic;

namespace GeoAPI.DataStructures
{
    public static class NumberBaseConverter
    {
        private static readonly Dictionary<Char, Int32> _base10DigitValueTable = new Dictionary<Char, Int32>();

        static NumberBaseConverter()
        {
            const Int32 numeralStart = '0';
            const Int32 upperStart = 'A';
            const Int32 lowerStart = 'a';

            for (Int32 i = 0; i < 10; i++)
            {
                _base10DigitValueTable[(Char)(numeralStart + i)] = i;
            }

            for (Int32 i = 10; i <= 36; i++)
            {
                _base10DigitValueTable[(Char)(upperStart + i)] = i;
                _base10DigitValueTable[(Char)(lowerStart + i)] = i;
            }
        }

        public static String ConvertToHex(Int32 value, Int32 digits)
        {
            String leading = new String('0', digits);
            String hex = leading + ConvertBase10ToAny(value, 16);
            return hex.Substring(hex.Length - digits);
        }

        public static void ConvertFromHex(String hex, out Int32 value)
        {
            value = (Int32)ConvertAnyToBase10(hex, 16);
        }

        public static void ConvertFromHex(String hex, out UInt32 value)
        {
            value = (UInt32)ConvertAnyToBase10(hex, 16);
        }

        public static Double ConvertAnyToBase10(String value, Int32 valueBase)
        {
            Double base10Value = 0;

            if (valueBase == 10)
            {
                return Double.Parse(value);
            }

            Int32 digitPlace = value.Length;

            for (Int32 k = 0; k < value.Length; k++)
            {
                Int32 base10DigitValue = _base10DigitValueTable[value[k]];

                if (base10Value > valueBase - 1)
                {
                    throw new FormatException("Value contains a digit which is not supported in the given base.");
                }

                digitPlace--;
                base10Value += base10DigitValue * Math.Pow(valueBase, digitPlace);
            }

            return base10Value;
        }

        public static String ConvertBase10ToAny(Double value, Int32 resultBase)
        {
            const Int32 ResultBlockSize = 128;
            Char[] result = new Char[ResultBlockSize];

            const String digits = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Int32 index = result.Length - 1;

            while (value > 0)
            {
                Int32 remainder = (Int32)(value % resultBase);
                value = (value - remainder) / resultBase;
                result[index--] = digits[remainder];

                if (index < 0)
                {
                    Char[] expanded = new Char[result.Length + ResultBlockSize];
                    Buffer.BlockCopy(result, 0, expanded, ResultBlockSize, result.Length);
                    index = ResultBlockSize - 1;
                    result = expanded;
                }
            }

            return new String(result, index, (result.Length - 1) - index);
        }

        /// <summary>
        /// Convert the given numeric value (passed as <see cref="String"/>) 
        /// of the base specified by <paramref name="baseIn"/> to the value specified by 
        /// <paramref name="baseOut"/>.
        /// </summary>
        /// <param name="value">Numeric value to be converted, as <see cref="String"/>.</param>
        /// <param name="baseIn">Base of input value.</param>
        /// <param name="baseOut">Base to use for conversion.</param>
        /// <returns>Converted value, as String.</returns>
        public static String ConvertAnyToAny(String value, Int32 baseIn, Int32 baseOut)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            if ((baseIn < 2) || (baseIn > 36))
            {
                throw new ArgumentOutOfRangeException("baseIn",
                                                      baseIn,
                                                      "Base of value to convert must be between 2 and 36");
            }

            if ((baseOut < 2) || (baseOut > 36))
            {
                throw new ArgumentOutOfRangeException("baseOut",
                                                      baseOut,
                                                      "Base to convert value to must be between 2 and 36");
            }

            if (value.Trim().Length == 0)
            {
                throw new ArgumentException("Value is an empty string.");
            }

            // se baseIn e baseOut sono uguali la conversione è già fatta!
            if (baseIn == baseOut)
            {
                return value;
            }

            Double base10Value = ConvertAnyToBase10(value, baseIn);

            // generazione del risultato final
            // se il risultato da generare è in base 10 non c'è
            // bisogno di calcoli
            if (baseOut == 10)
            {
                return base10Value.ToString();
            }

            return ConvertBase10ToAny(base10Value, baseOut);
        }
    }
}