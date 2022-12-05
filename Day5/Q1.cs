
namespace Day5
{
    using System.Text;
    public class Q1:BaseQuestion
    {
        public override object Run()
        {
            string[] lines = ReadAllLines();

            int emptyLineIndex = GetEmptyLineIndex(lines);
            int numOfStacks = ParseTotalStackCount(lines[emptyLineIndex-1]);
            Stack<char>[] stacks = CreateInitialStacks(lines,emptyLineIndex-2,numOfStacks);
            StringBuilder sb = new();

            for(int i = emptyLineIndex+1; i< lines.Length;i++)
            {
                var rearrangementCommand = ParseRearrangement(lines[i]);
                for(int m =0;m< rearrangementCommand.numOfItemsToMove; m++)
                {
                    stacks[rearrangementCommand.targetStackIndex].Push(stacks[rearrangementCommand.sourceStackIndex].Pop());
                }

            }
            
            for(int i = 0; i < numOfStacks;i++)
            {
                sb.Append(stacks[i].Peek());
            }

            return sb.ToString(); 
        }

        
        private int GetEmptyLineIndex(string[] lines)
        {
            for(int i = 0; i< lines.Length; i++)
            {
                if(string.IsNullOrEmpty(lines[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        private int ParseTotalStackCount(string crateCountLine)
        {
            string[] crateNumberLineParts = crateCountLine.Split(' ');
            for(int i = crateNumberLineParts.Length-1; i> 0;i--)
            {
                if(!string.IsNullOrEmpty(crateNumberLineParts[i]) && !string.IsNullOrWhiteSpace(crateNumberLineParts[i]))
                {
                    return Convert.ToInt32(crateNumberLineParts[i]);
                }
            }
            return -1;
        }
        private Stack<char>[] CreateInitialStacks(string[] lines, int crateItemLastIndex,int numOfCrates)
        {
            Stack<char>[] returnVal = new Stack<char>[numOfCrates];
            for(int i =0;i < numOfCrates;i++)
            {
                returnVal[i] = new Stack<char>();
            }
            for(int i=crateItemLastIndex;i>-1;i--)
            {
                for(int charIndex =0; charIndex < numOfCrates;charIndex++)
                {
                    if(lines[i][charIndex*4]=='[')
                    {
                        returnVal[charIndex].Push(lines[i][charIndex*4+1]);
                    }
                }
            }
            return returnVal;
        }

        private (int numOfItemsToMove, int sourceStackIndex, int targetStackIndex) ParseRearrangement(string line)
        {
            string[] parts = line.Split(' ');
            return (Convert.ToInt32(parts[1]),Convert.ToInt32(parts[3])-1,Convert.ToInt32(parts[5])-1);
        }
        
        
    }
}