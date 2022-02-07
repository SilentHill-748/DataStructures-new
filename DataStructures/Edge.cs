namespace DataStructures
{
    public class Edge
    {
        public Vertex From { get; set; }
        public Vertex To { get; set; }
        public double Weight { get; set; }


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

        public override int GetHashCode()
        {
            return HashCode.Combine(From, To, Weight);
        }
    }
}
