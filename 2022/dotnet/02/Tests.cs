namespace _02
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void File_should_load()
        {
            string fileInput = Utilities.FileHelper.GetFileContents("TestFiles/line1.txt");
            Assert.That(fileInput, Is.EqualTo("A Y"));
        }

        [Test]
        public void Line1_Test()
        {
            string fileInput = Utilities.FileHelper.GetFileContents("TestFiles/line1.txt");

            var result = GameHelper.PlayGamesFromStrategy(fileInput);

            // We have one game (one line in file)
            Assert.That(result.Count, Is.EqualTo(1));

            // Check result and score matches expectation
            Assert.That(result[0].Outcome, Is.EqualTo(GameOutcome.Win));
            Assert.That(result[0].GetOutcomeScore(), Is.EqualTo(6));
            Assert.That(result[0].GetMoveScore(), Is.EqualTo(2));
            Assert.That(result[0].GetScore(), Is.EqualTo(8));
        }

        [Test]
        public void Line2_Test()
        {
            string fileInput = Utilities.FileHelper.GetFileContents("TestFiles/line2.txt");

            var result = GameHelper.PlayGamesFromStrategy(fileInput);

            // We have one game (one line in file)
            Assert.That(result.Count, Is.EqualTo(1));

            // Check result and score matches expectation
            Assert.That(result[0].Outcome, Is.EqualTo(GameOutcome.Lose));
            Assert.That(result[0].GetOutcomeScore(), Is.EqualTo(0));
            Assert.That(result[0].GetMoveScore(), Is.EqualTo(1));
            Assert.That(result[0].GetScore(), Is.EqualTo(1));
        }

        [Test]
        public void Line3_Test()
        {
            string fileInput = Utilities.FileHelper.GetFileContents("TestFiles/line3.txt");

            var result = GameHelper.PlayGamesFromStrategy(fileInput);

            // We have one game (one line in file)
            Assert.That(result.Count, Is.EqualTo(1));

            // Check result and score matches expectation
            Assert.That(result[0].Outcome, Is.EqualTo(GameOutcome.Draw));
            Assert.That(result[0].GetOutcomeScore(), Is.EqualTo(3));
            Assert.That(result[0].GetMoveScore(), Is.EqualTo(3));
            Assert.That(result[0].GetScore(), Is.EqualTo(6));
        }

        [TestCase(GameMove.Rock, GameMove.Rock, GameOutcome.Draw)]
        [TestCase(GameMove.Rock, GameMove.Paper, GameOutcome.Win)]
        [TestCase(GameMove.Rock, GameMove.Scissors, GameOutcome.Lose)]
        [TestCase(GameMove.Paper, GameMove.Rock, GameOutcome.Lose)]
        [TestCase(GameMove.Paper, GameMove.Paper, GameOutcome.Draw)]
        [TestCase(GameMove.Paper, GameMove.Scissors, GameOutcome.Win)]
        [TestCase(GameMove.Scissors, GameMove.Rock, GameOutcome.Win)]
        [TestCase(GameMove.Scissors, GameMove.Paper, GameOutcome.Lose)]
        [TestCase(GameMove.Scissors, GameMove.Scissors, GameOutcome.Draw)]
        public void Gameoutcomes_test(GameMove opening, GameMove response, GameOutcome outcome)
        {
            var game = new Game(opening, response);
            Assert.That(game.Outcome, Is.EqualTo(outcome));
        }


        [TestCase("D", "W")]
        public void Invalid_input(string opening, string response)
        {
            Assert.Throws<ArgumentException>(() => new Game(opening, response));
        }

        [Test]
        public void Part_one_example_strategy_score()
        {
            string fileInput = Utilities.FileHelper.GetFileContents("TestFiles/given-example.txt");
            var result = GameHelper.PartOneCalculateScore(fileInput);
            Assert.That(result, Is.EqualTo(15));
        }

        [Test]
        public void Part_one_strategy_score()
        {
            string fileInput = Utilities.FileHelper.GetFileContents("TestFiles/puzzle-input.txt");
            var result = GameHelper.PartOneCalculateScore(fileInput);
            Assert.That(result, Is.EqualTo(11449));
        }

        [TestCase(GameMove.Rock, GameOutcome.Draw, GameMove.Rock, 4)]
        [TestCase(GameMove.Paper, GameOutcome.Lose, GameMove.Rock, 1)]
        [TestCase(GameMove.Scissors, GameOutcome.Win, GameMove.Rock, 7)]
        public void Game_response_test(GameMove opening, GameOutcome outcome, GameMove response, int score)
        {
            var game = new Game(opening, outcome);
            Assert.That(game.Response, Is.EqualTo(response));
            Assert.That(game.GetScore(), Is.EqualTo(score));
        }

        [TestCase(GameMove.Rock, GameOutcome.Win, GameMove.Paper)]
        [TestCase(GameMove.Rock, GameOutcome.Draw, GameMove.Rock)]
        [TestCase(GameMove.Rock, GameOutcome.Lose, GameMove.Scissors)]
        [TestCase(GameMove.Paper, GameOutcome.Win, GameMove.Scissors)]
        [TestCase(GameMove.Paper, GameOutcome.Draw, GameMove.Paper)]
        [TestCase(GameMove.Paper, GameOutcome.Lose, GameMove.Rock)]
        [TestCase(GameMove.Scissors, GameOutcome.Win, GameMove.Rock)]
        [TestCase(GameMove.Scissors, GameOutcome.Draw, GameMove.Scissors)]
        [TestCase(GameMove.Scissors, GameOutcome.Lose, GameMove.Paper)]
        public void Game_response_test(GameMove opening, GameOutcome outcome, GameMove response)
        {
            var game = new Game(opening, outcome);
            Assert.That(game.Response, Is.EqualTo(response));
        }

        [Test]
        public void Part_two_example_strategy_score()
        {
            string fileInput = Utilities.FileHelper.GetFileContents("TestFiles/given-example.txt");
            var result = GameHelper.PartTwoCalculateScore(fileInput);
            Assert.That(result, Is.EqualTo(12));
        }

        [Test]
        public void Part_two_strategy_score()
        {
            string fileInput = Utilities.FileHelper.GetFileContents("TestFiles/puzzle-input.txt");
            var result = GameHelper.PartTwoCalculateScore(fileInput);
            Assert.That(result, Is.EqualTo(11449));
        }

    }
}