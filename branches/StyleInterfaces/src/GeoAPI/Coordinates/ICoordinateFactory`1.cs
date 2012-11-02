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
using NPack;
using NPack.Interfaces;

namespace GeoAPI.Coordinates
{
    public interface ICoordinateFactory<TCoordinate> : ICoordinateFactory
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>,
                            IComparable<TCoordinate>, IConvertible, 
                            IComputable<Double, TCoordinate>
    {
        new TCoordinate Create(Double x, Double y);
        new TCoordinate Create(Double x, Double y, Double m);
        new TCoordinate Create(params Double[] coordinates);
        new TCoordinate Create(IVector<DoubleComponent> coordinate);
        TCoordinate Create(TCoordinate coordinate);
        TCoordinate Create(ICoordinate coordinate);

        new TCoordinate Create3D(Double x, Double y, Double z);
        new TCoordinate Create3D(Double x, Double y, Double z, Double m);
        new TCoordinate Create3D(params Double[] coordinates);
        TCoordinate Create3D(TCoordinate coordinate);
        TCoordinate Create3D(ICoordinate coordinate);

        //TMatrix CreateTransform<TMatrix>(TCoordinate scaleVector,
        //                                 Double rotation,
        //                                 TCoordinate translateVector,
        //                                 ILinearFactory<DoubleComponent, TCoordinate, TMatrix> linearFactory)
        //    where TMatrix : IMatrix<DoubleComponent, TMatrix>;

        //TMatrix CreateTransform<TMatrix>(TCoordinate scaleVector, 
        //                                 TCoordinate rotationAxis,
        //                                 Double rotation,
        //                                 TCoordinate translateVector,
        //                                 ILinearFactory<DoubleComponent, TCoordinate, TMatrix> linearFactory)
        //    where TMatrix : IMatrix<DoubleComponent, TMatrix>;

        TCoordinate Homogenize(TCoordinate coordinate);
        IEnumerable<TCoordinate> Homogenize(IEnumerable<TCoordinate> coordinate);
        TCoordinate Dehomogenize(TCoordinate coordinate);
        IEnumerable<TCoordinate> Dehomogenize(IEnumerable<TCoordinate> coordinate);

        new IPrecisionModel<TCoordinate> PrecisionModel { get; }

        new IPrecisionModel<TCoordinate> CreatePrecisionModel(PrecisionModelType type);
        new IPrecisionModel<TCoordinate> CreatePrecisionModel(Double scale);
    }
}
