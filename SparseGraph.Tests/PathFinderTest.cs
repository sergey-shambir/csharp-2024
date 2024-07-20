namespace SparseGraph.Tests;

public class PathFinderTest
{
    [Fact]
    public void TestForGraphWithFourVerticies()
    {
        {
            var graph = new SparseGraph(4);
            graph.AddUndirectedEdge(0, 3, 240.0);
            graph.AddUndirectedEdge(0, 2, 200.0);
            graph.AddUndirectedEdge(2, 3, 100.0);
            graph.AddUndirectedEdge(1, 3, 300.0);

            AssertDistances([0.0, 540.0, 200.0, 240.0], PathFinder.FindDistances(graph, 0));
            AssertDistances([540.0, 0.0, 400.0, 300.0], PathFinder.FindDistances(graph, 1));
            AssertDistances([200.0, 400.0, 0.0, 100.0], PathFinder.FindDistances(graph, 2));
            AssertDistances([240.0, 300.0, 100.0, 0.0], PathFinder.FindDistances(graph, 3));
        }
        {
            var graph = new SparseGraph(4);
            graph.AddUndirectedEdge(0, 3, 240.0);
            graph.AddUndirectedEdge(0, 2, 200.0);
            graph.AddUndirectedEdge(2, 3, 20.0);
            graph.AddUndirectedEdge(1, 3, 300.0);

            AssertDistances([0.0, 520.0, 200.0, 220.0], PathFinder.FindDistances(graph, 0));
            AssertDistances([520.0, 0.0, 320.0, 300.0], PathFinder.FindDistances(graph, 1));
            AssertDistances([200.0, 320.0, 0.0, 20.0], PathFinder.FindDistances(graph, 2));
            AssertDistances([220.0, 300.0, 20.0, 0.0], PathFinder.FindDistances(graph, 3));
        }
    }

    [Fact]
    public void TestForGraphWithOneVerticies()
    {
        var graph = new SparseGraph(1);
        AssertDistances([0.0], PathFinder.FindDistances(graph, 0));
    }

    private void AssertDistances(double[] expected, List<double> actual, double tolerance = 0.001)
    {
        Assert.Equal(expected.Length, actual.Count);
        for (int i = 0; i < expected.Length; ++i)
        {
            Assert.Equal(expected[i], actual[i], tolerance);
        }
    }
}
