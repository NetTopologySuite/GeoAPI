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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
#if DOTNET35
using sl = System.Linq;
#else
using sl = GeoAPI.DataStructures;
#endif

namespace GeoAPI.DataStructures
{
    /// <summary>
    /// A class of methods to act on instances of <see cref="IEnumerable{TItem}"/>
    /// to get specific items or sets of items in the enumeration.
    /// </summary>
    // yes, the conditional on the whole class is heavy-handed,
    // but it will allow us to flag where this is used in the code
    // and subsequently change it to allow use of .Net 3.5 types.
    public static class Slice
    {

        public static Pair<TItem>? GetPair<TItem>(IEnumerable<TItem> items)
            where TItem : IEquatable<TItem>, IComparable<TItem>
        {
            return GetPairAt(items, 0);
        }

        public static Triple<TItem>? GetTriple<TItem>(IEnumerable<TItem> items)
            where TItem : IEquatable<TItem>, IComparable<TItem>
        {
            return GetTripleAt(items, 0);
        }

        public static Pair<TItem>? GetLastPair<TItem>(IEnumerable<TItem> items)
            where TItem : IEquatable<TItem>, IComparable<TItem>
        {
            if (items == null)
            {
                return null;
            }

            if (items is IList<TItem>)
            {
                IList<TItem> list = items as IList<TItem>;
                return GetPairAt(list, list.Count - 2);
            }
            else
            {
                TItem nextToLast = default(TItem), last = default(TItem);

                foreach (TItem item in items)
                {
                    nextToLast = last;
                    last = item;
                }

                Pair<TItem> pair = new Pair<TItem>(nextToLast, last);
                return pair;
            }
        }

        public static Pair<TItem>? GetPairAt<TItem>(IEnumerable<TItem> items, Int32 index)
            where TItem : IEquatable<TItem>, IComparable<TItem>
        {
            if (items == null)
            {
                return null;
            }

            if (index < 0)
            {
                return null;
            }

            if (items is IList<TItem>)
            {
                IList<TItem> list = items as IList<TItem>;

                if (index + 1 >= list.Count)
                {
                    return null;
                }

                Pair<TItem> pair = new Pair<TItem>(list[index], list[index + 1]);
                return pair;
            }
            else
            {
                Int32 currentIndex = 0;

                TItem item1 = default(TItem), item2 = default(TItem);

                foreach (TItem item in items)
                {
                    if (currentIndex == index)
                    {
                        item1 = item;
                    }
                    else if (currentIndex == index + 1)
                    {
                        item2 = item;
                        break;
                    }

                    currentIndex++;
                }

                if (currentIndex < index + 1)
                {
                    return null;
                }

                Pair<TItem> pair = new Pair<TItem>(item1, item2);
                return pair;
            }
        }

        public static Triple<TItem>? GetTripleAt<TItem>(IEnumerable<TItem> items, Int32 index)
            where TItem : IEquatable<TItem>, IComparable<TItem>
        {
            if (items == null)
            {
                return null;
            }

            if (index < 0)
            {
                return null;
            }

            if (items is IList<TItem>)
            {
                IList<TItem> list = items as IList<TItem>;

                if (index + 2 >= list.Count)
                {
                    return null;
                }

                Triple<TItem> triple = new Triple<TItem>(
                    list[index], list[index + 1], list[index + 2]);
                return triple;
            }
            else
            {
                Int32 currentIndex = 0;

                TItem item1 = default(TItem), item2 = default(TItem),
                    item3 = default(TItem);

                foreach (TItem item in items)
                {
                    if (currentIndex == index)
                    {
                        item1 = item;
                    }
                    else if (currentIndex == index + 1)
                    {
                        item2 = item;
                        break;
                    }
                    else if (currentIndex == index + 2)
                    {
                        item3 = item;
                        break;
                    }

                    currentIndex++;
                }

                if (currentIndex < index + 1)
                {
                    return null;
                }

                Triple<TItem> triple = new Triple<TItem>(item1, item2, item3);
                return triple;
            }
        }

        public static Pair<TItem>[] GetOverlappingPairs<TItem>(IEnumerable<TItem> items)
            where TItem : IEquatable<TItem>, IComparable<TItem>
        {
            List<TItem> tmp = new List<TItem>(items);
            if ( tmp.Count < 2)
                return null;
            Int32 count = tmp.Count-1;
            Pair<TItem>[] ret = new Pair<TItem>[count];
            for(int i = 0; i < count; i++)
                ret[i] = new Pair<TItem>(tmp[i], tmp[i+1]);
            return ret;
        }
        /*
        public static IEnumerable<Pair<TItem>> GetOverlappingPairs<TItem>(IEnumerable<TItem> items)
            where TItem : IEquatable<TItem>, IComparable<TItem>
        {
            IEnumerator<TItem> en = items.GetEnumerator();
            Boolean moveNext = typeof (TItem).IsValueType ? en.Current.Equals(default(TItem)) : en.Current == null;
            if (moveNext && !en.MoveNext())
                yield break;

            TItem previous = en.Current;//GetFirst(items);
            while (en.MoveNext())
            {
                yield return new Pair<TItem>(previous, en.Current);
                previous = en.Current;
            }

            //previous = GetFirst(items);
            //foreach (TItem item in Enumerable.Skip(items,1))
            //{
            //    yield return new Pair<TItem>(previous, item);
            //    previous = item;
            //}

            ////Boolean isPreviousSet = false;
            ////TItem previous = default(TItem);

            ////foreach (TItem item in items)
            ////{
            ////    if (!isPreviousSet)
            ////    {
            ////        isPreviousSet = true;
            ////        previous = item;
            ////    }
            ////    else
            ////    {
            ////        yield return new Pair<TItem>(previous, item);
            ////        previous = item;
            ////    }
            ////}
        }
        */
        public static IEnumerable<Triple<TItem>> GetOverlappingTriples<TItem>(IEnumerable<TItem> items)
            where TItem : IComparable<TItem>, IEquatable<TItem>
        {
            IEnumerator<TItem> it = items.GetEnumerator();

            Boolean moveNext = typeof (TItem).IsValueType ? it.Current.Equals(default(TItem)) : it.Current == null;
            if(moveNext)
            {
                if ( !it.MoveNext() ) yield break;
            }

            TItem previous1 = it.Current;
            if (!it.MoveNext()) yield break;

            TItem previous2 = it.Current;

            //foreach (TItem item in Enumerable.Skip(items,2))
            while( it.MoveNext())
            {
                yield return new Triple<TItem>(previous1, previous2, it.Current);
                previous1 = previous2;
                previous2 = it.Current;
            }
        //    Int32 previousSetCount = 0;

        //    TItem previous1 = default(TItem);
        //    TItem previous2 = default(TItem);

        //    foreach (TItem item in items)
        //    {
        //        if (previousSetCount < 2)
        //        {
        //            if (previousSetCount == 0)
        //            {
        //                previous1 = item;
        //            }
        //            else
        //            {
        //                previous2 = item;
        //            }
        //            previousSetCount += 1;
        //        }
        //        else
        //        {
        //            yield return new Triple<TItem>(previous1, previous2, item);
        //            previous1 = previous2;
        //            previous2 = item;
        //        }
        //    }
        }

        public static IEnumerable<TItem> Append<TItem>(IEnumerable<TItem> items, TItem item)
        {
            return aggregateEnumerable(items, item);
        }

        public static IEnumerable<TItem> Append<TItem>(IEnumerable<TItem> items, IEnumerable<TItem> itemsToAdd)
        {
            return aggregateEnumerable(items, itemsToAdd);
        }

        public static IEnumerable<TItem> Prepend<TItem>(IEnumerable<TItem> items, TItem item)
        {
            return aggregateEnumerable(item, items);
        }

        public static IEnumerable<TItem> Prepend<TItem>(IEnumerable<TItem> items, IEnumerable<TItem> itemsToPrepend)
        {
            return aggregateEnumerable(itemsToPrepend, items);
        }
        
//#if !DOTNET35
        public static Int32 GetLength<TItem>(IEnumerable<TItem> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (items is ICollection<TItem>)
            {
                return (items as ICollection<TItem>).Count;
            }
            else
            {
                Int32 count = 0;

                IEnumerator<TItem> enumerator = items.GetEnumerator();

                while (enumerator.MoveNext())
                {
                    count++;
                }

                return count;
            }
        }

        public static TItem GetFirst<TItem>(IEnumerable<TItem> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (items is IList<TItem>)
            {
                IList<TItem> list = items as IList<TItem>;
                return list.Count > 0 ? list[0] : default(TItem);
            }
            else
            {
                IEnumerator<TItem> enumerator = items.GetEnumerator();

                if (!enumerator.MoveNext())
                {
                    return default(TItem);
                }

                return enumerator.Current;
            }
        }

        public static TItem GetLast<TItem>(IEnumerable<TItem> items)
        {
            if (items is IList<TItem>)
            {
                IList<TItem> list = items as IList<TItem>;
                return list[list.Count - 1];
            }
            else
            {
                TItem last = default(TItem);

                foreach (TItem item in items)
                {
                    last = item;
                }

                return last;
            }
        }

        public static TItem GetAt<TItem>(IEnumerable<TItem> items, Int32 index)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException("index", index,
                    "Parameter must be greater than or equal to 0.");
            }

            if (items is IList<TItem>)
            {
                IList<TItem> list = items as IList<TItem>;

                if (list.Count <= index)
                {
                    throw new ArgumentOutOfRangeException("index", index,
                            "Parameter must be less than the number of items in "+
                            "the enumeration minus one.");
                }

                return list[index];
            }
            else
            {
                TItem found = default(TItem);
                Int32 count = 0;

                foreach (TItem item in items)
                {
                    found = item;

                    if (count == index)
                    {
                        break;
                    }
                }

                return found;
            }
        }

        public static IEnumerable<TItem> GetRange<TItem>(IEnumerable<TItem> items, Int32 start, Int32 end)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (start < 0)
            {
                throw new ArgumentOutOfRangeException("start", start,
                    "Parameter must be greater than or equal to 0.");
            }

            if (end - start < 0)
            {
                yield break;
            }

            if (items is IList<TItem>)
            {
                IList<TItem> list = items as IList<TItem>;

                if (list.Count <= end)
                {
                    throw new ArgumentOutOfRangeException("end", end,
                            "Parameter must be less than the number of items in " +
                            "the enumeration minus one.");
                }

                for (Int32 i = start; i <= end; i++)
                {
                    yield return list[i];
                }
            }
            else
            {
                Int32 count = 0;

                foreach (TItem item in items)
                {
                    if (count <= start)
                    {
                        yield return item;
                    }

                    if (count == end)
                    {
                        yield break;
                    }

                    Debug.Assert(count < end);

                    count++;
                }
            }
        }

        public static IEnumerable<TItem> ReverseStartAt<TItem>(IEnumerable<TItem> items, Int32 startIndex)
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

                for (Int32 i = (list.Count - 1) - startIndex; i >= 0; i--)
                {
                    yield return list[i];
                }
            }
            else
            {
                foreach (TItem item in sl.Enumerable.Skip(sl.Enumerable.Reverse(items), startIndex))
                {
                    yield return item;
                }
            }
        }

        public static Boolean CountGreaterThan(IEnumerable items, Int32 count)
        {
            if (items == null)
            {
                throw new ArgumentNullException("items");
            }

            if (count < 0)
            {
                return true;
            }

            if (items is ICollection)
            {
                ICollection collection = items as ICollection;
                return collection.Count > count;
            }
            Int32 currentCount = 0;
            IEnumerator e = items.GetEnumerator();

            while (e.MoveNext())
            {
                currentCount++;
                if (currentCount > count)
                {
                    return true;
                }
            }

            return false;
        }

        public static Boolean IsEmpty(IEnumerable items)
        {
            return !CountGreaterThan(items, 0);
        }

        public static Boolean IsEmpty<TItem>(IEnumerable<TItem> items)
        {
            return !CountGreaterThan(items, 0);
        }

        public static Int32 Compare<TItem>(IEnumerable<TItem> left, IEnumerable<TItem> right)
            where TItem : IComparable<TItem>
        {
            IEnumerator<TItem> i = left.GetEnumerator();
            IEnumerator<TItem> j = right.GetEnumerator();

            while (i.MoveNext() && j.MoveNext())
            {
                TItem aElement = i.Current;
                TItem bElement = j.Current;

                Int32 comparison = aElement.CompareTo(bElement);

                if (comparison != 0)
                {
                    return comparison;
                }
            }

            if (i.MoveNext())
            {
                return 1;
            }

            if (j.MoveNext())
            {
                return -1;
            }

            return 0;
        }
//#endif
        #region Private helper functions

        private static IEnumerable<TItem> aggregateEnumerable<TItem>(IEnumerable<TItem> items, TItem addItem)
        {
            foreach (TItem item in items)
            {
                yield return item;
            }

            yield return addItem;
        }

        private static IEnumerable<TItem> aggregateEnumerable<TItem>(TItem addItem, IEnumerable<TItem> items)
        {
            yield return addItem;

            foreach (TItem item in items)
            {
                yield return item;
            }
        }

        private static IEnumerable<TItem> aggregateEnumerable<TItem>(IEnumerable<TItem> items, IEnumerable<TItem> addItem)
        {
            foreach (TItem item in items)
            {
                yield return item;
            }

            foreach (TItem item in addItem)
            {
                yield return item;
            }
        } 
        #endregion
    }
}
