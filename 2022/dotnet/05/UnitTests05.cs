using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace _05
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Sample()
        {
            var mgr = new StackManager("TestFiles/sample.txt");
            Console.WriteLine(mgr);
        }

        [Test]
        public void Puzzle()
        {
            var mgr = new StackManager("TestFiles/puzzle.txt");
            Console.WriteLine(mgr);
        }

        [Test]
        public void PuzzleStep2()
        {
            var mgr = new StackManager("TestFiles/puzzle.txt", true);
            Console.WriteLine(mgr);
        }
    }

    public class StackManager
    {
        public List<string> Instructions = new List<string>();
        public Stack<string> Setup = new Stack<string>();
        public List<Stack<string>> Stacks = new List<Stack<string>>();

        public StackManager(string filePath, bool step2 = false)
        {
            var lines = Utilities.FileHelper.GetFileAsRows(filePath);
            foreach(var line in lines)
            {
                if (line.StartsWith("m"))
                    Instructions.Add(line);
                else
                    Setup.Push(line);       
            }

            CreateStacks();
            RunInstructions(step2);
        }

        public void CreateStacks()
        {
            // initialise stacks
            var stacksStr = Setup.Pop();
            foreach(var c in stacksStr.ToCharArray())
            {
                if (Char.IsNumber(c))
                {
                    Stacks.Add(new Stack<string>());
                }
            }

            // setup initial items
            string line = string.Empty; 
            while (Setup.TryPop(out line))
            {
                char[] lineArr = line.ToCharArray();
                for (int i = 0; i < lineArr.Length; i++)
                {
                    char c = lineArr[i];
                    if (Char.IsUpper(c))
                    {
                        var stackIndex = ((i - 1) / 4);
                        //Console.WriteLine($"i:{i}, stack:{stackIndex}");
                        Stacks[stackIndex].Push(c.ToString());
                    }
                }
            }
        }

        public void RunInstructions(bool step2)
        {
            String regex = "move (\\d*) from (\\d*) to (\\d*)";

            foreach (var instruction in Instructions)
            {
                MatchCollection coll = Regex.Matches(instruction, regex);
                var move = int.Parse(coll[0].Groups[1].Value);
                var from = int.Parse(coll[0].Groups[2].Value);
                var to = int.Parse(coll[0].Groups[3].Value);

                if (step2)
                    ExecuteMoveStep2(from, to, move);
                else
                    ExecuteMoveStep1(from, to, move);
            }
        }

        private void ExecuteMoveStep1(int from, int to, int toMove)
        {
            for (int i = 0; i < toMove; i++)
            {
                Stacks[to - 1]
                    .Push(Stacks[from - 1].Pop());
            }
        }

        private void ExecuteMoveStep2(int from, int to, int toMove)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < toMove; i++)
            {
                list.Add(Stacks[from - 1].Pop());
            }
            list.Reverse();
            for (int i = 0; i < toMove; i++)
            {
                Stacks[to - 1].Push(list[i]);
            }
        }

        public override string ToString()
        {
            var str = string.Empty;
            int maxHeight = 0;
            // max heights
            for (int i = 0; i < Stacks.Count; i++)
            {
                maxHeight = (Stacks[i].Count > maxHeight) ? Stacks[i].Count : maxHeight;
            }
            // contents
            for (int h = maxHeight; h > 0; h--)
            {
                for (int c = 0; c < Stacks.Count; c++)
                {
                    if (Stacks[c].Count < h)
                        str += " ";
                    else
                        str += Stacks[c]
                            .ToArray()
                            .Reverse()
                            .ToList()[h-1].ToString();
                }
                str += Environment.NewLine;
            }
            for (int i = 0; i < Stacks.Count; i++)
            {
                str += i;
            }

            return str;
        }
    }
}