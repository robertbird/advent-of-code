namespace _14
{
    public class Cave
    {
        public int FloorExtension = 300;
        public int MinX = 0;
        public int MaxX = 0;
        public int MinY = 0;
        public int MaxY = 0;

        public List<RockLine> RockLines { get; private set; }

        public char[,] Points;

        public (int, int) SandStartingPoint = (500, 0);

        public int GrainCount = 0;

        public Cave(List<RockLine> rockLines, bool includeFloor = false)
        {
            RockLines = rockLines;
            MaxY = RockLines.Max(r => r.MaxY);
            MinX = RockLines.Min(r => r.MinX);
            MaxX = RockLines.Max(r => r.MaxX);

            if (includeFloor)
            {
                RockLines.Add(new RockLine()
                {
                    Points = new List<Point> {
                        new Point(MinX-FloorExtension, MaxY+2),
                        new Point(MinX+FloorExtension, MaxY+2)
                    }
                });
            }

            MinX = RockLines.Min(r => r.MinX);
            MaxX = RockLines.Max(r => r.MaxX);
            MinY = 0;
            MaxY = RockLines.Max(r => r.MaxY);
            Points = new char[(MaxX-MinX)+1, (MaxY-MinY)+1];
            

            for (int y = TranslateY(MinY); y <= TranslateY(MaxY); y++)
                for (int x = 0; x <= TranslateX(MaxX); x++)
                    Points[x, y] = '.';

            ProcessRockLines();

            Points[TranslateX(500), 0] = '+';

            Console.WriteLine(this.ToString());
            GrainCount = SimulateSand();
            // in part2 will have a last grain of sand over the start point
            if (includeFloor)
                GrainCount++;

            Console.WriteLine($"(FINAL)=================================================================");
            Console.WriteLine(this.ToString());
            Console.WriteLine("Grains: " + GrainCount);
        }

        private int SimulateSand()
        {
            int i = 1;
            while(AddSandGrain())// && i<19000)
            {
                //Console.WriteLine($"(r:{i})=================================================================");
                //Console.WriteLine(this.ToString());
                i++;
            }
            return i-1;
        }

        private bool AddSandGrain()
        {
            var restingPlace = NextSandGrainRestingPlace(SandStartingPoint.Item1, SandStartingPoint.Item2);
            if(restingPlace == (0, 0))
            {
                return false;
            }
            Points[TranslateX(restingPlace.Item1), TranslateY(restingPlace.Item2)] = 'o';
            return true;
        }

        private (int, int) NextSandGrainRestingPlace(int currentX, int currentY) 
        {
            // if at any point we are out of bounds, return (0,0)
            if(currentY+1 > MaxY
                || currentX < MinX
                || currentX > MaxX)
                return (0, 0);

            var newY = currentY+1;

            // check directly down, if available recurse on this position
            if (Points[TranslateX(currentX), TranslateY(newY)] != '#' 
                && Points[TranslateX(currentX), TranslateY(newY)] != 'o')
            {
                // keep going
                return NextSandGrainRestingPlace(currentX, newY);
            }

            // Check down to left
            else if(currentX <= MinX || 
                (currentX > MinX 
                && Points[TranslateX(currentX-1), TranslateY(newY)] != '#'
                && Points[TranslateX(currentX-1), TranslateY(newY)] != 'o'))
            {
                // keep going
                return NextSandGrainRestingPlace(currentX-1, newY);
            }

            // Check down to right
            else if (currentX >= MaxX ||
                (currentX < MaxX
                && Points[TranslateX(currentX + 1), TranslateY(newY)] != '#'
                && Points[TranslateX(currentX + 1), TranslateY(newY)] != 'o'))
            {
                // keep going
                return NextSandGrainRestingPlace(currentX+1, newY);
            }

            else
            {
                // resting place
                if(currentX == SandStartingPoint.Item1
                    && currentY == SandStartingPoint.Item2)
                {
                    // we haven't been able to move, so exit
                    return (0, 0);
                }
                return (currentX, currentY);
            }

            return (0, 0);
        }

        private void ProcessRockLines()
        {
            foreach (var rockLine in RockLines)
            {
                for (int p = 1; p < rockLine.Points.Count; p++)
                {
                    DrawLine(rockLine.Points[p - 1], rockLine.Points[p]);
                }
            }
        }

        private void DrawLine(Point p1, Point p2)
        {
            if (p1.X == p2.X && p1.Y <= p2.Y)
            {
                // vertical
                for (int y = p1.Y; y <= p2.Y; y++)
                {
                    Points[TranslateX(p1.X), TranslateY(y)] = '#';
                }
            }
            else if (p1.X == p2.X && p1.Y > p2.Y)
            {
                // vertical
                for (int y = p2.Y; y <= p1.Y; y++)
                {
                    Points[TranslateX(p1.X), TranslateY(y)] = '#';
                }
            }
            else if (p1.Y == p2.Y && p1.X <= p2.X)
            {
                // horizontal
                for (int x = p1.X; x <= p2.X; x++)
                {
                    Points[TranslateX(x), TranslateY(p1.Y)] = '#';
                }
            }
            else if (p1.Y == p2.Y && p1.X > p2.X)
            {
                // horizontal
                for (int x = p2.X; x <= p1.X; x++)
                {
                    Points[TranslateX(x), TranslateY(p1.Y)] = '#';
                }
            }
        }

        public int TranslateY(int y) => y - MinY;
        public int TranslateX(int x) => x - MinX;

        public override string ToString()
        {
            var str = "";
            for (int y = TranslateY(MinY); y <= TranslateY(MaxY); y++)
            {
                for (int x = 0; x <= TranslateX(MaxX); x++)
                {
                    str += $"{Points[x, y]}";
                }
                str += Environment.NewLine;
            }
            return str;
        }

    }
}