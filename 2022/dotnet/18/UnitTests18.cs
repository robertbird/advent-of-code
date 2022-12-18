using System.Linq;

namespace _18
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SimpleExample_single()
        {
            var cave = new Cave(new List<(int, int, int)>() {
                new (1,2,3)
            });
            var result = cave.CalculateSurface();
            Assert.That(result, Is.EqualTo(6));
        }

        [Test]
        public void SimpleExample_two_joined()
        {
            var cave = new Cave(new List<(int, int, int)>() {
                new (1,1,1),
                new (2,1,1)
            });
            var result = cave.CalculateSurface();
            Assert.That(result, Is.EqualTo(10));
        }

        [Test]
        public void Sample_part1()
        {
            var cave = Cave.BuildCave("TestFiles/sample.txt");
            var result = cave.CalculateSurface();
            Assert.That(result, Is.EqualTo(64));
        }

        [Test]
        public void Puzzle_part1()
        {
            var cave = Cave.BuildCave("TestFiles/puzzle.txt");
            var result = cave.CalculateSurface();
            Assert.That(result, Is.EqualTo(4314));
        }

        [Test]
        public void Sample_part2()
        {
            var cave = Cave.BuildCave("TestFiles/sample.txt");
            var surface = cave.CalculateSurface();
            var airPockets = cave.CalculateAirPockets();
            Assert.That(airPockets, Is.EqualTo(1));
            Assert.That(surface - (6 * airPockets), Is.EqualTo(58));
        }

        [Test]
        public void Puzzle_part2()
        {
            var cave = Cave.BuildCave("TestFiles/puzzle.txt");
            var surface = cave.CalculateSurface();
            var airPockets = cave.CalculateAirPockets();
            Assert.That(surface - (6 * airPockets), Is.EqualTo(58));
        }
    }

    public class Cave
    {
        HashSet<(int, int, int)> Lava { get; set; } = new HashSet<(int, int, int)>();

        int MaxX, MaxY, MaxZ;

        private (int, int, int)[] Offsets = new (int, int, int)[6]
        {
            new (1, 0, 0),
            new (-1, 0, 0),
            new (0, 1, 0),
            new (0, -1, 0),
            new (0, 0, 1),
            new (0, 0, -1)
        };

        public static Cave BuildCave(string filename)
        {
            var result = new List<(int, int, int)>();
            var lines = Utilities.FileHelper.GetFileAsRows(filename);
            foreach (var line in lines)
            {
                var pieces = line.Split(',');
                var x = int.Parse(pieces[0]);
                var y = int.Parse(pieces[1]);
                var z = int.Parse(pieces[2]);
                result.Add((x, y, z));
            }
            return new Cave(result);
        }

        public Cave(List<(int, int, int)> input)
        {
            foreach(var item in input)
            {
                var added = Lava.Add(item);
                if (!added)
                    throw new Exception($"Duplicate particle found {item.Item1},{item.Item2},{item.Item3}");

                // record min/max x,y,y
                if (item.Item1 > MaxX) MaxX = item.Item1;
                if (item.Item2 > MaxY) MaxY = item.Item2;
                if (item.Item3 > MaxZ) MaxZ = item.Item3;
            }
        }
    
        public int CalculateSurface()
        {
            var total = 0;
            foreach(var item in Lava)
            {
                foreach(var offset in Offsets)
                {
                    if(!Lava.Contains( (item.Item1 - offset.Item1, item.Item2 - offset.Item2, item.Item3 - offset.Item3) ))
                    {
                        total++;
                    }
                }
            }
            return total;
        }

        public int CalculateAirPockets()
        {
            int total = 0;
            // simple loop through the whole space and count where there is lava all around
            // hmm this does not work for air pockets larger than 1x1x1 :( 
            for (int x = 0; x <= MaxX; x++)
            {
                for (int y = 0; y <= MaxY; y++)
                {
                    for (int z = 0; z <= MaxZ; z++)
                    {
                        // if we are air, not lava
                        if (!Lava.Contains((x, y, z)))
                        {
                            int lavaAround = 0;
                            // check we are surrounded by lava
                            foreach (var offset in Offsets)
                            {
                                if (Lava.Contains((x - offset.Item1, y - offset.Item2, z - offset.Item3)))
                                {
                                    lavaAround++;
                                }
                            }
                            if(lavaAround == 6)
                                total++;
                        }
                    }
                }
            }
            return total;
        }
    }

}