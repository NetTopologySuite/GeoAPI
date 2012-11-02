// Portions copyright 2005 - 2007: Diego Guidi
// Portions copyright 2006 - 2008: Rory Plaire (codekaizen@gmail.com)
//
// This file is part of GeoAPI.Net.
// GeoAPI.Net is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// GeoAPI.Net is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with GeoAPI.Net; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

// =======================================================
// yes, the conditional on the whole class is heavy-handed,
// but it will allow us to flag where this is used in the code
// and subsequently change it to allow use of .Net 3.5 types.
#if !DOTNET35
using System;
using System.Collections;
using System.Collections.Generic;

namespace GeoAPI.DataStructures
{
    public static class Enumerable
    {
        /// <summary>
        /// Executes a function on each item in a <see cref="ICollection{T}" /> 
        /// but does not accumulate the result.
        /// </summary>
        public static void Apply<TItem>(IEnumerable<TItem> items, Action<TItem> action)
        {
            foreach (TItem item in items)
            {
                action(item);
            }
        }

        public static Double Average(IEnumerable<Int32> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            Int64 total = 0;
            Int64 count = 0;

            foreach (Int32 item in items)
            {
                total += item;
                count += 1;
            }

            if (count == 0)
            {
                throw new InvalidOperationException("No items in the enumeration.");
            }

            return (((Double)total) / ((Double)count));
        }

        public static Boolean All<TItem>(IEnumerable<TItem> source, Func<TItem, Boolean> predicate)
        {
            checkSourceAndPredicate(source, predicate);

            foreach (TItem local in source)
            {
                if (!predicate(local))
                {
                    return false;
                }
            }

            return true;
        }

        public static Boolean Contains<TItem>(IEnumerable<TItem> source, TItem item)
        {
            return Contains(source, item, null);
        }

        public static Boolean Contains<TItem>(IEnumerable<TItem> source, TItem item, IEqualityComparer<TItem> comparer)
        {
            ICollection<TItem> collection = source as ICollection<TItem>;

            if (collection != null)
            {
                return collection.Contains(item);
            }

            checkSource(source);

            comparer = comparer ?? EqualityComparer<TItem>.Default;

            foreach (TItem sourceItem in source)
            {
                if (comparer.Equals(sourceItem, item))
                {
                    return true;
                }
            }

            return false;
        }

        public static TItem First<TItem>(IEnumerable<TItem> source)
        {
            checkSource(source);
            IEnumerator<TItem> enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException("Source enumeration is empty.");
            }

            return enumerator.Current;
        }

        public static TItem Last<TItem>(IEnumerable<TItem> source)
        {
            checkSource(source);
            IEnumerator<TItem> enumerator = source.GetEnumerator();

            TItem last = default(TItem);

            if (!enumerator.MoveNext())
            {
                throw new InvalidOperationException("Source enumeration is empty.");
            }

            do
            {
                last = enumerator.Current;
            } while (enumerator.MoveNext());

            return last;
        }

        public static IEnumerable<TItem> Reverse<TItem>(IEnumerable<TItem> items)
        {
            IList<TItem> list = items as IList<TItem>;

            if (list != null)
            {
                return reverseList(list);
            }

            ICollection<TItem> collection = items as ICollection<TItem>;

            if (collection != null)
            {
                return reverseCollection(collection);
            }

            return Reverse(items.GetEnumerator());
        }

        // jd:changed to prevent stack overflow due to recursion
        // [codekaizen] anyway to tail call this in C#?
        public static IEnumerable<TItem> Reverse<TItem>(IEnumerator<TItem> enumerator)
        {
            List<TItem> list = new List<TItem>();

            while (enumerator.MoveNext())
            {
                list.Add(enumerator.Current);
            }

            for (Int32 i = list.Count - 1; i >= 0; i--)
            {
                yield return list[i];
            }
        }

        public static IEnumerable<TItem> Skip<TItem>(IEnumerable<TItem> items, Int32 startIndex)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (startIndex < 0)
            {
                throw new ArgumentOutOfRangeException("startIndex", startIndex,
                    "Parameter must be greater than or equal to 0.");
            }

            if (items is IList<TItem>)
            {
                IList<TItem> list = items as IList<TItem>;

                for (Int32 i = startIndex; i < list.Count; i++)
                {
                    yield return list[i];
                }
            }
            else
            {
                Int32 currentIndex = 0;

                foreach (TItem item in items)
                {
                    if (currentIndex >= startIndex)
                    {
                        yield return item;
                    }

                    currentIndex += 1;
                }
            }
        }

        public static Boolean SequenceEqual<TItem>(IEnumerable<TItem> source, IEnumerable<TItem> target)
        {
            return SequenceEqual(source, target, null);
        }

        public static Boolean SequenceEqual<TItem>(IEnumerable<TItem> source,
                                                   IEnumerable<TItem> target,
                                                   IEqualityComparer<TItem> comparer)
        {
            checkSource(source);

            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            IEnumerator<TItem> sourceEnumerator = source.GetEnumerator();
            IEnumerator<TItem> targetEnumerator = target.GetEnumerator();

            comparer = comparer ?? EqualityComparer<TItem>.Default;

            while (sourceEnumerator.MoveNext())
            {
                if (!targetEnumerator.MoveNext() ||
                    !comparer.Equals(sourceEnumerator.Current, targetEnumerator.Current))
                {
                    return false;
                }
            }

            return !targetEnumerator.MoveNext();
        }

        public static Double Sum<T>(IEnumerable<T> items, Func<T, Double> selector)
        {
            Double sum = 0;

            foreach (T item in items)
            {
                sum += selector(item);
            }

            return sum;
        }

        public static IEnumerable<TItem> Take<TItem>(IEnumerable<TItem> items, Int32 count)
        {
            Int32 itemIndex = 0;

            foreach (TItem item in items)
            {
                if (itemIndex == count)
                {
                    break;
                }

                yield return item;
                itemIndex++;
            }
        }

        ////jd: moved to Processor.Where

        ///// <summary>
        ///// Executes a function on each item in a <see cref="IEnumerable{T}" />
        ///// and collects all the entries for which the result
        ///// of the function is equal to <see langword="true"/>.
        ///// </summary>
        //public static IEnumerable<TItem> Select<TItem>(IEnumerable<TItem> items,
        //                                               Predicate<TItem> where)
        //{
        //    foreach (TItem item in items)
        //    {
        //        if (where(item))
        //        {
        //            yield return item;
        //        }
        //    }
        //}

        public static Int32 Count<TItem>(IEnumerable<TItem> items)
        {
            if (items == null)
            {
                return 0;
            }

            ICollection<TItem> collection = items as ICollection<TItem>;

            if (collection != null)
            {
                return collection.Count;
            }

            IEnumerator<TItem> enumerator = items.GetEnumerator();

            Int32 count = 0;

            while (enumerator.MoveNext()) count++;

            return count;
        }

        public static TItem[] ToArray<TItem>(IEnumerable<TItem> items)
        {
            if (ReferenceEquals(items, null))
            {
                return null;
            }

            List<TItem> list = items as List<TItem>;

            if (!ReferenceEquals(list, null))
            {
                return list.ToArray();
            }

            ICollection<TItem> collection = items as ICollection<TItem>;

            if (!ReferenceEquals(collection, null))
            {
                TItem[] array = new TItem[collection.Count];
                collection.CopyTo(array, 0);
                return array;
            }

            list = new List<TItem>(items);
            return list.ToArray();
        }

        public static Boolean All(IEnumerable source, Func<Object, Boolean> predicate)
        {
            checkSourceAndPredicate(source, predicate);

            foreach (Object local in source)
            {
                if (!predicate(local))
                {
                    return false;
                }
            }

            return true;
        }

        public static Boolean Contains(IEnumerable source, Object item)
        {
            IList list = source as IList;

            if (list != null)
            {
                return list.Contains(item);
            }

            checkSource(source);

            foreach (Object sourceItem in source)
            {
                if (Equals(sourceItem, item))
                {
                    return true;
                }
            }

            return false;
        }

        public static Int32 Count(IEnumerable source)
        {
            Int32 count = 0;

            ICollection collection = source as ICollection;

            if (collection != null)
            {
                return collection.Count;
            }

            IEnumerator enumerator = source.GetEnumerator();

            while (enumerator.MoveNext())
            {
                count++;
            }

            return count;
        }

        public static Int32 Count(IEnumerable source, Func<Object, Boolean> func)
        {
            Int32 count = 0;

            IEnumerator enumerator = source.GetEnumerator();

            while (enumerator.MoveNext() && func(enumerator.Current))
            {
                count++;
            }

            return count;
        }

        public static Object FirstOrDefault(IEnumerable source)
        {
            if (source == null)
            {
                return null;
            }

            IEnumerator enumerator = source.GetEnumerator();

            return enumerator.MoveNext()
                       ? enumerator.Current
                       : null;
        }

        public static T FirstOrDefault<T>(IEnumerable<T> source)
        {
            if (source == null)
            {
                return default(T);
            }

            IEnumerator<T> enumerator = source.GetEnumerator();

            return enumerator.MoveNext()
                       ? enumerator.Current
                       : default(T);
        }

        public static Boolean SequenceEqual(IEnumerable source, IEnumerable target)
        {
            checkSource(source);

            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            IEnumerator sourceEnumerator = source.GetEnumerator();
            IEnumerator targetEnumerator = target.GetEnumerator();

            while (sourceEnumerator.MoveNext())
            {
                if (!targetEnumerator.MoveNext() ||
                    !Equals(sourceEnumerator.Current, targetEnumerator.Current))
                {
                    return false;
                }
            }

            return !targetEnumerator.MoveNext();
        }

        public static Object[] ToArray(IEnumerable items)
        {
            if (ReferenceEquals(items, null))
            {
                return null;
            }

            ICollection collection = items as ICollection;

            if (!ReferenceEquals(collection, null))
            {
                Object[] array = new Object[collection.Count];
                collection.CopyTo(array, 0);
                return array;
            }

            List<Object> list = new List<Object>();

            foreach (Object item in items)
            {
                list.Add(item);
            }

            return list.ToArray();
        }

        public static T[] ToArray<T>(IEnumerable items)
        {
            if (ReferenceEquals(items, null))
            {
                return null;
            }

            ICollection collection = items as ICollection;

            if (!ReferenceEquals(collection, null))
            {
                T[] array = new T[collection.Count];
                collection.CopyTo(array, 0);
                return array;
            }

            List<T> list = new List<T>();

            foreach (T item in items)
            {
                list.Add(item);
            }

            return list.ToArray();
        }

        #region Private helper methods

        private static void checkSource(IEnumerable source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
        }

        private static void checkSourceAndPredicate(IEnumerable source, Delegate predicate)
        {
            checkSource(source);

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }
        }

        private static IEnumerable<TItem> reverseList<TItem>(IList<TItem> list)
        {
            for (Int32 i = list.Count - 1; i >= 0; i--)
            {
                yield return list[i];
            }
        }

        private static IEnumerable<TItem> reverseCollection<TItem>(ICollection<TItem> collection)
        {
            TItem[] array = new TItem[collection.Count];
            collection.CopyTo(array, 0);
            return reverseList(array);
        }
        #endregion
    }
}
#endif