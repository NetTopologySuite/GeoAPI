using System;
using System.Collections;
using System.Collections.Generic;

namespace GeoAPI.DataStructures.Collections.Generic
{
    public class CollectionWrapper<T> : ICollection<T>
    {
        private readonly ICollection _innerCollection;

        public CollectionWrapper(ICollection toWrap)
        {
            _innerCollection = toWrap;
        }

        #region ICollection<T> Members

        public void Add(T item)
        {
            throwReadOnlyException();
        }

        public void Clear()
        {
            throwReadOnlyException();
        }

        public Boolean Contains(T item)
        {
            foreach (Object o in _innerCollection)
            {
                if ((Object)item == o)
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            _innerCollection.CopyTo(array, arrayIndex);
        }

        public Int32 Count
        {
            get { return _innerCollection.Count; }
        }

        public Boolean IsReadOnly
        {
            get
            {
                return true; //always return true since the old ICollection does not support mutation 
            }
        }

        public Boolean Remove(T item)
        {
            throwReadOnlyException();
            return false;
        }

        #endregion

        private static void throwReadOnlyException()
        {
            throw new NotSupportedException("The ICollection is read-only.");
        }

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            foreach (Object o in _innerCollection)
            {
                yield return (T)o;
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}