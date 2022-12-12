namespace _03
{
    public class RucksackManager
    {
        public List<int> Priorities { get; set; } = new List<int>();
        private List<string> Contents = new List<string>(); 

        public RucksackManager() { }

        public RucksackManager(string filePath)
        {
            Contents = Utilities.FileHelper.GetFileAsRows(filePath);
            Organise(Contents);
        }

        private void Organise(List<string> contents)
        {
            Priorities.Clear();
            foreach(var rucksack in contents)
            {
                var compartments = SplitRucksack(rucksack);
                var overlap = FindInteresction(compartments.Item1, compartments.Item2);
                Priorities.Add(CalculateScore(overlap));
            }
        }

        public int CalculateScore(List<char> overlap)
        {
            /* 
                Lowercase item types a through z have priorities 1 through 26.
                Uppercase item types A through Z have priorities 27 through 52.
             */
            int result = 0;
            foreach(char c in overlap)
            {
                var ascii = (int)c;
                if(Char.IsUpper(c))
                {
                    result += (ascii - 38);
                } 
                else if (Char.IsLower(c))
                {
                    result += (ascii - 96);
                } else
                {
                    throw new ArgumentException("Invalid character");
                }
            }
            return result;
        }

        public Tuple<string, string> SplitRucksack(string rucksack)
        {
            var firstHalf = rucksack.Substring(0, rucksack.Length / 2);
            var secondHalf = rucksack.Substring((rucksack.Length / 2), rucksack.Length / 2);

            return Tuple.Create<string, string>(firstHalf, secondHalf);
        }

        public List<char> FindInteresction(string str1, string str2)
        {
            return str1.ToCharArray().Intersect(str2.ToCharArray()).ToList();
        }
        public List<char> FindInteresction2(string str1, string str2, string str3)
        {
            return str1.ToCharArray()
                .Intersect(str2.ToCharArray())
                .Intersect(str3.ToCharArray())
                .ToList();
        }

        internal object CalculateSumOfPriorities()
        {
            return Priorities.Sum();
        }

        public int CalculateSumOfPrioritiesPart2()
        {
            int result = 0;
            for (int i = 0; i < Contents.Count; i++)
            {
                if((i+1) % 3 == 0)
                {
                    var intersection = FindInteresction2(Contents[i], Contents[i - 1], Contents[i - 2]);
                    result += CalculateScore(intersection);
                }
            }
            return result;
        }
    }
}