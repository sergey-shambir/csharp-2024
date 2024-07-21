using System.Globalization;

namespace DataStructures;

public class LFUCache<K, V>(int capacity)
where K : IComparable<K>
where V : class
{
    public V? Get(in K key)
    {
        if (keyToIndexMap.TryGetValue(key, out var index))
        {
            var entry = minHeap[index];
            entry.frequency++;
            minHeap[index] = entry;
            HeapDown(index);

            return entry.value;
        }
        return null;
    }

    public void Put(in K key, in V value)
    {
        if (keyToIndexMap.TryGetValue(key, out var index))
        {
            // Update existing entry
            var entry = minHeap[index];
            entry.value = value;
            entry.frequency++;
            minHeap[index] = entry;
            HeapDown(index);
        }
        else
        {
            var entry = new Entry(key, value, ++lastGeneration);
            if (minHeap.Count >= capacity)
            {
                // Replace least frequency used or max age entry
                keyToIndexMap.Remove(minHeap[0].key);
                minHeap[0] = entry;
                keyToIndexMap.Add(entry.key, 0);
                HeapDown(0);
            }
            else
            {
                // Add new entry without evicting other entries
                minHeap.Add(entry);
                keyToIndexMap.Add(entry.key, minHeap.Count - 1);
                HeapUp(minHeap.Count - 1);
            }
        }
    }

    private void HeapDown(int i)
    {
        int left = 2 * i + 1;
        int right = 2 * i + 2;
        int smallest = i;
        if (left < minHeap.Count && LessInHeap(left, smallest))
        {
            smallest = left;
        }
        if (right < minHeap.Count && LessInHeap(right, smallest))
        {
            smallest = right;
        }
        if (smallest != i)
        {
            SwapInHeap(i, smallest);
            HeapDown(smallest);
        }
    }

    private void HeapUp(int i)
    {
        int parent = (i - 1) / 2;
        if (i != 0 && LessInHeap(i, parent))
        {
            SwapInHeap(i, parent);
            HeapUp(parent);
        }
    }

    // Element with less use frequency or greater age will be evicted earlier.
    private bool LessInHeap(int i1, int i2)
    {
        Entry entry1 = minHeap[i1];
        Entry entry2 = minHeap[i2];
        uint age1 = lastGeneration - entry1.generation;
        uint age2 = lastGeneration - entry2.generation;

        return entry1.frequency < entry2.frequency || (entry1.frequency == entry2.frequency && age1 > age2);
    }

    private void SwapInHeap(int i, int j)
    {
        (minHeap[i], minHeap[j]) = (minHeap[j], minHeap[i]);
        keyToIndexMap[minHeap[i].key] = i;
        keyToIndexMap[minHeap[j].key] = j;
    }

    private readonly int capacity = capacity;
    private uint lastGeneration = 0;
    private readonly List<Entry> minHeap = [];
    private readonly Dictionary<K, int> keyToIndexMap = [];

    private struct Entry(in K key, in V value, uint generation)
    {
        public K key = key;
        public V value = value;
        public int frequency = 1;
        // Note: assume overflow will never happen or will not violate heap invariants.
        public uint generation = generation;
    };
}
