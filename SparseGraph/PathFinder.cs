namespace SparseGraph;

public class PathFinder
{
    public static List<double> FindDistances(SparseGraph graph, int fromVertex)
    {
        var distances = Enumerable.Repeat(double.PositiveInfinity, graph.VertexCount).ToList();
        var queue = new MinHeap<int, double>();

        distances[fromVertex] = 0.0;
        queue.Enqueue(fromVertex, 0);

        while (!queue.Empty)
        {
            (int vertex, double distance) = queue.Dequeue();
            double currentDistance = distances[vertex];
            if (distance > currentDistance)
            {
                continue; // Ignore  outdated distances from queue
            }
            foreach ((int toVertex, double weight) in graph.GetAdjacencyList(vertex))
            {
                var newDistance = currentDistance + weight;
                if (newDistance < distances[toVertex])
                {
                    distances[toVertex] = newDistance;
                    queue.Enqueue(toVertex, newDistance);
                }
            }
        }

        return distances;
    }

    // Returns valid result if and only if IsEulerGraph() returns true
    // See https://neerc.ifmo.ru/wiki/index.php?title=Алгоритм_построения_Эйлерова_цикла
    // See https://algorithmica.org/ru/dfs
    public static List<int> FindDirectedEulerPath(SparseGraph graph)
    {
        int startVertex = 0;
        for (int vertex = 0, vertexCount = graph.VertexCount; vertex < vertexCount; ++vertex)
        {
            if (graph.GetVertexDegree(vertex) % 2 == 1)
            {
                startVertex = vertex;
                break;
            }
        }

        Stack<int> vertexStack = new();
        HashSet<(int, int)> visitedEdges = [];
        List<int> path = [];

        vertexStack.Push(startVertex);
        while (vertexStack.Count > 0)
        {
            int fromVertex = vertexStack.Peek();
            bool foundEdge = false;
            foreach ((var toVertex, _) in graph.GetAdjacencyList(fromVertex))
            {
                (int, int) edge = new(fromVertex, toVertex);
                if (!visitedEdges.Contains(edge))
                {
                    foundEdge = true;
                    visitedEdges.Add(edge);
                    vertexStack.Push(toVertex);
                    break;
                }
            }
            if (!foundEdge)
            {
                vertexStack.Pop();
                path.Add(fromVertex);
            }
        }

        return path;
    }

    public static bool HasEulerGraph(SparseGraph graph)
    {
        int vertexCount = graph.VertexCount;
        int oddVertexCount = 0;
        for (int vertex = 0; vertex < vertexCount; ++vertex)
        {
            if (graph.GetVertexDegree(vertex) % 2 == 1)
            {
                ++oddVertexCount;
            }
        }
        if (oddVertexCount > 2)
        {
            // Euler graph must have 0 or 2 verticies with odd edge count
            return false;
        }

        return DeepFirstSearch(graph, 0).All((bool value) => value);
    }

    private static bool[] DeepFirstSearch(SparseGraph graph, int startVertex)
    {
        bool[] visited = new bool[graph.VertexCount];
        visited[startVertex] = true;
        DeepFirstSearchImpl(graph, startVertex, visited);

        return visited;
    }

    private static void DeepFirstSearchImpl(SparseGraph graph, int vertex, bool[] visited)
    {
        foreach ((var nextVertex, _) in graph.GetAdjacencyList(vertex))
        {
            if (!visited[nextVertex])
            {
                visited[nextVertex] = true;
                DeepFirstSearchImpl(graph, nextVertex, visited);
            }
        }
    }
}
