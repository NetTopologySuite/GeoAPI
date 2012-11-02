using System;
using NUnit.Framework;

namespace GeoAPI.Tests.IO.WellKnownBinary
{
    [TestFixture]
    public class WKBTests
    {
        [Test]
        [Ignore("Test not implemented")]
        public void WritingHomogenizedCoordinatesDoesNotWriteTheHomogenousOrdinate()
        {
            // I think I spotted a bug in the WKT writer which will cause this
            // test to fail.
        }

        [Test]
        [Ignore("Test not implemented")]
        public void Writing3DCoordinatesSucceeds()
        {

        }
    }
}