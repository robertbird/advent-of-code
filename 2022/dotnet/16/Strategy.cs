namespace _16
{
    public class Strategy
    {
        public Strategy() { }

        public Strategy(Strategy s)
        {
            StrategyList.AddRange(s.StrategyList);
            foreach(var n in s.StrategyList)
            {
                VisitedNodes.Add(n.Item3);
                if (n.Item2 > 0)
                    OpenedNodes.Add(n.Item3);
            }
        }

        public List<(int, int, string)> StrategyList { get; set; } = new List<(int, int, string)>();

        public HashSet<string> VisitedNodes { get; set; } = new HashSet<string>();
        public HashSet<string> OpenedNodes { get; set; } = new HashSet<string>();

        public void ValveChanged(int secondsRemaining, int rate, string node)
        {
            OpenedNodes.Add(node);
            VisitedNodes.Add(node);
            StrategyList.Add((secondsRemaining, rate, node));
        }
        internal void RecordVisit(int secondsRemaining, string node)
        {
            StrategyList.Add((secondsRemaining, 0, node));
            VisitedNodes.Add(node);
        }

        public int CalculateTotalPressure()
        {
            var score = 0;
            foreach(var sl in StrategyList)
            {
                score += (sl.Item1) * sl.Item2;
            }
            return score;
        }
        public override string ToString()
        {
            var str = "";
            var score = 0;
            foreach (var sl in StrategyList)
            {
                var cont = (sl.Item1) * sl.Item2;
                score += cont;
                str += $"s: {sl.Item1}, node: {sl.Item3}, rate: {sl.Item2}, contributes: {cont}, runningTotal: {score}{Environment.NewLine}";
            }
            return str;
        }

        internal bool HaveVisited(string currentNode)
        {
            return VisitedNodes.Contains(currentNode);
        }

        internal bool HaveOpened(string currentNode)
        {
            return OpenedNodes.Contains(currentNode);
        }

    }
}