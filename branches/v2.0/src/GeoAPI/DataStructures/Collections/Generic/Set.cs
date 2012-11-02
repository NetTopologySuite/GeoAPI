/* Copyright © 2002-2004 by Aidant Systems, Inc., and by Jason Smith. */

using System;
using System.Collections;
using System.Collections.Generic;

namespace GeoAPI.DataStructures.Collections.Generic
{
    /// <summary>
    /// A collection that contains no duplicate elements.  
    /// This class models the mathematical <see cref="Set{T}"/> abstraction,
    /// and is the base class for all other <see cref="Set{T}"/> implementations.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The order of elements in a set is dependant on 
    /// (a) the data-structure implementation, and 
    /// (b) the implementation of the various <see cref="Set{T}"/> methods, 
    /// and thus is not guaranteed.
    /// </para>
    ///  
    /// <para>
    /// None of the <see cref="Set"/> implementations in this library 
    /// are guranteed to be thread-safe in any way.
    /// </para>
    /// 
    /// <p>The following table summarizes the binary operators that are supported by the <c>Set</c> class.</p>
    /// <list type="table">
    ///		<listheader>
    ///			<term>Operation</term>
    ///			<term>Description</term>
    ///			<term>Method</term>
    ///			<term>Operator</term>
    ///		</listheader>
    ///		<item>
    ///			<term>Union (OR)</term>
    ///			<term>Element included in result if it exists in either <c>A</c> OR <c>B</c>.</term>
    ///			<term><c>Union()</c></term>
    ///			<term><c>|</c></term>
    ///		</item>
    ///		<item>
    ///			<term>Intersection (AND)</term>
    ///			<term>Element included in result if it exists in both <c>A</c> AND <c>B</c>.</term>
    ///			<term><c>InterSect()</c></term>
    ///			<term><c>&amp;</c></term>
    ///		</item>
    ///		<item>
    ///			<term>Exclusive Or (XOR)</term>
    ///			<term>Element included in result if it exists in one, but not both, of <c>A</c> and <c>B</c>.</term>
    ///			<term><c>ExclusiveOr()</c></term>
    ///			<term><c>^</c></term>
    ///		</item>
    ///		<item>
    ///			<term>Minus (n/a)</term>
    ///			<term>Take all the elements in <c>A</c>.  Now, if any of them exist in <c>B</c>, remove
    ///			them.  Note that unlike the other operators, <c>A - B</c> is not the same as <c>B - A</c>.</term>
    ///			<term><c>Minus()</c></term>
    ///			<term><c>-</c></term>
    ///		</item>
    /// </list>
    /// <para>
    /// <see cref="Set{T}"/>; supports operator between <see cref="Set{T}"/>; 
    /// and <see cref="ISet"/>, however, the <see cref="ISet"/> must contains 
    /// only elements of type <typeparamref name="T"/>.
    /// </para>
    /// </remarks>
    [Serializable]
    public abstract class Set<T> : ISet<T>, ISet
    {
        public static ISet<T> Create(IEnumerable<T> range)
        {
            Set<T> set = new HybridSet<T>(range);
            return set;
        }

        public static ISet<T> Create(Func<T> generator, Predicate<T> rangeCondition)
        {
            Set<T> set = new HybridSet<T>();

            T value = generator();

            while (rangeCondition(value))
            {
                set.Add(value);
                value = generator();
            }

            return set;
        }

        #region members of ISet<T>

        /// <summary>
        /// Performs a "union" of the two sets, where all the elements
        /// in both sets are present.  That is, the element is included if it is in either <c>a</c> or <c>b</c>.
        /// Neither this set nor the input set are modified during the operation.  The return value
        /// is a <c>Clone()</c> of this set with the extra elements added in.
        /// </summary>
        /// <param name="a">A collection of elements.</param>
        /// <returns>A new <c>Set</c> containing the union of this <c>Set</c> with the specified collection.
        /// Neither of the input objects is modified by the union.</returns>
        public virtual ISet<T> Union(ISet<T> a)
        {
            ISet<T> resultSet = Clone();

            if (a != null)
            {
                resultSet.AddRange(a);
            }

            return resultSet;
        }


        /// <summary>
        /// Performs a "union" of two sets, where all the elements
        /// in both are present.  That is, the element is included if it is in either <c>a</c> or <c>b</c>.
        /// The return value is a <c>Clone()</c> of one of the sets (<c>a</c> if it is not <see langword="null" />) with elements of the other set
        /// added in.  Neither of the input sets is modified by the operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>A set containing the union of the input sets.  <see langword="null" /> if both sets are <see langword="null" />.</returns>
        public static ISet<T> Union(ISet<T> a, ISet<T> b)
        {
            if (a == null && b == null)
            {
                return null;
            }
            else if (a == null)
            {
                return b.Clone();
            }
            else if (b == null)
            {
                return a.Clone();
            }
            else
            {
                return a.Union(b);
            }
        }

        /// <summary>
        /// Performs a "union" of two sets, where all the elements
        /// in both are present.  That is, the element is included if it is in either <c>a</c> or <c>b</c>.
        /// The return value is a <c>Clone()</c> of one of the sets (<c>a</c> if it is not <see langword="null" />) with elements of the other set
        /// added in.  Neither of the input sets is modified by the operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>A set containing the union of the input sets.  <see langword="null" /> if both sets are <see langword="null" />.</returns>
        public static Set<T> operator |(Set<T> a, Set<T> b)
        {
            return (Set<T>)Union(a, b);
        }

        /// <summary>
        /// Performs an "intersection" of the two sets, where only the elements
        /// that are present in both sets remain.  That is, the element is included if it exists in
        /// both sets.  The <c>Intersect()</c> operation does not modify the input sets.  It returns
        /// a <c>Clone()</c> of this set with the appropriate elements removed.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <returns>The intersection of this set with <c>a</c>.</returns>
        public virtual ISet<T> Intersect(ISet<T> a)
        {
            ISet<T> resultSet = Clone();

            if (a != null)
            {
                resultSet.RetainAll(a);
            }
            else
            {
                resultSet.Clear();
            }

            return resultSet;
        }

        /// <summary>
        /// Performs an "intersection" of the two sets, where only the elements
        /// that are present in both sets remain.  That is, the element is included only if it exists in
        /// both <c>a</c> and <c>b</c>.  Neither input Object is modified by the operation.
        /// The result Object is a <c>Clone()</c> of one of the input objects (<c>a</c> if it is not <see langword="null" />) containing the
        /// elements from the intersect operation. 
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>The intersection of the two input sets.  <see langword="null" /> if both sets are <see langword="null" />.</returns>
        public static ISet<T> Intersect(ISet<T> a, ISet<T> b)
        {
            if (a == null && b == null)
            {
                return null;
            }
            else if (a == null)
            {
                return b.Intersect(a);
            }
            else
            {
                return a.Intersect(b);
            }
        }

        /// <summary>
        /// Performs an "intersection" of the two sets, where only the elements
        /// that are present in both sets remain.  That is, the element is included only if it exists in
        /// both <c>a</c> and <c>b</c>.  Neither input Object is modified by the operation.
        /// The result Object is a <c>Clone()</c> of one of the input objects (<c>a</c> if it is not <see langword="null" />) containing the
        /// elements from the intersect operation. 
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>The intersection of the two input sets.  <see langword="null" /> if both sets are <see langword="null" />.</returns>
        public static Set<T> operator &(Set<T> a, Set<T> b)
        {
            return (Set<T>)Intersect(a, b);
        }

        /// <summary>
        /// Performs a "minus" of set <c>b</c> from set <c>a</c>.  This returns a set of all
        /// the elements in set <c>a</c>, removing the elements that are also in set <c>b</c>.
        /// The original sets are not modified during this operation.  The result set is a <c>Clone()</c>
        /// of this <c>Set</c> containing the elements from the operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <returns>A set containing the elements from this set with the elements in <c>a</c> removed.</returns>
        public virtual ISet<T> Minus(ISet<T> a)
        {
            ISet<T> resultSet = Clone();

            if (a != null)
            {
                resultSet.RemoveAll(a);
            }

            return resultSet;
        }

        /// <summary>
        /// Performs a "minus" of set <c>b</c> from set <c>a</c>.  This returns a set of all
        /// the elements in set <c>a</c>, removing the elements that are also in set <c>b</c>.
        /// The original sets are not modified during this operation.  The result set is a <c>Clone()</c>
        /// of set <c>a</c> containing the elements from the operation. 
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>A set containing <c>A - B</c> elements.  <see langword="null" /> if <c>a</c> is <see langword="null" />.</returns>
        public static ISet<T> Minus(ISet<T> a, ISet<T> b)
        {
            if (a == null)
            {
                return null;
            }
            else
            {
                return a.Minus(b);
            }
        }

        /// <summary>
        /// Performs a "minus" of set <c>b</c> from set <c>a</c>.  This returns a set of all
        /// the elements in set <c>a</c>, removing the elements that are also in set <c>b</c>.
        /// The original sets are not modified during this operation.  The result set is a <c>Clone()</c>
        /// of set <c>a</c> containing the elements from the operation. 
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>A set containing <c>A - B</c> elements.  <see langword="null" /> if <c>a</c> is <see langword="null" />.</returns>
        public static Set<T> operator -(Set<T> a, Set<T> b)
        {
            return (Set<T>)Minus(a, b);
        }

        /// <summary>
        /// Performs an "exclusive-or" of the two sets, keeping only the elements that
        /// are in one of the sets, but not in both.  The original sets are not modified
        /// during this operation.  The result set is a <c>Clone()</c> of this set containing
        /// the elements from the exclusive-or operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <returns>A set containing the result of <c>a ^ b</c>.</returns>
        public virtual ISet<T> ExclusiveOr(ISet<T> a)
        {
            ISet<T> resultSet = Clone();

            foreach (T element in a)
            {
                if (resultSet.Contains(element))
                {
                    resultSet.Remove(element);
                }
                else
                {
                    resultSet.Add(element);
                }
            }

            return resultSet;
        }

        /// <summary>
        /// Performs an "exclusive-or" of the two sets, keeping only the elements that
        /// are in one of the sets, but not in both.  The original sets are not modified
        /// during this operation.  The result set is a <c>Clone()</c> of one of the sets
        /// (<c>a</c> if it is not <see langword="null" />) containing
        /// the elements from the exclusive-or operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>A set containing the result of <c>a ^ b</c>.  <see langword="null" /> if both sets are <see langword="null" />.</returns>
        public static ISet<T> ExclusiveOr(ISet<T> a, ISet<T> b)
        {
            if (a == null && b == null)
            {
                return null;
            }
            else if (a == null)
            {
                return b.Clone();
            }
            else if (b == null)
            {
                return a.Clone();
            }
            else
            {
                return a.ExclusiveOr(b);
            }
        }

        /// <summary>
        /// Performs an "exclusive-or" of the two sets, keeping only the elements that
        /// are in one of the sets, but not in both.  The original sets are not modified
        /// during this operation.  The result set is a <c>Clone()</c> of one of the sets
        /// (<c>a</c> if it is not <see langword="null" />) containing
        /// the elements from the exclusive-or operation.
        /// </summary>
        /// <param name="a">A set of elements.</param>
        /// <param name="b">A set of elements.</param>
        /// <returns>A set containing the result of <c>a ^ b</c>.  <see langword="null" /> if both sets are <see langword="null" />.</returns>
        public static Set<T> operator ^(Set<T> a, Set<T> b)
        {
            return (Set<T>)ExclusiveOr(a, b);
        }

        /// <summary>
        /// Adds the specified element to this set if it is not already present.
        /// </summary>
        /// <param name="item">The item to add to the set.</param>
        /// <returns>
        /// <see langword="true"/> if the item was added, 
        /// <see langword="false"/> if it was already present.
        /// </returns>	
        public abstract Boolean Add(T item);

        /// <summary>
        /// Adds all the elements in the specified enumeration to the 
        /// set if they are not already present.
        /// </summary>
        /// <param name="items">An enumeration of items to add to the set.</param>
        /// <returns>
        /// <see langword="true"/> is the set changed as a result 
        /// of this operation, <see langword="false"/> if not.
        /// </returns>
        public abstract Boolean AddRange(IEnumerable<T> items);

        /// <summary>
        /// Returns <see langword="true"/> if the set contains all the elements 
        /// in the specified enumeration.
        /// </summary>
        /// <param name="items">An enumeration of items.</param>
        /// <returns>
        /// <see langword="true"/> if the set contains all the 
        /// elements in the specified collection, <see langword="false"/> otherwise.
        /// </returns>
        public abstract Boolean ContainsAll(IEnumerable<T> items);

        /// <summary>
        /// Removes the specified element from the set.
        /// </summary>
        /// <param name="item">The element to be removed.</param>
        /// <returns>
        /// <see langword="true"/> if the set contained the specified element, 
        /// <see langword="false"/> otherwise.
        /// </returns>
        public abstract Boolean Remove(T item);

        /// <summary>
        /// Remove all the specified elements from this set, 
        /// if they exist in this set.
        /// </summary>
        /// <param name="items">An enumeration of elements to remove.</param>
        /// <returns>
        /// <see langword="true"/> if the set was modified as
        /// a result of this operation.
        /// </returns>
        public abstract Boolean RemoveAll(IEnumerable<T> items);

        /// <summary>
        /// Retains only the elements in this set that are contained 
        /// in the specified enumeration.
        /// </summary>
        /// <param name="items">
        /// An enumeration that defines the set of elements to be retained.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if this set changed as a result of this operation.
        /// </returns>
        public abstract Boolean RetainAll(IEnumerable<T> items);

        /// <summary>
        /// Removes all elements from the set.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Returns <see langword="true"/> if this set contains no elements.
        /// </summary>
        public abstract Boolean IsEmpty { get; }

        /// <summary>
        /// Returns <see langword="true"/> if this set contains the specified element.
        /// </summary>
        /// <param name="item">The element to look for.</param>
        /// <returns>
        /// <see langword="true"/> if this set contains the specified element, 
        /// <see langword="false"/> otherwise.
        /// </returns>
        public abstract Boolean Contains(T item);

        #endregion

        #region members of ISet

        Boolean ISet.Add(Object o)
        {
            return Add((T)o);
        }

        Boolean ISet.AddRange(IEnumerable c)
        {
            return AddRange(getEnumerableWrapper(c));
        }

        Boolean ISet.Contains(Object o)
        {
            return Contains((T)o);
        }

        Boolean ISet.Remove(Object o)
        {
            return Remove((T)o);
        }

        Boolean ISet.ContainsAll(IEnumerable c)
        {
            return ContainsAll(getEnumerableWrapper(c));
        }

        Boolean ISet.RetainAll(IEnumerable c)
        {
            return RetainAll(getEnumerableWrapper(c));
        }

        Boolean ISet.RemoveAll(IEnumerable c)
        {
            return RemoveAll(getEnumerableWrapper(c));
        }

        ISet ISet.Union(ISet a)
        {
            return (ISet)Union(getWrapper(a));
        }

        ISet ISet.Minus(ISet a)
        {
            return (ISet)Minus(getWrapper(a));
        }

        ISet ISet.Intersect(ISet a)
        {
            return (ISet)Intersect(getWrapper(a));
        }

        ISet ISet.ExclusiveOr(ISet a)
        {
            return (ISet)ExclusiveOr(getWrapper(a));
        }

        #endregion

        #region members of ICloneable

        /// <summary>
        /// Returns a clone of the <c>Set</c> instance.  This will work for derived <c>Set</c>
        /// classes if the derived class implements a constructor that takes no arguments.
        /// </summary>
        /// <returns>A clone of this object.</returns>
        public virtual ISet<T> Clone()
        {
            Set<T> newSet = (Set<T>)Activator.CreateInstance(GetType());
            newSet.AddRange(this);
            return newSet;
        }

        Object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

        #region members of ICollection

        void ICollection.CopyTo(Array array, Int32 index)
        {
            checkTargetArray(array, index);
            T[] buf = new T[Count];
            CopyTo(buf, index);
            Array.Copy(buf, 0, array, index, Count);
        }

        /// <summary>
        /// The number of elements currently contained in this collection.
        /// </summary>
        public abstract Int32 Count { get; }

        Boolean ICollection.IsSynchronized { get { return false; } }

        Object ICollection.SyncRoot
        {
            get { return null; }
        }

        #endregion

        #region members of ICollection<T>

        void ICollection<T>.Add(T o)
        {
            Add(o);
        }

        /// <summary>
        /// Copies the elements in the <c>Set</c> to an array.  The type of array needs
        /// to be compatible with the objects in the <c>Set</c>, obviously.
        /// </summary>
        /// <param name="array">An array that will be the target of the copy operation.</param>
        /// <param name="index">The zero-based index where copying will start.</param>
        public abstract void CopyTo(T[] array, Int32 index);

        /// <summary>
        /// Returns whether this collection is readonly.
        /// </summary>
        public virtual Boolean IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region members of IEnumerable<T>

        /// <summary>
        /// Gets an enumerator for the elements in the <c>Set</c>.
        /// </summary>
        /// <returns>An <c>IEnumerator</c> over the elements in the <c>Set</c>.</returns>
        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Private methods

        private static ISet<T> getWrapper(ISet set)
        {
            if (set == null)
            {
                return null;
            }

            return new SetWrapper<T>(set);
        }

        private static IEnumerable<T> getEnumerableWrapper(IEnumerable c)
        {
            if (c == null)
            {
                yield break;
            }

            foreach (Object o in c)
            {
                yield return (T)o;
            }
        }

        private void checkTargetArray(Array array, Int32 arrayIndex)
        {
            if (array == null)
            {
                throw new ArgumentNullException("array");
            }

            if (array.Rank > 1)
            {
                throw new ArgumentException(
                    "Argument cannot be multidimensional.", "array");
            }

            if (arrayIndex < 0)
            {
                throw new ArgumentOutOfRangeException("arrayIndex",
                                                      arrayIndex, "Argument cannot be negative.");
            }

            if (arrayIndex >= array.Length)
            {
                throw new ArgumentException(
                    "Argument must be less than array length.", "arrayIndex");
            }

            if (Count > array.Length - arrayIndex)
            {
                throw new ArgumentException(
                    "Argument section must be large enough for collection.", "array");
            }
        }

        #endregion

    }
}