namespace DataStructures;

public class LRUCache<K, V>(int capacity)
where K : IComparable<K>
where V : class
{
    public V? Get(in K key)
    {
        if (keyToNodeDict.TryGetValue(key, out var node))
        {
            nodes.MoveBack(node);
            return node.value;
        }
        return null;
    }

    public void Put(in K key, in V value)
    {
        if (keyToNodeDict.TryGetValue(key, out var node))
        {
            node.value = value;
            nodes.MoveBack(node);
        }
        else if (keyToNodeDict.Count >= capacity)
        {
            node = nodes.Front()!;
            nodes.MoveBack(node);

            keyToNodeDict.Remove(node.key);
            keyToNodeDict[key] = node;
            node.key = key;
            node.value = value;
        }
        else
        {
            node = new(key, value);
            nodes.PushBack(node);
            keyToNodeDict[key] = node;
        }
    }

    private readonly int capacity = capacity;
    private readonly DoubleLinkedList<K, V> nodes = new();
    private readonly Dictionary<K, DoubleLinkedList<K, V>.Node> keyToNodeDict = [];
}
