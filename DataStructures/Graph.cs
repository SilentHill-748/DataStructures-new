using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class Graph
    {
        private List<Vertex> _vertices;
        private List<Edge> _edges;


        public Graph()
        {
            _vertices = new List<Vertex>();
            _edges = new List<Edge>();
        }


        public void AddVertex(Vertex vertex)
        {
            if (_vertices.Contains(vertex))
                return;

            _vertices.Add(vertex);
        }

        public void AddVertexRange(IEnumerable<Vertex> vertices)
        {
            foreach (Vertex v in vertices)
                this.AddVertex(v);
        }

        public void AddEdge(Vertex from, Vertex to)
        {
            if (!_vertices.Contains(from)) _vertices.Add(from);

            if (!_vertices.Contains(to)) _vertices.Add(to);

            _edges.Add(new Edge(from, to));
        }

        public int[,] GetMatrix()
        {
            int[,] matrix = new int[_vertices.Count, _vertices.Count];
            
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

        // Рекурсивный метод обхода графа в ширину.
        public List<Vertex> BreadthFirstSearch(Vertex vertex, Vertex target)
        {
            List<Vertex> result = new();
            List<Vertex> visited = new();

            Queue<Vertex> queue = new();
            queue.Enqueue(vertex);

            while (queue.Count > 0)
            {
                Vertex temp = queue.Dequeue();

                foreach (Vertex neighbor in GetMergedVertices(temp))
                    if (!result.Contains(neighbor))
                    {
                        queue.Enqueue(neighbor);
                        visited.Add(neighbor);

                        if (neighbor.Equals(target))
                        {
                            result.Add(temp);
                            result.Add(neighbor);
                            return result;
                        }
                    }
            }

            return result;
        }

        // алгоритм обхода в глубину для поиска пути. Если путь есть, то True. Иначе False.
        public bool DepthFirstSearch(Vertex vertex, Vertex target)
        {
            List<Vertex> visited = new();
            return DepthFirstSearch(visited, vertex, target);
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
    }

    public class Vertex
    {
        public int Number { get; set; }


        public Vertex(int number)
        {
            Number = number;
        }


        public override string ToString()
        {
            return Number.ToString();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not null && obj is Vertex v)
                return this.Number.Equals(v.Number);

            return false;
        }
    }

    public class Edge
    {
        public Vertex From { get; set; }
        public Vertex To { get; set; }
        public bool IsOriented { get; set; }
        public int Weight { get; set; }


        public Edge(Vertex from, Vertex to, int weight = 1)
        {
            From = from;
            To = to;
            Weight = weight;
        }


        public override string ToString()
        {
            return $"{From} - {To}, Weight: {Weight}";
        }
    }
}
