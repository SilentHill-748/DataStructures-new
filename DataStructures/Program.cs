using DataStructures;

namespace DataStructures;

public class Program
{
    static void Main(string[] args)
    {
        int vertexCount = 5;
        Graph graph = new(vertexCount);

        graph.AddEdge(1, 2, -1);
        graph.AddEdge(1, 3, 4);
        graph.AddEdge(2, 3, 3);
        graph.AddEdge(2, 4, 2);
        graph.AddEdge(2, 5, 2);
        graph.AddEdge(4, 2, 1);
        graph.AddEdge(4, 3, 5);
        graph.AddEdge(5, 4, -3);

        graph.PrintMatrix();

        Console.WriteLine($"\nКратчайшие пути из {graph.Vertices[0].Number}:\n");

        foreach (int weight in graph.BellmanFordAlgorithm())
        {
            Console.Write($"{weight} ");
        }
    }
}