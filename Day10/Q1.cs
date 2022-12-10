
namespace Day10
{
    public class Q1 : BaseQuestion
    {
        public override object Run()
        {
            string[] commands = ReadAllLines();
            int cycleCount = 0;
            int register = 1;
            int commandIndex =0;
            int passedCyleCountFromLastCommand = 0;
            string[] commandParts;
            string currentCommand = string.Empty;
            int currentVal = 0;
            long sumOfSignals = 0;

            while(true)
            {
                if((cycleCount-20)% 40 == 0)
                {
                    sumOfSignals += (register * cycleCount);
                }

                if(currentCommand == "addx" && passedCyleCountFromLastCommand == 2)
                {
                    register += currentVal;
                    passedCyleCountFromLastCommand = 0;
                }
                else if(currentCommand == "noop" && passedCyleCountFromLastCommand == 1)
                {
                    passedCyleCountFromLastCommand = 0;
                }
                
                if(passedCyleCountFromLastCommand == 0)
                {
                    if(commandIndex == commands.Length)
                    {
                        break;
                    }
                    commandParts = commands[commandIndex].Split(' ');
                    currentCommand = commandParts[0];
                    if(currentCommand == "addx")
                    {
                        currentVal = Convert.ToInt32(commandParts[1]);
                    }
                    commandIndex++;
                }
                passedCyleCountFromLastCommand++;
                cycleCount++;
            }

            return sumOfSignals;
        }
    }

}