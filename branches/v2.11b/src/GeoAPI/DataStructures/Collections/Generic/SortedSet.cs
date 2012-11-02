/* Copyright © 2002-2004 by Aidant Systems, Inc., and by Jason Smith. */

using System;
using System.Collections.Generic;

namespace GeoAPI.DataStructures.Collections.Generic
{
    /// <summary>
    /// Implements a <see cref="Set{T}"/> based on a sorted tree.  
    /// This gives good performance for operations on very
    /// large data-sets, though not as good - asymptotically - as a 
    /// <see cref="HashedSet{T}"/>.  However, iteration
    /// occurs in order.  Elements that you put into this type of collection 
    /// must implement <see cref="IComparable{T}"/>,
    /// and they must actually be comparable.  
    /// You can't mix <see cref="String"/> and <see cref="Int32"/> values, 
    /// for example.
    /// </summary>
    [Serializable]
    public class SortedSet<T> : Set<T>
    {
        private readonly TreeList<T> _tree;

        /// <summary>
        /// Creates a new set instance based on a sorted tree.
        /// </summary>
        public SortedSet()
            : this(null, null) { }

        /// <summary>
        /// Creates a new set instance based on a sorted tree.
        /// </summary>
        /// <param name="comparer">
        /// The <see cref="IComparer{T}"/> to use for sorting.
        /// </param>
        public SortedSet(IComparer<T> comparer)
            : this(null, comparer) { }

        /// <summary>
        /// Creates a new set instance based on a sorted tree and
        /// initializes it based on a collection of elements.
        /// </summary>
        /// <param name="initialValues">
        /// An enumeration of elements that defines the initial set contents.
        /// </param>
        public SortedSet(IEnumerable<T> initialValues)
            : this(initialValues, null)
        {
            AddRange(initialValues);
        }

        /// <summary>
        /// Creates a new set instance based on a sorted tree and
        /// initializes it based on a collection of elements.
        /// </summary>
        /// <param name="initialValues">
        /// An enumeration of elements that defines the initial set contents.
        /// </param>
        /// <param name="comparer">
        /// The <see cref="IComparer{T}"/> to use for sorting.
        /// </param>
        public SortedSet(IEnumerable<T> initialValues, IComparer<T> comparer)
        {
            _tree = new TreeList<T>(comparer);
            if (initialValues != null) AddRange(initialValues);
        }

        public Int32 IndexOf(T item)
        {
            return _tree.IndexOf(item);
        }

        public override Boolean Add(T item)
        {
            if (!_tree.Contains(item))
            {
                _tree.Add(item);
                return true;
            }

            return false;
        }

        public override Boolean AddRange(IEnumerable<T> items)
        {
            Boolean changed = false;

            foreach (T item in items)
            {
                changed |= Add(item);
            }

            return changed;
        }

        public override Boolean ContainsAll(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                if (!Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        public override Boolean Remove(T item)
        {
            return _tree.Remove(item);
        }

        public override Boolean RemoveAll(IEnumerable<T> items)
        {
            Boolean removed = false;

            foreach (T item in items)
            {
                removed |= Remove(item);
            }

            return removed;
        }

        public override Boolean RetainAll(IEnumerable<T> items)
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            _tree.Clear();
        }

        public override Boolean IsEmpty
        {
            get { return _tree.Count == 0; }
        }

        public override Boolean Contains(T item)
        {
            return _tree.Contains(item);
        }

        public override Int32 Count
        {
            get { return _tree.Count; }
        }

        public override void CopyTo(T[] array, Int32 index)
        {
            _tree.CopyTo(array, index);
        }

        public override IEnumerator<T> GetEnumerator()
        {
            foreach (T item in _tree)
            {
                yield return item;
            }
        }

        public Int32 CountBefore(T item)
        {
            return _tree.CountBefore(item);
        }

        public Int32 CountAtAndBefore(T item)
        {
            return _tree.CountAtAndBefore(item);
        }

        public Int32 CountAfter(T item)
        {
            return _tree.CountAfter(item);
        }

        public Int32 CountAtAndAfter(T item)
        {
            return _tree.CountAtAndAfter(item);
        }
    }
}