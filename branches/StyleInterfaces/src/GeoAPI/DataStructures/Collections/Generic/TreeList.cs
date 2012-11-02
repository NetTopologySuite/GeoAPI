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
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;

namespace GeoAPI.DataStructures.Collections.Generic
{
    public class TreeList<TItem> : IList<TItem>, ICloneable
    {
        #region Nested types
        #region NodeColor
        private enum NodeColor : byte
        {
            Red,
            Black
        } 
        #endregion

        #region PrintEntry

        struct PrintEntry
        {
            public PrintEntry(Node node, Int32 level, Single width, Int32 position)
            {
                Node = node;
                Level = level;
                Width = width;
                Position = position;
            }

            public readonly Node Node;
            public readonly Int32 Level;
            public readonly Single Width;
            public readonly Int32 Position;
        }

        #endregion

        #region Node

        private sealed class Node
        {
            public Node Parent;
            public Node Left;
            public Node Right;
            public TItem Item;
            public Int32 ChildCount;
            public NodeColor Color;

            public override string ToString()
            {
                return String.Format("{0}, {1}, Children: {2} (Left: {3}; Right: {4}), Parent: {5}",
                                     Item,
                                     Color,
                                     ChildCount,
                                     Left == null ? "<null>" : Left.Item.ToString(),
                                     Right == null ? "<null>" : Right.Item.ToString(),
                                     Parent == null ? "<null>" : Parent.Item.ToString());

            }

            public Int32 LeftChildCount
            {
                get
                {
                    return Left == null ? 0 : Left.ChildCount + 1;
                }
            }

            public Int32 RightChildCount
            {
                get
                {
                    return Right == null ? 0 : Right.ChildCount + 1;
                }
            }

            public Boolean IsRightChild
            {
                get
                {
                    return Parent != null && Parent.Right == this;
                }
            }

            public Boolean IsLeftChild
            {
                get
                {
                    return Parent != null && Parent.Left == this;
                }
            }

            public Boolean IsBlack
            {
                get { return Color == NodeColor.Black; }
            }

            public Boolean IsRed
            {
                get { return Color == NodeColor.Red; }
            }

            public Node Grandparent
            {
                get
                {
#if DEBUG
                    if (Parent == null)
                    {
                        return null;
                    }
#endif
                    return Parent.Parent;
                }
            }
        }

        #endregion

        #region TreeEnumerator

        private sealed class TreeEnumerator : IEnumerator<TItem>
        {
            private readonly TreeList<TItem> _tree;
            private Node _current;
            private Boolean _reset;
            private Boolean _isDisposed;

            public TreeEnumerator(TreeList<TItem> tree)
            {
                _tree = tree;
                Reset();
            }

            // Move to the next item in the iteration order.
            public Boolean MoveNext()
            {
                checkDisposed();

                if (_reset)
                {
                    // Start with the left-most node in the tree.
                    _current = _tree._leftMost;
                    _reset = false;
                }
                else if (_current == null)
                {
                    // We already reached the end of the tree.
                    return false;
                }
                else if (_current.Right != null)
                {
                    // Move to the left-most node in the right sub-tree.
                    _current = _current.Right;

                    while (_current.Left != null)
                    {
                        _current = _current.Left;
                    }
                }
                else
                {
                    // Move up ancestors until we are no longer
                    // the right-most child of our parent.
                    Node parent = _current.Parent;

                    while (parent != null && parent.Right == _current)
                    {
                        _current = parent;
                        parent = _current.Parent;
                    }

                    _current = parent;
                }

                return (_current != null);
            }

            // Reset the iterator to the start.
            public void Reset()
            {
                checkDisposed();
                _current = null;
                _reset = true;
            }

            public TItem Current
            {
                get { return _current.Item; }
            }

            #region IDisposable Members

            public void Dispose()
            {
                _current = null;
                GC.SuppressFinalize(this);
                _isDisposed = true;
            }

            #endregion

            #region IEnumerator Members

            Object System.Collections.IEnumerator.Current
            {
                get { return Current; }
            }

            #endregion

            private void checkDisposed()
            {
                if (_isDisposed)
                {
                    throw new ObjectDisposedException(GetType().ToString());
                }
            }
        } 
        #endregion
        #endregion

        private readonly IComparer<TItem> _comparer;
        private Node _root;
        private Node _leftMost;
        private Int32 _count;

        public TreeList() : this(null) { }

        public TreeList(IComparer<TItem> comparer)
        {
            _comparer = comparer ?? Comparer<TItem>.Default;

            _root = null;
            _leftMost = null;
            _count = 0;
        }

        public override String ToString()
        {
            return String.Format("Node count: {0}", Count);
        }

        #region Public Methods
        public void Print(TextWriter writer)
        {
            Print(writer, CultureInfo.InvariantCulture, 4);
        }

        public void Print(TextWriter writer, Int32 itemFieldWidth)
        {
            Print(writer, CultureInfo.InvariantCulture, itemFieldWidth);
        }

        public void Print(TextWriter writer, IFormatProvider formatProvider, Int32 itemFieldWidth)
        {
            Queue<PrintEntry> levelQueue = new Queue<PrintEntry>();

            Int32 currentLevel = 0;
            Single width = (Single)DoubleBits.FindNextHigherPowerOf2(Count * 2 * itemFieldWidth);
            levelQueue.Enqueue(new PrintEntry(_root, currentLevel, width, 0));

            Int32 linePosition = 0;

            while (levelQueue.Count > 0)
            {
                PrintEntry next = levelQueue.Dequeue();

                if (currentLevel != next.Level)
                {
                    linePosition = 0;
                    writer.WriteLine();
                    currentLevel = next.Level;
                }

                width = next.Width;
                Int32 fieldOffset = next.Position - linePosition;
                linePosition += fieldOffset + 1;

                Int32 printOffset = (Int32)Math.Round(fieldOffset * width);

                if (next.Node == null)
                {
                    printSpace(writer, width, printOffset);
                    continue;
                }

                Node node = next.Node;

                printItem(node.Item, writer, formatProvider, width, printOffset);

                Int32 nextLevel = currentLevel + 1;
                Single nextWidth = width / 2;
                Int32 nextPos = next.Position * 2;

                // Add left child to queue
                PrintEntry entry = new PrintEntry(node.Left, nextLevel, nextWidth, nextPos);
                levelQueue.Enqueue(entry);

                // Add right child to queue
                //nextWidth += width % 2;
                entry = new PrintEntry(node.Right, nextLevel, nextWidth, nextPos + 1);
                levelQueue.Enqueue(entry);
            }

            writer.WriteLine();
        }

        public void Add(TItem item, Boolean throwIfExists)
        {
            Node parent;
            Node existing = findNodeOrParent(item, out parent);

            if (existing != null)
            {
                if (throwIfExists)
                {
                    throw new ArgumentException("Item already exists in tree set.");
                }

                return;
            }

            Node newNode = new Node();
            newNode.Item = item;

            if (parent == null)
            {
                newNode.ChildCount = 0;
                _root = newNode;
                _leftMost = newNode;
            }
            else if (_comparer.Compare(item, parent.Item) < 0)
            {
                parent.Left = newNode;
                parent.ChildCount++;

                if (parent == _leftMost)
                {
                    _leftMost = newNode;
                }
            }
            else
            {
                parent.Right = newNode;
                parent.ChildCount++;
            }

            newNode.Parent = parent;

            if (parent != null)
            {
                addOneChildToCount(parent.Parent);
            }

            insertRebalance(newNode);

            ++_count;
        }

        public Int32 CountBefore(TItem item)
        {
            return countBefore(item, true);
        }

        public Int32 CountAtAndBefore(TItem item)
        {
            return countBefore(item, false);
        }

        public Int32 CountAfter(TItem item)
        {
            return countAfter(item, true);
        }

        public Int32 CountAtAndAfter(TItem item)
        {
            return countAfter(item, false);
        }

        #endregion

        #region IList<TItem> Members

        public Int32 IndexOf(TItem item)
        {
            Node node = findNode(item);

            if (node == null)
            {
                return -1;
            }

            return CountBefore(item);
        }

        public void Insert(Int32 index, TItem item)
        {
            throw new NotSupportedException(
                "Insertions are defined by sort order of tree.");
        }

        public void RemoveAt(Int32 index)
        {
            Remove(this[index]);
        }

        public TItem this[Int32 index]
        {
            get
            {
                if (index < 0 || index > Count)
                {
                    throw new IndexOutOfRangeException();
                }

                Int32 currentIndex = 0;
                Node current = _leftMost;
                Boolean turned = false;

                while (currentIndex != index)
                {
                    // turn at the root
                    turned |= current.Parent == null;

                    Node rightNode;
                    Int32 rightIndex;

                    if (turned)
                    {
                        // turn right until we get a non-null node
                        rightNode = current.Right ?? current.Left;
                        Debug.Assert(rightNode != null);
                        rightIndex = rightNode.LeftChildCount + currentIndex + 1;
                    }
                    else
                    {
                        rightNode = current.Parent;
                        rightIndex = rightNode.LeftChildCount;
                    }

                    if (rightIndex <= index)
                    {
                        current = rightNode;
                        currentIndex = rightIndex;
                    }
                    else
                    {
                        if (turned && currentIndex >= index)
                        {
                            current = current.Left;
                            currentIndex -= current.RightChildCount + 1;
                        }
                        else
                        {
                            current = current.Right;
                            currentIndex += current.LeftChildCount + 1;
                            turned = true;
                        }

                        //if (currentIndex < index)
                        //{
                        //    current = current.Right;
                        //    currentIndex += current.LeftChildCount + 1;
                        //    turned = true;
                        //}
                        //else
                        //{
                        //    if (turned)
                        //    {
                        //        current = current.Left;
                        //        currentIndex -= current.RightChildCount + 1;
                        //    }
                        //    else
                        //    {
                        //        current = current.Right;
                        //        currentIndex += current.LeftChildCount + 1;
                        //        turned = true;
                        //    }
                        //}
                    }
                }

                return current.Item;
            }
            set
            {
                throw new NotSupportedException("Setting a node value is undefined. " +
                                                "Remove item and re-add.");
            }
        }

        #endregion

        #region ICollection<TItem> Members
        public void Add(TItem item)
        {
            Add(item, true);
        }

        public void Clear()
        {
            _root = null;
            _leftMost = null;
            _count = 0;
        }

        public Boolean Contains(TItem item)
        {
            Node current = _root;
            Int32 result;

            while (current != null)
            {
                if ((result = _comparer.Compare(item, current.Item)) < 0)
                {
                    current = current.Left;
                }
                else if (result > 0)
                {
                    current = current.Right;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public Boolean Remove(TItem item)
        {
            Node current = _root;
            Int32 result;

            while (current != null)
            {
                if ((result = _comparer.Compare(item, current.Item)) < 0)
                {
                    current = current.Left;
                }
                else if (result > 0)
                {
                    current = current.Right;
                }
                else
                {
                    removeNode(current);
                    return true;
                }
            }

            return false;
        }

        public void CopyTo(TItem[] array, Int32 index)
        {
            foreach (TItem item in this)
            {
                array[index++] = item;
            }
        }

        public Int32 Count
        {
            get { return _count; }
        }

        public Boolean IsFixedSize
        {
            get { return false; }
        }

        public Boolean IsReadOnly
        {
            get { return false; }
        }

        public Boolean IsSynchronized
        {
            get { return false; }
        }

        public Object SyncRoot
        {
            get { return this; }
        }
        #endregion

        #region IEnumerable<TItem> Members
        public IEnumerator<TItem> GetEnumerator()
        {
            return new TreeEnumerator(this);
        }
        #endregion

        #region IClonable Members
        public Object Clone()
        {
            TreeList<TItem> tree = new TreeList<TItem>(_comparer);

            foreach (TItem item in this)
            {
                tree.Add(item);
            }

            return tree;
        }
        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private helper members

        private Int32 countBefore(TItem item, Boolean skipItemNode)
        {
            Node next = _root;
            Int32 count = 0;
            Int32 compareValue = Convert.ToInt32(skipItemNode) * -1;

            while (next != null)
            {
                // if node item is less, count it, it's left node (if one exists)
                // and it's left node's children (if any), and descend right
                if (_comparer.Compare(next.Item, item) <= compareValue)
                {
                    count += 1 + next.LeftChildCount;
                    next = next.Right;
                }
                else // descend left
                {
                    next = next.Left;
                }
            }

            return count;
        }

        private Int32 countAfter(TItem item, Boolean skipItemNode)
        {
            Node next = _root;
            Int32 count = 0;
            Int32 compareValue = Convert.ToInt32(skipItemNode);

            while (next != null)
            {
                // if node item is greater, count it, it's right node (if one exists)
                // and it's right node's children (if any), and descend left
                if (_comparer.Compare(next.Item, item) >= compareValue)
                {
                    count += 1 + next.RightChildCount;
                    next = next.Left;
                }
                else // descend right
                {
                    next = next.Right;
                }
            }

            return count;
        }

        private void removeNode(Node target)
        {
            Node closestRightDescendant = target;
            Node closestRightDescendantRightmostChild;
            Node closestRightDescendantParent;

            --_count;

            // pick the rightmost descendents of the target node
            if (target.Left == null)
            {
                closestRightDescendantRightmostChild = target.Right;
            }
            else if (target.Right == null)
            {
                closestRightDescendantRightmostChild = target.Left;
            }
            else
            {
                closestRightDescendant = target.Right;

                // find the left-most right descendant
                while (closestRightDescendant.Left != null)
                {
                    closestRightDescendant = closestRightDescendant.Left;
                }

                closestRightDescendantRightmostChild = closestRightDescendant.Right;
            }

            NodeColor removedNodeColor = NodeColor.Red;

            // if target is a branch node, reassign its children, and
            // its children's parent pointers to closestRightDescendant
            if (closestRightDescendant != target)
            {
                target.Left.Parent = closestRightDescendant;
                closestRightDescendant.Left = target.Left;

                // if there are any intervening nodes on the path to the target, 
                // relink the right nodes of the closestRightDescendant node
                // to it, so we can move closestRightDescendant into target's position
                if (closestRightDescendant != target.Right)
                {
                    closestRightDescendantParent = closestRightDescendant.Parent;

                    if (closestRightDescendantRightmostChild != null)
                    {
                        closestRightDescendantRightmostChild.Parent
                            = closestRightDescendantParent;
                    }

                    closestRightDescendantParent.Left = closestRightDescendantRightmostChild;
                    closestRightDescendant.Right = target.Right;
                    target.Right.Parent = closestRightDescendant;
                }
                else
                {
                    closestRightDescendantParent = closestRightDescendant;
                }

                if (_root == target)
                {
                    _root = closestRightDescendant;
                }
                else if (target.IsLeftChild)
                {
                    target.Parent.Left = closestRightDescendant;
                }
                else
                {
                    Debug.Assert(target.IsRightChild);
                    target.Parent.Right = closestRightDescendant;
                }

                closestRightDescendant.Parent = target.Parent;

                removedNodeColor = closestRightDescendant.Color;
                closestRightDescendant.Color = target.Color;
            }
            else
            {
                closestRightDescendantParent = closestRightDescendant.Parent;

                if (closestRightDescendantRightmostChild != null)
                {
                    closestRightDescendantRightmostChild.Parent
                        = closestRightDescendantParent;
                }

                if (_root == target)
                {
                    _root = closestRightDescendantRightmostChild;
                }
                else if (target.IsLeftChild)
                {
                    target.Parent.Left = closestRightDescendantRightmostChild;
                }
                else
                {
                    Debug.Assert(target.IsRightChild);
                    target.Parent.Right = closestRightDescendantRightmostChild;
                }

                if (_leftMost == target)
                {
                    if (target.Right == null)
                    {
                        _leftMost = target.Parent;
                    }
                    else
                    {
                        _leftMost = closestRightDescendantRightmostChild;

                        while (_leftMost != null && _leftMost.Left != null)
                        {
                            _leftMost = _leftMost.Left;
                        }
                    }
                }
            }

            updateChildrenCountUpward(closestRightDescendantParent);

            verifyCounts();

            // removing a red node means the tree is still balanced
            // (all the paths from the root have the same number of black nodes)
            if (removedNodeColor == NodeColor.Red)
            {
                return;
            }

            removeRebalance(closestRightDescendantRightmostChild,
                            closestRightDescendantParent);
        }

        private void rotateLeft(Node target)
        {
            Node newParent = target.Right;
            Int32 newParentLeftCount = newParent.LeftChildCount;
            Int32 targetRightCount = target.RightChildCount;

            target.ChildCount -= targetRightCount;
            target.ChildCount += newParentLeftCount;
            target.Right = newParent.Left;

            if (newParent.Left != null)
            {
                newParent.Left.Parent = target;
            }

            newParent.Parent = target.Parent;

            if (target == _root)
            {
                _root = newParent;
            }
            else if (target.IsLeftChild)
            {
                target.Parent.Left = newParent;
            }
            else
            {
                Debug.Assert(target.IsRightChild);
                target.Parent.Right = newParent;
            }

            newParent.ChildCount -= newParentLeftCount;
            newParent.ChildCount += target.ChildCount + 1;
            newParent.Left = target;
            target.Parent = newParent;
        }

        private void rotateRight(Node target)
        {
            Node newParent = target.Left;
            Int32 newParentRightCount = newParent.RightChildCount;
            Int32 targetLeftCount = target.LeftChildCount;

            target.ChildCount -= targetLeftCount;
            target.ChildCount += newParentRightCount;
            target.Left = newParent.Right;

            if (newParent.Right != null)
            {
                newParent.Right.Parent = target;
            }

            newParent.Parent = target.Parent;

            if (target == _root)
            {
                _root = newParent;
            }
            else if (target.IsRightChild)
            {
                target.Parent.Right = newParent;
            }
            else
            {
                Debug.Assert(target.IsLeftChild);
                target.Parent.Left = newParent;
            }

            newParent.ChildCount -= newParentRightCount;
            newParent.ChildCount += target.ChildCount + 1;
            newParent.Right = target;
            target.Parent = newParent;
        }

        private void insertRebalance(Node inserted)
        {
            Node parentSibling;

            inserted.Color = NodeColor.Red;

            while (inserted != _root && inserted.Parent.IsRed)
            {
                if (inserted.Parent == inserted.Grandparent.Left)
                {
                    parentSibling = inserted.Grandparent.Right;

                    if (parentSibling != null && parentSibling.IsRed)
                    {
                        inserted.Parent.Color = NodeColor.Black;
                        parentSibling.Color = NodeColor.Black;
                        inserted.Grandparent.Color = NodeColor.Red;
                        inserted = inserted.Grandparent;
                    }
                    else
                    {
                        if (inserted == inserted.Parent.Right)
                        {
                            inserted = inserted.Parent;
                            rotateLeft(inserted);
                        }

                        inserted.Parent.Color = NodeColor.Black;
                        inserted.Grandparent.Color = NodeColor.Red;
                        rotateRight(inserted.Grandparent);
                    }
                }
                else
                {
                    parentSibling = inserted.Grandparent.Left;

                    if (parentSibling != null && parentSibling.IsRed)
                    {
                        inserted.Parent.Color = NodeColor.Black;
                        parentSibling.Color = NodeColor.Black;
                        inserted.Grandparent.Color = NodeColor.Red;
                        inserted = inserted.Grandparent;
                    }
                    else
                    {
                        if (inserted == inserted.Parent.Left)
                        {
                            inserted = inserted.Parent;
                            rotateRight(inserted);
                        }

                        inserted.Parent.Color = NodeColor.Black;
                        inserted.Grandparent.Color = NodeColor.Red;
                        rotateLeft(inserted.Grandparent);
                    }
                }
            }

            // Set the root color to black.
            _root.Color = NodeColor.Black;
        }

        private void removeRebalance(Node target, Node parent)
        {
            while (target != _root && (target == null || target.IsBlack))
            {
                Node rotateTarget;

                if (target == parent.Left)
                {
                    rotateTarget = parent.Right;
#if DEBUG
                    if (rotateTarget == null)
                    {
                        if (Debugger.IsAttached)
                        {
                            Debugger.Break();
                        }
                        else
                        {
                            throw new InvalidOperationException();
                        }
                    }
#endif
                    Debug.Assert(rotateTarget != null);

                    if (rotateTarget.IsRed)
                    {
                        rotateTarget.Color = NodeColor.Black;
                        parent.Color = NodeColor.Red;
                        rotateLeft(parent);
                        rotateTarget = parent.Right;
                    }

                    if ((rotateTarget.Left == null || rotateTarget.Left.IsBlack) &&
                        (rotateTarget.Right == null || rotateTarget.Right.IsBlack))
                    {
                        rotateTarget.Color = NodeColor.Red;
                        target = parent;
                        parent = parent.Parent;
                    }
                    else
                    {
                        if (rotateTarget.Right == null || rotateTarget.Right.IsBlack)
                        {
                            if (rotateTarget.Left != null)
                            {
                                rotateTarget.Left.Color = NodeColor.Black;
                            }

                            rotateTarget.Color = NodeColor.Red;
                            rotateRight(rotateTarget);
                            rotateTarget = parent.Right;
                        }

                        rotateTarget.Color = parent.Color;
                        parent.Color = NodeColor.Black;

                        if (rotateTarget.Right != null)
                        {
                            rotateTarget.Right.Color = NodeColor.Black;
                        }

                        rotateLeft(parent);
                        break;
                    }
                }
                else
                {
                    rotateTarget = parent.Left;

                    if (rotateTarget.IsRed)
                    {
                        rotateTarget.Color = NodeColor.Black;
                        parent.Color = NodeColor.Red;
                        rotateRight(parent);
                        rotateTarget = parent.Left;
                    }

                    if ((rotateTarget.Right == null || rotateTarget.Right.IsBlack) &&
                        (rotateTarget.Left == null || rotateTarget.Left.IsBlack))
                    {
                        rotateTarget.Color = NodeColor.Red;
                        target = parent;
                        parent = parent.Parent;
                    }
                    else
                    {
                        if (rotateTarget.Left == null || rotateTarget.Left.IsBlack)
                        {
                            if (rotateTarget.Right != null)
                            {
                                rotateTarget.Right.Color = NodeColor.Black;
                            }

                            rotateTarget.Color = NodeColor.Red;
                            rotateLeft(rotateTarget);
                            rotateTarget = parent.Left;
                        }

                        rotateTarget.Color = parent.Color;
                        parent.Color = NodeColor.Black;

                        if (rotateTarget.Left != null)
                        {
                            rotateTarget.Left.Color = NodeColor.Black;
                        }

                        rotateRight(parent);
                        break;
                    }
                }

                target.Color = NodeColor.Black;
            }
        }

        private static void addOneChildToCount(Node parent)
        {
            while (parent != null)
            {
                parent.ChildCount++;
                parent = parent.Parent;
            }
        }

        private static void updateChildrenCountUpward(Node node)
        {
            while (node != null)
            {
                node.ChildCount = node.LeftChildCount + node.RightChildCount;
                node = node.Parent;
            }
        }

        [Conditional("CHECKCOUNTS")]
        private void verifyCounts()
        {
            HashedSet<Node> visited = new HashedSet<Node>();

            Node current = _leftMost;

            Int32 nodeCount = Count;

            while (visited.Count < nodeCount)
            {
                if (visited.Contains(current))
                {
                    visited.Add(current);
                }

                if (current.ChildCount != current.LeftChildCount + current.RightChildCount)
                {
                    Print(Console.Out);
                    Debugger.Break();
                }

                if (current.Left != null && !visited.Contains(current.Left))
                {
                    current = current.Left;
                }
                else if (current.Right != null && !visited.Contains(current.Right))
                {
                    current = current.Right;
                }
                else
                {
                    current = current.Parent;
                }
            }
        }

        private Node findNode(TItem item)
        {
            Node parent;
            return findNodeOrParent(item, out parent);
        }

        private Node findNodeOrParent(TItem item, out Node parent)
        {
            parent = null;
            Node node = _root;

            while (node != null)
            {
                parent = node;

                Int32 result;

                if ((result = _comparer.Compare(item, node.Item)) < 0)
                {
                    node = node.Left;
                }
                else if (result > 0)
                {
                    node = node.Right;
                }
                else
                {
                    break;
                }
            }

            return node;
        }

        private static void printSpace(TextWriter writer,
                                       Single width,
                                       Int32 offset)
        {
            if (offset > 0)
            {
                writer.Write(new String(' ', offset));
            }

            writer.Write(new String(' ', (Int32)Math.Round(width)));
        }

        private static void printItem(TItem item,
                                      TextWriter writer,
                                      IFormatProvider formatProvider,
                                      Single width,
                                      Int32 offset)
        {
            if (offset > 0)
            {
                writer.Write(new String(' ', offset));
            }

            String itemText = String.Format(formatProvider, "{0}", item);

            width -= itemText.Length;
            Int32 padWidth = (Int32)Math.Round(Math.Max(0, width / 2f));

            String padding = new String(' ', padWidth);

            writer.Write(padding);
            //if (width > 0 && width % 2 > 0) writer.Write(' ');
            writer.Write(itemText);
            writer.Write(padding);
        } 
        #endregion
    }
}
