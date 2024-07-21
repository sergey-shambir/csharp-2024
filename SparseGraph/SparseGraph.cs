namespace SparseGraph;

public class SparseGraph
{
    public SparseGraph(int vertexCount)
    {
        vertexEdges = new(vertexCount);
        for (int i = 0; i < vertexCount; ++i)
        {
            vertexEdges.Add([]);
        }
        vertexDegrees = Enumerable.Repeat(0, vertexCount).ToList();
    }

    public int VertexCount
    {
        get => vertexEdges.Count;
    }

    public int GetVertexDegree(int vertex)
    {
        return vertexDegrees[vertex];
    }

    public bool HasEdge(int fromVertex, int toVertex)
    {
        return vertexEdges[fromVertex].Contains(toVertex);
    }

    public double? GetEdgeWeight(int fromVertex, int toVertex)
    {
        return vertexEdges[fromVertex].Get(toVertex);
    }

    public FlatMap<int, double> GetAdjacencyList(int vertex)
    {
        return vertexEdges[vertex];
    }

    public void AddDirectedEdge(int fromVertex, int toVertex, double weight = 1.0)
    {
        if (!vertexEdges[fromVertex].Contains(toVertex) && !vertexEdges[toVertex].Contains(fromVertex))
        {
            vertexDegrees[fromVertex]++;
            vertexDegrees[toVertex]++;
        }
        vertexEdges[fromVertex].Add(toVertex, weight);
    }

    public void AddUndirectedEdge(int fromVertex, int toVertex, double weight = 1.0)
    {
        if (!vertexEdges[fromVertex].Contains(toVertex) && !vertexEdges[toVertex].Contains(fromVertex))
        {
            vertexDegrees[fromVertex]++;
            vertexDegrees[toVertex]++;
        }
        vertexEdges[fromVertex].Add(toVertex, weight);
        vertexEdges[toVertex].Add(fromVertex, weight);
    }

    private readonly List<FlatMap<int, double>> vertexEdges;
    private readonly List<int> vertexDegrees;
};
