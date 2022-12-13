
namespace Day12
{
    public class Q1 : BaseQuestion
    {
        public override object Run()
        {
            string[] commands = ReadAllLines();
            List<(int x, int y)> directions = new List<(int x, int y)> { (0, 1), (0, -1), (1, 0), (-1, 0) };
            HashSet<(int x, int y)> visited = new();
            (int x, int y) end = (0, 0);
            (int x, int y) start = (0, 0);
            int[,] elevationMap = new int[commands.Length, commands[0].Length];

            for (int i = 0; i < commands.Length; i++)
            {
                for (int j = 0; j < commands[i].Length; j++)
                {
                    int value = commands[i][j] - 'a' + 1;
                    if (commands[i][j] == 'S')
                    {
                        start = (i, j);
                        value = 0;
                    }
                    else if (commands[i][j] == 'E')
                    {
                        value = 'z' - 'a' + 1;
                        end = (i, j);
                    }
                    elevationMap[i, j] = value;
                }
            }
            visited.Add(start);
            Queue<(int x, int y)> queue = new();

            queue.Enqueue(start);
            queue.Enqueue((-1, -1));//breaker
            int step = 0;

            while (queue.Count > 1)
            {
                var curItem = queue.Dequeue();
                if (curItem.x == -1)
                {
                    step++;
                    queue.Enqueue(curItem);
                    continue;
                }
                if (curItem == end)
                {
                    return step;
                }
                foreach (var direct in directions)
                {
                    (int x, int y) newDirection = (curItem.x + direct.x, curItem.y + direct.y);
                    if (!visited.Contains(newDirection))
                    {
                        if (newDirection.x >= 0 && newDirection.y >= 0
                        && newDirection.x < elevationMap.GetLength(0)
                        && newDirection.y < elevationMap.GetLength(1))
                        {
                            if (elevationMap[newDirection.x, newDirection.y] <= elevationMap[curItem.x, curItem.y] + 1)
                            {
                                visited.Add(newDirection);
                                queue.Enqueue(newDirection);
                            }
                        }
                    }
                }
            }

            return -1;
        }

    }

}