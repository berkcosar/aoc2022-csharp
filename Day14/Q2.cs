
namespace Day14
{
    public class Q2 : BaseQuestion
    {
        public override object Run()
        {
            string[] commands = ReadAllLines();

            HashSet<(int column, int row)> rockCoordinates = new();
            for (int i = 0; i < commands.Length; i++)
            {
                string[] lineCoordinatesString = commands[i].Split(" -> ");
                List<(int column, int row)> lineCoordinates = new();
                for (int j = 0; j < lineCoordinatesString.Length; j++)
                {
                    string[] lineCoordinateParts = lineCoordinatesString[j].Split(',');

                    int column = Convert.ToInt32(lineCoordinateParts[0]);
                    int row = Convert.ToInt32(lineCoordinateParts[1]);
                    lineCoordinates.Add((column, row));
                }
                for (int j = 1; j < lineCoordinates.Count; j++)
                {
                    if (lineCoordinates[j - 1].column == lineCoordinates[j].column)
                    {
                        int rowSmall = Math.Min(lineCoordinates[j].row, lineCoordinates[j - 1].row);
                        int rowBig = Math.Max(lineCoordinates[j].row, lineCoordinates[j - 1].row);
                        for (int rowIndex = rowSmall; rowIndex <= rowBig; rowIndex++)
                        {
                            rockCoordinates.Add((lineCoordinates[j].column, rowIndex));
                        }
                    }
                    else
                    {
                        int colSmall = Math.Min(lineCoordinates[j].column, lineCoordinates[j - 1].column);
                        int colBig = Math.Max(lineCoordinates[j].column, lineCoordinates[j - 1].column);
                        for (int colIndex = colSmall; colIndex <= colBig; colIndex++)
                        {
                            rockCoordinates.Add((colIndex,lineCoordinates[j].row));
                        }
                    }
                }
            }

            int maxColumn = rockCoordinates.Max(p => p.column);
            int minColumn = rockCoordinates.Min(p => p.column);

            int maxRow = rockCoordinates.Max(p => p.row);
            int minRow = rockCoordinates.Min(p => p.row);

            int maxRowLength = maxRow + 3;
            int maxColumnLength = maxRowLength *2 + (maxColumn - minColumn) + 1;
            int columnIndexOffset =  minColumn -maxRowLength - 1;
            (int column, int row) sandStart = (500 - columnIndexOffset, 0);

            char[,] cave = new char[maxRowLength, maxColumnLength];

            for (int row = 0; row < maxRowLength; row++)
            {
                for (int col = 0; col < maxColumnLength; col++)
                {
                    if (row == maxRowLength-1 || rockCoordinates.Contains((col + columnIndexOffset, row)))
                    {
                        cave[row, col] = '#';
                    }
                    else
                    {
                        cave[row, col] = '.';
                    }
                }
            }

            int sandCount = 0;
            (int column, int row) sand = sandStart;
            while(true)
            {
                (int column, int row) sandNew = FallNext(cave,sand);
                cave[sandNew.row,sandNew.column] = 'o';
                sandCount++;
                if(sandNew == sandStart)
                {
                    break;
                }
            }

            return sandCount;
        }

        private (int column, int row) FallNext(char[,] cave ,(int column, int row) sand)
        {
            (int column, int row) returnVal = sand;
            if(cave[returnVal.row+1,returnVal.column] == '.')
            {
                return FallNext(cave, (returnVal.column, returnVal.row+1));
            }
            else
            {
                if(returnVal.column > 0 && returnVal.row < cave.GetLength(0)-1 && cave[returnVal.row+1,returnVal.column -1] == '.')
                {
                    return FallNext(cave, (returnVal.column-1, returnVal.row+1));
                }
                else if(returnVal.column < cave.GetLength(1)-1 && returnVal.row < cave.GetLength(0)-1 && cave[returnVal.row+1,returnVal.column +1] == '.')
                {
                     return FallNext(cave, (returnVal.column+1, returnVal.row+1));
                }
                else
                {
                    return returnVal;
                }
            }
        }

        private void PrintCave(char[,] cave)
        {
            for (int row = 0; row < cave.GetLength(0); row++)
            {
                for (int col = 0; col < cave.GetLength(1); col++)
                {
                    Console.Write(cave[row,col]);
                }
                Console.WriteLine();
            }
        }

    }
}