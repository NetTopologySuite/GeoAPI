using System;

namespace GeoAPI
{
    public static class BitConverterEx
    {
        public static Int64 DoubleToInt64Bits(this double self)
        {
            var tmp = BitConverter.GetBytes(self);
            return BitConverter.ToInt64(tmp, 0);
        }

        public static Double Int64BitsToDouble(this Int64 self)
        {
            var tmp = BitConverter.GetBytes(self);
            return BitConverter.ToDouble(tmp, 0);
        }
    }
}