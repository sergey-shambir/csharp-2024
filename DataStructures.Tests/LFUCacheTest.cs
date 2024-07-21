namespace DataStructures.Tests;

public class LFUCacheTest
{
    [Fact]
    public void TestCacheWithCapacity2()
    {
        LFUCache<int, string> c = new(2);
        c.Put(1, "apple");
        c.Put(2, "banana");
        Assert.Equal("apple", c.Get(1)); // Saved in cache

        c.Put(3, "coconut");
        Assert.Null(c.Get(2)); // Removed from LFU  cache

        c.Put(4, "dragonfruit");
        Assert.Null(c.Get(3)); // Removed from LFU cache
        Assert.Equal("apple", c.Get(1));
        Assert.Equal("dragonfruit", c.Get(4));

        Assert.Equal("dragonfruit", c.Get(4));
        Assert.Equal("apple", c.Get(1));
    }
}
