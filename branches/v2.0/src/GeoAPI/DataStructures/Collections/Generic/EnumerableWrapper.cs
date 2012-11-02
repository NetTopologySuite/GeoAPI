using System;
using System.Collections;
using System.Collections.Generic;

namespace GeoAPI.DataStructures.Collections.Generic
{
    /// <summary>
    /// A Simple Wrapper for wrapping an regular Enumerable as a generic Enumberable&lt;T&gt
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <exception cref="InvalidCastException">
    /// If the wrapped has any item that is not of Type T, InvalidCastException could be thrown at any time
    /// </exception>
    public class EnumerableWrapper<T> : IEnumerable<T>
    {
        private IEnumerable innerEnumerable;

        public EnumerableWrapper(IEnumerable innerEnumerable)
        {
            this.innerEnumerable = innerEnumerable;
        }

        public override Boolean Equals(object obj)
        {
            if (!obj.GetType().Equals(GetType()))
            {
                return false;
            }
            
            if (obj == this)
            {
                return true;
            }

            return innerEnumerable.Equals(
                ((EnumerableWrapper<T>) obj).innerEnumerable);
        }

        public override Int32 GetHashCode()
        {
            return innerEnumerable.GetHashCode();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new EnumeratorWrapper<T>(innerEnumerable.GetEnumerator());
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return innerEnumerable.GetEnumerator();
        }
    }
}