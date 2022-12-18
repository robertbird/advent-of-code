using Newtonsoft.Json.Linq;
using NUnit.Framework;
using static System.Formats.Asn1.AsnWriter;

namespace _16
{
    public class Tests16
    {
        [SetUp]
        public void Setup()
        {
        }

        [Ignore("Too inefficient, see version 2")]
        public void Test16()
        {
            var map = new TunelMap("TestFiles/sample.txt");
            var result = map.FindRouteVersion1();
            Console.WriteLine($"Best Score: {result}");
            Assert.That(result, Is.EqualTo(1651));
        }

        [Test]
        public void Sample_Part1()
        {
            var map = new TunelMap("TestFiles/sample.txt");
            var result = map.FindRouteVersion2();
            Console.WriteLine($"Best Score: {result}");
            Assert.That(result, Is.EqualTo(1751));
        }

        [Test]
        public void Puzzle_Part1()
        {
            var map = new TunelMap("TestFiles/puzzle.txt");
            var result = map.FindRouteVersion2();
            Console.WriteLine($"Best Score: {result}");
            Assert.That(result, Is.EqualTo(1651));
        }

        [Test]
        public void Sample_Part2()
        {
            var map = new TunelMap("TestFiles/sample.txt");
            var result = map.FindRouteVersion2TwoPeople();
            Console.WriteLine($"Best Score: {result}");
            Assert.That(result, Is.EqualTo(1707));
        }


        [Test]
        public void Puzzle_Part2()
        {
            var map = new TunelMap("TestFiles/puzzle.txt");
            var result = map.FindRouteVersion2TwoPeople();
            Console.WriteLine($"Best Score: {result}");
            Assert.That(result, Is.EqualTo(1707));
        }

    }
}