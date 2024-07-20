using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace DataStructures;

public class AvlTreeMap<K, V>
where K : IComparable
{
    public V Get(in K key)
    {
        (var value, var exists) = FindImpl(root, key);
        if (!exists)
        {
            throw new KeyNotFoundException($"no key '{key}' in map");
        }
        return value!;
    }

    public bool Contains(in K key)
    {
        (_, var exists) = FindImpl(root, key);
        return exists;
    }

    public void Insert(in K key, in V value)
    {
        root = InsertImpl(root, key, value);
    }

    public void Remove(in K key)
    {
        root = RemoveImpl(root, key);
    }

    private static Node InsertImpl(Node? node, in K key, in V value)
    {
        if (node == null)
        {
            return new Node(key, value);
        }
        int order = key.CompareTo(node.key);
        if (order < 0) // key < node.key
        {
            node.left = InsertImpl(node.left, key, value);
        }
        else if (order > 0) // key > node.key
        {
            node.right = InsertImpl(node.right, key, value);
        }
        else // key == node.key
        {
            node.value = value;
        }
        return node.Balance();
    }

    private static Node? RemoveImpl(Node? node, in K key)
    {
        if (node == null)
        {
            return null;
        }
        int order = key.CompareTo(node.key);
        if (order < 0) // key < node.key
        {
            node.left = RemoveImpl(node.left, key);
        }
        else if (order > 0) // key > node.key
        {
            node.right = RemoveImpl(node.right, key);
        }
        else // key == node.key
        {
            Node? left = node.left;
            Node? right = node.right;
            node.left = null;
            node.right = null;

            if (right == null)
            {
                return left;
            }
            Node newRoot = right.FindLeftmostChild();
            newRoot.right = right.RemoveLeftmostChild();
            newRoot.left = left;

            return newRoot.Balance();
        }
        return node.Balance();
    }

    private static (V?, bool) FindImpl(Node? node, in K key)
    {
        if (node == null)
        {
            return (default, false);
        }
        int order = key.CompareTo(node.key);
        if (order == 0)
        {
            return (node.value, true);
        }
        if (order < 0)
        {
            return FindImpl(node.left, key);
        }
        return FindImpl(node.right, key);
    }

    private Node? root = null;

    private class Node(in K key, in V value)
    {
        public K key = key;
        public V value = value;
        public sbyte height = 1;
        public Node? left = null;
        public Node? right = null;

        public Node FindLeftmostChild()
        {
            return left != null ? left.FindLeftmostChild() : this;
        }

        public Node? RemoveLeftmostChild()
        {
            if (left == null)
            {
                return right;
            }
            left = left.RemoveLeftmostChild();
            return Balance();
        }

        public Node Balance()
        {
            UpdateHeight();
            sbyte factor = GetBalanceFactor();
            if (factor == 2)
            {
                if (right!.GetBalanceFactor() < 0)
                {
                    right = right!.RotateRight();
                }
                return RotateLeft();
            }
            if (factor == -2)
            {
                if (left!.GetBalanceFactor() > 0)
                {
                    left = left!.RotateLeft();
                }
                return RotateRight();
            }
            return this;
        }

        private Node RotateRight()
        {
            Node newRoot = left!;
            left = newRoot.right;
            newRoot.right = this;

            UpdateHeight();
            newRoot.UpdateHeight();

            return newRoot;
        }

        private Node RotateLeft()
        {
            Node newRoot = right!;
            right = newRoot.left;
            newRoot.left = this;

            UpdateHeight();
            newRoot.UpdateHeight();

            return newRoot;
        }

        private sbyte GetBalanceFactor()
        {
            return (sbyte) (GetHeight(right) - GetHeight(left));
        }

        private void UpdateHeight()
        {
            height = (sbyte)(1 + Math.Max(GetHeight(left), GetHeight(right)));
        }

        private static sbyte GetHeight(Node? node)
        {
            return (node != null) ? node.height : (sbyte) 0;
        }
    }
}
