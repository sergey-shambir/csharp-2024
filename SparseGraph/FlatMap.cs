using System.Collections;

namespace SparseGraph;

public struct KeyValue<K, V>
(K key, V value)
where K : IComparable<K>
{
    public K key = key;
    public V value = value;

    public readonly void Deconstruct(out K key, out V value)
    {
        key = this.key;
        value = this.value;
    }
}

public class FlatMap<K, V> : IEnumerable<KeyValue<K, V>>
where K : IComparable<K>
{

    public List<K> Keys()
    {
        return items.Select((KeyValue<K, V> kv) => kv.key).ToList();
    }

    public List<V> Values()
    {
        return items.Select((KeyValue<K, V> kv) => kv.value).ToList();
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
        return default;
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
            items[index] = new KeyValue<K, V>(key, value);
        }
        else
        {
            items.Insert(index, new KeyValue<K, V>(key, value));
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

    IEnumerator<KeyValue<K, V>> IEnumerable<KeyValue<K, V>>.GetEnumerator()
    {
        return items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<KeyValue<K, V>>)this).GetEnumerator();
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

    private readonly List<KeyValue<K, V>> items = [];
}
