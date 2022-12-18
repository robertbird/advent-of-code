namespace _16
{
    public class Node
    {
        public bool Visited { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public List<string> Tunnels { get; set; }
        public Dictionary<string, int> MinDistanceToNode { get; set; } = new Dictionary<string, int>();
    }
}