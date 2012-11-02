
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using GeoAPI.DataStructures.Collections.Generic;
using NPack;
using NUnit.Framework;

namespace GeoAPI.Tests.DataStructures.Collections.Generic
{
    [TestFixture]
    public class TreeListTests
    {
        private Random _rnd = new MersenneTwister();

        [Test]
        public void CreateDefaultTreeListSucceeds()
        {
            TreeList<Double> tree = new TreeList<Double>();
            Assert.IsNotNull(tree);
        }

        public class ReverseTestComparer : IComparer<Double>
        {
            #region IComparer<Double> Members

            public Int32 Compare(Double x, Double y)
            {
                return x.CompareTo(y) * -1;
            }

            #endregion
        }

        [Test]
        public void CreateTreeListWithCustomComparerSucceeds()
        {
            TreeList<Double> tree = new TreeList<Double>(new ReverseTestComparer());
            tree.Add(0);
            tree.Add(1);
            tree.Add(2);

            Assert.AreEqual(0, tree.IndexOf(2));
        }

        [Test]
        public void AddSucceeds()
        {
            TreeList<Double> tree = new TreeList<Double>();

            tree.Add(0);
            Assert.AreEqual(1, tree.Count);

            tree.Add(1);
            Assert.AreEqual(2, tree.Count);

            tree.Add(2);
            Assert.AreEqual(3, tree.Count);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void AddWithExistingFails()
        {
            TreeList<Double> tree = new TreeList<Double>();
            tree.Add(0);
            tree.Add(0);
        }

        [Test]
        public void AddWithoutThrowSucceeds()
        {
            TreeList<Double> tree = new TreeList<Double>();
            tree.Add(0);
            tree.Add(0, false);
            Assert.AreEqual(1, tree.Count);
        }

        [Test]
        public void ClearSucceeds()
        {
            TreeList<Double> tree = new TreeList<Double>();

            for (Int32 i = 0; i < 10000; i++)
            {
                tree.Add(_rnd.NextDouble(), false);
            }

            Assert.Greater(tree.Count, 0);

            tree.Clear();

            Assert.AreEqual(0, tree.Count);
        }

        [Test]
        public void ContainsSucceeds()
        {
            TreeList<Double> tree = new TreeList<Double>();
            tree.Add(0.1);
            Assert.IsTrue(tree.Contains(0.1));

            for (Int32 i = 0; i < 10000; i++)
            {
                tree.Add(_rnd.NextDouble(), false);
            }

            Assert.IsTrue(tree.Contains(0.1));
        }

        [Test]
        public void CountBeforeSucceeds()
        {
            TreeList<Double> tree = new TreeList<Double>();

            // 10001 elements
            for (Int32 i = 0; i <= 10000; i++)
            {
                tree.Add(i);
            }

            // 5000 should be the middle element
            Assert.AreEqual(5000, tree.CountBefore(5000.0));

            Assert.AreEqual(0, tree.CountBefore(0));

            Assert.AreEqual(10000, tree.CountBefore(10000));
        }

        [Test]
        public void CountAfterSucceeds()
        {
            TreeList<Double> tree = new TreeList<Double>();

            // 10001 elements
            for (Int32 i = 0; i <= 10000; i++)
            {
                tree.Add(i);
            }

            // 5000 should be the middle element
            Assert.AreEqual(5000, tree.CountAfter(5000.0));

            Assert.AreEqual(0, tree.CountAfter(10000));

            Assert.AreEqual(10000, tree.CountAfter(0));
        }

        [Test]
        public void RemoveSucceeds()
        {
            TreeList<Double> tree = new TreeList<Double>();

            // 10001 elements 
            for (Int32 i = 0; i <= 10000; i++)
            {
                tree.Add(i);
            }

            Assert.AreEqual(10001, tree.Count);

            for (Int32 i = 0; i <= 10000; i++)
            {
                tree.Remove(i);
                Assert.IsFalse(tree.Contains(i));
            }

            Assert.AreEqual(0, tree.Count);
        }

        [Test]
        public void RemoveAtSucceeds()
        {
            TreeList<Double> tree = new TreeList<Double>();

            for (Int32 i = 0; i < 3; i++)
            {
                tree.Add(i);
            }

            tree.RemoveAt(0);
            Assert.AreEqual(2, tree.Count);
            Assert.AreEqual(0, tree.IndexOf(1));

            tree.RemoveAt(0);
            Assert.AreEqual(1, tree.Count);
            Assert.AreEqual(0, tree.IndexOf(2));

            tree = new TreeList<Double>();

            for (Int32 i = 0; i < 15; i++)
            {
                tree.Add(i);
            }

            //tree.Print(Console.Out);
            tree.RemoveAt(0);
            Assert.AreEqual(14, tree.Count);
            Assert.AreEqual(0, tree.IndexOf(1));

            //tree.Print(Console.Out);
            tree.RemoveAt(7);
            Assert.AreEqual(13, tree.Count);
            Assert.AreEqual(7, tree.IndexOf(9));

            //tree.Print(Console.Out);
            tree.RemoveAt(9);
            Assert.AreEqual(12, tree.Count);
            Assert.AreEqual(9, tree.IndexOf(12));

            return;
            // TODO: the following test still finds an error in the removal.
            // On line 934 of TreeList.cs, parent.Right is null, and shouldn't be.
            tree.Clear();

            // 10001 elements 
            for (Int32 i = 0; i <= 10000; i++)
            {
                tree.Add(i);
            }

            while (tree.Count > 0)
            {
                Int32 initialCount = tree.Count;
                Int32 index = _rnd.Next(initialCount);
                Debug.WriteLine(index);
                tree.RemoveAt(index);
                Assert.AreEqual(initialCount - 1, tree.Count);
            }
        }

        [Test]
        public void CopyToSucceeds()
        {
            TreeList<Double> tree = new TreeList<Double>();

            // 10001 elements
            for (Int32 i = 0; i <= 10000; i++)
            {
                tree.Add(i);
            }

            Double[] values = new Double[10001];

            tree.CopyTo(values, 0);

            for (Int32 i = 1; i < values.Length; i++)
            {
                Assert.Less(values[i - 1], values[i]);
            }
        }

        [Test]
        public void IsFixedSizeIsFalse()
        {
            TreeList<Double> tree = new TreeList<Double>();
            Assert.IsFalse(tree.IsFixedSize);
        }

        [Test]
        public void IsReadOnlyIsFalse()
        {
            TreeList<Double> tree = new TreeList<Double>();
            Assert.IsFalse(tree.IsReadOnly);
        }

        [Test]
        public void IsSynchronizedIsFalse()
        {
            TreeList<Double> tree = new TreeList<Double>();
            Assert.IsFalse(tree.IsSynchronized);
        }

        [Test]
        public void SyncRootIsThisObjectReference()
        {
            TreeList<Double> tree = new TreeList<Double>();
            Assert.AreSame(tree, tree.SyncRoot);
        }

        [Test]
        public void GetEnumeratorSucceeds()
        {
            TreeList<Double> tree = new TreeList<Double>();
            IEnumerator<Double> enumerator = tree.GetEnumerator();
            Assert.IsNotNull(enumerator);
        }

        [Test]
        public void GetEnumeratorIteratesInSortedOrder()
        {
            TreeList<Double> tree = new TreeList<Double>();

            // 10001 elements
            for (Int32 i = 0; i <= 10000; i++)
            {
                tree.Add(i);
            }

            Double previous = -1;

            foreach (Double current in tree)
            {
                Assert.Less(previous, current);
                previous = current;
            }
        }

        [Test]
        public void IndexerSucceeds()
        {
            TreeList<Int32> tree = new TreeList<Int32>();

            // 10001 elements
            for (Int32 i = 0; i <= 10000; i++)
            {
                tree.Add(i);
            }

            for (Int32 i = 0; i <= 10000; i++)
            {
                Assert.AreEqual(i, tree[i], "Indexer failed at index: " + i);
            }
        }

        [Test]
        public void IndexingIntoTreeWithNodeHavingNullRightChildSucceeds()
        {
            TreeList<Int32> tree = new TreeList<Int32>();

            // create a tree with the following shape:
            //
            //      1
            //     / \
            //    0   3
            //       /
            //      2
            //
            // There was a defect in the indexer where a NullReferenceException
            // as being thrown when the node '3' had a null right child. This was
            // fixed by coalescing with the left child on the right turn on line 126
            // of the indexer of TreeList`1 in rev. 17477. This test should pass
            // on this and later revisions.
            tree.Add(1);
            tree.Add(0);
            tree.Add(3);
            tree.Add(2);

            Assert.AreEqual(2, tree[2]);
        }

        [Test]
        public void CloneCreatesAShallowCopy()
        {
            TreeList<Object> tree = new TreeList<Object>();

            // 10001 elements
            for (Int32 i = 0; i <= 10000; i++)
            {
                tree.Add(i);
            }

            TreeList<Object> clone = tree.Clone() as TreeList<Object>;

            Assert.AreEqual(tree.Count, clone.Count);

            for (Int32 i = 0; i <= 10000; i++)
            {
                Assert.AreSame(tree[i], clone[i]);
            }
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void InsertFails()
        {
            TreeList<Object> tree = new TreeList<Object>();
            tree.Insert(0, new Object());
        }

        [Test]
        [ExpectedException(typeof(NotSupportedException))]
        public void IndexerSetFails()
        {
            TreeList<Double> tree = new TreeList<Double>();
            tree.Add(0);
            tree[0] = 1;
        }

        [Test]
        [Ignore]
        public void PerformanceTest()
        {
            TreeList<Double> tree = new TreeList<Double>();

            // Ten million-ish elements (could be some dupes)
            Int32 count = 10000000;

            for (Int32 i = 0; i < count; i++)
            {
                tree.Add(_rnd.NextDouble(), false);
            }

            for (Int32 i = 0; i < count; i++)
            {
                Int32 index = tree.IndexOf(tree[i]);
                Assert.AreEqual(i, index);
            }

            // very probably lots of misses
            for (Int32 i = 0; i < count; i++)
            {
                tree.Remove(_rnd.NextDouble());
            }

            while(tree.Count > 0)
            {
                Int32 removeIndex = _rnd.Next(tree.Count);
                tree.RemoveAt(removeIndex);
            }
        }
    }
}
