namespace SparseGraph.Tests;

public class PathFinderTest
{
    [Fact]
    public void TestFindDistancesWithFourVerticies()
    {
        {
            SparseGraph g = new(4);
            g.AddUndirectedEdge(0, 3, 240.0);
            g.AddUndirectedEdge(0, 2, 200.0);
            g.AddUndirectedEdge(2, 3, 100.0);
            g.AddUndirectedEdge(1, 3, 300.0);

            AssertDistances([0.0, 540.0, 200.0, 240.0], PathFinder.FindDistances(g, 0));
            AssertDistances([540.0, 0.0, 400.0, 300.0], PathFinder.FindDistances(g, 1));
            AssertDistances([200.0, 400.0, 0.0, 100.0], PathFinder.FindDistances(g, 2));
            AssertDistances([240.0, 300.0, 100.0, 0.0], PathFinder.FindDistances(g, 3));
        }
        {
            SparseGraph g = new(4);
            g.AddUndirectedEdge(0, 3, 240.0);
            g.AddUndirectedEdge(0, 2, 200.0);
            g.AddUndirectedEdge(2, 3, 20.0);
            g.AddUndirectedEdge(1, 3, 300.0);

            AssertDistances([0.0, 520.0, 200.0, 220.0], PathFinder.FindDistances(g, 0));
            AssertDistances([520.0, 0.0, 320.0, 300.0], PathFinder.FindDistances(g, 1));
            AssertDistances([200.0, 320.0, 0.0, 20.0], PathFinder.FindDistances(g, 2));
            AssertDistances([220.0, 300.0, 20.0, 0.0], PathFinder.FindDistances(g, 3));
        }
    }

    [Fact]
    public void TestFindDistancesWithOneVertex()
    {
        SparseGraph g = new(1);
        AssertDistances([0.0], PathFinder.FindDistances(g, 0));
    }

    [Fact]
    public void TestFindEulerPathFails()
    {
        SparseGraph g = new(5);
        g.AddUndirectedEdge(0, 1);
        g.AddUndirectedEdge(1, 2);
        g.AddUndirectedEdge(1, 3);
        g.AddUndirectedEdge(3, 4);

        Assert.False(PathFinder.HasEulerGraph(g));
    }

    [Fact]
    public void TestFindEulerPathSucceeds()
    {
        SparseGraph g = new(5);
        g.AddUndirectedEdge(0, 1);
        g.AddUndirectedEdge(1, 2);
        g.AddUndirectedEdge(1, 3);
        g.AddUndirectedEdge(3, 4);
        g.AddUndirectedEdge(0, 2);

        Assert.True(PathFinder.HasEulerGraph(g));
        AssertDirectedEulerPath(g, PathFinder.FindDirectedEulerPath(g));
    }

    private static void AssertDistances(double[] expected, List<double> actual, double tolerance = 0.001)
    {
        Assert.Equal(expected.Length, actual.Count);
        for (int i = 0; i < expected.Length; ++i)
        {
            Assert.Equal(expected[i], actual[i], tolerance);
        }
    }

    private static void AssertDirectedEulerPath(SparseGraph graph, List<int> path)
    {
        HashSet<(int, int)> remainingEdges = [];
        for (int fromVertex = 0, vertexCount = graph.VertexCount; fromVertex < vertexCount; fromVertex++)
        {
            foreach ((int toVertex, _) in graph.GetAdjacencyList(fromVertex))
            {
                remainingEdges.Add((fromVertex, toVertex));
            }
        }

        Assert.Equal(remainingEdges.Count + 1, path.Count);

        int prevVertex = -1;
        foreach (int vertex in path)
        {
            if (prevVertex != -1)
            {
                bool removed = remainingEdges.Remove((prevVertex, vertex));
                if (!removed)
                {
                    Assert.Fail($"Euler path contains non-existing edge ({prevVertex}, {vertex})");
                }
            }
            prevVertex = vertex;
        }
        if (remainingEdges.Count > 0)
        {
            string formattedEdges = "[" + string.Join(", ", remainingEdges.ToList().Select(edge => "(" + string.Join(", ", edge) + ")")) + "]";
            Assert.Fail($"Euler path does not contain edges {formattedEdges}");
        }
    }
}
