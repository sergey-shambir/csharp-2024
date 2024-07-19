namespace SparseGraph.Tests;

public class MinHeapTest
{
    [Fact]
    public void TestMinHeapWithIntPriority()
    {
        MinHeap<string, int> h = new();
        Assert.Throws<IndexOutOfRangeException>(() => h.Dequeue());
        Assert.Equal(0, h.Count);
        Assert.True(h.Empty);

        h.Enqueue("banana", 17);
        Assert.Equal(("banana", 17), h.Top());
        Assert.Equal(1, h.Count);
        Assert.False(h.Empty);

        h.Enqueue("cherry", 3);
        Assert.Equal(("cherry", 3), h.Top());
        Assert.Equal(2, h.Count);
        Assert.False(h.Empty);

        h.Enqueue("apple", 20);
        h.Enqueue("orange", 10);
        h.Enqueue("lime", 30);
        Assert.Equal(("cherry", 3), h.Top());
        Assert.Equal(5, h.Count);
        Assert.False(h.Empty);

        Assert.Equal(("cherry", 3), h.Dequeue());
        Assert.Equal(("orange", 10), h.Dequeue());
        Assert.Equal(("banana", 17), h.Dequeue());
        Assert.Equal(2, h.Count);
        Assert.False(h.Empty);

        h.Enqueue("pineapple", 8);
        h.Enqueue("pear", 14);
        h.Enqueue("banana", 17);
        h.Enqueue("orange", 10);
        Assert.Equal(6, h.Count);
        Assert.False(h.Empty);

        Assert.Equal(("pineapple", 8), h.Dequeue());
        Assert.Equal(("orange", 10), h.Dequeue());
        Assert.Equal(("pear", 14), h.Dequeue());
        Assert.Equal(("banana", 17), h.Dequeue());
        Assert.Equal(("apple", 20), h.Dequeue());
        Assert.Equal(("lime", 30), h.Dequeue());
        Assert.Equal(0, h.Count);
        Assert.True(h.Empty);
    }
    [Fact]
    public void TestMinHeapWithDobule()
    {
        MinHeap<string, double> h = new();
        h.Enqueue("apple", 2.2);
        h.Enqueue("lime", 3.3);
        h.Enqueue("pineapple", 0.8);
        h.Enqueue("pear", 1.4);
        h.Enqueue("banana", 1.7);
        h.Enqueue("orange", 1.0);
        Assert.Equal(6, h.Count);
        Assert.False(h.Empty);

        Assert.Equal(("pineapple", 0.8), h.Dequeue());
        Assert.Equal(("orange", 1.0), h.Dequeue());
        Assert.Equal(("pear", 1.4), h.Dequeue());
        Assert.Equal(("banana", 1.7), h.Dequeue());
        Assert.Equal(("apple", 2.2), h.Dequeue());
        Assert.Equal(("lime", 3.3), h.Dequeue());
        Assert.Equal(0, h.Count);
        Assert.True(h.Empty);
    }
}
