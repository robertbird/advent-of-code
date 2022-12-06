using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace _01
{
    public class Tests
    {
        private int PartOneTopElf(string fileInput)
        {
            var perElfSum = fileInput.Split("\r\n\r\n")
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.Split("\r\n"))
                .Select(x => new List<int>(x.Select(int.Parse)).Sum())
                .ToList();

            return perElfSum.Max();
        }

        private int PartTwoTopThreeElfs(string fileInput)
        {
            var perElfSum = fileInput.Split("\r\n\r\n")
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.Split("\r\n"))
                .Select(x => new List<int>(x.Select(int.Parse)).Sum())
                .ToList();

            return perElfSum.OrderByDescending(i => i).Take(3).Sum();
        }


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void File_should_load()
        {
            string fileInput = Utilities.FileHelper.GetFileContents("TestFiles/single-line.txt");
            Assert.AreEqual("1000", fileInput);
        }

        [Test]
        public void Simple_test_add_all()
        {
            var fileInput = Utilities.FileHelper.GetFileContents("TestFiles/simple-test.txt");

            var listOfStr = fileInput.Split("\r\n")
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();

            var sum = listOfStr.Select(int.Parse).Sum();

            Assert.AreEqual(7000, sum);
        }

        [Test]
        public void Simple_test_each_group()
        {
            var fileInput = Utilities.FileHelper.GetFileContents("TestFiles/simple-test.txt");

            var maxSum = PartOneTopElf(fileInput);

            Assert.AreEqual(6000, maxSum);
        }
    


        [Test]
        public void Given_example_part_one()
        {
            // 
            var fileInput = Utilities.FileHelper.GetFileContents("TestFiles/given-example.txt");

            var maxSum = PartOneTopElf(fileInput);

            Assert.AreEqual(24000, maxSum);
        }


        [Test]
        public void Puzzle_Input_part_one()
        {
            // 
            var fileInput = Utilities.FileHelper.GetFileContents("TestFiles/puzzle-input.txt");

            var maxSum = PartOneTopElf(fileInput);

            Debug.WriteLine($"MaxSum: {maxSum}");

            Assert.AreEqual(69693, maxSum);
        }

        [Test]
        public void Given_example_part_two()
        {
            // 
            var fileInput = Utilities.FileHelper.GetFileContents("TestFiles/given-example.txt");

            var maxSum = PartTwoTopThreeElfs(fileInput);

            Assert.AreEqual(45000, maxSum);
        }

        [Test]
        public void Puzzle_Input_part_two()
        {
            // 
            var fileInput = Utilities.FileHelper.GetFileContents("TestFiles/puzzle-input.txt");

            var maxSum = PartTwoTopThreeElfs(fileInput);

            Debug.WriteLine($"Top 3 sum: {maxSum}");

            Assert.AreEqual(200945, maxSum);
        }
    }
}