using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02
{
    public static class GameHelper
    {

        public static List<Game> PlayGamesFromStrategy(string input)
        {
            return input.Split("\r\n")
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.Split(" "))
                .Select(x => new Game(x[0], x[1]))
                .ToList();
        }

        public static int PartOneCalculateScore(string input)
        {
            var games = PlayGamesFromStrategy(input);

            int result = 0;
            foreach (var game in games)
            {
                result += game.GetScore();
            }
            return result;
        }

        public static int PartTwoCalculateScore(string input)
        {
            var games = input.Split("\r\n")
                .Where(x => !string.IsNullOrEmpty(x))
                .Select(x => x.Split(" "))
                .Select(x => new Game(Game.ParseMove(x[0]), Game.ParseOutcome(x[1])) )
                .ToList();

            int result = 0;
            foreach (var game in games)
            {
                result += game.GetScore();
            }
            return result;
        }
    }
}
