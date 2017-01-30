using System;

// Ref: http://www.yortondotnet.com/2009/11/tryparse-for-compact-framework.html

namespace GeoAPI
{
    /// <summary>
    /// Provides methods to parse simple value types without throwing format exception.
    /// </summary>
    internal static class ValueParser
    {
        /// <summary>
        /// Attempts to parse the string provided into an integer value. 
        /// </summary>
        /// <remarks>Returns 0 in the result parameter if the parse fails.</remarks>
        /// <param name="s">The string to attempt to parse.</param>
        /// <param name="result">The result of the parsed string, or zero if parsing failed.</param>
        /// <returns>A boolean value indicating whether or not the parse succeeded.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static bool TryParse(string s, out int result)
        {
            bool retVal = false;
#if WindowsCE
            try
            {
                result = Convert.ToInt32(s);
                retVal = true;
            }
            catch (FormatException) { result = 0; }
            catch (InvalidCastException) { result = 0; }
#else
            retVal = int.TryParse(s, out result);
#endif
            return retVal;
        }
        /// <summary>
        /// Attempts to parse the string provided into a byte value. 
        /// </summary>
        /// <remarks>Returns 0 in the result parameter if the parse fails.</remarks>
        /// <param name="s">The string to attempt to parse.</param>
        /// <param name="result">The result of the parsed string, or zero if parsing failed.</param>
        /// <returns>A boolean value indicating whether or not the parse succeeded.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static bool TryParse(string s, out byte result)
        {
            bool retVal = false;
#if WindowsCE
            try
            {
                result = Convert.ToByte(s);
                retVal = true;
            }
            catch (FormatException) { result = 0; }
            catch (InvalidCastException) { result = 0; }
#else
            retVal = byte.TryParse(s, out result);
#endif
            return retVal;
        }
        /// <summary>
        /// Attempts to parse the string provided into an Int16 value. 
        /// </summary>
        /// <remarks>Returns 0 in the result parameter if the parse fails.</remarks>
        /// <param name="s">The string to attempt to parse.</param>
        /// <param name="result">The result of the parsed string, or zero if parsing failed.</param>
        /// <returns>A boolean value indicating whether or not the parse succeeded.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static bool TryParse(string s, out short result)
        {
            bool retVal = false;
#if WindowsCE
            try
            {
                result = Convert.ToInt16(s);
                retVal = true;
            }
            catch (FormatException) { result = 0; }
            catch (InvalidCastException) { result = 0; }
#else
            retVal = short.TryParse(s, out result);
#endif
            return retVal;
        }
        /// <summary>
        /// Attempts to parse the string provided into an Int64 value. 
        /// </summary>
        /// <remarks>Returns 0 in the result parameter if the parse fails.</remarks>
        /// <param name="s">The string to attempt to parse.</param>
        /// <param name="result">The result of the parsed string, or zero if parsing failed.</param>
        /// <returns>A boolean value indicating whether or not the parse succeeded.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static bool TryParse(string s, out long result)
        {
            bool retVal = false;
#if WindowsCE
            try
            {
                result = Convert.ToInt64(s);
                retVal = true;
            }
            catch (FormatException) { result = 0; }
            catch (InvalidCastException) { result = 0; }
#else
            retVal = long.TryParse(s, out result);
#endif
            return retVal;
        }
        /// <summary>
        /// Attempts to parse the string provided into a decimal value. 
        /// </summary>
        /// <remarks>Returns 0 in the result parameter if the parse fails.</remarks>
        /// <param name="s">The string to attempt to parse.</param>
        /// <param name="result">The result of the parsed string, or zero if parsing failed.</param>
        /// <returns>A boolean value indicating whether or not the parse succeeded.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static bool TryParse(string s, out decimal result)
        {
            bool retVal = false;
#if WindowsCE
            try
            {
                result = Convert.ToDecimal(s);
                retVal = true;
            }
            catch (FormatException) { result = 0; }
            catch (InvalidCastException) { result = 0; }
#else
            retVal = decimal.TryParse(s, out result);
#endif
            return retVal;
        }
        /// <summary>
        /// Attempts to parse the string provided into a float value. 
        /// </summary>
        /// <remarks>Returns 0 in the result parameter if the parse fails.</remarks>
        /// <param name="s">The string to attempt to parse.</param>
        /// <param name="result">The result of the parsed string, or zero if parsing failed.</param>
        /// <returns>A boolean value indicating whether or not the parse succeeded.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static bool TryParse(string s, out float result)
        {
            bool retVal = false;
#if WindowsCE
            try
            {
                result = (float)Convert.ToDecimal(s);
                retVal = true;
            }
            catch (FormatException) { result = 0; }
            catch (InvalidCastException) { result = 0; }
#else
            retVal = float.TryParse(s, out result);
#endif
            return retVal;
        }
        /// <summary>
        /// Attempts to parse the string provided into a double value. 
        /// </summary>
        /// <remarks>Returns 0 in the result parameter if the parse fails.</remarks>
        /// <param name="s">The string to attempt to parse.</param>
        /// <param name="result">The result of the parsed string, or zero if parsing failed.</param>
        /// <returns>A boolean value indicating whether or not the parse succeeded.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static bool TryParse(string s, out double result)
        {
            bool retVal = false;
#if WindowsCE
            try
            {
                result = Convert.ToDouble(s);
                retVal = true;
            }
            catch (FormatException) { result = 0; }
            catch (InvalidCastException) { result = 0; }
#else
            retVal = double.TryParse(s, out result);
#endif
            return retVal;
        }
        /// <summary>
        /// Attempts to parse the string provided into an sbyte value. 
        /// </summary>
        /// <remarks>Returns 0 in the result parameter if the parse fails.</remarks>
        /// <param name="s">The string to attempt to parse.</param>
        /// <param name="result">The result of the parsed string, or zero if parsing failed.</param>
        /// <returns>A boolean value indicating whether or not the parse succeeded.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static bool TryParse(string s, out sbyte result)
        {
            bool retVal = false;
#if WindowsCE
            try
            {
                result = (sbyte)Convert.ToInt32(s);
                retVal = true;
            }
            catch (FormatException) { result = 0; }
            catch (InvalidCastException) { result = 0; }
#else
            retVal = sbyte.TryParse(s, out result);
#endif
            return retVal;
        }
        /// <summary>
        /// Attempts to parse the string provided into a uint value. 
        /// </summary>
        /// <remarks>Returns 0 in the result parameter if the parse fails.</remarks>
        /// <param name="s">The string to attempt to parse.</param>
        /// <param name="result">The result of the parsed string, or zero if parsing failed.</param>
        /// <returns>A boolean value indicating whether or not the parse succeeded.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static bool TryParse(string s, out uint result)
        {
            bool retVal = false;
#if WindowsCE
            try
            {
                result = (uint)Convert.ToUInt64(s);
                retVal = true;
            }
            catch (FormatException) { result = 0; }
            catch (InvalidCastException) { result = 0; }
#else
            retVal = uint.TryParse(s, out result);
#endif
            return retVal;
        }
        /// <summary>
        /// Attempts to parse the string provided into a ulong value. 
        /// </summary>
        /// <remarks>Returns 0 in the result parameter if the parse fails.</remarks>
        /// <param name="s">The string to attempt to parse.</param>
        /// <param name="result">The result of the parsed string, or zero if parsing failed.</param>
        /// <returns>A boolean value indicating whether or not the parse succeeded.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static bool TryParse(string s, out ulong result)
        {
            bool retVal = false;
#if WindowsCE
            try
            {
                result = (ulong)Convert.ToUInt64(s);
                retVal = true;
            }
            catch (FormatException) { result = 0; }
            catch (InvalidCastException) { result = 0; }
#else
            retVal = ulong.TryParse(s, out result);
#endif
            return retVal;
        }
        /// <summary>
        /// Attempts to parse the string provided into a ushort value. 
        /// </summary>
        /// <remarks>Returns 0 in the result parameter if the parse fails.</remarks>
        /// <param name="s">The string to attempt to parse.</param>
        /// <param name="result">The result of the parsed string, or zero if parsing failed.</param>
        /// <returns>A boolean value indicating whether or not the parse succeeded.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static bool TryParse(string s, out ushort result)
        {
            bool retVal = false;
#if WindowsCE
            try
            {
                result = (ushort)Convert.ToUInt64(s);
                retVal = true;
            }
            catch (FormatException) { result = 0; }
            catch (InvalidCastException) { result = 0; }
#else
            retVal = ushort.TryParse(s, out result);
#endif
            return retVal;
        }
        /// <summary>
        /// Attempts to parse the string provided into an <see cref="System.DateTime"/> value. 
        /// </summary>
        /// <remarks>Returns <see cref="System.DateTime.MinValue"/> in the result parameter if the parse fails.</remarks>
        /// <param name="s">The string to attempt to parse.</param>
        /// <param name="result">The result of the parsed string, or <see cref="System.DateTime.MinValue"/> if parsing failed.</param>
        /// <returns>A boolean value indicating whether or not the parse succeeded.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static bool TryParse(string s, out DateTime result)
        {
            bool retVal = false;
#if WindowsCE
            try
            {
                result = Convert.ToDateTime(s);
                retVal = true;
            }
            catch (FormatException) { result = DateTime.MinValue; }
            catch (InvalidCastException) { result = DateTime.MinValue; }
#else
            retVal = DateTime.TryParse(s, out result);
#endif
            return retVal;
        }
        /// <summary>
        /// Attempts to parse the string provided into an integer value. 
        /// </summary>
        /// <remarks>Returns false in the result parameter if the parse fails.</remarks>
        /// <param name="s">The string to attempt to parse.</param>
        /// <param name="result">The result of the parsed string, or false if parsing failed.</param>
        /// <returns>A boolean value indicating whether or not the parse succeeded.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static bool TryParse(string s, out bool result)
        {
            bool retVal = false;
#if WindowsCE
            try
            {
                result = Convert.ToBoolean(s);
                retVal = true;
            }
            catch (FormatException) { result = false; }
            catch (InvalidCastException) { result = false; }
#else
            retVal = bool.TryParse(s, out result);
#endif
            return retVal;
        }


        /// <summary>
        /// Attempts to convert the string representation of a number in a
        /// specified style and culture-specific format to its double-precision
        /// floating-point number equivalent.
        /// </summary>
        /// <param name="s">The string to attempt to parse.</param>
        /// <param name="style">
        /// A bitwise combination of <see cref="System.Globalization.NumberStyles"/>
        /// values that indicates the permitted format of <paramref name="s"/>.
        /// </param>
        /// <param name="provider">
        /// An <see cref="System.IFormatProvider"/> that supplies
        /// culture-specific formatting information about <paramref name="s"/>.
        /// </param>
        /// <param name="result">The result of the parsed string, or zero if parsing failed.</param>
        /// <returns>A boolean value indicating whether or not the parse succeeded.</returns>
        /// <remarks>Returns 0 in the result parameter if the parse fails.</remarks>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "s")]
        public static bool TryParse(string s, System.Globalization.NumberStyles style, IFormatProvider provider, out double result)
        {
            bool retVal = false;
#if WindowsCE
            try
            {
                result = double.Parse(s, style, provider);
                retVal = true;
            }
            catch (FormatException) { result = 0; }
            catch (InvalidCastException) { result = 0; }
#else
            retVal = double.TryParse(s, out result);
#endif
            return retVal;
        }
    }
}
