
namespace Day9
{
    public class Q2 : BaseQuestion
    {
        public override object Run()
        {
            string[] commands = ReadAllLines();
            HashSet<(int x, int y)> tailVisited = new();

            (int x, int y) head = (0,0);
            (int x, int y)[] rope = new (int x, int y)[10];
            for(int i = 0; i<10;i++)
            {
                rope[i] = (0,0);
            }
            tailVisited.Add(rope[rope.Length-1]);
            foreach(var motion in commands)
            {
                string[] parts = motion.Split(' ');
                string direction = parts[0];
                int steps = Convert.ToInt32(parts[1]);
                head = rope[0];
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
                    rope[0] = head;
                    CalculateRopeCoordinate(rope,previousHead);
                    if(!tailVisited.Contains(rope[rope.Length-1]))
                    {
                        tailVisited.Add(rope[rope.Length-1]);
                    }
                }
                
            }

            return tailVisited.Count;
        }
        private void CalculateRopeCoordinate((int x, int y)[] rope,  (int x, int y) previousHead)
        {
            bool movedDiagonal = false;
            for(int i = 1; i< rope.Length;i++)
            {
                var tempPrevious = rope[i];

                (rope[i],movedDiagonal) = CalculateTailCoordinate(rope[i-1], rope[i],previousHead,movedDiagonal);
                if(rope[i]==tempPrevious)//did not move so break iteration
                {
                    break;
                }
                previousHead = tempPrevious;
            }
        }

        private ((int x, int y),bool movedDiagonal) CalculateTailCoordinate((int x, int y) head, (int x, int y) tail, (int x, int y) previousHead,bool movedDiagonal)
        {
            double distance = Math.Sqrt(Math.Pow(head.x-tail.x,2) + Math.Pow(head.y-tail.y,2));
            ((int x, int y) tail,bool movedDiagonal) returnVal = (tail,false);
            if(distance == 2)//need to move horizontal or vertical
            {
                if(head.x == tail.x)
                {
                    int step = (head.y - tail.y)/2;
                    returnVal.tail.y += step;
                }
                else
                {
                    int step = (head.x - tail.x)/2;
                    returnVal.tail.x += step;
                }
            }
            else if(distance > 2)//need to move diagonal
            {
                if(movedDiagonal)
                {
                    (int x, int y) newPosition = tail;
                    newPosition.x += (head.x - previousHead.x);
                    newPosition.y += (head.y - previousHead.y);

                    returnVal = (newPosition,true);
                    
                }
                else
                {
                    returnVal = (previousHead,true);
                }

            }
            return returnVal;
        }

    }

}