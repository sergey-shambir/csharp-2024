namespace SparseGraph.Tests;
public class SparseGraphTest
{
    [Fact]
    public void TestCanStoreUndirectedEdges()
    {
        SparseGraph graph = new(4);
        graph.AddUndirectedEdge(0, 3, 240.0);
        graph.AddUndirectedEdge(0, 2, 200.0);
        graph.AddUndirectedEdge(2, 3, 100.0);
        graph.AddUndirectedEdge(1, 3, 300.0);

        Assert.False(graph.HasEdge(0, 1));
        Assert.False(graph.HasEdge(1, 1));

        Assert.True(graph.HasEdge(0, 3));
        Assert.True(graph.HasEdge(3, 0));
        Assert.True(graph.HasEdge(0, 2));
        Assert.True(graph.HasEdge(2, 0));
        Assert.True(graph.HasEdge(1, 3));
        Assert.True(graph.HasEdge(3, 0));

        Assert.Equal(0.0, graph.GetEdgeWeight(0, 1)!.Value);
        Assert.Equal(0.0, graph.GetEdgeWeight(1, 1)!.Value);
        Assert.Equal(240.0, graph.GetEdgeWeight(0, 3)!.Value, 0.001);
        Assert.Equal(240.0, graph.GetEdgeWeight(3, 0)!.Value, 0.001);
        Assert.Equal(200.0, graph.GetEdgeWeight(0, 2)!.Value, 0.001);
        Assert.Equal(200.0, graph.GetEdgeWeight(2, 0)!.Value, 0.001);
        Assert.Equal(300.0, graph.GetEdgeWeight(1, 3)!.Value, 0.001);
        Assert.Equal(300.0, graph.GetEdgeWeight(3, 1)!.Value, 0.001);
        Assert.Equal(100.0, graph.GetEdgeWeight(2, 3)!.Value, 0.001);
        Assert.Equal(100.0, graph.GetEdgeWeight(3, 2)!.Value, 0.001);
    }
}
