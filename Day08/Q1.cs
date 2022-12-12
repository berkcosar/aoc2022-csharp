
namespace Day08
{
    using Microsoft.Toolkit.HighPerformance;
    using Microsoft.Toolkit.HighPerformance.Enumerables;
    using System.Linq;
    public class Q1 : BaseQuestion
    {
        public override object Run()
        {
            string[] commands = ReadAllLines();
            int[,] trees = new int[commands.Length, commands[0].Length];

            for (int i = 0; i < trees.GetLength(0); i++)
            {
                for (int j = 0; j < trees.GetLength(1); j++)
                {
                    trees[i, j] = commands[i][j] - '0';
                }
            }
            int visibleTreeCount = (trees.GetLength(0) * 2 + trees.GetLength(1) * 2) - 4;
            ReadOnlySpan2D<int> spanTrees = trees;
            int rowLenToIterate = trees.GetLength(0) - 1;
            int columnLenToIterate = trees.GetLength(1) - 1;
            
            for (int row = 1; row < rowLenToIterate; row++)
            {
                for (int column = 1; column < columnLenToIterate; column++)
                {
                    var left = spanTrees[row..(row + 1), 0..column];
                    var right = spanTrees[row..(row + 1), (column + 1)..];
                    var up = spanTrees[0..row, column..(column + 1)];
                    var down = spanTrees[(row + 1).., column..(column + 1)];
                    
                    if (spanTrees[row, column] > GetMax(left))
                    {
                        visibleTreeCount++;
                    }
                    else if (spanTrees[row, column] > GetMax(right))
                    {
                        visibleTreeCount++;
                    }
                    else if (spanTrees[row, column] > GetMax(up))
                    {
                        visibleTreeCount++;
                    }
                    else if (spanTrees[row, column] > GetMax(down))
                    {
                        visibleTreeCount++;
                    }
                }
            }

            return visibleTreeCount;
        }

        public int GetMax(ReadOnlySpan2D<int> arr)
        {
            int max = Int32.MinValue;
            foreach (var item in arr)
            {
                max = Math.Max(item, max);
            }
            return max;
        }
    }

}