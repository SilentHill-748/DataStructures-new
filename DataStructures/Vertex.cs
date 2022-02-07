namespace DataStructures
{
    public class Vertex
    {
        public int Number { get; }


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
        public override int GetHashCode()
        {
            return HashCode.Combine(Number);
        }
    }
}
