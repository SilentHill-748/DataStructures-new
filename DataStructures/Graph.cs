using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class Graph
    {
        private readonly List<Vertex> _vertices;
        private readonly List<Edge> _edges;

        private readonly Dictionary<Vertex, double> _costs;
        private readonly Dictionary<Vertex, Vertex?> _parents;
        private readonly List<Vertex> _visited;


        public Graph()
        {
            _vertices = new List<Vertex>();
            _edges = new List<Edge>();
            _costs = new Dictionary<Vertex, double>();
            _parents = new Dictionary<Vertex, Vertex?>();
            _visited = new List<Vertex>();
        }

        public Graph(int vertices) : this()
        {
            for (int i = 0; i < vertices; i++)
            {
                Vertex vertex = new(i + 1);

                _vertices.Add(vertex);

                _costs[vertex] = double.PositiveInfinity; // условная бесконечность.
            }
        }


        public List<Vertex> Vertices => _vertices;

        public List<Edge> Edges => _edges;


        public void AddEdge(int from, int to, int cost)
        {
            Vertex? fromVertex = _vertices.FirstOrDefault(x => x.Number == from);
            Vertex? toVertex = _vertices.FirstOrDefault(x => x.Number == to);

            if (fromVertex is null ||
                toVertex is null)
            {
                throw new ArgumentException("Someone vertex is not conteins in graph!");
            }

            Edge edge = new(fromVertex, toVertex, cost);
            _edges.Add(edge);
        }

        public double[,] GetMatrix()
        {
            double[,] matrix = new double[_vertices.Count, _vertices.Count];

            for (int i = 0; i < _edges.Count; i++)
            {
                var row = _edges[i].From.Number - 1;
                var column = _edges[i].To.Number - 1;

                matrix[row, column] = _edges[i].Weight;
            }

            return matrix;
        }

        public List<Vertex> GetMergedVertices(Vertex vertex)
        {
            var result = new List<Vertex>();

            foreach (var edge in _edges)
            {
                if (edge.From.Equals(vertex))
                {
                    result.Add(edge.To);
                }
            }

            return result;
        }

        public bool BFS(Vertex first, Vertex target)
        {
            return BFS(first, target, new Dictionary<int, Vertex>());
        }

        // алгоритм обхода в глубину для поиска пути. Если путь есть, то True. Иначе False.
        public bool DepthFirstSearch(Vertex vertex, Vertex target)
        {
            List<Vertex> visited = new();
            return DepthFirstSearch(visited, vertex, target);
        }

        public List<Vertex> DijkstrasAlgorithm(Vertex start, Vertex end)
        {
            _visited.Clear();
            InitNodesForDijkstrasAlgorithm(start);
         
            Vertex? node = GetMinVertex();
            
            while (node is not null)
            {
                double cost = _costs[node];

                foreach (Vertex neighbor in GetMergedVertices(node))
                {
                    Edge edgeToNeighbor = _edges.First(x => x.From.Equals(node) && x.To.Equals(neighbor));
                    double newCost = cost + edgeToNeighbor.Weight;
                    
                    if (newCost < _costs[neighbor])
                    {
                        _costs[neighbor] = newCost;
                        _parents[neighbor] = node;
                    }
                }
                _visited.Add(node);
                node = GetMinVertex();
            }
            
            return GetResult(end);
        }

        // Возвращает список весов - кратчайших путей из вершины start до других вершин.
        public List<double> BellmanFordAlgorithm()
        {
            ResetCosts();

            _costs[_vertices[0]] = 0;

            for (int i = 1; i < _vertices.Count - 1; i++)
                foreach (Edge edge in _edges)
                {
                    double newCost = _costs[edge.From] + edge.Weight;

                    if (_costs[edge.To] > newCost)
                        _costs[edge.To] = newCost;
                }

            return _costs.Select(x => x.Value).ToList();
        }

        public void PrintMatrix()
        {
            double[,] matrix = GetMatrix();
            int n = matrix.GetLength(0);

            for (int i = 0; i < n; i++)
            {
                string line = "[";
                for (int j = 0; j < n; j++)
                    line += $"{matrix[i, j]}".PadLeft(3);

                line = line.Trim() + "]";
                Console.WriteLine(line);
            }
        }

        private List<Vertex> GetResult(Vertex vertex)
        {
            List<Vertex> result = new();

            while (vertex is not null)
            {
                result.Add(vertex);
                vertex = _parents[vertex]!; // Nullable type is checked later
            }

            result.Reverse();
            return result;
        }

        private Vertex? GetMinVertex()
        {
            Vertex? minVertex = null;
            double minCost = double.PositiveInfinity;

            foreach (KeyValuePair<Vertex, double> pair in _costs)
            {
                Vertex v = pair.Key;

                if ((!_visited.Contains(v)) && pair.Value < minCost)
                {
                    minCost = pair.Value;
                    minVertex = v;
                }
            }

            return minVertex;
        }

        private void InitNodesForDijkstrasAlgorithm(Vertex start)
        {
            _costs[start] = 0;
            _parents[start] = null;

            foreach (Vertex vertex in Vertices)
            {
                if (vertex.Equals(start)) continue;

                Edge? fromStartEdge = _edges.FirstOrDefault(x => x.From.Equals(start) && x.To.Equals(vertex));
                if (fromStartEdge is null)
                {
                    _costs[vertex] = int.MaxValue;
                }
                else
                {
                    _costs[vertex] = fromStartEdge.Weight;
                    _parents[vertex] = start;
                }
            }
        }

        private bool DepthFirstSearch(List<Vertex> visited, Vertex vertex, Vertex target)
        {
            if (vertex.Equals(target)) return true;

            if (visited.Contains(vertex)) return false;

            visited.Add(vertex);

            foreach (Vertex neighbor in GetMergedVertices(vertex))
            {
                //Версия от хабра
                bool reached = DepthFirstSearch(visited, neighbor, target);

                if (reached) return true;
            }

            return false;
        }

        private bool BFS(Vertex first, Vertex target, Dictionary<int, Vertex> visited)
        {
            if (first.Equals(target)) return true;

            Queue<Vertex> queue = new();
            queue.Enqueue(first);
            visited.Add(first.Number, first);

            while(queue.Count > 0)
            {
                Vertex v = queue.Dequeue();

                foreach (Vertex neighbor in GetMergedVertices(v))
                {
                    if (!visited.ContainsKey(neighbor.Number))
                    {
                        if (neighbor.Equals(target)) return true;

                        queue.Enqueue(neighbor);
                        visited.Add(neighbor.Number, neighbor);
                    }
                }
            }

            return false;
        }

        private void ResetCosts()
        {
            for (int i = 0; i < _vertices.Count; i++)
            {
                _costs[_vertices[i]] = double.PositiveInfinity;
            }
        }
    }
}
