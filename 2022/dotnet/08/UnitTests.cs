using System.Data.SqlTypes;
using System.Diagnostics;

namespace _08
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
        public void Forest_should_initialise()
        {
            Forest f = new Forest("TestFiles/given-example.txt");
            Assert.That(f.ForestFloor.Length, Is.EqualTo(25));
            Assert.Pass();
        }

        [Test]
        public void Forest_should_initialise_with_puzzle_input()
        {
            Forest f = new Forest("TestFiles/puzzle-input.txt");
            Assert.Pass();
        }

        [Test]
        public void Forest_should_populate_height()
        {
            Forest f = new Forest("TestFiles/given-example.txt");
            Assert.That(f.ForestFloor.Length, Is.EqualTo(25));
            Debug.WriteLine(f.ToString());
            Assert.Pass();
        }

        [Test]
        public void Given_example_should_match_expected_output()
        {
            Forest f = new Forest("TestFiles/given-example.txt");
            Assert.That(f.ForestFloor.Length, Is.EqualTo(25));
            var output = f.ToString();
            Debug.WriteLine(output);
            var expected = Utilities.FileHelper.GetFileContents("TestFiles/given-example-expected-result.txt");
            Assert.That(output, Is.EqualTo(expected));
        }

        [Test]
        public void Given_example_should_match_expected_result()
        {
            Forest f = new Forest("TestFiles/given-example.txt");
            Assert.That(f.ForestFloor.Length, Is.EqualTo(25));
            Assert.That(f.GetCountOfVisible(), Is.EqualTo(21));
        }

        [Test]
        public void Puzzle_input_part1_result()
        {
            Forest f = new Forest("TestFiles/puzzle-input.txt");
            Assert.That(f.GetCountOfVisible(), Is.EqualTo(1681));
        }

        [Test]
        public void Part2_screnic_score_example1()
        {
            Forest f = new Forest("TestFiles/given-example.txt");
            // hopefully we have the right tree
            Assert.That(f.ForestFloor[2, 1].Height, Is.EqualTo(5));
            Assert.That(f.CalculateScenicScoreForTree(2,1), Is.EqualTo(4));
        }

        [Test]
        public void Part2_screnic_left_edge()
        {
            Forest f = new Forest("TestFiles/given-example.txt");
            // hopefully we have the right tree
            Assert.That(f.ForestFloor[0, 2].Height, Is.EqualTo(6));
            Assert.That(f.CalculateScenicScoreForTree(0, 2), Is.EqualTo(0));
        }
        [Test]
        public void Part2_screnic_right_edge()
        {
            Forest f = new Forest("TestFiles/given-example.txt");
            // hopefully we have the right tree
            Assert.That(f.ForestFloor[4, 3].Height, Is.EqualTo(9));
            Assert.That(f.CalculateScenicScoreForTree(4, 3), Is.EqualTo(0));
        }

        [Test]
        public void Part2_screnic_score_example2()
        {
            Forest f = new Forest("TestFiles/given-example.txt");
            // hopefully we have the right tree
            Assert.That(f.ForestFloor[2, 3].Height, Is.EqualTo(5));
            Assert.That(f.CalculateScenicScoreForTree(2, 3), Is.EqualTo(8));
        }

        [Test]
        public void Part2_screnic_score_result()
        {
            Forest f = new Forest("TestFiles/given-example.txt");
            // hopefully we have the right tree
            Assert.That(f.CalculateScenicScoreForForest(), Is.EqualTo(8));
        }

        [Test]
        public void Part2_screnic_score_result_for_puzzle_input()
        {
            Forest f = new Forest("TestFiles/puzzle-input.txt");
            // hopefully we have the right tree
            Assert.That(f.CalculateScenicScoreForForest(), Is.EqualTo(201684));
        }

    }


}