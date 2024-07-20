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
