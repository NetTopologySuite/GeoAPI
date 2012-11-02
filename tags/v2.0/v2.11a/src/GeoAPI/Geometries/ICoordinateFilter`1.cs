// Portions copyright 2005, 2007 - Diego Guidi
// Portions copyright 2006, 2007 - Rory Plaire (codekaizen@gmail.com)
//
// This file is part of GeoAPI.
// GeoAPI is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// GeoAPI is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with GeoAPI; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using GeoAPI.Coordinates;

namespace GeoAPI.Geometries
{
    // DESIGN_NOTE: Use Expression Over Interface.
    /// <summary>
    /// <see cref="IGeometry{TCoordinate}"/> classes support the concept of applying a
    /// coordinate filter to every coordinate in the <c>IGeometry<TCoordinate></c> instance. A
    /// coordinate filter can either record information about each coordinate or
    /// change the coordinate in some way. Coordinate filters implement the
    /// interface <c>ICoordinateFilter</c>. 
    /// <c>ICoordinateFilter</c> is an example of the Gang-of-Four Visitor pattern. 
    /// Coordinate filters can be
    /// used to implement such things as coordinate transformations, centroid and
    /// envelope computation, and many other functions.
    /// </summary>
    public interface ICoordinateFilter<TCoordinate>
        where TCoordinate : ICoordinate
    {
        /// <summary>
	    /// Performs an operation with or on <c>coord</c>.
    	/// </summary>
        /// <param name="coord"><c>Coordinate</c> to which the filter is applied.</param>
        void Filter(TCoordinate coord);
    }

}
