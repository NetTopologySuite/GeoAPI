namespace GeoAPI.Geometries
{
    /// <summary>
    /// Static utility functions for dealing with <see cref="Ordinates"/> and dimension
    /// </summary>
    public static class OrdinatesUtility
    {
        /// <summary>
        /// Translates the <paramref name="ordinates"/>-flag to a number of dimensions.
        /// </summary>
        /// <param name="ordinates">The ordinates flag</param>
        /// <returns>The number of dimensions</returns>
        public static int OrdinatesToDimension(Ordinates ordinates)
        {
            var ret = 2;
            if ((ordinates & Ordinates.Z) != 0) ret++;
            if ((ordinates & Ordinates.M) != 0) ret++;

            return ret;
        }

        /// <summary>
        /// Translates a dimension value to an <see cref="Ordinates"/>-flag.
        /// </summary>
        /// <remarks>The flag for <see cref="Ordinate.Z"/> is set first.</remarks>
        /// <param name="dimension">The dimension.</param>
        /// <returns>The ordinates-flag</returns>
        public static Ordinates DimensionToOrdinates(int dimension)
        {
            if (dimension == 3)
                return Ordinates.XYZ;
            if (dimension == 4)
                return Ordinates.XYZM;
            return Ordinates.XY;
        }
    }
}