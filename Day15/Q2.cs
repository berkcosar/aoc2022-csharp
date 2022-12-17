
namespace Day15
{


    public class Q2 : BaseQuestion
    {
        public override object Run()
        {
            string[] commands = ReadAllLines();
            List<Sensor> sensorList = new();
            int maxVal = 4000000;

            foreach (var line in commands)
            {
                sensorList.Add(new Sensor(line));
            }
            for (int row = 0; row < maxVal; row++)
            {
                List<Range> cannotExistPositionVectors = new();
                foreach (var sensor in sensorList)
                {
                    var cannotExistResults = sensor.CalculatePositionsForBeaconCannotExist(row, 0, maxVal);
                    if (cannotExistResults.found)
                    {
                        cannotExistPositionVectors.Add(cannotExistResults.range);
                    }
                }
                if (cannotExistPositionVectors.Count > 0)
                {
                    cannotExistPositionVectors = MergeVectors(cannotExistPositionVectors);
                    if (cannotExistPositionVectors.Count > 1)
                    {
                        int x = cannotExistPositionVectors[0].End.x + 1;
                        int y = cannotExistPositionVectors[0].End.y;
                        return x * (long)4000000 + y;
                    }
                    else
                    {
                        if (cannotExistPositionVectors[0].Start.x != 0)
                        {
                            int x = 0;
                            int y = cannotExistPositionVectors[0].End.y;
                            return x * (long)4000000 + y;
                        }
                        else if (cannotExistPositionVectors[0].End.x != maxVal)
                        {
                            int x = maxVal;
                            int y = cannotExistPositionVectors[0].End.y;
                            return x * (long)4000000 + y;
                        }
                    }
                }
            }
            return (long)-1;
        }


        public List<Range> MergeVectors(List<Range> listOfranges)
        {
            List<Range> returnList = new();
            listOfranges.Sort((p, q) =>
            {
                if (p.Start.x > q.Start.x)
                {
                    return 1;
                }
                else if (p.Start.x < q.Start.x)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            });
            returnList.Add(listOfranges[0]);

            for (int i = 1; i < listOfranges.Count; i++)
            {
                if (returnList[returnList.Count - 1].End.x + 1 >= listOfranges[i].Start.x)
                {
                    returnList[returnList.Count - 1].End.x = Math.Max(returnList[returnList.Count - 1].End.x, listOfranges[i].End.x);
                }
                else
                {
                    returnList.Add(listOfranges[i]);
                }
            }
            return returnList;

        }

        private void PrintMatrix(char[,] cave)
        {
            for (int row = 0; row < cave.GetLength(0); row++)
            {
                for (int col = 0; col < cave.GetLength(1); col++)
                {
                    Console.Write(cave[col, row]);
                }
                Console.WriteLine();
            }
        }

    }
}