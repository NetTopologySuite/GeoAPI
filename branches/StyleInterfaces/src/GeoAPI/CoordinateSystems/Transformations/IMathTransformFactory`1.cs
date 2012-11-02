// Portions copyright 2005 - 2006: Morten Nielsen (www.iter.dk)
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
using GeoAPI.Coordinates;
using NPack;
using NPack.Interfaces;

namespace GeoAPI.CoordinateSystems.Transformations
{
    /// <summary>
    /// Creates math transforms.
    /// </summary>
    /// <remarks>
    /// <para>
    /// An <see cref="IMathTransformFactory{TCoordinate}"/> is a low level 
    /// factory that is used to create <see cref="IMathTransform{TCoordinate}"/> 
    /// instances. Many high level GIS applications will never need to use a 
    /// IMathTransformFactory directly; they can use an 
    /// <see cref="ICoordinateTransformationFactory{TCoordinate}"/> instnace instead. 
    /// However, the IMathTransformFactory interface is specified here, since it can 
    /// be used directly by applications that wish to transform other types of 
    /// coordinates (e.g. color coordinates, or image pixel coordinates).
    /// </para>
    /// <para>
    /// The following comments assume that the same vendor implements the math 
    /// transform factory interfaces and math transform interfaces.
    /// </para>
    /// <para>
    /// A math transform is an object that actually does the work of applying formulae 
    /// to coordinate values. The math transform does not know or care how the 
    /// coordinates relate to positions in the real world. This lack of semantics 
    /// makes implementing IMathTransformFactory significantly easier than it would 
    /// be otherwise.
    /// </para>
    /// <para>
    /// For example IMathTransformFactory can create affine math transforms. 
    /// The affine transform applies a matrix to the coordinates without knowing how 
    /// what it is doing relates to the real world. So if the matrix scales Z values 
    /// by a factor of 1000, then it could be converting meters into millimeters, or 
    /// it could be converting kilometers into meters.
    /// </para>
    /// <para>
    /// Because math transforms have low semantic value (but high mathematical value), 
    /// programmers who do not have much knowledge of how GIS applications use 
    /// coordinate systems, or how those coordinate systems relate to the real world 
    /// can implement IMathTransformFactory.
    /// </para>
    /// <para>
    /// The low semantic content of math transforms also means that they will be 
    /// useful in applications that have nothing to do with GIS coordinates. 
    /// For example, a math transform could be used to map color coordinates between 
    /// different color spaces, such as converting (red, green, blue) 
    /// colors into (hue, light, saturation) colors.
    /// </para>
    /// <para>
    /// Since a math transform does not know what its source and target coordinate 
    /// systems mean, it is not necessary or desirable for a math transform object 
    /// to keep information on its source and target coordinate systems.
    /// </para>
    /// </remarks>
    public interface IMathTransformFactory<TCoordinate>
        where TCoordinate : ICoordinate<TCoordinate>, IEquatable<TCoordinate>,
                            IComparable<TCoordinate>, IConvertible,
                            IComputable<Double, TCoordinate> 
    {
        /// <summary>
        /// Creates an affine transform from a matrix.
        /// </summary>
        /// <remarks>
        /// If the transform's input dimension is M, and output dimension is N, 
        /// then the matrix will have size [N+1][M+1]. The +1 in the matrix dimensions 
        /// allows the matrix to do a shift, as well as a rotation. The [M][j] element 
        /// of the matrix will be the j'th ordinate of the moved origin. The [i][N] 
        /// element of the matrix will be 0 for i less than M, and 1 for i equals M.
        /// </remarks>
        /// <param name="matrix"></param>
        /// <returns></returns>
        IMathTransform<TCoordinate> CreateAffineTransform(IMatrix<DoubleComponent> matrix);

        /// <summary>
        /// Creates a transform by concatenating two existing transforms. A concatenated 
        /// transform acts in the same way as applying two transforms, one after the other.
        /// </summary>
        /// <remarks>
        /// The dimension of the output space of the first transform must match the dimension 
        /// of the input space in the second transform. If you wish to concatenate more than 
        /// two transforms, then you can repeatedly use this method.
        /// </remarks>
        /// <param name="transform1"></param>
        /// <param name="transform2"></param>
        /// <returns></returns>
        IMathTransform<TCoordinate> CreateConcatenatedTransform(
                                            IMathTransform<TCoordinate> transform1, 
                                            IMathTransform<TCoordinate> transform2);

        /// <summary>
        /// Creates a math transform from a Well-Known Text string.
        /// </summary>
        /// <param name="wkt"></param>
        /// <returns></returns>
        IMathTransform<TCoordinate> CreateFromWkt(String wkt);

        /// <summary>
        /// Creates a math transform from XML.
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        IMathTransform<TCoordinate> CreateFromXml(String xml);

        /// <summary>
        /// Creates a transform from a classification name and parameters.
        /// </summary>
        /// <remarks>
        /// The client must ensure that all the linear parameters are expressed in meters, 
        /// and all the angular parameters are expressed in degrees. Also, they must 
        /// supply "semi_major" and "semi_minor" parameters for cartographic projection transforms.
        /// </remarks>
        /// <param name="classification"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IMathTransform<TCoordinate> CreateParameterizedTransform(String classification,
                                                                 IEnumerable<Parameter> parameters);

        /// <summary>
        /// Creates a transform which passes through a subset of ordinates to another transform.
        /// </summary>
        /// <remarks>
        /// This allows transforms to operate on a subset of ordinates. For example, 
        /// if you have (Lat, Lon, Height) coordinates, then you may wish to convert the 
        /// height values from meters to feet without affecting the (Lat, Lon) values. 
        /// If you wanted to affect the (Lat, Lon) values and leave the Height values alone, 
        /// then you would have to swap the ordinates around to (Height, Lat, Lon). 
        /// You can do this with an affine map.
        /// </remarks>
        /// <param name="firstAffectedOrdinate"></param>
        /// <param name="subTransform"></param>
        /// <returns></returns>
        IMathTransform<TCoordinate> CreatePassThroughTransform(Int32 firstAffectedOrdinate,
                                                               IMathTransform<TCoordinate> subTransform);

        /// <summary>
        /// Tests whether parameter is angular. Clients must ensure that all 
        /// angular parameter values are in degrees.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        Boolean IsParameterAngular(String parameterName);

        /// <summary>
        /// Tests whether parameter is linear. Clients must ensure that all 
        /// linear parameter values are in meters.
        /// </summary>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        Boolean IsParameterLinear(String parameterName);
    }
}