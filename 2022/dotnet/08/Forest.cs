namespace _08
{

    public struct Tree
    {
        public int Height;
        public bool Visible;
        public int ScenicScore;
    }


    public class Forest
    {
        public Tree[,] ForestFloor;
        public int Height { private set; get; }
        public int Width { private set; get; }

        public Forest(string filePath)
        {
            var lines = Utilities.FileHelper.GetFileAsRows(filePath);
            ForestFloor = ParseLines(lines);
            MarkVisibleTrees();
            CalculateScenicScoreForForest();
        }

        public void MarkVisibleTrees()
        {
            // scan from each direction
            // we know width and height, so for loops forward and backwards, current value found, if larger found then mark visible and update current largest

            Func<int, int, int, int> checkCurrentTreeIsVisible = (int w, int h, int tallestTreeSoFar)  =>
            {
                if (ForestFloor[w, h].Height > tallestTreeSoFar)
                {
                    ForestFloor[w, h].Visible = true;
                    tallestTreeSoFar = ForestFloor[w, h].Height;
                }
                return tallestTreeSoFar;
            };

            // top to bottom
            int tallestTree;
            for (int w = 0; w < Width; w++)
            {
                // new row, reset 
                tallestTree = -1;

                for (int h = 0; h < Height; h++)
                {
                    tallestTree = checkCurrentTreeIsVisible(w, h, tallestTree);
                }
            }

            // bottom to top
            // for(w 0, to max)
            // for(h max, to 0)
            for (int w = 0; w < Width; w++)
            {
                // new row, reset 
                tallestTree = -1;

                for (int h = Height-1; h >= 0; h--)
                {
                    tallestTree = checkCurrentTreeIsVisible(w, h, tallestTree);
                }
            }

            // left to right
            // for(h 0, to max)
            //   for(w 0, to max)
            for (int h = 0; h < Height; h++) 
            {
                // new row, reset 
                tallestTree = -1;

                for (int w = 0; w < Width; w++)
                {
                    tallestTree = checkCurrentTreeIsVisible(w, h, tallestTree);
                }
            }

            // right to left
            // for(h 0, to max)
            //   for(w max, to 0)
            for (int h = 0; h < Height; h++)
            {
                // new row, reset 
                tallestTree = -1;

                for (int w = Width - 1; w >= 0; w--)
                {
                    tallestTree = checkCurrentTreeIsVisible(w, h, tallestTree);
                }
            }

        }


        private Tree[,] ParseLines(List<string> lines)
        {
            // Create Array
            Height = lines.Count;
            if(lines.Count== 0)
            {
                throw new ArgumentException("File contains no data");
            }
            Width = lines[0].Length;
            var forestFloor = new Tree[Width, Height];

            //Populate Array
            for (int h = 0; h < lines.Count; h++)
            {
                string? line = lines[h];
                for (int w = 0; w < line.Length; w++)
                {
                    char c = line[w];
                    forestFloor[w, h] = new Tree {
                        Height = int.Parse(c.ToString())
                    };
                }
            }

            return forestFloor;
        }

        public int GetCountOfVisible()
        {
            int count = 0;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    count += ForestFloor[j, i].Visible ? 1 : 0;
                }
            }
            return count;
        }

        public int CalculateScenicScoreForForest()
        {
            int highestScore = 0;
            for (int w = 0; w < Width; w++)
            {
                for (int h = 0; h < Height; h++)
                {
                    ForestFloor[w, h].ScenicScore = CalculateScenicScoreForTree(w, h);
                    if(ForestFloor[w, h].ScenicScore > highestScore)
                    {
                        highestScore = ForestFloor[w, h].ScenicScore;
                    }
                }
            }
            return highestScore;
        }

        public int CalculateScenicScoreForTree(int wTree, int hTree)
        {
            // up
            int upScore = 0;
            for (int h = hTree-1; h >= 0; h--)
            {
                if(!CompareTrees(wTree, hTree, wTree, h, ref upScore))
                {
                    break;
                }
            }
            // down
            int downScore = 0;
            for (int h = hTree + 1; h < Height; h++)
            {
                if (!CompareTrees(wTree, hTree, wTree, h, ref downScore))
                {
                    break;
                }
            }

            // left
            int leftScore = 0;
            for (int w = wTree - 1; w >= 0; w--)
            {
                if (!CompareTrees(wTree, hTree, w, hTree, ref leftScore))
                {
                    break;
                }
            }

            // right
            int rightScore = 0;
            for (int w = wTree + 1; w < Width; w++)
            {
                if (!CompareTrees(wTree, hTree, w, hTree, ref rightScore))
                {
                    break;
                }
            }

            return upScore * downScore * leftScore * rightScore;
        }

        private bool CompareTrees(int wTree, int hTree, int wNextTree, int hNextTree, ref int score)
        {
            if (ForestFloor[wTree, hTree].Height > ForestFloor[wNextTree, hNextTree].Height)
            {
                score++;
                return true;
            }
            //else if (ForestFloor[wTree, hTree].Height == ForestFloor[wNextTree, hNextTree].Height)
            //{
            //    score++;
            //    return false;
            //}
            else
            {
                score++;
                return false;
            }
        }

        public override string ToString()
        {
            string str = string.Empty;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    str += string.Format($"({ForestFloor[j, i].Height}:{(ForestFloor[j, i].Visible ? "1" : "0")})");
                }
                str += Environment.NewLine + Environment.NewLine;
            }
            return str;
        }
    }


}