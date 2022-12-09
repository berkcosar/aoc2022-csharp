
namespace Day9
{
    public class Q1 : BaseQuestion
    {
        public override object Run()
        {
            string[] commands = ReadAllLines();
            HashSet<(int x, int y)> tailVisited = new();

            (int x, int y) head = (0,0);
            (int x, int y) tail = (0,0);
            tailVisited.Add(tail);
            foreach(var motion in commands)
            {
                string[] parts = motion.Split(' ');
                string direction = parts[0];
                int steps = Convert.ToInt32(parts[1]);
                
                for(int i =0 ; i<steps;i++)
                {
                    (int x, int y) previousHead = head;
                    if(direction == "U")
                    {
                        head.y++;
                    }
                    else if(direction == "D")
                    {
                        head.y--;
                    }
                    else if(direction == "R")
                    {
                        head.x++;
                    }
                    else if(direction == "L")
                    {
                        head.x--;
                    }
                    tail = CalculateTailCoordinate(head,tail,previousHead);
                    if(!tailVisited.Contains(tail))
                    {
                        tailVisited.Add(tail);
                    }
                }
                
            }

            return tailVisited.Count;
        }

        private (int x, int y) CalculateTailCoordinate((int x, int y) head, (int x, int y) tail, (int x, int y) previousHead)
        {
            double distance = Math.Sqrt(Math.Pow(head.x-tail.x,2) + Math.Pow(head.y-tail.y,2));
            (int x, int y) returnVal = tail;
            if(distance == 2)//need to move horizontal or vertical
            {
                if(head.x == tail.x)
                {
                    int step = (head.y - tail.y)/2;
                    returnVal.y += step;
                }
                else
                {
                    int step = (head.x - tail.x)/2;
                    returnVal.x += step;
                }
            }
            else if(distance > 2)//need to move diagonal
            {
                returnVal = previousHead;
            }
            return returnVal;
        }

    }

}