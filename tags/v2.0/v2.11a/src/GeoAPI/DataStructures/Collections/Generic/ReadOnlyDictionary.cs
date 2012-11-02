using System;
using System.Collections;
using System.Collections.Generic;

namespace GeoAPI.DataStructures.Collections.Generic
{
    public class ReadOnlyDictionary<K, V> : IDictionary<K, V>, IDictionary
    {
        private readonly Dictionary<K, V> _internalDictionary;

        public ReadOnlyDictionary(Dictionary<K, V> dictionary)
        {
            _internalDictionary = new Dictionary<K, V>(dictionary);
        }

        void ICollection<KeyValuePair<K, V>>.Add(KeyValuePair<K, V> pair)
        {
            throw readOnly();
        }

        void IDictionary<K, V>.Add(K key, V value)
        {
            throw readOnly();
        }

        void IDictionary.Add(Object key, Object value)
        {
            throw readOnly();
        }

        void IDictionary.Clear()
        {
            throw readOnly();
        }

        void ICollection<KeyValuePair<K, V>>.Clear()
        {
            throw readOnly();
        }

        Boolean IDictionary.Contains(Object key)
        {
            return (_internalDictionary as IDictionary).Contains(key);
        }

        public Boolean Contains(KeyValuePair<K, V> pair)
        {
            return (_internalDictionary as ICollection<KeyValuePair<K, V>>).Contains(pair);
        }

        public Boolean ContainsKey(K key)
        {
            return _internalDictionary.ContainsKey(key);
        }

        public void CopyTo(Array array, Int32 index)
        {
            (_internalDictionary as ICollection).CopyTo(array, index);
        }

        public void CopyTo(KeyValuePair<K, V>[] array, Int32 arrayIndex)
        {
            (_internalDictionary as ICollection<KeyValuePair<K, V>>).CopyTo(array, arrayIndex);
        }

        Boolean IDictionary<K, V>.Remove(K key)
        {
            throw readOnly();
        }

        Boolean ICollection<KeyValuePair<K, V>>.Remove(KeyValuePair<K, V> pair)
        {
            throw readOnly();
        }

        void IDictionary.Remove(Object key)
        {
            throw readOnly();
        }

        IEnumerator<KeyValuePair<K, V>> IEnumerable<KeyValuePair<K, V>>.GetEnumerator()
        {
            return _internalDictionary.GetEnumerator();
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return _internalDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _internalDictionary.GetEnumerator();
        }

        public Boolean TryGetValue(K key, out V value)
        {
            return _internalDictionary.TryGetValue(key, out value);
        }

        // Properties
        public Int32 Count
        {
            get { return _internalDictionary.Count; }
        }

        public Boolean IsFixedSize
        {
            get { return true; }
        }

        public Boolean IsReadOnly
        {
            get { return true; }
        }

        Boolean ICollection.IsSynchronized
        {
            get { return ((ICollection) _internalDictionary).IsSynchronized; }
        }

        public V this[K key]
        {
            get { return _internalDictionary[key]; }
            set { throw readOnly(); }
        }

        Object IDictionary.this[Object key]
        {
            get { return ((IDictionary)_internalDictionary)[key]; }
            set { throw readOnly(); }
        }

        public ICollection<K> Keys
        {
            get { return _internalDictionary.Keys; }
        }

        Object ICollection.SyncRoot
        {
            get { return ((ICollection) _internalDictionary).SyncRoot; }
        }

        ICollection IDictionary.Keys
        {
            get { return ((IDictionary) _internalDictionary).Keys; }
        }

        ICollection IDictionary.Values
        {
            get { return ((IDictionary) _internalDictionary).Values; }
        }

        public ICollection<V> Values
        {
            get { return _internalDictionary.Values; }
        }

        private static Exception readOnly()
        {
            return new NotSupportedException("Dictionary is read-only.");
        }
    }
}