namespace Day12
{
    public struct Cell
    {
        public int Height;
        public bool Visited;
        public int X;
        public int Y;
    }

    public class TerrainManager
    {
        private int Width, Height;
        public Cell[,] Terrian;
        public Cell Start;
        public Cell End;

        public TerrainManager(string filePath)
        {
            var lines = Utilities.FileHelper.GetFileAsRows(filePath);
            Height = lines.Count;
            Width = lines[0].Length;
            Terrian = new Cell[Height, Width];
            InitializeMap(lines);
        }

        private void InitializeMap(List<string> lines)
        {
            for (int h = 0; h < Height; h++)
            {
                string line = lines[h];
                for (int w = 0; w < Width; w++)
                {
                    char c = line[w];
                    if (c == 'S')
                    {
                        Terrian[h, w] = new Cell { Height = 1, X = w, Y = h };
                        Start = Terrian[h, w];
                    }
                    else if (c == 'E')
                    {
                        Terrian[h, w] = new Cell { Height = 27, X = w, Y = h };
                        End = Terrian[h, w];
                    }
                    else
                    {
                        Terrian[h, w] = new Cell { Height = ((int)c) - 96, X = w, Y = h }; // a = 1
                    }
                }
            }
        }

        public int GetShortestPath()
        {
            var result = PlotAllPaths(Start, new List<Cell>());
            return result.Item2.Count;
        }
            
        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentCell"></param>
        /// <param name="journeySoFar"></param>
        /// <returns>successful?, shortest journey route</returns>
        public Tuple<bool, List<Cell>> PlotAllPaths(Cell currentCell, List<Cell> journeySoFar) 
        {
            // recursive calls
            Console.WriteLine($"current x:{currentCell.X} y:{currentCell.Y} h:{currentCell.Height} visited: {journeySoFar.Count}");
            Cell nextCell;
            var nextList = new List<Cell>(journeySoFar);
            nextList.Add(currentCell);
            List<Tuple<bool, List<Cell>>> options = new List<Tuple<bool, List<Cell>>>();
            var directions = new List<int[]>
            {
                new int[2] { -1, 0 }, // up
                new int[2] { 0, 1 }, // right
                new int[2] { 1, 0 }, // down
                new int[2] { 0, -1 }  // left
            };

            // if(end point in reach, we are 27 and next to us is 27) return
            if (CanReachTop(currentCell, out nextCell, journeySoFar))
            {
                return Tuple.Create(true, nextList);
            }

            foreach(var direction in directions) 
            {
                var result = CanTravel(direction[0], direction[1], currentCell, out nextCell, journeySoFar);
                if(result == -1)
                {
                    return Tuple.Create(false, nextList);
                }
                if(result >= currentCell.Height)
                {
                    options.Add(PlotAllPaths(nextCell, nextList));
                }
            }

            // work out available options up, down, left right. (not in list, within 1 step up)
            //if (CanTravelUp(currentCell, out nextCell, journeySoFar) >= currentCell.Height)
            //{
            //    options.Add(PlotAllPaths(nextCell, nextList));
            //}
            //if (CanTravelDown(currentCell, out nextCell, journeySoFar) >= currentCell.Height)
            //{
            //    options.Add(PlotAllPaths(nextCell, nextList));
            //}
            //if (CanTravelLeft(currentCell, out nextCell, journeySoFar) >= currentCell.Height)
            //{
            //    options.Add(PlotAllPaths(nextCell, nextList));
            //}
            //if (CanTravelRight(currentCell, out nextCell, journeySoFar) >= currentCell.Height)
            //{
            //    options.Add(PlotAllPaths(nextCell, nextList));
            //}

            //   return shortest successful route available// only consider successful journeys
            var successfulPaths = options.Where(x => x.Item1);
            if(!successfulPaths.Any())
            {
                Console.WriteLine($"##################### END OF PATH x:{currentCell.X} y:{currentCell.Y} visited: {journeySoFar.Count}");
                return Tuple.Create(false, nextList);
            }
            var minPath = successfulPaths.Min(x => x.Item2.Count);
            return successfulPaths.Where(x => x.Item2.Count == minPath).FirstOrDefault();
        }

        private int CanTravel(int y, int x, Cell currentCell, out Cell nextCell, List<Cell> journeySoFar)
        {
            nextCell = new Cell { X = currentCell.X + x, Y = currentCell.Y + y };
            if (nextCell.Y < Height - 1
                && nextCell.Y >= 0
                && nextCell.X < Width - 1
                && nextCell.X >= 0
                && nextCell.Height <= currentCell.Height + 1)
            {
                nextCell = Terrian[currentCell.Y + y, currentCell.X + x];
                var found = journeySoFar.IndexOf(nextCell);
                if (found >= 0)
                {
                    // we found it, if recent return 0, if way ago return -1 to exit the whole path
                    if (found < (journeySoFar.Count - 3))
                        return -1;
                    return 0;
                }
                return nextCell.Height;
            }
            return 0;
        }

        private int CanTravelUp(Cell currentCell, out Cell nextCell, List<Cell> journeySoFar)
        {
            nextCell = new Cell();
            if(currentCell.Y > 0 
                && Terrian[currentCell.Y-1, currentCell.X].Height <= currentCell.Height+1)
            {
                if (journeySoFar.Contains(Terrian[currentCell.Y - 1, currentCell.X])) return 0;
                nextCell = Terrian[currentCell.Y - 1, currentCell.X];
                return Terrian[currentCell.Y - 1, currentCell.X].Height;
            }
            return 0;
        }
        private int CanTravelDown(Cell currentCell, out Cell nextCell, List<Cell> journeySoFar)
        {
            nextCell = Terrian[currentCell.Y + 1, currentCell.X];
            if (currentCell.Y < Height-1
                && nextCell.Height <= currentCell.Height + 1)
            {
                if (journeySoFar.Contains(nextCell)) return 0;
                return nextCell.Height;
            }
            return 0;
        }
        private int CanTravelLeft(Cell currentCell, out Cell nextCell, List<Cell> journeySoFar)
        {
            nextCell = new Cell();
            if (currentCell.X > 0
                && Terrian[currentCell.Y, currentCell.X-1].Height <= currentCell.Height + 1)
            {
                if (journeySoFar.Contains(Terrian[currentCell.Y, currentCell.X - 1])) return 0;
                nextCell = Terrian[currentCell.Y, currentCell.X-1];
                return Terrian[currentCell.Y, currentCell.X-1].Height;
            }
            return 0;
        }
        private int CanTravelRight(Cell currentCell, out Cell nextCell, List<Cell> journeySoFar)
        {
            nextCell = new Cell();
            if (currentCell.X + 1 < Width
                && Terrian[currentCell.Y, currentCell.X + 1].Height <= currentCell.Height + 1)
            {
                if (journeySoFar.Contains(Terrian[currentCell.Y, currentCell.X + 1])) return 0;
                nextCell = Terrian[currentCell.Y, currentCell.X + 1];
                return Terrian[currentCell.Y, currentCell.X + 1].Height;
            }
            return 0;
        }
        private bool CanReachTop(Cell currentCell, out Cell nextCell, List<Cell> journeySoFar)
        {
            nextCell = new Cell();
            if (currentCell.Height != 26)
            {
                return false;
            }
            if (CanTravelUp(currentCell, out nextCell, journeySoFar) == 27)
                return true;
            if (CanTravelDown(currentCell, out nextCell, journeySoFar) == 27)
                return true;
            if (CanTravelLeft(currentCell, out nextCell, journeySoFar) == 27)
                return true;
            if (CanTravelRight(currentCell, out nextCell, journeySoFar) == 27)
                return true;

            return false;
        }

        public override string ToString()
        {
            var str = string.Empty;
            for (int h = 0; h < Height; h++)
            {
                for (int w = 0; w < Width; w++)
                {
                    str += $"{Terrian[h, w].Height.ToString().PadLeft(3)}";
                }
                str += Environment.NewLine;
            }
            return str;
        }

    }
}

