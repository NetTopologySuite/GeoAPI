// Copyright 2006 - 2008: Rory Plaire (codekaizen@gmail.com)
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
using System.Collections.Generic;

namespace GeoAPI.Algorithms
{
    /// <summary>
    /// Implements the well-known quick sort algorithm.
    /// </summary>
    public static class QuickSort
    {
        /// <summary>
        /// Sorts a list in-place, given the <paramref name="comparison"/>
        /// method.
        /// </summary>
        /// <typeparam name="T">Type of element in the list.</typeparam>
        /// <param name="list">The list to sort.</param>
        /// <param name="comparison">The method used to compare list elements.</param>
        public static void Sort<T>(IList<T> list, Comparison<T> comparison)
        {
            if (list == null) throw new ArgumentNullException("list");
            if (comparison == null) throw new ArgumentNullException("comparison");

            if (list.Count < 2)
            {
                return;
            }

            Int32 middle = (list.Count - 1) / 2;
            Int32 partitionIndex = partition(list, comparison, 0, list.Count - 1, middle);
            sortRange(list, comparison, 0, partitionIndex);
            sortRange(list, comparison, partitionIndex + 1, list.Count - 1);
        }

        private static Int32 partition<T>(IList<T> list, Comparison<T> comparison,
            Int32 minIndex, Int32 maxIndex, Int32 pivotIndex)
        {
            T pivotItem = list[pivotIndex];
            swap(list, pivotIndex, maxIndex);

            Int32 minCompareIndex = minIndex - 1;

            for (Int32 i = minIndex; i <= maxIndex - 1; i++)
            {
                if (comparison(list[i], pivotItem) < 0)
                {
                    minCompareIndex++;
                    swap(list, minCompareIndex, i);
                }
            }

            minCompareIndex++;  
            swap(list, maxIndex, minCompareIndex);

            return minCompareIndex;
        }

        private static void sortRange<T>(IList<T> list, Comparison<T> comparison, Int32 minIndex, Int32 maxIndex)
        {
            if (minIndex >= maxIndex)
            {
                return;
            }

            Int32 middle = (maxIndex - minIndex) / 2 + minIndex;
            Int32 partitionIndex = partition(list, comparison, minIndex, maxIndex, middle);
            sortRange(list, comparison, minIndex, partitionIndex);
            sortRange(list, comparison, partitionIndex + 1, maxIndex);
        }

        private static void swap<T>(IList<T> list, Int32 minIndex, Int32 maxIndex)
        {
            T item = list[minIndex];
            list[minIndex] = list[maxIndex];
            list[maxIndex] = item;
        }
    }
}
