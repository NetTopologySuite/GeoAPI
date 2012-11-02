// Copyright 2009: Johan Liesén, Astando AB <johan.liesen@astando.se>
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

using GeoAPI.CoordinateSystems;

namespace GeoAPI.IO.WellKnownText
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Coordinates;
    using Geometries;
    using NPack.Interfaces;

    /// <summary>
    /// Parser for the Well-Known Text (WKT) format as specified by
    /// <see href="http://www.opengeospatial.org">Open Geospatial Consortium Inc.</see> in
    /// <see href="http://www.opengeospatial.org/standards/sfa">OpenGIS� Implementation
    /// Specification for Geographic information - Simple feature access - Part 1: Common
    /// architecture</see> (version 1.2.0).
    /// </summary>
    /// <remarks>
    /// Parsing of the geometry type PolyhedralSurface is not implemented.
    /// </remarks>
    /// <typeparam name="TCoordinate">Coordinate type</typeparam>
    public class WktReader<TCoordinate> : IWktGeometryReader<TCoordinate>, IWktCoordinateSystemReader<TCoordinate>
        where TCoordinate : ICoordinate<TCoordinate>,
                            IEquatable<TCoordinate>,
                            IComparable<TCoordinate>,
                            IConvertible,
                            IComputable<double, TCoordinate>
    {
        /// <summary>
        /// Tokenizer for splitting up the input.
        /// </summary>
        private WktTokenizer _tokenizer;

        /// <summary>
        /// Factory for creating geometries.
        /// </summary>
        private IGeometryFactory<TCoordinate> _geometryFactory;

        private ICoordinateSystemFactory<TCoordinate> _coordinateSystemFactory;
        private ICoordinateSystemAuthorityFactory<TCoordinate> _coordinateSystemAuthorityFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="WktReader{TCoordinate}"/> class.
        /// </summary>
        /// <param name="geometryFactory">Factory for constructing geometries</param>
        public WktReader(IGeometryFactory<TCoordinate> geometryFactory)
        {
            _geometryFactory = geometryFactory;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WktReader{TCoordinate}"/> class.
        /// </summary>
        /// <param name="geometryFactory">Factory for constructing geometries</param>
        /// <param name="coordinateSystemFactory">Factory for constructing coordinate systems</param>
        public WktReader(IGeometryFactory<TCoordinate> geometryFactory, ICoordinateSystemFactory<TCoordinate> coordinateSystemFactory, ICoordinateSystemAuthorityFactory<TCoordinate> coordinateSystemAuthorityFactory)
            :this(geometryFactory)
        {
            _coordinateSystemFactory = coordinateSystemFactory;
            _coordinateSystemAuthorityFactory = coordinateSystemAuthorityFactory;
        }

        public static IGeometry<TCoordinate> ToGeometry(string wkt, IGeometryFactory<TCoordinate> factory)
        {
            return new WktReader<TCoordinate>(factory).Read(wkt);
        }

        public static ICoordinateSystem<TCoordinate> ToCoordinateSystemInfo(string wkt, ICoordinateSystemFactory<TCoordinate> coordinateSystemFactory)
        {
            return CoordinateSystemWktReader.Parse(wkt, coordinateSystemFactory) as ICoordinateSystem<TCoordinate>;
        }

        /// <summary>
        /// Flags specifying how many components to expect when reading coordinates.
        /// </summary>
        [Flags]
        protected enum OrdinateFlags
        {
            None = 0,
            X = 1,
            Y = 2,
            Z = 4,
            M = 8,
            XY = X | Y,
            XYZ = XY | Z,
            XYM = XY | M,
            XYZM = XYZ | M
        }

        /// <summary>
        /// Sets the geometry factory.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public IGeometryFactory<TCoordinate> GeometryFactory
        {
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }

                _geometryFactory = value;
            }
        }

        #region IWktGeometryReader

        ICoordinateSystemFactory IWktCoordinateSystemReader.CoordinateSystemFactory
        {
            set { CoordinateSystemFactory = (ICoordinateSystemFactory<TCoordinate>)value; }
        }

        public ICoordinateSystemAuthorityFactory<TCoordinate> AuthorityFactory
        {
            set { _coordinateSystemAuthorityFactory = value; }
        }

        public ICoordinateSystemFactory<TCoordinate> CoordinateSystemFactory
        {
            set { _coordinateSystemFactory = value; }
        }

        ICoordinateSystemAuthorityFactory IWktCoordinateSystemReader.AuthorityFactory
        {
            set { AuthorityFactory = (ICoordinateSystemAuthorityFactory<TCoordinate>)value; }
        }

        IGeometry IWktGeometryReader.Read(string wkt)
        {
            return Read(wkt);
        }

        ICoordinateSystem<TCoordinate> IWktCoordinateSystemReader<TCoordinate>.Read(TextReader wkt)
        {
            return (ICoordinateSystem<TCoordinate>)CoordinateSystemWktReader.Parse(wkt, _coordinateSystemFactory);
        }

        ICoordinateSystem<TCoordinate> IWktCoordinateSystemReader<TCoordinate>.Read(string wkt)
        {
            return (ICoordinateSystem<TCoordinate>)CoordinateSystemWktReader.Parse(wkt, _coordinateSystemFactory);
        }

        ICoordinateSystem IWktCoordinateSystemReader.Read(TextReader wkt)
        {
            return (ICoordinateSystem)CoordinateSystemWktReader.Parse(wkt, _coordinateSystemFactory); ;
        }

        ICoordinateSystem IWktCoordinateSystemReader.Read(string wkt)
        {
            return (ICoordinateSystem)CoordinateSystemWktReader.Parse(wkt, _coordinateSystemFactory);
        }

        IGeometry IWktGeometryReader.Read(TextReader wktData)
        {
            return Read(wktData);
        }

        IEnumerable<IGeometry> IWktGeometryReader.ReadAll(TextReader wktData)
        {
            while (wktData.Peek() >= 0)
            {
                yield return GeometryFromWkt.Parse(wktData, _geometryFactory);
            }
        }

        IGeometryFactory IWktGeometryReader.GeometryFactory
        {
            set
            {
                if (value != null && !(value is IGeometryFactory<TCoordinate>))
                {
                    throw new ArgumentException("not type IGeometryFactory<TCoordinate>", "value");
                }

                GeometryFactory = value as IGeometryFactory<TCoordinate>;
            }
        }

        #endregion

        #region IWktGeometryReader<>

        /// <summary>
        /// Parses <paramref name="wellKnownText"/> (a WKT string) into a
        /// <see cref="IGeometry{TCoordinate}"/>.
        /// </summary>
        /// <param name="wellKnownText">Well-known text.</param>
        /// <returns>A geometry as specified by <paramref name="wellKnownText"/></returns>
        public IGeometry<TCoordinate> Read(string wellKnownText)
        {
            return Read(new StringReader(wellKnownText));
        }


        /// <summary>
        /// Parses a WKT structure into a geometry.
        /// </summary>
        /// <param name="wktData">The input WKT.</param>
        /// <returns>A geometry as specified by <paramref name="wktData"/></returns>
        public IGeometry<TCoordinate> Read(TextReader wktData)
        {
            _tokenizer = new WktTokenizer(wktData);
            return ReadGeometryTaggedText();
        }

        ///<summary>
        /// Parses all WKT structures to geometries
        ///</summary>
        ///<param name="wktData">The input WKT.</param>
        ///<returns>An enumeration if geometries specified by <see paramref="wktData"/></returns>
        public IEnumerable<IGeometry<TCoordinate>> ReadAll(TextReader wktData)
        {
            while (wktData.Peek() >= 0)
            {
                yield return Read(wktData);
            }
        }

        #endregion

        #region Parse Methods

        protected IGeometry<TCoordinate> ReadGeometryTaggedText()
        {
            string tag = ReadNextWord();
            OrdinateFlags ordinateFlags = OrdinateFlags.XY;

            switch (_tokenizer.NextToken.ToUpper())
            {
                case WktTokens.M:
                    _tokenizer.ReadToken(WktTokens.M);
                    ordinateFlags |= OrdinateFlags.M;
                    break;
                
                case WktTokens.ZM:
                    _tokenizer.ReadToken(WktTokens.ZM);
                    ordinateFlags |= OrdinateFlags.Z | OrdinateFlags.M;
                    break;

                case WktTokens.Z:
                    _tokenizer.ReadToken(WktTokens.Z);
                    ordinateFlags |= OrdinateFlags.Z;
                    break;

                default:
                    break;
            }

            return ReadGeometryText(tag, ordinateFlags);
        }

        /// <summary>
        /// Reads a geometry tagged text ...
        /// </summary>
        /// <param name="ordinateFlags">The ordinate flags.</param>
        /// <returns></returns>
        protected IGeometry<TCoordinate> ReadGeometryTaggedText(OrdinateFlags ordinateFlags)
        {
            string tag = ReadNextWord();

            if ((ordinateFlags & OrdinateFlags.Z) > 0)
            {
                _tokenizer.ReadToken(WktTokens.Z);
            }

            return ReadGeometryText(tag, ordinateFlags);
        }

        /// <summary>
        /// Parses the text part of a geometry.
        /// </summary>
        /// <param name="geometryTag">The tag of the geometry to read</param>
        /// <param name="ordinateFlags">Flags for the type of coordinates to expect</param>
        /// <returns>A geometry/</returns>
        /// <exception cref="NotSupportedException">If the geometry type can not be read</exception>
        protected IGeometry<TCoordinate> ReadGeometryText(
                string geometryTag, OrdinateFlags ordinateFlags)
        {
            switch (geometryTag)
            {
                case WktTokens.Point:
                    return ReadPointText(ordinateFlags);

                case WktTokens.LineString:
                    return ReadLineStringText(ordinateFlags);

                case WktTokens.MultiPoint:
                    return ReadMultiPointText(ordinateFlags);

                case WktTokens.MultiLineString:
                    return ReadMultiLineStringText(ordinateFlags);

                case WktTokens.Polygon:
                    return ReadPolygonText(ordinateFlags);

                case WktTokens.MultiPolygon:
                    return ReadMultiPolygonText(ordinateFlags);

                case WktTokens.GeometryCollection:
                    return ReadGeometryCollectionText(ordinateFlags);

                case WktTokens.PolyhedralSurface:
                    break;

                case WktTokens.LinearRing:
                    return ReadLinearRingText(ordinateFlags);
            }

            throw new NotSupportedException(
                string.Format(
                    WktTokenizer.NumberFormat_enUS,
                    "Geometrytype '{0}' is not supported.",
                    geometryTag));
        }

        protected IGeometry<TCoordinate> ReadPointText(OrdinateFlags ordinateFlags)
        {
            if (GetEmptyOrLeftParen() == WktTokens.EmptySet)
            {
                return _geometryFactory.CreatePoint();
            }

            TCoordinate coordinate = ReadCoordinate(ordinateFlags);
            GetRightParen();
            return _geometryFactory.CreatePoint(coordinate);
        }

        protected IGeometry<TCoordinate> ReadMultiPointText(OrdinateFlags ordinateFlags)
        {
            //MULTIPOINT has to valid notations
            //MULTIPOINT(0 0, 10 10, 20 30) or
            //MULTIPOINT((0 0), (10 10), (20 30))
            return _geometryFactory.CreateMultiPoint(ReadCoordinates(ordinateFlags, true));
        }

        protected IGeometry<TCoordinate> ReadLineStringText(OrdinateFlags ordinateFlags)
        {
            return _geometryFactory.CreateLineString(ReadCoordinates(ordinateFlags));
        }

        protected IGeometry<TCoordinate> ReadLinearRingText(OrdinateFlags ordinateFlags)
        {
            return _geometryFactory.CreateLinearRing(ReadCoordinates(ordinateFlags));
        }

        protected IGeometry<TCoordinate> ReadMultiLineStringText(OrdinateFlags ordinateFlags)
        {
            return ReadMany(
                _geometryFactory.CreateMultiLineString(),
                () => ReadLineStringText(ordinateFlags));
        }

        protected IGeometry<TCoordinate> ReadPolygonText(OrdinateFlags ordinateFlags)
        {
            if (GetEmptyOrLeftParen() == WktTokens.EmptySet)
            {
                return _geometryFactory.CreatePolygon();
            }

            ILinearRing<TCoordinate> shell = _geometryFactory.CreateLinearRing(
                ReadCoordinates(ordinateFlags));
            IList<ILinearRing<TCoordinate>> interiorRings = new List<ILinearRing<TCoordinate>>();

            while (GetCommaOrRightParen() == WktTokens.Comma)
            {
                // Add holes
                interiorRings.Add(_geometryFactory.CreateLinearRing(ReadCoordinates(ordinateFlags)));
            }

            if (interiorRings.Count == 0)
            {
                return _geometryFactory.CreatePolygon(shell);
            }

            return _geometryFactory.CreatePolygon(shell, interiorRings);
        }

        protected IGeometry<TCoordinate> ReadMultiPolygonText(OrdinateFlags ordinateFlags)
        {
            return ReadMany(
                _geometryFactory.CreateMultiPolygon(),
                () => ReadPolygonText(ordinateFlags));
        }

        protected IGeometry<TCoordinate> ReadGeometryCollectionText(OrdinateFlags ordinateFlags)
        {
            return ReadMany(
                _geometryFactory.CreateGeometryCollection(),
                () => ReadGeometryTaggedText(ordinateFlags));
        }

        /// <summary>
        /// Reads zero or more geometries separated by ',' into <paramref name="geometries"/>.
        /// Stops when ')' is encountered.
        /// </summary>
        /// <typeparam name="TGeometryCollection">A collection of geometries</typeparam>
        /// <param name="geometries">A geometry that is itself a collection of geometries</param>
        /// <param name="nextGeometry">Function that yields the next geometry</param>
        /// <returns><paramref name="geometries"/> concatenated with geometries</returns>
        protected TGeometryCollection ReadMany<TGeometryCollection>(
                TGeometryCollection geometries, Func<IGeometry<TCoordinate>> nextGeometry)
            where TGeometryCollection : IGeometryCollection<TCoordinate>
        {
            if (GetEmptyOrLeftParen() == WktTokens.EmptySet)
            {
                return geometries;
            }

            do
            {
                geometries.Add(nextGeometry());
            }
            while (GetCommaOrRightParen() == WktTokens.Comma);

            return geometries;
        }

        /// <summary>
        /// Reads the next coordinate.
        /// </summary>
        /// <param name="ordinateFlags">Number of ordinates to expect.</param>
        /// <returns>A coordinate.</returns>
        private TCoordinate ReadCoordinate(OrdinateFlags ordinateFlags)
        {
            return ReadCoordinate(ordinateFlags, false);
        }

        private TCoordinate ReadCoordinate(OrdinateFlags ordinateFlags, bool tryParens)
        {
            if ((ordinateFlags & OrdinateFlags.XY) != OrdinateFlags.XY)
            {
                throw new ArgumentException(
                    "Coordinates must have at least X and Y components",
                    "ordinateFlags");
            }

            bool opened = false;
            if (tryParens && _tokenizer.NextToken == "(")
            {
                _tokenizer.Read();
                opened = true;
            }

            double x = ReadNumber();
            double y = ReadNumber();

            TCoordinate coord;

            double m;
            if ((ordinateFlags & OrdinateFlags.Z) > 0)
            {
                double z = ReadNumber();
                if ((ordinateFlags & OrdinateFlags.M) > 0)
                {
                    m = ReadNumber();
                    coord = _geometryFactory.CoordinateFactory.Create3D(x, y, z, m);
                }
                else
                    coord = _geometryFactory.CoordinateFactory.Create3D(x, y, z);
            }
            else
            if ((ordinateFlags & OrdinateFlags.M) > 0)
            {
                 m = ReadNumber();
                 coord = _geometryFactory.CoordinateFactory.Create(x, y, m);
            }
            else
                coord = _geometryFactory.CoordinateFactory.Create(x, y);

            if (opened)
                _tokenizer.ReadToken(")");

            return coord;
        }

        /// <summary>
        /// Reads a sequence of coordinates.
        /// </summary>
        /// <param name="ordinateFlags"></param>
        /// <returns>A sequence of coordinates.</returns>
        private IEnumerable<TCoordinate> ReadCoordinates(OrdinateFlags ordinateFlags)
        {
            return ReadCoordinates(ordinateFlags, false);
        }
            /// <summary>
        /// Reads a sequence of coordinates.
        /// </summary>
        /// <param name="ordinateFlags"></param>
        /// <returns>A sequence of coordinates.</returns>
        private IEnumerable<TCoordinate> ReadCoordinates(OrdinateFlags ordinateFlags, bool tryParens)
        {
            var coordinates = new List<TCoordinate>();

            if (GetEmptyOrLeftParen() == WktTokens.EmptySet)
            {
                return coordinates;
            }

            do
            {
                coordinates.Add(ReadCoordinate(ordinateFlags, tryParens));
            }
            while (GetCommaOrRightParen() == WktTokens.Comma);

            return coordinates;
        }

        /// <summary>
        /// Reads the next word or number from the input.
        /// </summary>
        /// <returns>The next word</returns>
        /// <exception cref="ParseException"></exception>
        private string ReadNextWord()
        {
            TokenType type = _tokenizer.Read();
            string token = _tokenizer.CurrentToken;

            switch (type)
            {
                case TokenType.Number:
                    throw new ParseException("Expected a number but encountered '" + token + "'");

                case TokenType.Word:
                    return token.ToUpper();
            }

            switch (token)
            {
                case WktTokens.LeftParen:
                case WktTokens.RightParen:
                case WktTokens.Comma:
                    return token;
            }

            throw new ParseException("Not a valid symbol in WKT format.");
        }

        /// <summary>
        /// Expects 'EMPTY' or '('.
        /// </summary>
        /// <returns>The token.</returns>
        private string GetEmptyOrLeftParen()
        {
            string nextWord = ReadNextWord();

            if (nextWord == WktTokens.EmptySet || nextWord == WktTokens.LeftParen)
            {
                return nextWord;
            }

            throw Expected(WktTokens.EmptySet, WktTokens.LeftParen);
        }

        /// <summary>
        /// Reads the next token and verifies that it is either a ',' or a ')'.
        /// </summary>
        /// <returns>Either ',' or ')'.</returns>
        /// <exception cref="ParseException">If token is not ',' or a ')'.</exception>
        private string GetCommaOrRightParen()
        {
            string nextWord = ReadNextWord();

            if (nextWord == WktTokens.Comma || nextWord == WktTokens.RightParen)
            {
                return nextWord;
            }

            throw Expected(WktTokens.RightParen, WktTokens.Comma);
        }

        /// <summary>
        /// Verifies that the next token is a ')'.
        /// </summary>
        private void GetRightParen()
        {
            _tokenizer.ReadToken(WktTokens.RightParen);
        }

        /// <summary>
        /// Reads the next token as a number.
        /// </summary>
        /// <returns>Next token as a number</returns>
        private double ReadNumber()
        {
            switch (_tokenizer.NextTokenType)
            {
                case TokenType.Number:
                    _tokenizer.Read();
                    return _tokenizer.CurrentTokenAsNumber;

                case TokenType.Symbol:
                    if (_tokenizer.NextToken != WktTokens.Period)
                    {
                        throw new ParseException("Expected number or '.' but encountered '" +
                            _tokenizer.NextToken + "'");
                    }

                    _tokenizer.Read();

                    if (_tokenizer.NextTokenType != TokenType.Number)
                    {
                        throw new ParseException("Expected number but encountered '" +
                            _tokenizer.NextToken + "'");
                    }

                    double number = double.Parse("0." + _tokenizer.NextToken);
                    _tokenizer.Read();
                    return number;
            }

            throw new ParseException("Expected number but encountered '" +
                _tokenizer.NextToken + "'");
        }

        #endregion

        /// <summary>
        /// Constructs an <see cref="ParseException"/> with a message appropriate for the 
        /// <paramref name="expectedTokens">expected tokens</paramref> passed as the argument.
        /// </summary>
        /// <param name="expectedTokens">Expected tokens</param>
        /// <returns>A <see cref="ParseException"/></returns>
        private ParseException Expected(params string[] expectedTokens)
        {
            if (expectedTokens.Length == 0)
            {
                throw new ArgumentException("must contain at least one element", "expectedTokens");
            }

            StringBuilder message = new StringBuilder("Expected '").
                Append(expectedTokens[0]).
                Append("'");

            if (expectedTokens.Length > 1)
            {
                int i = 1;

                // Append all expected tokens but the last
                while (i < expectedTokens.Length - 1)
                {
                    message.Append(", '").Append(expectedTokens[i++]).Append("'");
                }

                message.Append(" or '").Append(expectedTokens[i]).Append("'");
            }

            message.Append(" but encountered '").Append(_tokenizer.CurrentToken).Append("'");

            // Add location
            message.Append(" near line: ").Append(_tokenizer.LineNumber).
                Append(", column: ").Append(_tokenizer.Column).
                Append('.');

            return new ParseException(message.ToString());
        }
    }
}
