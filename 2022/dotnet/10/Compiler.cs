using System.Diagnostics;

namespace _10
{
    public class Cycle
    {
        public int CycleNum { get; set; }
        public int X { get; set; }
        public int SignalStrength { get; set; }
        public string Line { get; set; }
        public char Crt { get; set; }

    }

    public class Compiler
    {
        public List<Cycle> Cycles { get; private set; } = new List<Cycle>();
        public string CrtOutput { get; set; }

        private int CycleNum = 0;

        public Compiler(string relativePath)
        {
            Process(relativePath);
        }

        public int Calculate()
        {
            return
                Cycles[19].SignalStrength
                + Cycles[59].SignalStrength
                + Cycles[99].SignalStrength
                + Cycles[139].SignalStrength
                + Cycles[179].SignalStrength
                + Cycles[219].SignalStrength;
        }

        private void Process(string relativePath)
        {
            var lines = Utilities.FileHelper.GetFileAsRows(relativePath);
            lines.Add("end");
            lines.Add("end"); //  follow up on finishing any pending addx commands
            int x = 1;

            for (int i = 0; i < lines.Count; i++)
            {
                string? line = lines[i];

                if (line.StartsWith("addx"))
                {
                    var amount = int.Parse(line.Replace("addx", ""));

                    // It takes 2 cycles to process
                    RunCycle(x, line + " (1)", 0);
                    RunCycle(x, line + " (2)", amount);

                    // increment
                    x += amount;
                } 
                else
                {
                    RunCycle(x, line, 0);
                }
            }
            foreach(var c in Cycles)
            {
                //Debug.WriteLine($"Cycle {c.CycleNum}, x:{c.X}, strength: {c.SignalStrength}, line: {c.Line}");
            }
            Debug.WriteLine(CrtOutput);
        }

        private void RunCycle(int x, string line, int addx)
        {
            CycleNum++;
            Cycles.Add(new Cycle
            {
                X = x + addx,
                CycleNum = CycleNum,
                Line = line,
                SignalStrength = x * CycleNum
            });

            int col = (CycleNum-1) % 40;
            if (col == 0)
            {
                CrtOutput += Environment.NewLine;
            }
            CrtOutput += (col-x < -1 || col - x > 1) ? '.' : '#';
        }
    }
}