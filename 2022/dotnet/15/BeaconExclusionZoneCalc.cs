using System.Text.RegularExpressions;

namespace _15
{
    public class BeaconExclusionZoneCalc
    {
        public List<Sensor> Sensors { get; private set; } = new List<Sensor>();

        public BeaconExclusionZoneCalc(List<string> lines)
        {
            // parse lines into list of objects
            ParseLinesToSensors(lines);
        }

        private void ParseLinesToSensors(List<string> lines)
        {
            // example: Sensor at x=8, y=7: closest beacon is at x=2, y=10
            var regex = "Sensor at x=(-?\\d*), y=(-?\\d*): closest beacon is at x=(-?\\d*), y=(-?\\d*)";
            foreach (var line in lines)
            {
                MatchCollection coll = Regex.Matches(line, regex);
                var sX = int.Parse(coll[0].Groups[1].Value);
                var sY = int.Parse(coll[0].Groups[2].Value);
                var bX = int.Parse(coll[0].Groups[3].Value);
                var bY = int.Parse(coll[0].Groups[4].Value);

                Sensors.Add(new Sensor { Location = new Point(sX, sY), NearestBeacon = new Point(bX, bY) });
            }
        }

        public int CalculateNotPresentForRow(int r)
        {
            int countOfExclusions = CalculateExclusions(r).Count;
            int beacons = CountOfBeacons(r);

            return countOfExclusions - beacons;
        }

        private int CountOfBeacons(int row)
        {
            return Sensors
               .Where(s => s.NearestBeacon.Y == row)
               .Select(s => s.NearestBeacon)
               .Distinct()
               .Count();
        }

        private HashSet<int> CalculateExclusions(int row)
        {
            var excludedCells = new HashSet<int>();
           
            foreach (var sensor in Sensors)
            {
                var distance = Math.Abs(sensor.Location.X - sensor.NearestBeacon.X)
                    + Math.Abs(sensor.Location.Y - sensor.NearestBeacon.Y);

                var distanceOnRow = distance - Math.Abs(sensor.Location.Y - row);

                // if sensor is in range of this row
                if (distanceOnRow > 0)
                {
                    for(int i = sensor.Location.X - distanceOnRow; i <= sensor.Location.X + distanceOnRow; i++)
                    {
                        excludedCells.Add(i);
                    }
                }
            }
            return excludedCells;
        }

        public long Calculate(int rangeX, int rangeY)
        {
            // assume start 0 to rangeX/Y
            for(int row = 0; row<=rangeY; row++)
            {
                Sensors.Sort((s1, s2) => s1.MinXforY(row) < s2.MinXforY(row) ? -1 : 1);

                for (int x = 0; x <= rangeX; x++)
                {
                    bool xIsCovered = false;
                    foreach (var sensor in Sensors)
                    {
                        if (sensor.RangeCoversRow(row))
                        {
                            int maxX = sensor.MaxXforY(row);
                            if (x >= sensor.MinXforY(row) && x <= maxX) // we are covered by sensor
                            {
                                x = maxX; // skip forward to end of this sensors range
                                xIsCovered = true;
                            }
                        }
                    }
                    if(!xIsCovered)
                    {
                        return ((long)x * 4000000) + (long)row;
                    }
                }
            }

            return 0;
        }
    }
}