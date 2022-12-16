namespace _14
{
    public class RockLine
    {
        public List<Point> Points { get; set; } = new List<Point>();

        public int MinX
        {
            get
            {
                return Points.Min(p => p.X);
            }
        }
        public int MaxX
        {
            get
            {
                return Points.Max(p => p.X);
            }
        }
        public int MinY
        {
            get
            {
                return Points.Min(p => p.Y);
            }
        }
        public int MaxY
        {
            get
            {
                return Points.Max(p => p.Y);
            }
        }

        internal void AddPoint(int x, int y)
        {
            Points.Add(new Point(x, y));
        }
    }
}