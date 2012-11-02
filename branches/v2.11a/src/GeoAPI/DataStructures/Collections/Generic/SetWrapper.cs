using System;
using System.Collections;
using System.Collections.Generic;

namespace GeoAPI.DataStructures.Collections.Generic
{
    /// <summary>
    /// A wrapper that can wrap a ISet as a generic ISet&lt;T&gt; 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <remarks>
    /// In most operations, there is no copying of collections. The wrapper just delegate the function to the wrapped.
    /// The following functions' implementation may involve collection copying:
    /// Union, Intersect, Minus, ExclusiveOr, ContainsAll, AddAll, RemoveAll, RetainAll
    /// </remarks>
    /// <exception cref="InvalidCastException">
    /// If the wrapped has any item that is not of Type T, InvalidCastException could be thrown at any time
    /// </exception>
    public sealed class SetWrapper<T> : ISet<T>
    {
        private readonly ISet _innerSet;

        public SetWrapper(ISet toWrap)
        {
            if (toWrap == null)
            {
                throw new ArgumentNullException();
            }

            _innerSet = toWrap;
        }

        #region ISet<T> Members

        #region Operators

        public ISet<T> Union(ISet<T> a)
        {
            return getSetCopy().Union(a);
        }

        public ISet<T> Intersect(ISet<T> a)
        {
            return getSetCopy().Intersect(a);
        }

        public ISet<T> Minus(ISet<T> a)
        {
            return getSetCopy().Minus(a);
        }

        public ISet<T> ExclusiveOr(ISet<T> a)
        {
            return getSetCopy().ExclusiveOr(a);
        }

        #endregion

        public Boolean Contains(T o)
        {
            return _innerSet.Contains(o);
        }

        public Boolean ContainsAll(IEnumerable<T> c)
        {
            return _innerSet.ContainsAll(getSetCopy(c));
        }

        public Boolean IsEmpty
        {
            get { return _innerSet.IsEmpty; }
        }

        public Boolean Add(T o)
        {
            return _innerSet.Add(o);
        }

        public Boolean AddRange(IEnumerable<T> c)
        {
            return _innerSet.AddRange(getSetCopy(c));
        }

        public Boolean Remove(T o)
        {
            return _innerSet.Remove(o);
        }

        public Boolean RemoveAll(IEnumerable<T> c)
        {
            return _innerSet.RemoveAll(getSetCopy(c));
        }

        public Boolean RetainAll(IEnumerable<T> c)
        {
            return _innerSet.RemoveAll(getSetCopy(c));
        }

        public void Clear()
        {
            _innerSet.Clear();
        }

        public ISet<T> Clone()
        {
            return new SetWrapper<T>((ISet) _innerSet.Clone());
        }

        public Int32 Count
        {
            get { return _innerSet.Count; }
        }

        #endregion

        #region ICollection<T> Members

        void ICollection<T>.Add(T item)
        {
            Add(item);
        }

        public void CopyTo(T[] array, Int32 arrayIndex)
        {
            _innerSet.CopyTo(array, arrayIndex);
        }

        public Boolean IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            foreach (Object o in _innerSet)
            {
                yield return (T) o;
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerSet.GetEnumerator();
        }

        #endregion

        #region ICloneable Members

        Object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

        #region private methods

        private static Set<T> getSetCopy(IEnumerable<T> c)
        {
            return new HashedSet<T>(c);
        }

        private static Set<T> getSetCopy(IEnumerable c)
        {
            Set<T> retVal = new HashedSet<T>();
            ((ISet) retVal).AddRange(c);
            return retVal;
        }

        private Set<T> getSetCopy()
        {
            return getSetCopy(_innerSet);
        }

        #endregion
    }
}