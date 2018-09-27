using System;
using GeoAPI.Geometries;
using NUnit.Framework;

namespace GeoAPI.Tests.Geometries
{
    public abstract class CoordinateTest<T> where T:CoordinateXY
    {
        protected bool ZIndexValid = false;
        protected bool MIndexValid = false;

        protected abstract T CreateCoordinate2D(double x, double y);
        protected abstract T CreateCoordinate2DM(double x, double y, double m = double.NaN);
        protected abstract T CreateCoordinate3D(double x, double y, double z = double.NaN);
        protected abstract T CreateCoordinate3DM(double x, double y, double z = double.NaN, double m = double.NaN);
        protected abstract T CreateCoordinate(T coordinate);

        protected abstract T CreateCoordinate();

        protected void CheckIndexer(T coordinate, Ordinate index, double value)
        {
            double val = double.NaN;
            if (IsIndexValid(index))
                Assert.AreEqual(value, coordinate[index]);
            else
                Assert.Throws<ArgumentOutOfRangeException>(() => val = coordinate[index]);
        }

        protected void CheckGetter(Ordinate index, double expected, double actual)
        {
            expected = CorrectExpected(index, expected);
            Assert.AreEqual(expected, actual);
        }

        private double CorrectExpected(Ordinate index, double expected)
        {
            if (!IsIndexValid(index))
                return GetDefault(index);
            return expected;
        }

        private double GetDefault(Ordinate index)
        {
            return double.NaN;
        }

        protected bool IsIndexValid(Ordinate ordinate)
        {
            if (ordinate < Ordinate.Ordinate2)
                return true;
            if (ordinate == Ordinate.Z && ZIndexValid)
                return true;
            if (ordinate == Ordinate.M && MIndexValid)
                return true;
            return false;
        }

        [Test]
        public void TestConstructor3D()
        {
            T c = CreateCoordinate3D(350.2, 4566.8, 5266.3);
            Assert.AreEqual(350.2, c.X);
            Assert.AreEqual(4566.8, c.Y);
            CheckGetter(Ordinate.Z, 5266.3, c.Z);
            CheckGetter(Ordinate.M, CoordinateXY.NullOrdinate, c.M);
        }

        [Test]
        public void TestConstructor2D()
        {
            T c = CreateCoordinate2D(350.2, 4566.8);
            Assert.AreEqual(350.2, c.X);
            Assert.AreEqual(4566.8, c.Y);
            CheckGetter(Ordinate.Z, CoordinateXY.NullOrdinate, c.Z);
            CheckGetter(Ordinate.M, CoordinateXY.NullOrdinate, c.M);
        }

        [Test]
        public void TestDefaultConstructor()
        {
            T c = CreateCoordinate();
            Assert.AreEqual(0d, c.X);
            Assert.AreEqual(0d, c.Y);
            CheckGetter(Ordinate.Z, CoordinateXY.NullOrdinate, c.Z);
            CheckGetter(Ordinate.M, CoordinateXY.NullOrdinate, c.M);
        }

        [Test]
        public void TestCopyConstructor3D()
        {
            T orig = CreateCoordinate3D(350.2, 4566.8, 5266.3);
            T c = CreateCoordinate(orig);
            Assert.AreEqual(350.2, c.X);
            Assert.AreEqual(4566.8, c.Y);
            CheckGetter(Ordinate.Z, 5266.3, c.Z);
            CheckGetter(Ordinate.M, CoordinateXY.NullOrdinate, c.M);
        }

        [Test]
        public void TestCopyMethod()
        {
            var orig = CreateCoordinate3D(350.2, 4566.8, 5266.3);
            var c = orig.Copy();
            Assert.That(c is T, Is.True);

            Assert.AreEqual(350.2, c.X);
            Assert.AreEqual(4566.8, c.Y);
            CheckGetter(Ordinate.Z, 5266.3, c.Z);
            CheckGetter(Ordinate.M, CoordinateXY.NullOrdinate, c.M);

            Assert.That(ReferenceEquals(orig, c), Is.False);
        }

        [Test]
        public void TestSetCoordinate()
        {
            T orig = CreateCoordinate3D(350.2, 4566.8, 5266.3);
            T c = CreateCoordinate();
            c.CoordinateValue = orig;

            Assert.AreNotSame(orig, c);

            Assert.AreEqual(c.X, 350.2);
            Assert.AreEqual(c.Y, 4566.8);
            CheckGetter(Ordinate.Z, 5266.3, c.Z);
            CheckGetter(Ordinate.M, CoordinateXY.NullOrdinate, c.M);
        }

        [Test]
        public void TestGetOrdinate2D()
        {
            T c = CreateCoordinate2D(350.2, 4566.8);
            Assert.AreEqual(c[Ordinate.X], 350.2);
            Assert.AreEqual(c[Ordinate.Y], 4566.8);
            CheckIndexer(c, Ordinate.Z, double.NaN);
            CheckIndexer(c, Ordinate.M, double.NaN);
        }

        [Test]
        public void TestGetOrdinate3D()
        {
            T c = CreateCoordinate3D(350.2, 4566.8, 5266.3);
            Assert.AreEqual(c[Ordinate.X], 350.2);
            Assert.AreEqual(c[Ordinate.Y], 4566.8);
            CheckIndexer(c, Ordinate.Z, 5266.3);
            CheckIndexer(c, Ordinate.M, double.NaN);
        }

        [Test]
        public void TestGetOrdinate3DM()
        {
            T c = CreateCoordinate3DM(350.2, 4566.8, 5266.3, 6226.4);
            Assert.AreEqual(c[Ordinate.X], 350.2);
            Assert.AreEqual(c[Ordinate.Y], 4566.8);
            CheckIndexer(c, Ordinate.Z, 5266.3);
            CheckIndexer(c, Ordinate.M, 6226.4);
        }

        [Test]
        public void TestGetOrdinate2DM()
        {
            T c = CreateCoordinate2DM(350.2, 4566.8, 6226.4);
            Assert.AreEqual(c[Ordinate.X], 350.2);
            Assert.AreEqual(c[Ordinate.Y], 4566.8);
            CheckIndexer(c, Ordinate.Z, double.NaN);
            CheckIndexer(c, Ordinate.M, 6226.4);
        }

        [Test]
        public void TestSetOrdinate()
        {
            T c = CreateCoordinate();
            c[Ordinate.X] = 111;
            c[Ordinate.Y] = 222;
            if (ZIndexValid)
                c[Ordinate.Z] = 333;
            else {
                Assert.Throws<ArgumentOutOfRangeException>(() => c[Ordinate.Z] = 333);
                Assert.Throws<InvalidOperationException>(() => c.Z = 333);
            }

            if (MIndexValid)
                c[Ordinate.M] = 444;
            else {
                Assert.Throws<ArgumentOutOfRangeException>(() => c[Ordinate.M] = 444);
                Assert.Throws<InvalidOperationException>(() => c.M = 444);
            }

            Assert.AreEqual(c[Ordinate.X], 111.0);
            Assert.AreEqual(c[Ordinate.Y], 222.0);
            CheckIndexer(c, Ordinate.Z, 333d);
            CheckIndexer(c, Ordinate.M, 444d);
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

            var c3 = new CoordinateXYZ(1, 22, 3);
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

            Assert.AreEqual(-1, lowest.CompareTo(highest));
            Assert.AreEqual(1, highest.CompareTo(lowest));
            Assert.AreEqual(-1, highest.CompareTo(higherStill));
            Assert.AreEqual(0, highest.CompareTo(equalToHighest));
        }

        /// <summary>
        /// Expected string when calling <see cref="T.ToString()"/> method for x=100, y=200, z=50, m=25
        /// </summary>
        protected abstract string ExpectedToString { get; }

        [Test]
        public void TestToString()
        {
            string actualResult = CreateCoordinate3DM(100, 200, 50, 25).ToString();
            Assert.AreEqual(ExpectedToString, actualResult);
        }

        [Test]
        public void TestClone()
        {
            T c = CreateCoordinate3D(100.0, 200.0, 50.0);
            T clone = (T)c.Clone();
            Assert.IsTrue(c.Equals(clone));

        }

        [Test]
        public void TestDistance()
        {
            T coord1 = CreateCoordinate3D(0.0, 0.0, 0.0);
            T coord2 = CreateCoordinate3D(100.0, 200.0, 50.0);
            double distance = coord1.Distance(coord2);
            Assert.AreEqual(223.60679774997897, distance, 0.00001);
        }



        [Test]
        public void TestSettingOrdinateValuesViaIndexer()
        {
            var c = CreateCoordinate();
            Assert.DoesNotThrow(() => c[Ordinate.X] = 1);
            Assert.AreEqual(1d, c.X);
            Assert.AreEqual(c.X, c[Ordinate.X]);

            Assert.DoesNotThrow(() => c[Ordinate.Y] = 2);
            Assert.AreEqual(2d, c.Y);
            Assert.AreEqual(c.Y, c[Ordinate.Y]);

            if (ZIndexValid)
            {
                Assert.DoesNotThrow(() => c[Ordinate.Z] = 3);
                Assert.AreEqual(3d, c.Z);
                Assert.AreEqual(c.Z, c[Ordinate.Z]);
            }
            else
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => c[Ordinate.Z] = 3);
            }
            if (MIndexValid)
            {
                Assert.DoesNotThrow(() => c[Ordinate.M] = 4);
                Assert.AreEqual(4d, c.M);
                Assert.AreEqual(4d, c[Ordinate.M]);
            }
            else
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => c[Ordinate.M] = 4);
            }

        }
    }

    /// <summary>
    /// Implementation for <see cref="CoordinateXY"/>
    /// </summary>
    public class CoordinateXYTest : CoordinateTest<CoordinateXY>
    {
        public CoordinateXYTest()
        {
            ZIndexValid = false;
            MIndexValid = false;
        }
        protected override CoordinateXY CreateCoordinate2D(double x, double y)
        {
            return new CoordinateXY(x, y);
        }
        protected override CoordinateXY CreateCoordinate2DM(double x, double y, double m = double.NaN)
        {
            return new CoordinateXY(x, y);
        }
        protected override CoordinateXY CreateCoordinate3D(double x, double y, double z = double.NaN)
        {
            return new CoordinateXY(x, y);
        }

        protected override CoordinateXY CreateCoordinate3DM(double x, double y, double z = double.NaN, double m = double.NaN)
        {
            return new CoordinateXY(x, y);
        }

        protected override CoordinateXY CreateCoordinate(CoordinateXY coordinate)
        {
            return new CoordinateXY(coordinate);
        }

        protected override CoordinateXY CreateCoordinate()
        {
            return new CoordinateXY();
        }

        protected override string ExpectedToString
        {
            get { return "(100, 200)"; }
        }
    }

    /// <summary>
    /// Implementation for <see cref="CoordinateXYM"/>
    /// </summary>
    public class CoordinateXYMTest : CoordinateTest<CoordinateXYM>
    {
        public CoordinateXYMTest()
        {
            ZIndexValid = false;
            MIndexValid = true;
        }
        protected override CoordinateXYM CreateCoordinate2D(double x, double y)
        {
            return new CoordinateXYM(x, y);
        }
        protected override CoordinateXYM CreateCoordinate2DM(double x, double y, double m = double.NaN)
        {
            return new CoordinateXYM(x, y, m);
        }
        protected override CoordinateXYM CreateCoordinate3D(double x, double y, double z = double.NaN)
        {
            return new CoordinateXYM(x, y);
        }

        protected override CoordinateXYM CreateCoordinate3DM(double x, double y, double z = double.NaN, double m = double.NaN)
        {
            return new CoordinateXYM(x, y, m);
        }

        protected override CoordinateXYM CreateCoordinate(CoordinateXYM coordinate)
        {
            return new CoordinateXYM(coordinate);
        }

        protected override CoordinateXYM CreateCoordinate()
        {
            return new CoordinateXYM();
        }

        protected override string ExpectedToString
        {
            get { return "(100, 200, m=25)"; }
        }
    }

    /// <summary>
    /// Implementation for <see cref="CoordinateXYZ"/>
    /// </summary>
    public class CoordinateXYZTest : CoordinateTest<CoordinateXYZ>
    {
        public CoordinateXYZTest()
        {
            ZIndexValid = true;
            MIndexValid = false;
        }

        protected override CoordinateXYZ CreateCoordinate2D(double x, double y)
        {
            return new CoordinateXYZ(x, y);
        }
        protected override CoordinateXYZ CreateCoordinate2DM(double x, double y, double m = double.NaN)
        {
            return new CoordinateXYZ(x, y);
        }
        protected override CoordinateXYZ CreateCoordinate3D(double x, double y, double z = double.NaN)
        {
            return new CoordinateXYZ(x, y, z);
        }
        protected override CoordinateXYZ CreateCoordinate3DM(double x, double y, double z = double.NaN, double m = double.NaN)
        {
            return new CoordinateXYZ(x, y, z);
        }
        protected override CoordinateXYZ CreateCoordinate(CoordinateXYZ coordinate)
        {
            return new CoordinateXYZ(coordinate);
        }
        protected override CoordinateXYZ CreateCoordinate()
        {
            return new CoordinateXYZ();
        }

        protected override string ExpectedToString
        {
            get { return "(100, 200, 50)"; }
        }

        [Test]
        public void TestDistance3D()
        {
            var coord1 = new CoordinateXYZ(0.0, 0.0, 0.0);
            var coord2 = new CoordinateXYZ(100.0, 200.0, 50.0);
            double distance = coord1.Distance3D(coord2);
            Assert.AreEqual(229.128784747792, distance, 0.000001);
        }

        [Test]
        public void TestEquals3D()
        {
            var c1 = new CoordinateXYZ(1, 2, 3);
            var c2 = new CoordinateXYZ(1, 2, 3);
            Assert.IsTrue(c1.Equals3D(c2));

            var c3 = new CoordinateXYZ(1, 22, 3);
            Assert.IsFalse(c1.Equals3D(c3));

            var c4 = new CoordinateXYZ(1, 2, 5);
            Assert.IsFalse(c1.EqualInZ(c4, 0));
            Assert.IsFalse(c1.Equals3D(c4));
        }


        [Test]
        public void TestEqualsInZ()
        {

            var c = new CoordinateXYZ(100.0, 200.0, 50.0);
            var withSameZ = new CoordinateXYZ(100.1, 200.1, 50.1);
            Assert.IsTrue(c.EqualInZ(withSameZ, 0.2));
        }
    }

    /// <summary>
    /// Implementation for <see cref="CoordinateXYZM"/>
    /// </summary>
    public class CoordinateXYZMTest : CoordinateTest<CoordinateXYZM>
    {
        public CoordinateXYZMTest()
        {
            ZIndexValid = true;
            MIndexValid = true;
        }

        protected override CoordinateXYZM CreateCoordinate2D(double x, double y)
        {
            return new CoordinateXYZM(x, y);
        }
        protected override CoordinateXYZM CreateCoordinate2DM(double x, double y, double m = double.NaN)
        {
            return new CoordinateXYZM(x, y, CoordinateXY.NullOrdinate, m);
        }
        protected override CoordinateXYZM CreateCoordinate3D(double x, double y, double z = double.NaN)
        {
            return new CoordinateXYZM(x, y, z, CoordinateXY.NullOrdinate);
        }
        protected override CoordinateXYZM CreateCoordinate3DM(double x, double y, double z = double.NaN, double m = double.NaN)
        {
            return new CoordinateXYZM(x, y, z, m);
        }
        protected override CoordinateXYZM CreateCoordinate(CoordinateXYZM coordinate)
        {
            return new CoordinateXYZM(coordinate);
        }
        protected override CoordinateXYZM CreateCoordinate()
        {
            return new CoordinateXYZM();
        }

        protected override string ExpectedToString
        {
            get { return "(100, 200, 50, m=25)"; }
        }

        [Test]
        public void TestDistance3D()
        {
            var coord1 = new CoordinateXYZ(0.0, 0.0, 0.0);
            var coord2 = new CoordinateXYZ(100.0, 200.0, 50.0);
            double distance = coord1.Distance3D(coord2);
            Assert.AreEqual(229.128784747792, distance, 0.000001);
        }

        [Test]
        public void TestEquals3D()
        {
            var c1 = new CoordinateXYZ(1, 2, 3);
            var c2 = new CoordinateXYZ(1, 2, 3);
            Assert.IsTrue(c1.Equals3D(c2));

            var c3 = new CoordinateXYZ(1, 22, 3);
            Assert.IsFalse(c1.Equals3D(c3));

            var c4 = new CoordinateXYZ(1, 2, 5);
            Assert.IsFalse(c1.EqualInZ(c4, 0));
            Assert.IsFalse(c1.Equals3D(c4));
        }


        [Test]
        public void TestEqualsInZ()
        {

            var c = new CoordinateXYZ(100.0, 200.0, 50.0);
            var withSameZ = new CoordinateXYZ(100.1, 200.1, 50.1);
            Assert.IsTrue(c.EqualInZ(withSameZ, 0.2));
        }
    }

 }