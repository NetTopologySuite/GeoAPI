using System;
using GeoAPI.Geometries;
using NUnit.Framework;

namespace GeoAPI.Tests.Geometries
{
    public abstract class CoordinateBaseTest<T> where T:Coordinate
    {
        protected int? ZOrdinateIndex = null;
        protected int? MOrdinateIndex = null;

        protected int OrdinateCount => 2 + (ZOrdinateIndex.HasValue ? 1 : 0) + (MOrdinateIndex.HasValue ? 1 : 0);

        protected abstract T CreateCoordinate2D(double x, double y);
        protected abstract T CreateCoordinate2DM(double x, double y, double m = double.NaN);
        protected abstract T CreateCoordinate3D(double x, double y, double z = double.NaN);
        protected abstract T CreateCoordinate3DM(double x, double y, double z = double.NaN, double m = double.NaN);
        protected abstract T CreateCoordinate(T coordinate);

        protected abstract T CreateCoordinate();

        protected void CheckIndexer(T coordinate, int index, double value)
        {
            if (index < OrdinateCount)
            {
                Assert.AreEqual(value, coordinate[index]);
            }
            else
            {
                double val;
                Assert.That(() => val = coordinate[index], Throws.InstanceOf<ArgumentOutOfRangeException>());
            }
        }

        protected void CheckXYZM(Coordinate c, double expectedX, double expectedY, double expectedZ, double expectedM)
        {
            Assert.AreEqual(expectedX, c.X);
            Assert.AreEqual(expectedX, c[0]);

            Assert.AreEqual(expectedY, c.Y);
            Assert.AreEqual(expectedY, c[1]);

            if (ZOrdinateIndex.HasValue)
            {
                Assert.AreEqual(expectedZ, c.Z);
                Assert.AreEqual(expectedZ, c[ZOrdinateIndex.Value]);
            }
            else
            {
                Assert.AreEqual(Coordinate.NullOrdinate, c.Z);
            }

            if (MOrdinateIndex.HasValue)
            {
                Assert.AreEqual(expectedM, c.M);
                Assert.AreEqual(expectedM, c[MOrdinateIndex.Value]);
            }
            else
            {
                Assert.AreEqual(Coordinate.NullOrdinate, c.M);
            }
        }

        [Test]
        public void TestConstructor3D()
        {
            T c = CreateCoordinate3D(350.2, 4566.8, 5266.3);
            CheckXYZM(c, 350.2, 4566.8, 5266.3, Coordinate.NullOrdinate);
        }

        [Test]
        public void TestConstructor2D()
        {
            T c = CreateCoordinate2D(350.2, 4566.8);
            CheckXYZM(c, 350.2, 4566.8, Coordinate.NullOrdinate, Coordinate.NullOrdinate);
        }

        [Test]
        public void TestDefaultConstructor()
        {
            T c = CreateCoordinate();
            CheckXYZM(c, 0, 0, Coordinate.NullOrdinate, Coordinate.NullOrdinate);
        }

        [Test]
        public void TestCopyConstructor3D()
        {
            T orig = CreateCoordinate3D(350.2, 4566.8, 5266.3);
            T c = CreateCoordinate(orig);
            CheckXYZM(c, 350.2, 4566.8, 5266.3, Coordinate.NullOrdinate);
        }

        [Test]
        public void TestCopyMethod()
        {
            var orig = CreateCoordinate3D(350.2, 4566.8, 5266.3);
            var c = orig.Copy();
            Assert.That(c, Is.TypeOf<T>());

            CheckXYZM(c, 350.2, 4566.8, 5266.3, Coordinate.NullOrdinate);

            Assert.That(c, Is.Not.SameAs(orig));
        }

        [Test]
        public void TestSetCoordinate()
        {
            T orig = CreateCoordinate3D(350.2, 4566.8, 5266.3);
            T c = CreateCoordinate();
            c.CoordinateValue = orig;

            Assert.AreNotSame(orig, c);

            CheckXYZM(c, 350.2, 4566.8, 5266.3, Coordinate.NullOrdinate);
        }

        [Test]
        public void TestGetOrdinate2D()
        {
            T c = CreateCoordinate2D(350.2, 4566.8);
            CheckXYZM(c, 350.2, 4566.8, Coordinate.NullOrdinate, Coordinate.NullOrdinate);
        }

        [Test]
        public void TestGetOrdinate3D()
        {
            T c = CreateCoordinate3D(350.2, 4566.8, 5266.3);
            CheckXYZM(c, 350.2, 4566.8, 5266.3, Coordinate.NullOrdinate);
        }

        [Test]
        public void TestGetOrdinate3DM()
        {
            T c = CreateCoordinate3DM(350.2, 4566.8, 5266.3, 6226.4);
            CheckXYZM(c, 350.2, 4566.8, 5266.3, 6226.4);
        }

        [Test]
        public void TestGetOrdinate2DM()
        {
            T c = CreateCoordinate2DM(350.2, 4566.8, 6226.4);
            CheckXYZM(c, 350.2, 4566.8, Coordinate.NullOrdinate, 6226.4);
        }

        [Test]
        public void TestSetOrdinate()
        {
            T c = CreateCoordinate();
            c[0] = 111;
            c[1] = 222;

            if (ZOrdinateIndex.HasValue)
            {
                c[ZOrdinateIndex.Value] = 333;
            }
            else
            {
                Assert.That(() => c.Z = 333, Throws.InvalidOperationException);
            }

            if (MOrdinateIndex.HasValue)
            {
                c[MOrdinateIndex.Value] = 444;
            }
            else
            {
                Assert.That(() => c.M = 444, Throws.InvalidOperationException);
            }

            CheckXYZM(c, 111, 222, 333, 444);

            if (2 < OrdinateCount)
            {
                c[2] = 555;
                Assert.AreEqual(555, c[2]);
            }
            else
            {
                Assert.That(() => c[2] = 555, Throws.TypeOf<ArgumentOutOfRangeException>());
            }

            if (3 < OrdinateCount)
            {
                c[3] = 666;
                Assert.AreEqual(666, c[3]);
            }
            else
            {
                Assert.That(() => c[3] = 666, Throws.TypeOf<ArgumentOutOfRangeException>());
            }
        }

        [Test]
        public void TestEquals()
        {
            T c1 = CreateCoordinate3D(1, 2, 3);
            const string s = "Not a coordinate";
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.IsFalse(c1.Equals(s));

            T c2 = CreateCoordinate3D(1, 2, 3);
            Assert.IsTrue(c1.Equals2D(c2));

            var c3 = new CoordinateZ(1, 22, 3);
            Assert.IsFalse(c1.Equals2D(c3));
        }

        [Test]
        public void TestEquals2D()
        {
            T c1 = CreateCoordinate3D(1, 2, 3);
            T c2 = CreateCoordinate3D(1, 2, 3);
            Assert.IsTrue(c1.Equals2D(c2));

            T c3 = CreateCoordinate3D(1, 22, 3);
            Assert.IsFalse(c1.Equals2D(c3));
        }

        [Test]
        public void TestEquals2DWithinTolerance()
        {
            T c = CreateCoordinate3D(100.0, 200.0, 50.0);
            T aBitOff = CreateCoordinate3D(100.1, 200.1, 50.0);
            Assert.IsTrue(c.Equals2D(aBitOff, 0.2));
        }

        [Test]
        public void TestCompareTo()
        {
            T lowest = CreateCoordinate3D(10.0, 100.0, 50.0);
            T highest = CreateCoordinate3D(20.0, 100.0, 50.0);
            T equalToHighest = CreateCoordinate3D(20.0, 100.0, 50.0);
            T higherStill = CreateCoordinate3D(20.0, 200.0, 50.0);

            Assert.AreEqual(-1, lowest.CompareTo((object)highest));
            Assert.AreEqual(-1, lowest.CompareTo(highest));
            Assert.AreEqual(1, highest.CompareTo((object)lowest));
            Assert.AreEqual(1, highest.CompareTo(lowest));
            Assert.AreEqual(-1, highest.CompareTo((object)higherStill));
            Assert.AreEqual(-1, highest.CompareTo(higherStill));
            Assert.AreEqual(0, highest.CompareTo((object)equalToHighest));
            Assert.AreEqual(0, highest.CompareTo(equalToHighest));

            // Invalid arguments
            Assert.That(() => lowest.CompareTo((object)null), Throws.InstanceOf<ArgumentNullException>());
            Assert.That(() => lowest.CompareTo(new object()), Throws.InstanceOf<ArgumentException>());

            Assert.That(() => lowest.CompareTo((T)null), Throws.InstanceOf<ArgumentNullException>());
        }

        /// <summary>
        /// Expected string when calling <see cref="object.ToString()"/> method for x=100, y=200, z=50, m=25
        /// </summary>
        protected abstract string ExpectedToString { get; }

        [Test]
        public void TestToString()
        {
            string actualResult = CreateCoordinate3DM(100, 200, 50, 25).ToString();
            Assert.AreEqual(ExpectedToString, actualResult);
        }

        [Test]
        public void TestDistance()
        {
            T coord1 = CreateCoordinate3D(0.0, 0.0, 0.0);
            T coord2 = CreateCoordinate3D(100.0, 200.0, 50.0);
            double distance = coord1.Distance(coord2);
            Assert.AreEqual(223.60679774997897, distance, 0.00001);
        }
    }

    /// <summary>
    /// Implementation for <see cref="Coordinate"/>
    /// </summary>
    public class CoordinateTest : CoordinateBaseTest<Coordinate>
    {
        public CoordinateTest()
        {
            ZOrdinateIndex = null;
            MOrdinateIndex = null;
        }
        protected override Coordinate CreateCoordinate2D(double x, double y)
        {
            return new Coordinate(x, y);
        }
        protected override Coordinate CreateCoordinate2DM(double x, double y, double m = double.NaN)
        {
            return new Coordinate(x, y);
        }
        protected override Coordinate CreateCoordinate3D(double x, double y, double z = double.NaN)
        {
            return new Coordinate(x, y);
        }

        protected override Coordinate CreateCoordinate3DM(double x, double y, double z = double.NaN, double m = double.NaN)
        {
            return new Coordinate(x, y);
        }

        protected override Coordinate CreateCoordinate(Coordinate coordinate)
        {
            return new Coordinate(coordinate);
        }

        protected override Coordinate CreateCoordinate()
        {
            return new Coordinate();
        }

        protected override string ExpectedToString
        {
            get { return "(100, 200)"; }
        }
    }

    /// <summary>
    /// Implementation for <see cref="CoordinateM"/>
    /// </summary>
    public class CoordinateMTest : CoordinateBaseTest<CoordinateM>
    {
        public CoordinateMTest()
        {
            ZOrdinateIndex = null;
            MOrdinateIndex = 2;
        }
        protected override CoordinateM CreateCoordinate2D(double x, double y)
        {
            return new CoordinateM(x, y);
        }
        protected override CoordinateM CreateCoordinate2DM(double x, double y, double m = double.NaN)
        {
            return new CoordinateM(x, y, m);
        }
        protected override CoordinateM CreateCoordinate3D(double x, double y, double z = double.NaN)
        {
            return new CoordinateM(x, y);
        }

        protected override CoordinateM CreateCoordinate3DM(double x, double y, double z = double.NaN, double m = double.NaN)
        {
            return new CoordinateM(x, y, m);
        }

        protected override CoordinateM CreateCoordinate(CoordinateM coordinate)
        {
            return new CoordinateM(coordinate);
        }

        protected override CoordinateM CreateCoordinate()
        {
            return new CoordinateM();
        }

        protected override string ExpectedToString
        {
            get { return "(100, 200, m=25)"; }
        }
    }

    /// <summary>
    /// Implementation for <see cref="CoordinateZ"/>
    /// </summary>
    public class CoordinateZTest : CoordinateBaseTest<CoordinateZ>
    {
        public CoordinateZTest()
        {
            ZOrdinateIndex = 2;
            MOrdinateIndex = null;
        }

        protected override CoordinateZ CreateCoordinate2D(double x, double y)
        {
            return new CoordinateZ(x, y);
        }
        protected override CoordinateZ CreateCoordinate2DM(double x, double y, double m = double.NaN)
        {
            return new CoordinateZ(x, y);
        }
        protected override CoordinateZ CreateCoordinate3D(double x, double y, double z = double.NaN)
        {
            return new CoordinateZ(x, y, z);
        }
        protected override CoordinateZ CreateCoordinate3DM(double x, double y, double z = double.NaN, double m = double.NaN)
        {
            return new CoordinateZ(x, y, z);
        }
        protected override CoordinateZ CreateCoordinate(CoordinateZ coordinate)
        {
            return new CoordinateZ(coordinate);
        }
        protected override CoordinateZ CreateCoordinate()
        {
            return new CoordinateZ();
        }

        protected override string ExpectedToString
        {
            get { return "(100, 200, 50)"; }
        }

        [Test]
        public void TestDistance3D()
        {
            var coord1 = new CoordinateZ(0.0, 0.0, 0.0);
            var coord2 = new CoordinateZ(100.0, 200.0, 50.0);
            double distance = coord1.Distance3D(coord2);
            Assert.AreEqual(229.128784747792, distance, 0.000001);
        }

        [Test]
        public void TestEquals3D()
        {
            var c1 = new CoordinateZ(1, 2, 3);
            var c2 = new CoordinateZ(1, 2, 3);
            Assert.IsTrue(c1.Equals3D(c2));

            var c3 = new CoordinateZ(1, 22, 3);
            Assert.IsFalse(c1.Equals3D(c3));

            var c4 = new CoordinateZ(1, 2, 5);
            Assert.IsFalse(c1.EqualInZ(c4, 0));
            Assert.IsFalse(c1.Equals3D(c4));
        }


        [Test]
        public void TestEqualsInZ()
        {

            var c = new CoordinateZ(100.0, 200.0, 50.0);
            var withSameZ = new CoordinateZ(100.1, 200.1, 50.1);
            Assert.IsTrue(c.EqualInZ(withSameZ, 0.2));
        }
    }

    /// <summary>
    /// Implementation for <see cref="CoordinateZM"/>
    /// </summary>
    public class CoordinateZMTest : CoordinateBaseTest<CoordinateZM>
    {
        public CoordinateZMTest()
        {
            ZOrdinateIndex = 2;
            MOrdinateIndex = 3;
        }

        protected override CoordinateZM CreateCoordinate2D(double x, double y)
        {
            return new CoordinateZM(x, y);
        }
        protected override CoordinateZM CreateCoordinate2DM(double x, double y, double m = double.NaN)
        {
            return new CoordinateZM(x, y, Coordinate.NullOrdinate, m);
        }
        protected override CoordinateZM CreateCoordinate3D(double x, double y, double z = double.NaN)
        {
            return new CoordinateZM(x, y, z, Coordinate.NullOrdinate);
        }
        protected override CoordinateZM CreateCoordinate3DM(double x, double y, double z = double.NaN, double m = double.NaN)
        {
            return new CoordinateZM(x, y, z, m);
        }
        protected override CoordinateZM CreateCoordinate(CoordinateZM coordinate)
        {
            return new CoordinateZM(coordinate);
        }
        protected override CoordinateZM CreateCoordinate()
        {
            return new CoordinateZM();
        }

        protected override string ExpectedToString
        {
            get { return "(100, 200, 50, m=25)"; }
        }

        [Test]
        public void TestDistance3D()
        {
            var coord1 = new CoordinateZ(0.0, 0.0, 0.0);
            var coord2 = new CoordinateZ(100.0, 200.0, 50.0);
            double distance = coord1.Distance3D(coord2);
            Assert.AreEqual(229.128784747792, distance, 0.000001);
        }

        [Test]
        public void TestEquals3D()
        {
            var c1 = new CoordinateZ(1, 2, 3);
            var c2 = new CoordinateZ(1, 2, 3);
            Assert.IsTrue(c1.Equals3D(c2));

            var c3 = new CoordinateZ(1, 22, 3);
            Assert.IsFalse(c1.Equals3D(c3));

            var c4 = new CoordinateZ(1, 2, 5);
            Assert.IsFalse(c1.EqualInZ(c4, 0));
            Assert.IsFalse(c1.Equals3D(c4));
        }


        [Test]
        public void TestEqualsInZ()
        {

            var c = new CoordinateZ(100.0, 200.0, 50.0);
            var withSameZ = new CoordinateZ(100.1, 200.1, 50.1);
            Assert.IsTrue(c.EqualInZ(withSameZ, 0.2));
        }
    }

 }