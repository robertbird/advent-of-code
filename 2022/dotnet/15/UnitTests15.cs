namespace _15
{
    public class Tests15
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase(12, "Sensor at x=8, y=7: closest beacon is at x=2, y=10")]
        [TestCase(0, "Sensor at x=2, y=18: closest beacon is at x=-2, y=15")]
        public void TestSimpleLine(int expected, string line)
        {
            var lines = new List<string>() {line};
            var calc = new BeaconExclusionZoneCalc(lines);
            var result = calc.CalculateNotPresentForRow(10);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Sample()
        {
            var lines = Utilities.FileHelper.GetFileAsRows("TestFiles/sample.txt");
            var calc = new BeaconExclusionZoneCalc(lines);
            var result = calc.CalculateNotPresentForRow(10);
            Assert.That(result, Is.EqualTo(26));
        }

        [Test]
        public void PuzzlePart1()
        {
            var lines = Utilities.FileHelper.GetFileAsRows("TestFiles/puzzle.txt");
            var calc = new BeaconExclusionZoneCalc(lines);
            var result = calc.CalculateNotPresentForRow(2000000);
            Assert.That(result, Is.EqualTo(5335787));
        }

        [Test]
        public void PuzzlePart2()
        {
            var lines = Utilities.FileHelper.GetFileAsRows("TestFiles/puzzle.txt");
            var calc = new BeaconExclusionZoneCalc(lines);
            var result = calc.Calculate(4000000, 4000000);
            Assert.That(result, Is.EqualTo(13_673_971_349_056));
        }


    }
}