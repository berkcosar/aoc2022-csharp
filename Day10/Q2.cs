
namespace Day10
{
    public class Q2 : BaseQuestion
    {
        public override object Run()
        {
            string[] commands = ReadAllLines();
            char[,] crt = new char[6,40];
            for(int i = 0; i< crt.GetLength(0);i++)
            {
                for(int j=0; j< crt.GetLength(1); j++)
                {
                    crt[i,j]= '.';
                }
            }
            int cycleCount = 0;
            int crtXIndex = 0;
            int crtYIndex = 0;
            int register = 1;
            int commandIndex =0;
            int passedCyleCountFromLastCommand = 0;
            string[] commandParts;
            string currentCommand = string.Empty;
            int currentVal = 0;

            while(true)
            {
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
                crtXIndex = cycleCount / 40;
                crtYIndex = cycleCount % 40;
                if(register - 1 <= crtYIndex && register +1 >= crtYIndex)
                {
                    crt[crtXIndex,crtYIndex]= '#';
                }
                passedCyleCountFromLastCommand++;
                cycleCount++;
            }
            for(int i = 0; i< crt.GetLength(0);i++)
            {
                for(int j=0; j< crt.GetLength(1); j++)
                {
                    Console.Write(crt[i,j]);
                }
                Console.WriteLine();
            }
            return "Printed on screen! Read #'s as capital letters!";
        }
    }

}