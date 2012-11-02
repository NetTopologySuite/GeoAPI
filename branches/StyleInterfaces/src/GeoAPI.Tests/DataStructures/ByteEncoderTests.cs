using System;
using GeoAPI.DataStructures;
using NUnit.Framework;

namespace GeoAPI.Tests.DataStructures
{
    [TestFixture]
    public class ByteEncoderTests
    {
        [Test]
        public void ReversingByteOrderOfInt32ShouldBeReversed()
        {
            unchecked
            {
                if (BitConverter.IsLittleEndian)
                {
                    Assert.AreEqual(0x01000000, ByteEncoder.GetBigEndian(1));
                    Assert.AreEqual((Int32) 0xFFFFFF7F, ByteEncoder.GetBigEndian(Int32.MaxValue));
                    Assert.AreEqual(0x00000080, ByteEncoder.GetBigEndian(Int32.MinValue));
                }
                else
                {
                    Assert.AreEqual(0x01000000, ByteEncoder.GetLittleEndian(1));
                    Assert.AreEqual((Int32) 0xFFFFFF7F, ByteEncoder.GetLittleEndian(Int32.MaxValue));
                    Assert.AreEqual(0x00000080, ByteEncoder.GetLittleEndian(Int32.MinValue));
                }
            }
        }

        [Test]
        public void ReversingByteOrderOfUInt32ShouldBeReversed()
        {
            if (BitConverter.IsLittleEndian)
            {
                Assert.AreEqual(0x01000000, ByteEncoder.GetBigEndian(1U));
                Assert.AreEqual(0xFFFFFFFF, ByteEncoder.GetBigEndian(UInt32.MaxValue));
                Assert.AreEqual(0x00000000, ByteEncoder.GetBigEndian(UInt32.MinValue));
                Assert.AreEqual(0x87654321, ByteEncoder.GetBigEndian(0x21436587U));
            }
            else
            {
                Assert.AreEqual(0x01000000, ByteEncoder.GetLittleEndian(1U));
                Assert.AreEqual(0xFFFFFFFF, ByteEncoder.GetLittleEndian(UInt32.MaxValue));
                Assert.AreEqual(0x00000080, ByteEncoder.GetLittleEndian(UInt32.MinValue));
                Assert.AreEqual(0x87654321, ByteEncoder.GetLittleEndian(0x21436587U));
            }
        }

        [Test]
        public void ReversingByteOrderOfUInt16ShouldBeReversed()
        {
            if (BitConverter.IsLittleEndian)
            {
                Assert.AreEqual(0x0100, ByteEncoder.GetBigEndian((UInt16) 1));
                Assert.AreEqual(0xFFFF, ByteEncoder.GetBigEndian(UInt16.MaxValue));
                Assert.AreEqual(0x0000, ByteEncoder.GetBigEndian(UInt16.MinValue));
                Assert.AreEqual(0x4321, ByteEncoder.GetBigEndian((UInt16) 0x2143));
            }
            else
            {
                Assert.AreEqual(0x0100, ByteEncoder.GetLittleEndian((UInt16) 1));
                Assert.AreEqual(0xFFFF, ByteEncoder.GetLittleEndian(UInt16.MaxValue));
                Assert.AreEqual(0x0000, ByteEncoder.GetLittleEndian(UInt16.MinValue));
                Assert.AreEqual(0x4321, ByteEncoder.GetLittleEndian((UInt16) 0x2143));
            }
        }


        [Test]
        public void ReversingByteOrderOfDoublesShouldBeReversed()
        {
            if (BitConverter.IsLittleEndian)
            {
                Assert.AreEqual(1.0, ByteEncoder.GetBigEndian(ByteEncoder.GetBigEndian(1.0)));
                Assert.AreEqual(-1.0, ByteEncoder.GetBigEndian(ByteEncoder.GetBigEndian(-1.0)));
                Assert.AreEqual(Double.Epsilon, ByteEncoder.GetBigEndian(ByteEncoder.GetBigEndian(Double.Epsilon)));
                Assert.AreEqual(Double.MinValue, ByteEncoder.GetBigEndian(ByteEncoder.GetBigEndian(Double.MinValue)));
                Assert.AreEqual(Double.MaxValue, ByteEncoder.GetBigEndian(ByteEncoder.GetBigEndian(Double.MaxValue)));
                Assert.AreEqual(Double.PositiveInfinity,
                                ByteEncoder.GetBigEndian(ByteEncoder.GetBigEndian(Double.PositiveInfinity)));
                Assert.AreEqual(Double.NegativeInfinity,
                                ByteEncoder.GetBigEndian(ByteEncoder.GetBigEndian(Double.NegativeInfinity)));
                Assert.AreEqual(Double.NaN, ByteEncoder.GetBigEndian(ByteEncoder.GetBigEndian(Double.NaN)));
                Assert.AreEqual(0.0, ByteEncoder.GetBigEndian(ByteEncoder.GetBigEndian(0.0)));
                Assert.AreEqual(-0.0, ByteEncoder.GetBigEndian(ByteEncoder.GetBigEndian(-0.0)));
                Assert.AreEqual(1.0/3.0, ByteEncoder.GetBigEndian(ByteEncoder.GetBigEndian(1.0/3.0)));
            }
            else
            {
                Assert.AreEqual(1.0, ByteEncoder.GetLittleEndian(ByteEncoder.GetLittleEndian(1.0)));
                Assert.AreEqual(-1.0, ByteEncoder.GetLittleEndian(ByteEncoder.GetLittleEndian(-1.0)));
                Assert.AreEqual(Double.Epsilon, ByteEncoder.GetLittleEndian(ByteEncoder.GetLittleEndian(Double.Epsilon)));
                Assert.AreEqual(Double.MinValue,
                                ByteEncoder.GetLittleEndian(ByteEncoder.GetLittleEndian(Double.MinValue)));
                Assert.AreEqual(Double.MaxValue,
                                ByteEncoder.GetLittleEndian(ByteEncoder.GetLittleEndian(Double.MaxValue)));
                Assert.AreEqual(Double.PositiveInfinity,
                                ByteEncoder.GetLittleEndian(ByteEncoder.GetLittleEndian(Double.PositiveInfinity)));
                Assert.AreEqual(Double.NegativeInfinity,
                                ByteEncoder.GetLittleEndian(ByteEncoder.GetLittleEndian(Double.NegativeInfinity)));
                Assert.AreEqual(Double.NaN, ByteEncoder.GetLittleEndian(ByteEncoder.GetLittleEndian(Double.NaN)));
                Assert.AreEqual(0.0, ByteEncoder.GetLittleEndian(ByteEncoder.GetLittleEndian(0.0)));
                Assert.AreEqual(-0.0, ByteEncoder.GetLittleEndian(ByteEncoder.GetLittleEndian(-0.0)));
                Assert.AreEqual(1.0/3.0, ByteEncoder.GetLittleEndian(ByteEncoder.GetLittleEndian(1.0/3.0)));
            }
        }
    }
}