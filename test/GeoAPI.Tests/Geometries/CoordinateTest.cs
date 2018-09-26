﻿using System;
using GeoAPI.Geometries;
using NUnit.Framework;

namespace GeoAPI.Tests.Geometries
{
    public class CoordinateTest
    {
        [Test]
        public void TestConstructor3D()
        {
            Coordinate c = new Coordinate(350.2, 4566.8, 5266.3);
            Assert.AreEqual(c.X, 350.2);
            Assert.AreEqual(c.Y, 4566.8);
            Assert.AreEqual(c.Z, 5266.3);
            Assert.AreEqual(c.M, Coordinate.NullOrdinate);
        }

        [Test]
        public void TestConstructor2D()
        {
            Coordinate c = new Coordinate(350.2, 4566.8);
            Assert.AreEqual(c.X, 350.2);
            Assert.AreEqual(c.Y, 4566.8);
            Assert.AreEqual(c.Z, Coordinate.NullOrdinate);
            Assert.AreEqual(c.M, Coordinate.NullOrdinate);
        }

        [Test]
        public void TestDefaultConstructor()
        {
            Coordinate c = new Coordinate();
            Assert.AreEqual(c.X, 0.0);
            Assert.AreEqual(c.Y, 0.0);
            Assert.AreEqual(c.Z, Coordinate.NullOrdinate);
            Assert.AreEqual(c.M, Coordinate.NullOrdinate);
        }

        [Test]
        public void TestCopyConstructor3D()
        {
            Coordinate orig = new Coordinate(350.2, 4566.8, 5266.3);
            Coordinate c = new Coordinate(orig);
            Assert.AreEqual(c.X, 350.2);
            Assert.AreEqual(c.Y, 4566.8);
            Assert.AreEqual(c.Z, 5266.3);
            Assert.AreEqual(c.M, Coordinate.NullOrdinate);
        }

        [Test]
        public void TestCopyMethod()
        {
            Coordinate orig = new Coordinate(350.2, 4566.8, 5266.3);
            Coordinate c = orig.Copy();
            Assert.AreEqual(c.X, 350.2);
            Assert.AreEqual(c.Y, 4566.8);
            Assert.AreEqual(c.Z, 5266.3);
            Assert.AreEqual(c.M, Coordinate.NullOrdinate);

            Assert.That(ReferenceEquals(orig, c), Is.False);
        }

        [Test]
        public void TestSetCoordinate()
        {
            Coordinate orig = new Coordinate(350.2, 4566.8, 5266.3);
            Coordinate c = new Coordinate { CoordinateValue = orig };
            Assert.AreEqual(c.X, 350.2);
            Assert.AreEqual(c.Y, 4566.8);
            Assert.AreEqual(c.Z, 5266.3);
            Assert.AreEqual(c.M, Coordinate.NullOrdinate);
        }

        [Test]
        public void TestGetOrdinate()
        {
            Coordinate c = new Coordinate(350.2, 4566.8, 5266.3);
            Assert.AreEqual(c[Ordinate.X], 350.2);
            Assert.AreEqual(c[Ordinate.Y], 4566.8);
            Assert.AreEqual(c[Ordinate.Z], 5266.3);
            Assert.AreEqual(c.M, Coordinate.NullOrdinate);
        }

        [Test]
        public void TestSetOrdinate()
        {
            Coordinate c = new Coordinate();
            c[Ordinate.X] = 111;
            c[Ordinate.Y] = 222;
            c[Ordinate.Z] = 333;
            Assert.Throws<ArgumentOutOfRangeException>(() => c[Ordinate.M] = 444);
            Assert.AreEqual(c[Ordinate.X], 111.0);
            Assert.AreEqual(c[Ordinate.Y], 222.0);
            Assert.AreEqual(c[Ordinate.Z], 333.0);
            Assert.AreEqual(c.M, Coordinate.NullOrdinate);
        }

        [Test]
        public void TestEquals()
        {
            Coordinate c1 = new Coordinate(1, 2, 3);
            const string s = "Not a coordinate";
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.IsFalse(c1.Equals(s));

            Coordinate c2 = new Coordinate(1, 2, 3);
            Assert.IsTrue(c1.Equals2D(c2));

            Coordinate c3 = new Coordinate(1, 22, 3);
            Assert.IsFalse(c1.Equals2D(c3));
        }

        [Test]
        public void TestEquals2D()
        {
            Coordinate c1 = new Coordinate(1, 2, 3);
            Coordinate c2 = new Coordinate(1, 2, 3);
            Assert.IsTrue(c1.Equals2D(c2));

            Coordinate c3 = new Coordinate(1, 22, 3);
            Assert.IsFalse(c1.Equals2D(c3));
        }

        [Test]
        public void TestEquals3D()
        {
            Coordinate c1 = new Coordinate(1, 2, 3);
            Coordinate c2 = new Coordinate(1, 2, 3);
            Assert.IsTrue(c1.Equals3D(c2));

            Coordinate c3 = new Coordinate(1, 22, 3);
            Assert.IsFalse(c1.Equals3D(c3));
        }

        [Test]
        public void TestEquals2DWithinTolerance()
        {
            Coordinate c = new Coordinate(100.0, 200.0, 50.0);
            Coordinate aBitOff = new Coordinate(100.1, 200.1, 50.0);
            Assert.IsTrue(c.Equals2D(aBitOff, 0.2));
        }

        [Test]
        public void TestEqualsInZ()
        {

            Coordinate c = new Coordinate(100.0, 200.0, 50.0);
            Coordinate withSameZ = new Coordinate(100.1, 200.1, 50.1);
            Assert.IsTrue(c.EqualInZ(withSameZ, 0.2));
        }

        [Test]
        public void TestCompareTo()
        {
            Coordinate lowest = new Coordinate(10.0, 100.0, 50.0);
            Coordinate highest = new Coordinate(20.0, 100.0, 50.0);
            Coordinate equalToHighest = new Coordinate(20.0, 100.0, 50.0);
            Coordinate higherStill = new Coordinate(20.0, 200.0, 50.0);

            Assert.AreEqual(-1, lowest.CompareTo(highest));
            Assert.AreEqual(1, highest.CompareTo(lowest));
            Assert.AreEqual(-1, highest.CompareTo(higherStill));
            Assert.AreEqual(0, highest.CompareTo(equalToHighest));
        }

        [Test]
        public void TestToString()
        {
            const string expectedResult = "(100, 200, 50)";
            String actualResult = new Coordinate(100, 200, 50).ToString();
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestClone()
        {
            Coordinate c = new Coordinate(100.0, 200.0, 50.0);
            Coordinate clone = (Coordinate)c.Clone();
            Assert.IsTrue(c.Equals3D(clone));
        }

        [Test]
        public void TestDistance()
        {
            Coordinate coord1 = new Coordinate(0.0, 0.0, 0.0);
            Coordinate coord2 = new Coordinate(100.0, 200.0, 50.0);
            double distance = coord1.Distance(coord2);
            Assert.AreEqual(distance, 223.60679774997897, 0.00001);
        }

        [Test]
        public void TestDistance3D()
        {
            Coordinate coord1 = new Coordinate(0.0, 0.0, 0.0);
            Coordinate coord2 = new Coordinate(100.0, 200.0, 50.0);
            double distance = coord1.Distance3D(coord2);
            Assert.AreEqual(distance, 229.128784747792, 0.000001);
        }

        [Test]
        public void TestSettingOrdinateValuesViaIndexer()
        {
            var c = new Coordinate();
            Assert.DoesNotThrow(() => c[Ordinate.X] = 1);
            Assert.AreEqual(1d, c.X);
            Assert.AreEqual(c.X, c[Ordinate.X]);

            Assert.DoesNotThrow(() => c[Ordinate.Y] = 2);
            Assert.AreEqual(2d, c.Y);
            Assert.AreEqual(c.Y, c[Ordinate.Y]);

            Assert.DoesNotThrow(() => c[Ordinate.Z] = 3);
            Assert.AreEqual(3d, c.Z);
            Assert.AreEqual(c.Z, c[Ordinate.Z]);

            Assert.Throws<ArgumentOutOfRangeException>(() => c[Ordinate.M] = 4);
        }
    }
}