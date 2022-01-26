using DataStructures;

Graph graph = new();

Vertex v1 = new(1);
Vertex v2 = new(2);
Vertex v3 = new(3);
Vertex v4 = new(4);
Vertex v5 = new(5);

graph.AddVertexRange(new Vertex[] { v1, v2, v3, v4, v5 });

graph.AddEdge(v1, v2);
graph.AddEdge(v1, v4);
graph.AddEdge(v2, v5);
graph.AddEdge(v3, v1);
graph.AddEdge(v3, v4);
graph.AddEdge(v4, v2);
graph.AddEdge(v4, v3);
graph.AddEdge(v4, v4);
graph.AddEdge(v5, v2);

PrintGraph(graph);

var list = graph.GetMergedVertices(v4);

Console.WriteLine($"Для вершины 4, список вершин такой: ");
foreach (Vertex v in list)
    Console.WriteLine($"{v}");

Console.WriteLine();
var listOfvertices = graph.BreadthFirstSearch(v1, v5);

foreach (Vertex v in listOfvertices)
{
    Console.Write($"{v} ");
}
Console.WriteLine();


Console.WriteLine("v1 -> v2\t" + graph.DepthFirstSearch(v1, v2));
Console.WriteLine("v1 -> v4\t" + graph.DepthFirstSearch(v1, v4));
Console.WriteLine("v2 -> v5\t" + graph.DepthFirstSearch(v2, v5));
Console.WriteLine("v3 -> v1\t" + graph.DepthFirstSearch(v3, v1));
Console.WriteLine("v3 -> v4\t" + graph.DepthFirstSearch(v3, v4));
Console.WriteLine("v4 -> v2\t" + graph.DepthFirstSearch(v4, v2));
Console.WriteLine("v4 -> v3\t" + graph.DepthFirstSearch(v4, v3));
Console.WriteLine("v4 -> v4\t" + graph.DepthFirstSearch(v4, v4));
Console.WriteLine("v5 -> v2\t" + graph.DepthFirstSearch(v5, v2));
Console.WriteLine();
Console.WriteLine("v2 -> v3\t" + graph.DepthFirstSearch(v2, v3));
Console.WriteLine("v5 -> v4\t" + graph.DepthFirstSearch(v5, v4));
Console.WriteLine("v2 -> v4\t" + graph.DepthFirstSearch(v2, v4));
Console.WriteLine("v3 -> v5\t" + graph.DepthFirstSearch(v3, v5));
Console.WriteLine("v1 -> v5\t" + graph.DepthFirstSearch(v1, v5));


static void PrintGraph(Graph graph)
{
    int[,] matrix = graph.GetMatrix();

    Console.Write("Граф из 5 вершин:");
    for (int i = 0; i < 5; i++)
    {
        string line = "[";
        Console.WriteLine();

        for (int j = 0; j < 5; j++)
            line += $"{matrix[i, j]} ";

        line = line.Trim();
        line += "]";

        Console.Write(line);
    }
}

_ = Console.ReadLine();