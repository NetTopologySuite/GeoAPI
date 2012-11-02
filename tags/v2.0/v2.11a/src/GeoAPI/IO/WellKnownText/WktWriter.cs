// Copyright 2009: Johan Lies�n, Astando AB <johan.liesen@astando.se>
//
// This file is part of GeoAPI.Net.
// GeoAPI.Net is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// GeoAPI.Net is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with GeoAPI.Net; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

namespace GeoAPI.IO.WellKnownText
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Coordinates;
    using Geometries;
    using NPack;
    using NPack.Interfaces;

    /// <summary>
    /// Writer for the Well-Known Text (WKT) format as specified by 
    /// <see href="http://www.opengeospatial.org">Open Geospatial Consortium Inc</see> in 
    /// <see href="http://www.opengeospatial.org/standards/sfa">OpenGIS� Implementation 
    /// Specification for Geographic information - Simple feature access - Part 1: Common 
    /// architecture</see> (version 1.2.0).
    /// </summary>
    /// <seealso cref="WktReader{TCoordinate}"/>
    /// <seealso cref="WktWriter{TCoordinate}"/>
    /// <typeparam name="TCoordinate">A coordinate type</typeparam>
    public class WktWriter<TCoordinate> : IWktGeometryWriter<TCoordinate>
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>,
                            IComparable<TCoordinate>, IConvertible,
                            IComputable<double, TCoordinate>
    {
        /// <summary>
        /// Value indicating if <see cref="Ordinates.W"/> values should be emitted or not.
        /// </summary>
        public bool EmitW { get; set; }
        
        #region Implementation of IWktGeometryWriter

        /// <summary>
        /// Converts an <see cref="IGeometry"/> to WKT.
        /// </summary>
        /// <param name="geometry">The geometry</param>
        /// <returns>WKT representation of <paramref name="geometry"/></returns>
        public string Write(IGeometry geometry)
        {
            return Write((IGeometry<TCoordinate>) geometry);
        }

        /// <summary>
        /// Converts a collection of <see cref="IGeometry"/> objects to WKT (separated by a new 
        /// line).
        /// </summary>
        /// <param name="geometries">The geometries</param>
        /// <returns>WKT representation of <paramref name="geometries"/></returns>
        public string WriteAll(IEnumerable<IGeometry> geometries)
        {
            return WriteAll((IEnumerable<IGeometry<TCoordinate>>) geometries);
        }

        /// <summary>
        /// Converts an <see cref="IGeometry"/> to WKT. Writes the WKT to <paramref name="writer"/>.
        /// </summary>
        /// <param name="geometry">The geometry</param>
        /// <param name="writer">The writer</param>
        public void Write(IGeometry geometry, TextWriter writer)
        {
            Write((IGeometry<TCoordinate>) geometry, writer);
        }

        /// <summary>
        /// Converts a collection of <see cref="IGeometry"/> objects to WKT (separated by a new 
        /// line).
        /// </summary>
        /// <param name="geometries">The geometries</param>
        /// <param name="writer">The writer</param>
        public void WriteAll(IEnumerable<IGeometry> geometries, TextWriter writer)
        {
            WriteAll((IEnumerable<IGeometry<TCoordinate>>) geometries, writer);
        }

        #endregion

        #region Implementation of IWktGeometryWriter<TCoordinate>

        /// <summary>
        /// Creates a Well-Known text string from a <see cref="IGeometry{TCoordinate}"/>.
        /// </summary>
        /// <param name="geometry">A geometry</param>
        /// <returns>WKT representation of <paramref name="geometry"/></returns>
        public string Write(IGeometry<TCoordinate> geometry)
        {
            StringWriter writer = new StringWriter();
            Write(geometry, writer);
            return writer.ToString();
        }

        /// <summary>
        /// Writes each geometry as WKT on a separate line.
        /// </summary>
        /// <param name="geometries">Geometries to convert to WKT</param>
        /// <returns>Text containing all geometries as WKT separated by a new line</returns>
        public string WriteAll(IEnumerable<IGeometry<TCoordinate>> geometries)
        {
            StringWriter writer = new StringWriter();
            WriteAll(geometries, writer);
            return writer.ToString();
        }


        /// <summary>
        /// Creates a Well-Known text string from a <see cref="IGeometry{TCoordinate}"/>.
        /// </summary>
        /// <param name="geometry">A geometry</param>
        /// <param name="writer">The text writer to use.</param>
        public void Write(IGeometry<TCoordinate> geometry, TextWriter writer)
        {
            WriteGeometryTaggedText(geometry, writer);
        }

        /// <summary>
        /// Writes each geometry as WKT on a separate line.
        /// </summary>
        /// <param name="geometries">Geometries to convert to WKT</param>
        /// <param name="writer">The text writer to use.</param>
        public void WriteAll(IEnumerable<IGeometry<TCoordinate>> geometries, TextWriter writer)
        {
            foreach (IGeometry<TCoordinate> geometry in geometries)
            {
                Write(geometry, writer);
                writer.WriteLine();
            }
        }

        #endregion

        #region Write

        /// <summary>
        /// Writes a geometry, <paramref name="geometry"/> to <paramref name="writer"/>.
        /// </summary>
        /// <param name="geometry">The geometry</param>
        /// <param name="writer">The writer</param>
        /// <exception cref="ArgumentNullException">If <paramref name="geometry"/> is
        /// <see langword="null"/></exception>
        /// <exception cref="NotSupportedException">If an unsupported geometry is found</exception>
        protected virtual void WriteGeometryTaggedText(
            IGeometry<TCoordinate> geometry, TextWriter writer)
        {
            if (geometry == null)
            {
                throw new ArgumentNullException("geometry");
            }

            if (geometry is IPoint<TCoordinate>)
            {
                WritePointTaggedText((IPoint<TCoordinate>) geometry, writer);
            }
            else if (geometry is ILineString<TCoordinate>)
            {
                WriteLineStringTaggedText((ILineString<TCoordinate>) geometry, writer);
            }
            else if (geometry is IPolygon<TCoordinate>)
            {
                WritePolygonTaggedText((IPolygon<TCoordinate>) geometry, writer);
            }
            else if (geometry is IMultiPoint<TCoordinate>)
            {
                WriteMultiPointTaggedText((IMultiPoint<TCoordinate>) geometry, writer);
            }
            else if (geometry is IMultiLineString<TCoordinate>)
            {
                WriteMultiLineStringTaggedText((IMultiLineString<TCoordinate>) geometry, writer);
            }
            else if (geometry is IMultiPolygon<TCoordinate>)
            {
                WriteMultiPolygonTaggedText((IMultiPolygon<TCoordinate>) geometry, writer);
            }
            else if (geometry is IGeometryCollection<TCoordinate>)
            {
                WriteGeometryCollectionTaggedText(
                    (IGeometryCollection<TCoordinate>) geometry, writer);
            }
            else
            {
                throw new NotSupportedException(
                    "Unsupported geometry type: " + geometry.GeometryTypeName);
            }
        }

        #region Coordinate

        /// <summary>
        /// Writes a coordinate.
        /// </summary>
        /// <param name="coordinate">The coordinate</param>
        /// <param name="writer">The writer</param>
        protected virtual void WriteCoordinate(TCoordinate coordinate, TextWriter writer)
        {
            int componentCount = coordinate.ComponentCount;

            if (componentCount > 0)
            {
                WriteNumber(coordinate[0], writer);

                for (int i = 1; i < componentCount; i++)
                {
                    if ((i == (int)Ordinates.W) && !EmitW)
                        continue;
                    writer.Write(' ');
                    WriteNumber(coordinate[i], writer);
                }
            }
        }

        #endregion

        #region Number

        /// <summary>
        /// Writes a number.
        /// </summary>
        /// <param name="number">The number</param>
        /// <param name="writer">The writer</param>
        protected virtual void WriteNumber(DoubleComponent number, TextWriter writer)
        {
            writer.Write(number.ToString(writer.FormatProvider));
        }

        #endregion

        #region Point

        protected virtual void WritePointTaggedText(IPoint<TCoordinate> point, TextWriter writer)
        {
            WriteTag(point, WktTokens.Point, writer);
            WritePointText(point, writer);
        }

        protected virtual void WritePointText(IPoint<TCoordinate> point, TextWriter writer)
        {
            if (HandleEmptyGeometry(point, writer))
            {
                return;
            }

            writer.Write(WktTokens.LeftParen);
            WriteCoordinate(point.Coordinate, writer);
            writer.Write(WktTokens.RightParen);
        }

        #endregion

        #region LineString

        protected virtual void WriteLineStringTaggedText(
            ILineString<TCoordinate> lineString, TextWriter writer)
        {
            WriteTag(lineString, WktTokens.LineString, writer);
            WriteLineStringText(lineString, writer);
        }

        protected virtual void WriteLineStringText(
            ILineString<TCoordinate> lineString, TextWriter writer)
        {
            if (HandleEmptyGeometry(lineString, writer))
            {
                return;
            }

            WriteCoordinates(lineString.Coordinates, writer);
        }

        #endregion

        #region Polygon

        protected virtual void WritePolygonTaggedText(
            IPolygon<TCoordinate> polygon, TextWriter writer)
        {
            WriteTag(polygon, WktTokens.Polygon, writer);
            WritePolygonText(polygon, writer);
        }

        protected virtual void WritePolygonText(IPolygon<TCoordinate> polygon, TextWriter writer)
        {
            if (HandleEmptyGeometry(polygon, writer))
            {
                return;
            }

            writer.Write(WktTokens.LeftParen);
            WriteLineStringText(polygon.ExteriorRing, writer);

            foreach (ILinearRing<TCoordinate> interiorRing in polygon.InteriorRings)
            {
                writer.Write(", ");
                WriteLineStringText(interiorRing, writer);
            }

            writer.Write(WktTokens.RightParen);
        }

        #endregion

        #region MultiPoint

        protected virtual void WriteMultiPointTaggedText(
            IMultiPoint<TCoordinate> multiPoint, TextWriter writer)
        {
            WriteTag(multiPoint, WktTokens.MultiPoint, writer);
            WriteMultiPointText(multiPoint, writer);
        }

        protected virtual void WriteMultiPointText(
            IMultiPoint<TCoordinate> multiPoint, TextWriter writer)
        {
            if (HandleEmptyGeometry(multiPoint, writer))
            {
                return;
            }

            WriteGeometries<IPoint<TCoordinate>>(
                multiPoint, 
                WritePointText, 
                writer);
        }

        #endregion

        #region MultiLineString

        protected virtual void WriteMultiLineStringTaggedText(
            IMultiLineString<TCoordinate> multiLineString, TextWriter writer)
        {
            WriteTag(multiLineString, WktTokens.MultiLineString, writer);
            WriteMultiLineStringText(multiLineString, writer);
        }

        protected virtual void WriteMultiLineStringText(
            IMultiLineString<TCoordinate> multiLineString, TextWriter writer)
        {
            if (HandleEmptyGeometry(multiLineString, writer))
            {
                return;
            }

            WriteGeometries<ILineString<TCoordinate>>(
                multiLineString, 
                WriteLineStringText, 
                writer);
        }

        #endregion

        #region MultiPolygon

        protected virtual void WriteMultiPolygonTaggedText(
            IMultiPolygon<TCoordinate> multiPolygon, TextWriter writer)
        {
            WriteTag(multiPolygon, WktTokens.MultiPolygon, writer);
            WriteMultiPolygonText(multiPolygon, writer);
        }

        protected virtual void WriteMultiPolygonText(
            IMultiPolygon<TCoordinate> multiPolygon, TextWriter writer)
        {
            if (HandleEmptyGeometry(multiPolygon, writer))
            {
                return;
            }

            WriteGeometries<IPolygon<TCoordinate>>(
                multiPolygon,
                WritePolygonText,
                writer);
        }

        #endregion

        #region GeometryCollection

        protected virtual void WriteGeometryCollectionTaggedText(
            IGeometryCollection<TCoordinate> geometryCollection, TextWriter writer)
        {
            WriteTag(geometryCollection, WktTokens.GeometryCollection, writer);
            WriteGeometryCollectionText(geometryCollection, writer);
        }

        protected virtual void WriteGeometryCollectionText(
            IGeometryCollection<TCoordinate> geometryCollection, TextWriter writer)
        {
            if (HandleEmptyGeometry(geometryCollection, writer))
            {
                return;
            }

            WriteGeometries<IGeometry<TCoordinate>>(
                geometryCollection,
                WriteGeometryTaggedText,
                writer);
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Writes the geometry's type and dimensionality tag.
        /// </summary>
        /// <param name="geometry">The geometry</param>
        /// <param name="tag">Tag for <paramref name="geometry"/></param>
        /// <param name="writer">The writer</param>
        protected void WriteTag(IGeometry<TCoordinate> geometry, string tag, TextWriter writer)
        {
            writer.Write(tag);
            writer.Write(' ');
            WriteDimensionTag(geometry, writer);
        }

        /// <summary>
        /// Writes the geometry's "dimensionality tag"; a 'Z' if it's three dimensional.
        /// </summary>
        /// <param name="geometry">The geometry</param>
        /// <param name="writer">The writer</param>
        /// <exception cref="ArgumentException"></exception>
        protected virtual void WriteDimensionTag(IGeometry<TCoordinate> geometry, TextWriter writer)
        {
            if (geometry.IsEmpty)
            {
                return;
            }

            TCoordinate firstCoordinate = geometry.Coordinates.First;

            if (firstCoordinate.ComponentCount > 4 + (EmitW ? 1:0))
            {
                throw new ArgumentException(
                    "Coordinates has more components than allowed", "geometry");
            }

            if (firstCoordinate.ComponentCount > 2 && !double.IsNaN(firstCoordinate[Ordinates.Z]))
                writer.Write(WktTokens.Z);

            if (firstCoordinate.ComponentCount > 2 && !double.IsNaN(firstCoordinate[Ordinates.M]))
                writer.Write(WktTokens.M);

            if (EmitW)
            {
                //NOTE: this is not conformant to the standard but needed to possibly 
                if (firstCoordinate.ComponentCount > 2 && !double.IsNaN(firstCoordinate[Ordinates.W]))
                    writer.Write("W");
            }
            if (firstCoordinate.ComponentCount > 2)
                writer.Write(' ');
        }

        /// <summary>
        /// Writes 'EMPTY' if the geometry is <see langword="null"/> or empty.
        /// </summary>
        /// <param name="geometry">The geometry</param>
        /// <param name="writer">The writer</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="geometry"/> is <see langword="null"/> or
        /// empty; <see langword="false"/> otherwise
        /// </returns>
        protected virtual bool HandleEmptyGeometry(
            IGeometry<TCoordinate> geometry, TextWriter writer)
        {
            if (geometry == null || geometry.IsEmpty)
            {
                writer.Write(WktTokens.EmptySet);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Writes a sequence of geometries.
        /// </summary>
        /// <param name="geometries">The geometries</param>
        /// <param name="writeGeometry">A function to write a geometry from 
        /// <paramref name="geometries"/></param>
        /// <param name="writer">The writer</param>
        /// <typeparam name="TGeometry">A geometry type</typeparam>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        protected void WriteGeometries<TGeometry>(
            IEnumerable<TGeometry> geometries,
            Action<TGeometry, TextWriter> writeGeometry,
            TextWriter writer) where TGeometry : IGeometry<TCoordinate>
        {
            if (geometries == null)
            {
                throw new ArgumentNullException("geometries");
            }

            IEnumerator<TGeometry> it = geometries.GetEnumerator();

            if (!it.MoveNext())
            {
                throw new ArgumentException("Empty geometry", "geometries");
            }

            writer.Write(WktTokens.LeftParen);
            writeGeometry(it.Current, writer);

            while (it.MoveNext())
            {
                writer.Write(", ");
                writeGeometry(it.Current, writer);
            }

            writer.Write(WktTokens.RightParen);
        }

        /// <summary>
        /// Writes a sequence of coordinates, <see cref="ICoordinateSequence{TCoordinate}"/>.
        /// </summary>
        /// <remarks>Assumes that the coordinate sequence is not empty.</remarks>
        /// <param name="coordinates">The coordinate sequence</param>
        /// <param name="writer">The writer</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        protected virtual void WriteCoordinates(
            ICoordinateSequence<TCoordinate> coordinates, TextWriter writer)
        {
            if (coordinates == null)
            {
                throw new ArgumentNullException("coordinates");
            }

            IEnumerator<TCoordinate> it = coordinates.GetEnumerator();

            if (!it.MoveNext())
            {
                throw new ArgumentException("Empty coordinate sequence", "coordinates");
            }

            writer.Write(WktTokens.LeftParen);
            WriteCoordinate(it.Current, writer);

            while (it.MoveNext())
            {
                writer.Write(", ");
                WriteCoordinate(it.Current, writer);
            }

            writer.Write(WktTokens.RightParen);
        }

        #endregion

        #endregion // Write
    }
}
