/* Copyright © 2002-2004 by Aidant Systems, Inc., and by Jason Smith. */
using System;
using System.Collections.Generic;

namespace GeoAPI.DataStructures.Collections.Generic
{
    /// <summary>
    /// <see cref="DictionarySet{T}"/> is an abstract class that supports the 
    /// creation of new <see cref="Set{T}"/> types where the underlying 
    /// data store is an <see cref="IDictionary{TKey, TValue}"/> instance,
    /// allowing for fast lookup, addition and removal of values, but 
    /// at the cost of lacking any defined ordering.
    /// </summary>
    /// <remarks> 
    /// <para>
    /// You can use any object that implements the <see cref="IDictionary{TKey, TValue}"/>
    /// interface to hold set data. You can define your own, or you can use one of 
    /// the objects provided in the Framework. The type of 
    /// <see cref="IDictionary{TKey, TValue}"/> you choose will affect both the 
    /// performance and the behavior of the <see cref="Set{T}"/> using it. 
    /// </para>
    /// <para>To make a <see cref="Set{T}"/> typed based on your own 
    /// <see cref="IDictionary{TKey, TValue}"/>, simply derive a
    /// new class with a constructor that takes no parameters.  
    /// Some <see cref="Set{T}"/> implmentations
    /// cannot be defined with a default constructor.  
    /// If this is the case for your class, 
    /// you will need to override <see cref="ICloneable.Clone()"/> as well.
    /// </para>
    /// <para>
    /// It is also standard practice that at least one of your constructors 
    /// takes an <see cref="ICollection{T}"/> or an <see cref="ISet{T}"/> 
    /// as an argument.
    /// </para>
    /// </remarks>
    [Serializable]
    public abstract class DictionarySet<T> : Set<T>
    {
        /// <summary>
        /// Provides the storage for elements in the <c>Set</c>, stored as the key-set
        /// of the <c>IDictionary</c> object.  Set this Object in the constructor
        /// if you create your own <c>Set</c> class.  
        /// </summary>
        protected IDictionary<T, Object> InternalDictionary;

        private static readonly Object PlaceholderObject = new Object();

        /// <summary>
        /// The placeholder object used as the value for the <c>IDictionary</c> instance.
        /// </summary>
        /// <remarks>
        /// There is a single instance of this object globally, used for all <c>Sets</c>.
        /// </remarks>
        protected static Object Placeholder
        {
            get { return PlaceholderObject; }
        }

        public override Boolean Add(T o)
        {
            //The Object we are adding is just a placeholder.  The thing we are
            //really concerned with is 'o', the key.
            if (!InternalDictionary.ContainsKey(o))
            {
                InternalDictionary.Add(o, PlaceholderObject);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds all the elements in the specified collection to the set if they are not already present.
        /// </summary>
        /// <param name="c">A collection of objects to add to the set.</param>
        /// <returns><see langword="true"/> is the set changed as a result of this operation, <see langword="false"/> if not.</returns>
        public override Boolean AddRange(IEnumerable<T> c)
        {
            Boolean changed = false;

            foreach (T o in c)
            {
                if (! Contains(o))
                {
                    Add(o);
                    changed = true;
                }
            }

            return changed;
        }

        /// <summary>
        /// Removes all objects from the set.
        /// </summary>
        public override void Clear()
        {
            InternalDictionary.Clear();
        }

        /// <summary>
        /// Returns <see langword="true"/> if this set contains the specified element.
        /// </summary>
        /// <param name="o">The element to look for.</param>
        /// <returns><see langword="true"/> if this set contains the specified element, <see langword="false"/> otherwise.</returns>
        public override Boolean Contains(T o)
        {
            return InternalDictionary.ContainsKey(o);
        }

        /// <summary>
        /// Returns <see langword="true"/> if the set contains all the elements in the specified collection.
        /// </summary>
        /// <param name="c">A collection of objects.</param>
        /// <returns><see langword="true"/> if the set contains all the elements in the specified collection, <see langword="false"/> otherwise.</returns>
        public override Boolean ContainsAll(IEnumerable<T> c)
        {
            foreach (T o in c)
            {
                if (! Contains(o))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Returns <see langword="true"/> if this set contains no elements.
        /// </summary>
        public override Boolean IsEmpty
        {
            get { return InternalDictionary.Count == 0; }
        }

        /// <summary>
        /// Removes the specified element from the set.
        /// </summary>
        /// <param name="o">The element to be removed.</param>
        /// <returns><see langword="true"/> if the set contained the specified element, <see langword="false"/> otherwise.</returns>
        public override Boolean Remove(T o)
        {
            Boolean contained = Contains(o);

            if (contained)
            {
                InternalDictionary.Remove(o);
            }

            return contained;
        }

        /// <summary>
        /// Remove all the specified elements from this set, if they exist in this set.
        /// </summary>
        /// <param name="c">A collection of elements to remove.</param>
        /// <returns><see langword="true"/> if the set was modified as a result of this operation.</returns>
        public override Boolean RemoveAll(IEnumerable<T> c)
        {
            Boolean changed = false;

            foreach (T o in c)
            {
                changed |= Remove(o);
            }

            return changed;
        }

        /// <summary>
        /// Retains only the elements in this set that are contained in the 
        /// specified collection.
        /// </summary>
        /// <param name="c">
        /// Enumeration that defines the set of elements to be retained.
        /// </param>
        /// <returns>
        /// <see langword="true"/> if this set changed as a result of this operation.
        /// </returns>
        public override Boolean RetainAll(IEnumerable<T> c)
        {
            //Put data from C into a set so we can use the Contains() method.
            Set<T> cSet = new HybridSet<T>(c);

            //We are going to build a set of elements to remove.
            Set<T> removeSet = new HybridSet<T>();

            foreach (T o in this)
            {
                //If C does not contain O, then we need to remove O from our
                //set.  We can't do this while iterating through our set, so
                //we put it into RemoveSet for later.
                if (!cSet.Contains(o))
                {
                    removeSet.Add(o);
                }
            }

            return RemoveAll(removeSet);
        }


        /// <summary>
        /// Copies the elements in the <c>Set</c> to an array.  The type of array needs
        /// to be compatible with the objects in the <c>Set</c>, obviously.
        /// </summary>
        /// <param name="array">An array that will be the target of the copy operation.</param>
        /// <param name="index">The zero-based index where copying will start.</param>
        public override void CopyTo(T[] array, Int32 index)
        {
            InternalDictionary.Keys.CopyTo(array, index);
        }

        /// <summary>
        /// The number of elements contained in this collection.
        /// </summary>
        public override Int32 Count
        {
            get { return InternalDictionary.Count; }
        }

        /// <summary>
        /// Gets an enumerator for the elements in the <c>Set</c>.
        /// </summary>
        /// <returns>An <c>IEnumerator</c> over the elements in the <c>Set</c>.</returns>
        public override IEnumerator<T> GetEnumerator()
        {
            return InternalDictionary.Keys.GetEnumerator();
        }
    }
}