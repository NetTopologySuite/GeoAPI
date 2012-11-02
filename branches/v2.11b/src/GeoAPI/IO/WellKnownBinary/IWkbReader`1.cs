// Portions copyright 2005 - 2007: Diego Guidi
// Portions copyright 2006 - 2008: Rory Plaire (codekaizen@gmail.com)
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

using System;
using System.Collections.Generic;
using System.IO;
using GeoAPI.Coordinates;
using GeoAPI.Geometries;
using NPack.Interfaces;

namespace GeoAPI.IO.WellKnownBinary
{
    public interface IWkbReader<TCoordinate> : IWkbReader
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>,
                            IComparable<TCoordinate>, IConvertible,
                            IComputable<Double, TCoordinate>
    {
        new IGeometryFactory<TCoordinate> GeometryFactory { set; }
        new IGeometry<TCoordinate> Read(Byte[] wkb);
        new IGeometry<TCoordinate> Read(Byte[] wkb, Int32 offset);
        new IGeometry<TCoordinate> Read(IEnumerable<Byte> wkb);
        new IGeometry<TCoordinate> Read(Stream wkbData);
        new IGeometry<TCoordinate> Read(BinaryReader wkbData);
        new IEnumerable<IGeometry<TCoordinate>> ReadAll(Byte[] wkb);
        new IEnumerable<IGeometry<TCoordinate>> ReadAll(Byte[] wkb, Int32 offset);
        new IEnumerable<IGeometry<TCoordinate>> ReadAll(IEnumerable<Byte> wkb);
        new IEnumerable<IGeometry<TCoordinate>> ReadAll(BinaryReader wkbData);
    }
}
