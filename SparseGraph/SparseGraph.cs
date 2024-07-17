namespace SparseGraph;

public class SparseGraph
{
    public SparseGraph(int vertexCount)
    {
        verticies = [];
        for (int i = 0; i < vertexCount; ++i)
        {
            verticies.Add(new FlatMap<int, double>());
        }
    }

    public int VertexCount()
    {
        return verticies.Count;
    }

    public bool HasEdge(int fromVertex, int toVertex)
    {
        return verticies[fromVertex].Contains(toVertex);
    }

    public double? GetEdgeWeight(int fromVertex, int toVertex)
    {
        return verticies[fromVertex].Get(toVertex);
    }

    public FlatMap<int, double> GetAdjacencyList(int vertex)
    {
        return verticies[vertex];
    }

    public void AddDirectedEdge(int fromVertex, int toVertex, double weight = 1.0)
    {
        verticies[fromVertex].Add(toVertex, weight);
    }

    public void AddUndirectedEdge(int fromVertex, int toVertex, double weight = 1.0)
    {
        verticies[fromVertex].Add(toVertex, weight);
        verticies[toVertex].Add(fromVertex, weight);
    }

    private readonly List<FlatMap<int, double>> verticies;
};
