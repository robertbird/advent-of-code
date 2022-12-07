using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02
{
    public enum GameMove
    {
        Rock,
        Paper,
        Scissors
    }

    public enum GameOutcome
    {
        Win,
        Lose,
        Draw
    }

    public class Game
    {
        public Game(string opening, string response): this(ParseMove(opening), ParseMove(response))
        { }

        public Game(GameMove opening, GameMove response)
        {
            Opening = opening;

            Response = response;

            Outcome = GetGameOutcome(Opening, Response);
        }

        public Game(GameMove opening, GameOutcome outcome)
        {
            Opening = opening;

            Response = GetGameResponse(opening, outcome);

            Outcome = outcome;
        }

        private GameMove GetGameResponse(GameMove opening, GameOutcome outcome)
        {
            switch ((opening, outcome))
            {
                case (GameMove.Rock, GameOutcome.Win): return GameMove.Paper;
                case (GameMove.Rock, GameOutcome.Draw): return GameMove.Rock;
                case (GameMove.Rock, GameOutcome.Lose): return GameMove.Scissors;
                case (GameMove.Paper, GameOutcome.Win): return GameMove.Scissors;
                case (GameMove.Paper, GameOutcome.Draw): return GameMove.Paper;
                case (GameMove.Paper, GameOutcome.Lose): return GameMove.Rock;
                case (GameMove.Scissors, GameOutcome.Win): return GameMove.Rock;
                case (GameMove.Scissors, GameOutcome.Draw): return GameMove.Scissors;
                case (GameMove.Scissors, GameOutcome.Lose): return GameMove.Paper;
            }
            return GameMove.Paper;
        }

        public static GameMove ParseMove(string input)
        {
            switch (input)
            {
                case "A": return GameMove.Rock;
                case "B": return GameMove.Paper;
                case "C": return GameMove.Scissors;
                case "X": return GameMove.Rock;
                case "Y": return GameMove.Paper;
                case "Z": return GameMove.Scissors;
                default: throw new ArgumentException("invalid game move string");
            }
        }

        public static GameOutcome ParseOutcome(string input)
        {
            switch (input)
            {
                case "X": return GameOutcome.Lose;
                case "Y": return GameOutcome.Draw;
                case "Z": return GameOutcome.Win;
                default: throw new ArgumentException("invalid game outcome string");
            }
        }


        public GameMove Opening { get; private set; }

        public GameMove Response { get; private set; }

        public GameOutcome Outcome { get; private set; }


        public static GameOutcome GetGameOutcome(GameMove opening, GameMove response)
        {
            switch((opening, response))
            {
                case (GameMove.Rock, GameMove.Rock): return GameOutcome.Draw;
                case (GameMove.Rock, GameMove.Paper): return GameOutcome.Win;
                case (GameMove.Rock, GameMove.Scissors): return GameOutcome.Lose;
                case (GameMove.Paper, GameMove.Rock): return GameOutcome.Lose;
                case (GameMove.Paper, GameMove.Paper): return GameOutcome.Draw;
                case (GameMove.Paper, GameMove.Scissors): return GameOutcome.Win;
                case (GameMove.Scissors, GameMove.Rock): return GameOutcome.Win;
                case (GameMove.Scissors, GameMove.Paper): return GameOutcome.Lose;
                case (GameMove.Scissors, GameMove.Scissors): return GameOutcome.Draw;
            }
            return GameOutcome.Lose;
        }

        public int GetMoveScore()
        {
            switch(Response)
            {
                case GameMove.Rock: return 1;
                case GameMove.Paper: return 2;
                case GameMove.Scissors: return 3;
                default: throw new InvalidOperationException("Unknown response");
            }
        }

        public int GetOutcomeScore()
        {
            switch (Outcome)
            {
                case GameOutcome.Win: return 6;
                case GameOutcome.Lose: return 0;
                case GameOutcome.Draw: return 3;
            }
            return 0;
        }

        public int GetScore()
        {
            return GetMoveScore() + GetOutcomeScore();
        }

    }
}
