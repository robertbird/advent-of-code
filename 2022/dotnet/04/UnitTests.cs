using System.Diagnostics;

namespace _04
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Example()
        {
            var calc = new Calc("TestFiles/example.txt");
        }

        [Test]
        public void Puzzle()
        {
            var calc = new Calc("TestFiles/puzzle.txt");
        }
    }

    public class Calc
    {
        List<string> Lines;

        public Calc(string filePath) 
        { 
            Lines = Utilities.FileHelper.GetFileAsRows(filePath);
            FindOverlapping();
        }
        public void FindOverlapping()
        {
            int fullyOverlapping = 0;
            int partialOverlapping = 0;
            foreach(var line in Lines)
            {
                HashSet<int> elf1 = new HashSet<int>();
                HashSet<int> elf2 = new HashSet<int>();
                int elf1Overlap = 0;
                int elf2Overlap = 0;

                var elves = line.Split(",");
                var elf1Parts = elves[0].Split("-");
                var elf2Parts = elves[1].Split("-");
                var elf1Start = int.Parse(elf1Parts[0]);
                var elf1End = int.Parse(elf1Parts[1]);
                var elf2Start = int.Parse(elf2Parts[0]);
                var elf2End = int.Parse(elf2Parts[1]);

                for (int i = elf1Start; i <= elf1End; i++)
                {
                    elf1.Add(i);
                }
                for (int i = elf2Start; i <= elf2End; i++)
                {
                    elf2.Add(i);
                    if (elf1.Add(i) == false)
                    {
                        elf2Overlap++;
                        //Console.WriteLine("overlap");
                    }
                }
                for (int i = elf1Start; i <= elf1End; i++)
                {
                    elf1.Add(i);
                    if (elf2.Add(i) == false)
                    {
                        elf1Overlap++;
                        //Console.WriteLine("overlap");
                    }
                }

                // fully overlapping
                if (elf2Overlap == (elf2End - elf2Start)+1
                    || elf1Overlap == (elf1End - elf1Start) + 1)
                    fullyOverlapping++;

                if (elf2Overlap > 0 || elf1Overlap > 0)
                    partialOverlapping++;

            }
            Console.WriteLine($"fully overlaps: {fullyOverlapping}, partial: {partialOverlapping}");
        }
    }
}