using System.ComponentModel;

namespace Day12
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
            var mgr = new TerrainManager("TestFiles/sample.txt");
            var steps = mgr.GetShortestPath();
            Console.WriteLine(mgr.ToString());
            Assert.That(steps, Is.EqualTo(31));
        }

        [Test]
        public void Puzzle()
        {
            var mgr = new TerrainManager("TestFiles/puzzle.txt");
            var steps = mgr.GetShortestPath();
            Console.WriteLine(mgr.ToString());
            Assert.That(steps, Is.EqualTo(1));
        }
    }

}

