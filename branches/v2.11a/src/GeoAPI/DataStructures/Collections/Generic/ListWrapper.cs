using System;
using System.Collections;
using System.Collections.Generic;

namespace GeoAPI.DataStructures.Collections.Generic
{
    public class ListWrapper<T> : IList<T>
    {
        private readonly IList _innerList;

        public ListWrapper(IList toWrapp) 
        {
            _innerList = toWrapp;
        }

        #region IList<T> Members

        public Int32 IndexOf(T item)
        {
            return _innerList.IndexOf(item);
        }

        public void Insert(Int32 index, T item)
        {
            _innerList.Insert(index, item);
        }

        public void RemoveAt(Int32 index)
        {
            _innerList.Remove(index);
        }

        public T this[Int32 index]
        {
            get { return (T) _innerList[index]; }
            set { _innerList[index] = value; }
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            _innerList.Add(item);
        }

        public void Clear()
        {
            _innerList.Clear();
        }

        public Boolean Contains(T item)
        {
            return _innerList.Contains(item);
        }

        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            _innerList.CopyTo(array, arrayIndex);
        }

        public Int32 Count
        {
            get { return _innerList.Count; }
        }

        public Boolean IsReadOnly
        {
            get { return _innerList.IsReadOnly; }
        }

        public Boolean Remove(T item)
        {
            if (!_innerList.Contains(item))
            {
                return false;
            }
            _innerList.Remove(item);
            return true;
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            foreach (Object o in _innerList)
            {
                yield return (T) o;
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