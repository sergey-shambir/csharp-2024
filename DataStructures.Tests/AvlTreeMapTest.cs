namespace DataStructures.Tests;

public class AvlTreeMapTest
{
    [Fact]
    public void TestStringMap()
    {
        AvlTreeMap<string, string> m = new();
        m.Insert("banana", "fruit");
        m.Insert("coconut", "fruit");
        m.Insert("potato", "vegetable");
        m.Insert("dog", "animal");

        Assert.False(m.Contains("apple"));
        Assert.False(m.Contains("tomato"));

        Assert.True(m.Contains("coconut"));
        Assert.True(m.Contains("potato"));
        Assert.True(m.Contains("dog"));
        Assert.True(m.Contains("banana"));
        
        Assert.Equal("fruit", m.Get("banana"));
        Assert.Equal("fruit", m.Get("coconut"));
        Assert.Equal("animal", m.Get("dog"));
        Assert.Equal("vegetable", m.Get("potato"));
        
        Assert.Throws<KeyNotFoundException>(() => m.Get("apple"));
        Assert.Throws<KeyNotFoundException>(() => m.Get("tomato"));

        m.Remove("banana");
        m.Remove("potato");

        Assert.True(m.Contains("coconut"));
        Assert.True(m.Contains("dog"));
        Assert.False(m.Contains("potato"));
        Assert.False(m.Contains("banana"));
        Assert.Throws<KeyNotFoundException>(() => m.Get("banana"));
        Assert.Throws<KeyNotFoundException>(() => m.Get("potato"));
        Assert.Equal("animal", m.Get("dog"));
        Assert.Equal("fruit", m.Get("coconut"));
    }
}
