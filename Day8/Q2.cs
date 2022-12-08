
namespace Day8
{
    using Microsoft.Toolkit.HighPerformance;
    using Microsoft.Toolkit.HighPerformance.Enumerables;
    using System.Linq;
    using System;
    public class Q2 : BaseQuestion
    {
        public override object Run()
        {
            string[] commands = ReadAllLines();
            int[,] trees = new int[commands.Length, commands[0].Length];
            int maxScenicScore = Int32.MinValue;

            for (int i = 0; i < trees.GetLength(0); i++)
            {
                for (int j = 0; j < trees.GetLength(1); j++)
                {
                    trees[i, j] = commands[i][j] - '0';
                }
            }
            Span2D<int> spanTrees = trees;
            int rowLenToIterate = trees.GetLength(0);
            int columnLenToIterate = trees.GetLength(1) ;
            
            for (int row = 0; row < rowLenToIterate; row++)
            {
                for (int column = 0; column < columnLenToIterate; column++)
                {
                    var left = GetRow(trees,row,0,column);
                    left = left.Reverse();
                    var right = GetRow(trees,row,column+1,columnLenToIterate);
                    var up = GetColumn(trees,column,0,row);
                    up = up.Reverse();
                    var down = GetColumn(trees,column,row+1,rowLenToIterate);

                    int leftVisibleTreeCount= 0;
                    int rightVisibleTreeCount= 0;
                    int upVisibleTreeCount= 0;
                    int downVisibleTreeCount= 0;
                    int scenicScore = 0; 
                    foreach(var item in left)
                    {
                        leftVisibleTreeCount++;
                        if(item >= trees[row,column])
                        {
                            break;
                        }
                    }
                    foreach(var item in right)
                    {
                        rightVisibleTreeCount++;
                        if(item >= trees[row,column])
                        {
                            break;
                        }
                    }
                    foreach(var item in up)
                    {
                        upVisibleTreeCount++;
                        if(item >= trees[row,column])
                        {
                            break;
                        }
                    }
                     foreach(var item in down)
                    {
                        downVisibleTreeCount++;
                        if(item >= trees[row,column])
                        {
                            break;
                        }
                    }
                    scenicScore = leftVisibleTreeCount * rightVisibleTreeCount * upVisibleTreeCount * downVisibleTreeCount;
                    maxScenicScore = Math.Max(maxScenicScore, scenicScore);
                }
            }

            return maxScenicScore;
        }


        private IEnumerable<int> GetRow(int[,] matrix, int rowIndex, int colStartIndex, int colEndIndex)
        {
            for(int i= colStartIndex;i < colEndIndex; i++)
            {
                yield return matrix[rowIndex,i];
            }
        }

        private IEnumerable<int> GetColumn(int[,] matrix, int colIndex, int rowStartIndex, int rowEndIndex)
        {
            for(int i= rowStartIndex;i < rowEndIndex; i++)
            {
                yield return matrix[i,colIndex];
            }
        }

    }

}