namespace SparseGraph;

public class MinHeap<TValue, TPriority>
where TPriority : IComparable
{
    public int Count { get => minHeap.Count; }

    public bool Empty { get => minHeap.Count == 0; }

    public void Enqueue(TValue value, TPriority priority)
    {
        minHeap.Add(new TPair(value, priority));
        int index = minHeap.Count - 1;
        Up(index);
    }

    public (TValue, TPriority) Dequeue()
    {
        if (minHeap.Count == 0)
        {
            throw new IndexOutOfRangeException("min heap is empty");
        }
        TPair result = minHeap[0];
        Swap(0, minHeap.Count - 1);
        minHeap.RemoveAt(minHeap.Count - 1);
        Down(0);

        return (result.value, result.priority);
    }

    public (TValue, TPriority) Top()
    {
        if (minHeap.Count == 0)
        {
            throw new IndexOutOfRangeException("min heap is empty");
        }
        return (minHeap[0].value, minHeap[0].priority);
    }

    private struct TPair(TValue value, TPriority priority)
    {
        public TValue value = value;
        public TPriority priority = priority;
    }

    private void Down(int i)
    {
        int left = 2 * i + 1;
        int right = 2 * i + 2;
        int smallest = i;
        if (left < minHeap.Count && Less(left, smallest))
        {
            smallest = left;
        }
        if (right < minHeap.Count && Less(right, smallest))
        {
            smallest = right;
        }
        if (smallest != i)
        {
            Swap(i, smallest);
            Down(smallest);
        }
    }

    private void Up(int i)
    {
        int parent = (i - 1) / 2;
        if (i != 0 && Less(i, parent))
        {
            Swap(i, parent);
            Up(parent);
        }
    }

    private bool Less(int i, int j)
    {
        return minHeap[i].priority.CompareTo(minHeap[j].priority) < 0;
    }

    private void Swap(int i, int j)
    {
        (minHeap[i], minHeap[j]) = (minHeap[j], minHeap[i]);
    }

    private readonly List<TPair> minHeap = [];
};
