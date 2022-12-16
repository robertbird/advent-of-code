namespace _14
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Sample()
        {
            var input = Parser.ParseInput("TestFiles/sample.txt");
            var cave = new Cave(input);
            Assert.That(cave.GrainCount, Is.EqualTo(24));
        }

        [Test]
        public void Puzzle()
        {
            var input = Parser.ParseInput("TestFiles/puzzle.txt");
            var cave = new Cave(input);
            Assert.That(cave.GrainCount, Is.EqualTo(892));
        }

        [Test]
        public void Sample_part2()
        {
            var input = Parser.ParseInput("TestFiles/sample.txt");
            var cave = new Cave(input, true);
            Assert.That(cave.GrainCount, Is.EqualTo(93));
        }

        [Test]
        public void Puzzle_part2()
        {
            var input = Parser.ParseInput("TestFiles/puzzle.txt");
            var cave = new Cave(input, true);
            Assert.That(cave.GrainCount, Is.EqualTo(27155));
        }
    }

    public class Parser
    {
        public static List<RockLine> ParseInput(string filePath)
        {
            var lines = Utilities.FileHelper.GetFileAsRows(filePath);
            var rockLines = new List<RockLine>();
            foreach (var line in lines)
            {
                var points = line.Split("->");
                var rockLine = new RockLine();
                foreach (var point in points)
                {
                    var parts = point.Split(",");
                    rockLine.AddPoint(int.Parse(parts[0].Trim()), int.Parse(parts[1].Trim()));
                }
                rockLines.Add(rockLine);
            }
            return rockLines;
        }
    }
}