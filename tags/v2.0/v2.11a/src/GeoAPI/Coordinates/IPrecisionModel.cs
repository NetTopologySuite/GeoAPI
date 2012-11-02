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

namespace GeoAPI.Coordinates
{
    public interface IPrecisionModel : IComparable<IPrecisionModel>, 
                                       IEquatable<IPrecisionModel>
    {
        /// <summary>
        /// Gets the <see cref="ICoordinateFactory"/> assigned
        /// to generate new coordinates.
        /// </summary>
        ICoordinateFactory CoordinateFactory { get; }

        /// <summary> 
        /// Gets the type of this <see cref="IPrecisionModel"/>.
        /// </summary>
        PrecisionModelType PrecisionModelType { get; }

        /// <summary> 
        /// Tests whether the precision model supports floating point.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the precision model supports floating point.
        /// </returns>
        Boolean IsFloating { get; }

        /// <summary> 
        /// Rounds a numeric value to the <see cref="IPrecisionModel"/> 
        /// grid. Asymmetric arithmetic rounding is used, to provide
        /// uniform rounding behavior no matter where the number is
        /// on the number line.
        /// </summary>
        /// <param name="value">
        /// The value to make precise according to the 
        /// <see cref="IPrecisionModel"/>.
        /// </param>
        Double MakePrecise(Double value);

        /// <summary> 
        /// Rounds a <see name="ICoordinate"/> to the 
        /// <see cref="IPrecisionModel"/> grid.
        /// </summary>
        /// <param name="coord">
        /// The coordinate to make precise according to the precision model.
        /// </param>
        ICoordinate MakePrecise(ICoordinate coord);

        /// <summary>
        /// Returns the maximum number of significant digits provided by this
        /// precision model.
        /// Intended for use by routines which need to output precise values.
        /// </summary>
        /// <returns>
        /// The maximum number of decimal places provided by this precision model.
        /// </returns>
        Int32 MaximumSignificantDigits { get; }

        /// <summary> 
        /// Gets the scale factor which determines the number of 
        /// decimal places in fixed precision.
        /// </summary>
        /// <value>    
        /// The amount by which to multiply a coordinate after subtracting
        /// the offset.
        /// </value>
        Double Scale { get; }
    }
}
