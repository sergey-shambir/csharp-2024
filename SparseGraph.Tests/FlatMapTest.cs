using SparseGraph;

namespace SparseGraph.Tests;

public class FlatMapTest
{
    [Fact]
    public void TestWithIntKeys()
    {
        FlatMap<int, string> m = new();
        m.Add(5, "pear");
        m.Add(10, "banana");
        m.Add(7, "apple");

        Assert.Equal(new int[] { 5, 7, 10 }, m.Keys());
        Assert.Equal(new string[] { "pear", "apple", "banana" }, m.Values());

        Assert.False(m.Contains(1));
        Assert.Null(m.Get(1));
        Assert.False(m.Contains(8));
        Assert.Null(m.Get(8));

        Assert.True(m.Contains(5));
        Assert.Equal("banana", m.Get(10));
        Assert.Equal("pear", m.Get(5));
        Assert.Equal("apple", m.Get(7));

        Assert.True(m.Contains(7));
        Assert.True(m.Contains(10));
        
        m.Delete(7);
        Assert.Equal(new int[] { 5, 10 }, m.Keys());
        Assert.Equal(new string[] { "pear", "banana" }, m.Values());
        
        m.Add(4, "orange");
        m.Add(17, "lime");
        Assert.Equal(new int[] { 4, 5, 10, 17 }, m.Keys());
        Assert.Equal(new string[] { "orange", "pear", "banana", "lime" }, m.Values());
        
        Assert.True(m.Contains(4));
        Assert.Equal("orange", m.Get(4));
        Assert.True(m.Contains(17));
        Assert.Equal("lime", m.Get(17));
        Assert.True(m.Contains(5));
        Assert.Equal("pear", m.Get(5));
    }
}
