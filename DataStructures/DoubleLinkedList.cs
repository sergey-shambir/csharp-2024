namespace DataStructures;

internal class DoubleLinkedList<K, V>
where V : class
{
    internal void PushBack(Node node)
    {
        Node.Link(tail, node);
        node.next = null;
        if (tail == null)
        {
            head = node;
        }
        tail = node;
    }

    internal void MoveBack(Node node)
    {
        if (node == tail)
        {
            return;
        }
        if (node == head)
        {
            head = node.next;
        }
        Node.Link(node.prev, node.next);
        Node.Link(tail, node);
        node.next = null;
        tail = node;
    }

    internal Node? Front()
    {
        return head;
    }

    internal class Node(K key, V value)
    {
        public K key = key;
        public V value = value;
        public Node? prev = null;
        public Node? next = null;

        public static void Link(Node? first, Node? second)
        {
            if (first != null)
            {
                first.next = second;
            }
            if (second != null)
            {
                second.prev = null;
            }
        }
    }

    private Node? head = null;
    private Node? tail = null;
}
