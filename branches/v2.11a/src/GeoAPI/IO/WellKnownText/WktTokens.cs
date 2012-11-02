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

namespace GeoAPI.IO.WellKnownText
{
    /// <summary>
    /// Tokens used in the WKT grammar.
    /// </summary>
    internal static class WktTokens
    {
        public const string EmptySet = "EMPTY";

        public const string LeftParen = "(";

        public const string RightParen = ")";

        public const string Period = ".";

        public const string Comma = ",";

        public const string Z = "Z";

        public const string M = "M";

        public const string ZM = "ZM";

        public const string Point = "POINT";

        public const string LineString = "LINESTRING";

        public const string MultiPoint = "MULTIPOINT";

        public const string MultiLineString = "MULTILINESTRING";

        public const string Polygon = "POLYGON";

        public const string MultiPolygon = "MULTIPOLYGON";

        public const string GeometryCollection = "GEOMETRYCOLLECTION";

        public const string PolyhedralSurface = "POLYHEDRALSURFACE";

        public const string LinearRing = "LINEARRING";
    }
}
