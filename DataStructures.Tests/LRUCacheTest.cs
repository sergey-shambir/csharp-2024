namespace DataStructures.Tests;

public class LRUCacheTest
{
    [Fact]
    public void TestCacheWithCapacity2()
    {
        LRUCache<int, string> c = new(2);
        c.Put(1, "apple");
        c.Put(2, "banaana");
        Assert.Equal("apple", c.Get(1)); // Saved in cache

        c.Put(3, "coconut");
        Assert.Null(c.Get(2)); // Removed from cache

        c.Put(4, "dragonfruit");
        Assert.Null(c.Get(1));
        Assert.Equal("coconut", c.Get(3));
        Assert.Equal("dragonfruit", c.Get(4));

        Assert.Equal("dragonfruit", c.Get(4));
        Assert.Equal("coconut", c.Get(3));
        Assert.Null(c.Get(1));
    }

    [Fact]
    public void TestCacheWithCapacity4()
    {
        LRUCache<int, string> c = new(4);
        c.Put(1, "apple");
        c.Put(2, "banana");
        c.Put(3, "coconut");
        c.Put(4, "dragonfruit");
        c.Put(5, "grape");

        Assert.Null(c.Get(1)); // Removed from cache
        Assert.Equal("coconut", c.Get(3));
        Assert.Equal("banana", c.Get(2));
        Assert.Equal("grape", c.Get(5));
        Assert.Equal("dragonfruit", c.Get(4));
    }
}
