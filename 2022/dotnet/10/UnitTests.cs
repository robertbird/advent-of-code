using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace _10
{
    public class Tests
    {
        [OneTimeSetUp]
        public void StartTest()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
        }

        [OneTimeTearDown]
        public void EndTest()
        {
            Trace.Flush();
        }

        [Test]
        public void Simple_example_input()
        {
            var comp = new Compiler("TestFiles/simple.txt");
            Assert.That(comp.Cycles[0].X, Is.EqualTo(1));
            Assert.That(comp.Cycles[1].X, Is.EqualTo(1));
            Assert.That(comp.Cycles[2].X, Is.EqualTo(4));
            Assert.That(comp.Cycles[3].X, Is.EqualTo(4));
            Assert.That(comp.Cycles[4].X, Is.EqualTo(-1));
        }

        [Test]
        public void Example_given_input()
        {
            var comp = new Compiler("TestFiles/example-given.txt");
            Assert.That(comp.Cycles[19].X, Is.EqualTo(21));
            Assert.That(comp.Cycles[19].SignalStrength, Is.EqualTo(420));

            Assert.That(comp.Cycles[59].X, Is.EqualTo(19));
            Assert.That(comp.Cycles[59].SignalStrength, Is.EqualTo(1140));

            Assert.That(comp.Cycles[99].X, Is.EqualTo(18));
            Assert.That(comp.Cycles[99].SignalStrength, Is.EqualTo(1800));

            Assert.That(comp.Cycles[139].X, Is.EqualTo(21));
            Assert.That(comp.Cycles[139].SignalStrength, Is.EqualTo(2940));

            Assert.That(comp.Cycles[179].X, Is.EqualTo(16));
            Assert.That(comp.Cycles[179].SignalStrength, Is.EqualTo(2880));

            //Assert.That(comp.Cycles[219].X, Is.EqualTo(18));
            Assert.That(comp.Cycles[219].SignalStrength, Is.EqualTo(3960));

            Assert.That(comp.Calculate(), Is.EqualTo(13140));
        }

        [Test]
        public void Puzzle_input()
        {
            var comp = new Compiler("TestFiles/puzzle-input.txt");
            Assert.That(comp.Calculate(), Is.EqualTo(10760));
        }
    }

}