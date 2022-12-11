namespace _03
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Split()
        {
            var mgr = new RucksackManager();
            var result = mgr.SplitRucksack("ABCDEF");
            Assert.That(result.Item1, Is.EqualTo("ABC"));
            Assert.That(result.Item2, Is.EqualTo("DEF"));
        }

        [Test]
        public void Intersect()
        {
            var mgr = new RucksackManager();
            var result = mgr.FindInteresction("ABC", "BCD");
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0], Is.EqualTo('B'));
            Assert.That(result[1], Is.EqualTo('C'));
        }

        [TestCase("a", 1)]
        [TestCase("z", 26)]
        [TestCase("A", 27)]
        [TestCase("Z", 52)]
        public void Score(string input, int expectedScore)
        {
            var mgr = new RucksackManager();
            var score = mgr.CalculateScore(input.ToCharArray().ToList());
            Assert.That(score, Is.EqualTo(expectedScore));
        }

        [Test]
        public void Sample()
        {
            var mgr = new RucksackManager("TestFiles/sample.txt");
            var result = mgr.CalculateSumOfPriorities();
            Assert.That(result, Is.EqualTo(157));
        }

        [Test]
        public void Puzzle_part1()
        {
            var mgr = new RucksackManager("TestFiles/puzzle.txt");
            var result = mgr.CalculateSumOfPriorities();
            Assert.That(result, Is.EqualTo(7428));
        }

        [Test]
        public void Sample_part2()
        {
            var mgr = new RucksackManager("TestFiles/sample.txt");
            var result = mgr.CalculateSumOfPrioritiesPart2();
            Assert.That(result, Is.EqualTo(70));
        }

        [Test]
        public void Puzzle_part2()
        {
            var mgr = new RucksackManager("TestFiles/puzzle.txt");
            var result = mgr.CalculateSumOfPrioritiesPart2();
            Assert.That(result, Is.EqualTo(2650));
        }
    }
}