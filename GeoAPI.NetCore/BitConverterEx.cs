using System;

namespace GeoAPI
{
    /// <summary>
    /// A framework replacement for the System.BitConverter class
    /// </summary>
    /// <remarks>Only partial functionality is provided!</remarks>
    internal static class BitConverterEx
    {
        /// <summary>
        /// Function to convert the bits of a double to a the bits of a long
        /// </summary>
        /// <param name="self">The double value</param>
        /// <returns>The long value</returns>
        public static Int64 DoubleToInt64Bits(double self)
        {
            var tmp = BitConverter.GetBytes(self);
            return BitConverter.ToInt64(tmp, 0);
        }

        /// <summary>
        /// Function to convert the bits of a long to a the bits of a double
        /// </summary>
        /// <param name="self">The long value</param>
        /// <returns>The double value</returns>
        public static Double Int64BitsToDouble(Int64 self)
        {
            var tmp = BitConverter.GetBytes(self);
            return BitConverter.ToDouble(tmp, 0);
        }
    }

    /// <summary>
    /// A framework replacement for the System.ICloneable interface.
    /// </summary>
    public interface ICloneable
    {
        /// <summary>
        /// Function to create a new object that is a (deep) copy of the current instance.
        /// </summary>
        /// <returns>A new object that is a copy of this instance.</returns>
        object Clone();
    }
}