namespace GeoAPI.Coordinates
{
    /// <summary>
    /// Ordinate indexer class
    /// </summary>
    public sealed class OrdinateIndexer
    {
        private readonly int[] _index = new int[5];
        private readonly Ordinates[] _ordinates = new Ordinates[5];

        /// <summary>
        ///
        /// </summary>
        /// <param name="ordinates"></param>
        public OrdinateIndexer(params Ordinates[] ordinates)
        {
            for (var i = 0; i < 5; i++)
                _index[i] = -1;

            for (var i = 0; i < ordinates.Length; i++)
            {
                _index[(int)ordinates[i]] = i;
                _ordinates[i] = ordinates[i];
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ordinate"></param>
        public int this[Ordinates ordinate]
        {
            get { return _index[(int)ordinate]; }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="index"></param>
        public Ordinates this[int index]
        {
            get { return _ordinates[index]; }
        }
    }
}