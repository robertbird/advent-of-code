using System.Text.RegularExpressions;

namespace _16
{
    public class TunelMap
    {
        Dictionary<string,Node> AllNodes { get; set; } = new Dictionary<string, Node>();

        public int ValuesToOpen { get; private set; } = 0;

        public TunelMap(string filePath)
        {
            ParseInput(filePath);
            CalculateDistanceToNodes();
        }

        public int FindRouteVersion1()
        {
            var strategy = EvaluateNodeVersion1("AA", true, "", 23, new Strategy());
            Console.WriteLine(strategy.ToString());
            return strategy.CalculateTotalPressure();
        }

        public Strategy EvaluateNodeVersion1(string currentNode, bool openValve, string comingFrom, int secondsRemaining, Strategy strategy)
        {
            if (secondsRemaining <= 0 && strategy.OpenedNodes.Count == ValuesToOpen)
                return strategy;

            var node = AllNodes[currentNode];
            if(node.Rate > 0 && !strategy.HaveOpened(currentNode) && openValve)
            { 
                // spend time releasing valve and record the benefit this gives
                secondsRemaining--;
                strategy.ValveChanged(secondsRemaining, node.Rate, currentNode);
            }
            else
            {
                strategy.RecordVisit(secondsRemaining, currentNode);
            }

            if (secondsRemaining <= 0)
                return strategy;

            (int, Strategy) bestStrategy = (0, new Strategy());//(strategy.CalculateTotalPressure(), strategy);

            foreach (var tunnel in node.Tunnels)
            {
                if (tunnel == comingFrom && node.Tunnels.Count != 1)
                    continue;

                var nextNode = AllNodes[tunnel];

                // try tunnel and open valve when we get there
                var s = EvaluateNodeVersion1(tunnel, true, currentNode, secondsRemaining - 1, new Strategy(strategy));
                var score = s.CalculateTotalPressure();
                if (score >= bestStrategy.Item1)
                {
                    bestStrategy = (score, s);
                }

                // If tunnel has a rate, what happens if we skip over opening this tunnel
                if (!strategy.HaveOpened(tunnel))
                {
                    var sN = EvaluateNodeVersion1(tunnel, false, currentNode, secondsRemaining - 1, new Strategy(strategy));
                    var scoreN = sN.CalculateTotalPressure();
                    if (scoreN >= bestStrategy.Item1)
                    {
                        bestStrategy = (scoreN, sN);
                    }
                }
            }

            // we are at a dead-end
            // not sure this is right, because coming back is an option
            return bestStrategy.Item1 > strategy.CalculateTotalPressure() ?
                bestStrategy.Item2 : strategy;
        }

        public int FindRouteVersion2()
        {
            var valvesWithPressure = AllNodes.Values.Where(v => v.Rate > 0)
                .Select(v => (v.Name, v.Rate))
                .ToList();

            var result = EvaluateNodeVersion2("AA", 30, valvesWithPressure);
            //Console.WriteLine(strategy.ToString());
            return result;
        }

        private int EvaluateNodeVersion2(string currentNode, int secondsRemaining, List<(string Name, int Rate)> valvesWithPressure)
        {
            int highestScore = 0;
            var cur = AllNodes[currentNode];
            foreach (var valveToVisit in valvesWithPressure)
            {
                int newSecondsRemaining = secondsRemaining - cur.MinDistanceToNode[valveToVisit.Name] - 1;
                if (newSecondsRemaining > 0)
                {
                    int gain = 
                        newSecondsRemaining * valveToVisit.Rate
                        + EvaluateNodeVersion2(valveToVisit.Name, newSecondsRemaining, valvesWithPressure.Where(c => c.Name != valveToVisit.Name).ToList());
                    
                    if (highestScore < gain) 
                        highestScore = gain;
                }
            }
            return highestScore;
        }

        public int FindRouteVersion2TwoPeople()
        {
            var valvesWithPressure = AllNodes.Values.Where(v => v.Rate > 0)
                .Select(v => (v.Name, v.Rate))
                .ToList();

            var result = EvaluateNodeTwoPeople(new string[] { "AA", "AA" }, new int[] { 26, 26 }, valvesWithPressure);
            //Console.WriteLine(strategy.ToString());
            return result;
        }

        private int EvaluateNodeTwoPeople(string[] currentNode, int[] secondsRemaining, List<(string Name, int Rate)> valvesWithPressure)
        {
            int highestScore = 0;
            int personIndex = secondsRemaining[0] > secondsRemaining[1] ? 0 : 1;

            var cur = AllNodes[currentNode[personIndex]];
            foreach (var valveToVisit in valvesWithPressure)
            {
                int newSecondsRemaining = secondsRemaining[personIndex] - cur.MinDistanceToNode[valveToVisit.Name] - 1;
                if (newSecondsRemaining > 0)
                {
                    var newTimes = new int[] { newSecondsRemaining, secondsRemaining[1 - personIndex] };
                    var newLocs = new string[] { valveToVisit.Name, currentNode[1 - personIndex] };
                    int gain = 
                        newSecondsRemaining * valveToVisit.Rate 
                        + EvaluateNodeTwoPeople(newLocs, newTimes, valvesWithPressure.Where(c => c.Name != valveToVisit.Name).ToList());

                    if (highestScore < gain) 
                        highestScore = gain;
                }
            }
            return highestScore;
        }




        public void ParseInput(string filePath)
        {
            var lines = Utilities.FileHelper.GetFileAsRows(filePath);
            var regex = "Valve ([A-Z]{2}) has flow rate=(\\d*); tunnel[s]? lead[s]? to valve[s]? ([A-Z ,]*)";
            
            foreach (var line in lines)
            {
                MatchCollection coll = Regex.Matches(line, regex);
                var valve = coll[0].Groups[1].Value;
                var rate = coll[0].Groups[2].Value;
                var leadsTo = coll[0].Groups[3].Value;
                var leadsToNodes = leadsTo.Split(",");

                Console.WriteLine($"{valve} | {rate} | {leadsTo}");

                var node = new Node()
                {
                    Name = valve,
                    Rate = int.Parse(rate),
                    Tunnels = leadsToNodes.Select(s => s.Trim()).ToList()
                };
                AllNodes.Add(valve, node);
                if(node.Rate > 0)
                {
                    ValuesToOpen++;
                }
            }
        }

        public void CalculateDistanceToNodes()
        {
            // MinDistanceToNode

            foreach (var node in AllNodes.Values)
            {
                node.MinDistanceToNode[node.Name] = 0;
                DistanceToTarget(node, node.Name);
            }
        }

        private void DistanceToTarget(Node current, string target)
        {
            var visited = new HashSet<string>();

            while (current != null && visited.Count < AllNodes.Values.Count)
            {
                visited.Add(current.Name);
                int distance = current.MinDistanceToNode[target] + 1;
                foreach (var tunnel in current.Tunnels)
                {
                    if (!visited.Contains(tunnel))
                    {
                        var nextValve = AllNodes[tunnel];
                        if (nextValve.MinDistanceToNode.ContainsKey(target))
                        {
                            if (distance < nextValve.MinDistanceToNode[target])
                                nextValve.MinDistanceToNode[target] = distance;
                        }
                        else
                        {
                            nextValve.MinDistanceToNode[target] = distance;
                        }
                    }
                }
                current = AllNodes.Values
                    .Where(n => !visited.Contains(n.Name) && n.MinDistanceToNode.ContainsKey(target))
                    .OrderBy(c => c.MinDistanceToNode[target])
                    .FirstOrDefault();
            }
        }
    }
}