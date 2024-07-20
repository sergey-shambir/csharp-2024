namespace SparseGraph;

public class PathFinder
{
    public static List<double> FindDistances(SparseGraph graph, int fromVertex)
    {
        var distances = Enumerable.Repeat(double.PositiveInfinity, graph.VertexCount()).ToList();
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
}