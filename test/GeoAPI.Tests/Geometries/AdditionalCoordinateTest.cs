﻿using System;
using GeoAPI.Geometries;
using NUnit.Framework;

namespace GeoAPI.Tests.Geometries
{
    public class AdditionalCoordinateTest
    {
        [Test]
        public void TestCoordinateXY()
        {
            var xy = new CoordinateXY();
            checkZUnsupported(xy);
            checkMUnsupported(xy);

            xy = new CoordinateXY(1.0, 1.0);   // 2D
            var coord = new CoordinateXY(xy); // copy
            Assert.AreEqual(xy, coord);
            Assert.AreEqual(0, coord.CompareTo(xy));
        }

        [Test]
        public void TestCoordinateXYM()
        {
            var xym = new CoordinateXYM();
            checkZUnsupported(xym);

            xym.M = 1.0;
            Assert.AreEqual(1.0, xym.M);

            var coord = new CoordinateXYM(xym); // copy
            Assert.AreEqual(xym, coord);
        }

        [Test]
        public void TestCoordinateXYZ()
        {
            var xy = new CoordinateXYZ();
            checkMUnsupported(xy);

            xy = new CoordinateXYZ(1.0, 2.0);   // 2D
            var coord = new CoordinateXY(xy); // copy
            Assert.AreEqual(xy, coord);
            Assert.AreEqual(0, coord.CompareTo(xy));

            var xyz = new CoordinateXYZ(1, 2, 3);
            Assert.AreEqual(3, xyz.Z);
            var coordz = new CoordinateXYZ(xyz);
            Assert.AreEqual(xyz, coordz);
            Assert.IsTrue(xyz.EqualInZ(coordz, 0));

        }

        [Test]
        public void TestCoordinateXYZM()
        {
            var xyzm = new CoordinateXYZM();
            xyzm.Z = 3.0;
            Assert.AreEqual(3.0, xyzm.Z);
            xyzm.M = 4.0;
            Assert.AreEqual(4.0, xyzm.M);

            var coord = new CoordinateXYZ(xyzm); // copy
            Assert.AreEqual(xyzm, coord);
            Assert.True(xyzm.EqualInZ(coord, 0.000001));
            Assert.True(double.IsNaN(coord.M));

            coord = new CoordinateXYZ(1.0, 1.0, 1.0); // 2.5d
            xyzm = new CoordinateXYZM(coord); // copy
            Assert.AreEqual(xyzm, coord);
            Assert.True(xyzm.EqualInZ(coord, 0.000001));
            Assert.True(double.IsNaN(coord.M));
        }

        /**
         * Confirm the z field is not supported by getZ and setZ.
         */
        private void checkZUnsupported(CoordinateXY coord)
        {
            try
            {
                coord.Z = 0.0;
                Assert.IsTrue(false, coord.GetType().Name + " should not support setting Z");
            }
            catch (InvalidOperationException expected)
            {
            }
            Assert.IsTrue(double.IsNaN(coord.Z));

            //no fields anymore!
            //coord.z = 0.0;                      // field still public
            //assertTrue("z field not used", Double.isNaN(coord.getZ())); // but not used
        }
        /**
         * Confirm the m field is not supported by getM and setM.
         */
        private void checkMUnsupported(CoordinateXY coord)
        {
            try
            {
                coord.M = 0.0;
                Assert.IsTrue(false, coord.GetType().Name + " should not support setting M");
            }
            catch (InvalidOperationException expected)
            {
            }
            Assert.IsTrue(double.IsNaN(coord.M));
        }

    }
}