using System.Net.Sockets;
using System.Security.AccessControl;

namespace _13
{
    public class Tests13
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Simple1()
        {
            var proc = new Processor("TestFiles/simple1.txt");
            Assert.That(proc.Result, Is.EqualTo(1));
        }

        [Test]
        public void Simple2()
        {
            var proc = new Processor("TestFiles/simple2.txt");
            Assert.That(proc.Result, Is.EqualTo(1));
        }

        [Test]
        public void Simple3()
        {
            var proc = new Processor("TestFiles/simple3.txt");
            Assert.That(proc.Result, Is.EqualTo(0));
        }

        [Test]
        public void Sample()
        {
            var proc = new Processor("TestFiles/sample.txt");
            Assert.That(proc.Result, Is.EqualTo(13));
            Assert.That(proc.MarkerResult, Is.EqualTo(140));
        }

        [Test]
        public void Puzzle()
        {
            var proc = new Processor("TestFiles/puzzle.txt");
            Assert.That(proc.Result, Is.EqualTo(5675));
            Assert.That(proc.MarkerResult, Is.EqualTo(20383));
        }
    }

}