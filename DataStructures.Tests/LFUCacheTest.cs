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

    [Fact]
    public void TestCacheWithCapacity4()
    {
        LFUCache<int, string> c = new(4);
        c.Put(1, "apple");
        c.Get(1); // Increment usages
        c.Put(2, "banana");
        c.Put(3, "coconut");
        c.Put(4, "dragonfruit");
        c.Put(5, "feijoa");
        c.Put(6, "grape");

        Assert.Null(c.Get(2)); // Removed from cache
        Assert.Null(c.Get(3)); // Removed from cache
        Assert.Equal("apple", c.Get(1));
        Assert.Equal("dragonfruit", c.Get(4));
        Assert.Equal("feijoa", c.Get(5));
        Assert.Equal("grape", c.Get(6));

        c.Put(7, "kiwi");

        Assert.Null(c.Get(2)); // Removed from cache
        Assert.Null(c.Get(3)); // Removed from cache
        Assert.Null(c.Get(4)); // Removed from cache
        Assert.Equal("apple", c.Get(1));
        Assert.Equal("feijoa", c.Get(5));
        Assert.Equal("grape", c.Get(6));
        Assert.Equal("kiwi", c.Get(7));
        Assert.Null(c.Get(8)); // Never saved in cache
    }
}
