using System;
using System.Collections.Generic;
using GeoAPI.Algorithms;
using NUnit.Framework;

namespace GeoAPI.Tests.DataStructures
{
    [TestFixture]
    public class QuickSortTests
    {
        [Test]
        public void SortingDoubleArrayWithEvenNumberOfElementsSortsCorrectly()
        {
            Random rnd = new Random();
            List<Double> list = new List<Double>(100000);
            for (Int32 i = 0; i < 100000; i++)
            {
                list.Add(rnd.NextDouble());
            }

            QuickSort.Sort(list,
                           delegate(Double lhs, Double rhs)
                           {
                               return lhs < rhs
                                          ? -1
                                          : lhs > rhs
                                                ? 1
                                                : 0;
                           });

            for (Int32 i = 1; i < list.Count; i++)
            {
                Assert.GreaterOrEqual(list[i], list[i - 1]);
            }
        }

        [Test]
        public void SortingDoubleArrayWithOddNumberOfElementsSortsCorrectly()
        {
            Random rnd = new Random();
            List<Double> list = new List<Double>(100001);
            for (Int32 i = 0; i < 100001; i++)
            {
                list.Add(rnd.NextDouble());
            }

            QuickSort.Sort(list,
                           delegate(Double lhs, Double rhs)
                           {
                               return lhs < rhs
                                          ? -1
                                          : lhs > rhs
                                                ? 1
                                                : 0;
                           });

            for (Int32 i = 1; i < list.Count; i++)
            {
                Assert.GreaterOrEqual(list[i], list[i - 1]);
            }
        }
    }
}