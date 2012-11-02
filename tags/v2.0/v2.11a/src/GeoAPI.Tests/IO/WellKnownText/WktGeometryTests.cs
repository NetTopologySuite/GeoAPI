using System;
using GeoAPI.Coordinates;
using GeoAPI.CoordinateSystems;
using GeoAPI.Geometries;
using GeoAPI.IO.WellKnownText;
using NUnit.Framework;
using Rhino.Mocks;
using NetTopologySuite.Coordinates;

namespace GeoAPI.Tests.IO.WellKnownText
{
    [TestFixture]
    public class WktGeometryTests
    {
        [Test]
        [Ignore("Test partially implemented")]
        public void ParseGeometryCollection()
        {
            MockRepository mocks = new MockRepository();
            IPoint point1 = mocks.CreateMock<IPoint>();
            IPoint point2 = mocks.CreateMock<IPoint>();
            ILineString line = mocks.CreateMock<ILineString>();
            IGeometryCollection<BufferedCoordinate> geoCollection = mocks.CreateMock<IGeometryCollection<BufferedCoordinate>>();
            IGeometryFactory<BufferedCoordinate> geoFactory = mocks.CreateMock<IGeometryFactory<BufferedCoordinate>>();
            ICoordinateSystemFactory<BufferedCoordinate> coordSysFactory = mocks.CreateMock<ICoordinateSystemFactory<BufferedCoordinate>>();

            Expect.Call(geoFactory.CreatePoint());
            Expect.Call(delegate { geoCollection.Add(point1); });
            Expect.Call(geoFactory.CreateGeometryCollection()).Return(geoCollection);

            String geomCollectionWkt = "GEOMETRYCOLLECTION (POINT (10 10), POINT (30 30), LINESTRING (15 15, 20 20))";
            IWktGeometryReader decoder = new WktReader<BufferedCoordinate>(geoFactory, coordSysFactory, null);
            IGeometryCollection geom = decoder.Read(geomCollectionWkt) as IGeometryCollection;

            /*
            Assert.IsNotNull(geom);
            Assert.AreEqual(3, geom.NumGeometries);
            Assert.IsTrue(geom[0] is Point);
            Assert.IsTrue(geom[1] is Point);
            Assert.IsTrue(geom[2] is LineString);
            Assert.AreEqual(geomCollectionWkt, geom.AsText());
            geom = Geometry.FromText("GEOMETRYCOLLECTION EMPTY") as GeometryCollection;
            Assert.IsNotNull(geom);
            Assert.AreEqual(0, geom.NumGeometries);
            geomCollectionWkt = "GEOMETRYCOLLECTION (POINT (10 10), LINESTRING EMPTY, POINT (20 49))";
            geom = Geometry.FromText(geomCollectionWkt) as GeometryCollection;
            Assert.IsNotNull(geom);
            Assert.IsTrue(geom[1].IsEmpty());
            Assert.AreEqual(3, geom.NumGeometries);
            Assert.AreEqual(geomCollectionWkt, geom.AsText());
            Assert.AreEqual("GEOMETRYCOLLECTION EMPTY", new GeometryCollection().AsText());
            */
        }

        [Test]
        [Ignore("Test not implemented")]
        public void ParseMultipolygon()
        {
            /*
            String multipolygon = "MULTIPOLYGON (((0 0, 10 0, 10 10, 0 10, 0 0)), ((5 5, 7 5, 7 7, 5 7, 5 5)))";
            MultiPolygon geom = Geometry.FromText(multipolygon) as MultiPolygon;
            Assert.IsNotNull(geom);
            Assert.AreEqual(2, geom.NumGeometries);
            Assert.AreEqual(new Point(5, 5), geom[0].Centroid);
            Assert.AreEqual(multipolygon, geom.AsText());
            Assert.IsNotNull(Geometry.FromText("MULTIPOLYGON EMPTY"));
            Assert.IsTrue(Geometry.FromText("MULTIPOLYGON EMPTY").IsEmpty());
            geom = Geometry.FromText("MULTIPOLYGON (((0 0, 10 0, 10 10, 0 10, 0 0)), EMPTY, ((5 5, 7 5, 7 7, 5 7, 5 5)))")
                as MultiPolygon;
            Assert.IsNotNull(geom);
            Assert.IsTrue(geom[1].IsEmpty());
            Assert.AreEqual(new Point(5, 5), geom[2].ExteriorRing.EndPoint);
            Assert.AreEqual(new Point(5, 5), geom[2].ExteriorRing.StartPoint);
            Assert.AreEqual(geom[2].ExteriorRing.StartPoint, geom[2].ExteriorRing.EndPoint);
            Assert.AreEqual(3, geom.NumGeometries);
            Assert.AreEqual("MULTIPOLYGON EMPTY", new MultiPolygon().AsText());
            */
        }

        [Test]
        [Ignore("Test not implemented")]
        public void ParseLineString()
        {
            String linestring = "LINESTRING (20 20, 20 30, 30 30, 30 20, 40 20)";

            
            
            /*
            LineString geom = Geometry.FromText(linestring) as LineString;
            Assert.IsNotNull(geom);
            Assert.AreEqual(40, geom.Length);
            Assert.IsFalse(geom.IsRing);
            Assert.AreEqual(linestring, geom.AsText());
            Assert.IsTrue((Geometry.FromText("LINESTRING EMPTY") as LineString).IsEmpty());
            Assert.AreEqual("LINESTRING EMPTY", new LineString().AsText());
            */
        }

        [Test]
        [Ignore("Test not implemented")]
        public void ParseMultiLineString()
        {
            String multiLinestring = "MULTILINESTRING ((10 10, 40 50), (20 20, 30 20), (20 20, 50 20, 50 60, 20 20))";

            /*
            MultiLineString geom = Geometry.FromText(multiLinestring) as MultiLineString;
            Assert.IsNotNull(geom);
            Assert.AreEqual(3, geom.NumGeometries);
            Assert.AreEqual(180, geom.Length);
            Assert.AreEqual(120, geom[2].Length);
            Assert.IsFalse(geom[0].IsClosed, "[0].IsClosed");
            Assert.IsFalse(geom[1].IsClosed, "[1].IsClosed");
            Assert.IsTrue(geom[2].IsClosed, "[2].IsClosed");
            Assert.IsTrue(geom[0].IsSimple(), "[0].IsSimple");
            Assert.IsTrue(geom[1].IsSimple(), "[1].IsSimple");
            Assert.IsTrue(geom[2].IsSimple(), "[2].IsSimple");
            Assert.IsTrue(geom[2].IsRing, "Third line is a ring");
            Assert.AreEqual(multiLinestring, geom.AsText());
            Assert.IsTrue(Geometry.FromText("MULTILINESTRING EMPTY").IsEmpty());
            geom = Geometry.FromText(
                    "MULTILINESTRING ((10 10, 40 50), (20 20, 30 20), EMPTY, (20 20, 50 20, 50 60, 20 20))") as
                MultiLineString;
            Assert.IsNotNull(geom);
            Assert.IsTrue(geom[2].IsEmpty());
            Assert.AreEqual(4, geom.NumGeometries);
            Assert.AreEqual("MULTILINESTRING EMPTY", new MultiLineString().AsText());
            */
        }

        [Test]
        [Ignore("Test not implemented")]
        public void ParsePolygon()
        {
            String polygon = "POLYGON ((20 20, 20 30, 30 30, 30 20, 20 20))";

            /*
            Polygon geom = Geometry.FromText(polygon) as Polygon;
            Assert.IsNotNull(geom);
            Assert.AreEqual(40, geom.ExteriorRing.Length);
            Assert.AreEqual(100, geom.Area);
            Assert.AreEqual(polygon, geom.AsText());
            //Test interior rings
            polygon =
                "POLYGON ((20 20, 20 30, 30 30, 30 20, 20 20), (21 21, 29 21, 29 29, 21 29, 21 21), (23 23, 23 27, 27 27, 27 23, 23 23))";
            geom = Geometry.FromText(polygon) as Polygon;
            Assert.IsNotNull(geom);
            Assert.AreEqual(40, geom.ExteriorRing.Length);
            Assert.AreEqual(2, geom.InteriorRings.Count);
            Assert.AreEqual(52, geom.Area);
            Assert.AreEqual(geom.ExteriorRing.Area - geom.InteriorRings[0].Area + geom.InteriorRings[1].Area, geom.Area);
            Assert.AreEqual(polygon, geom.AsText());
            //Test empty geometry WKT
            Assert.IsTrue(Geometry.FromText("POLYGON EMPTY").IsEmpty());
            Assert.AreEqual("POLYGON EMPTY", new Polygon().AsText());
            */
        }

        [Test]
        public void ParsePoint()
        {
            BufferedCoordinateFactory coordFactory = new BufferedCoordinateFactory();
            BufferedCoordinate coord = coordFactory.Create(20.564, 346.3493254);

            MockRepository mocks = new MockRepository();
            IGeometryFactory<BufferedCoordinate> geoFactory = mocks.CreateMock<IGeometryFactory<BufferedCoordinate>>();
            IPoint<BufferedCoordinate> p = mocks.CreateMock<IPoint<BufferedCoordinate>>();

            String pointWkt = "POINT (20.564 346.3493254)";

            Expect.Call(geoFactory.CoordinateFactory).Repeat.Any().Return(coordFactory);
            Expect.Call(geoFactory.CreatePoint(coord)).Return(p);
            mocks.ReplayAll();

            IPoint point = WktReader<BufferedCoordinate>.ToGeometry(pointWkt,geoFactory) as IPoint;

            mocks.VerifyAll();


            /*
            Point geom = Geometry.FromText(point) as Point;
            Assert.IsNotNull(geom);
            Assert.AreEqual(20.564, geom.X);
            Assert.AreEqual(346.3493254, geom.Y);
            Assert.AreEqual(point, geom.AsText());
            Assert.IsTrue(Geometry.FromText("POINT EMPTY").IsEmpty());
            Assert.AreEqual("POINT EMPTY", new Point().AsText());
            */
        }

        [Test]
        [Ignore("Test not implemented")]
        public void ParseMultiPoint()
        {
            String multipoint = "MULTIPOINT (20.564 346.3493254, 45 32, 23 54)";
            /*
            MultiPoint geom = Geometry.FromText(multipoint) as MultiPoint;
            Assert.IsNotNull(geom);
            Assert.AreEqual(20.564, geom[0].X);
            Assert.AreEqual(54, geom[2].Y);
            Assert.AreEqual(multipoint, geom.AsText());
            Assert.IsTrue(Geometry.FromText("MULTIPOINT EMPTY").IsEmpty());
            Assert.AreEqual("MULTIPOINT EMPTY", new MultiPoint().AsText());
            */
        }

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