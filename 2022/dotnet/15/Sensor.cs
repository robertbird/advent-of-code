namespace _15
{
    public class Sensor
    {
        public Point Location { get; set; }
        public Point NearestBeacon { get; set; }

        public int DistanceOnRow(int row)
        {
            var distance = Math.Abs(Location.X - NearestBeacon.X)
                + Math.Abs(Location.Y - NearestBeacon.Y);

            return distance - Math.Abs(Location.Y - row);
        }

        public bool RangeCoversRow(int row)
        {
            return DistanceOnRow(row) > 0;
        }

        public int MaxXforY(int row)
        {
            return Location.X + DistanceOnRow(row);
        }

        public int MinXforY(int row)
        {
            return Location.X - DistanceOnRow(row);
        }
    }
}