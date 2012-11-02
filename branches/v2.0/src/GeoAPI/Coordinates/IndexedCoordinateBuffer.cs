using System;
using System.Collections.Generic;
using System.ComponentModel;
using NPack;
using NPack.Interfaces;

namespace GeoAPI.Coordinates
{
    public class IndexedCoordinateBuffer : IVectorBuffer<BufferedCoordinate2D, DoubleComponent>
    {
        #region IVectorBuffer<BufferedCoordinate2D,DoubleComponent> Members

        public Int32 Add(IVector<DoubleComponent> vector)
        {
            throw new NotImplementedException();
        }

        public BufferedCoordinate2D Add(params DoubleComponent[] components)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public Boolean Contains(IVector<DoubleComponent> item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(BufferedCoordinate2D[] array, Int32 startIndex, Int32 endIndex)
        {
            throw new NotImplementedException();
        }

        public Int32 Count
        {
            get { throw new NotImplementedException(); }
        }

        public Boolean IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public Int64 MaximumSize
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Remove(Int32 index)
        {
            throw new NotImplementedException();
        }

        public event EventHandler SizeIncreased;

        public event CancelEventHandler SizeIncreasing;

        public event EventHandler<VectorOperationEventArgs<BufferedCoordinate2D, DoubleComponent>> VectorChanged;

        public Int32 VectorLength
        {
            get { throw new NotImplementedException(); }
        }

        public BufferedCoordinate2D this[Int32 index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IEnumerable<BufferedCoordinate2D> Members

        public IEnumerator<BufferedCoordinate2D> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
