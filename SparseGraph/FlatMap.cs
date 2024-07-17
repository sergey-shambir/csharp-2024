namespace SparseGraph;


public class FlatMap<K, V>
where K : IComparable<K>
where V : class
{
    public struct KeyValue(K key, V value)
    {
        public K key = key;
        public V value = value;
    }

    public List<K> Keys()
    {
        return items.Select((KeyValue kv) => kv.key).ToList();
    }

    public List<V> Values()
    {
        return items.Select((KeyValue kv) => kv.value).ToList();
    }

    public int Size()
    {
        return items.Count;
    }

    public V? Get(K key)
    {
        var index = LowerBound(key);
        if (index != items.Count && items[index].key.CompareTo(key) == 0)
        {
            return items[index].value;
        }
        return null;
    }

    public bool Contains(K key)
    {
        var index = LowerBound(key);
        return index != items.Count && items[index].key.CompareTo(key) == 0;
    }

    public void Add(K key, V value)
    {
        var index = LowerBound(key);
        if (index != items.Count && items[index].key.CompareTo(key) == 0)
        {
            items[index] = new KeyValue(key, value);
        }
        else
        {
            items.Insert(index, new KeyValue(key, value));
        }
    }

    public void Delete(K key)
    {
        var index = LowerBound(key);
        if (index != items.Count && items[index].key.CompareTo(key) == 0)
        {
            items.RemoveAt(index);
        }
    }

    private int LowerBound(K key)
    {
        var left = 0;
        var right = items.Count;
        while (left != right)
        {
            var middle = (left + right) / 2;
            if (items[middle].key.CompareTo(key) < 0)
            {
                left = middle + 1;
            }
            else
            {
                right = middle;
            }
        }
        return left;
    }

    private readonly List<KeyValue> items = [];
}
