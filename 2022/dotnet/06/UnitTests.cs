namespace _06
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
        [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
        [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 6)]
        [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
        [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
        public void GivenExamples(string input, int expectedResult)
        {
            var result = Decoder.FindMarker(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GivenPuzzleInput()
        {
            string input = Utilities.FileHelper.GetFileContents("TestFiles/puzzle-input.txt");
            var result = Decoder.FindMarker(input);
            Assert.That(result, Is.EqualTo(1640));
        }


        [TestCase("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
        [TestCase("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
        [TestCase("nppdvjthqldpwncqszvftbrmjlhg", 23)]
        [TestCase("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
        [TestCase("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
        public void GivenPart2Examples(string input, int expectedResult)
        {
            var result = Decoder.FindMessageStart(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GivenPuzzleInputPart2()
        {
            string input = Utilities.FileHelper.GetFileContents("TestFiles/puzzle-input.txt");
            var result = Decoder.FindMessageStart(input);
            Assert.That(result, Is.EqualTo(3613));
        }
    }

    public static class Decoder 
    {
        public static int FindMarker(string input)
        {
            for(int i = 3; i < input.Length; i++)
            {
                var count = new List<char> { input[i], input[i-1], input[i-2], input[i-3] }
                    .Distinct()
                    .ToList()
                    .Count();
                if(count == 4)
                    return i+1;
            }
            return 0;
        }

        internal static int FindMessageStart(string input)
        {
            for (int i = 13; i < input.Length; i++)
            {
                var count = new List<char> { 
                    input[i], 
                    input[i - 1], 
                    input[i - 2],
                    input[i - 3],
                    input[i - 4],
                    input[i - 5],
                    input[i - 6],
                    input[i - 7],
                    input[i - 8],
                    input[i - 9],
                    input[i - 10],
                    input[i - 11],
                    input[i - 12],
                    input[i - 13]
                }
                    .Distinct()
                    .ToList()
                    .Count();
                if (count == 14)
                    return i + 1;
            }
            return 0;
        }
    }
}