namespace Day03
{
    public class Q1:BaseQuestion
    {
        public override object Run()
        {
            string[] lines = ReadAllLines();
            long totalScore= 0;
            foreach(var line in lines)
            {
                int firstHalfLength = line.Length/2;
                HashSet<char> set = new();
                for(int i = 0; i < firstHalfLength; i++)
                {
                    if(!set.Contains(line[i]))
                    {
                        set.Add(line[i]);
                    }
                }
                for(int i = firstHalfLength; i <line.Length;i++)
                {
                    if((set.Contains(line[i])))
                    {
                        totalScore += GetLetterScore(line[i]);
                        break;
                    }
                }
            }

            return totalScore; 
        }
        private int GetLetterScore(char ch)
        {
            int letterScore =0;
            bool isUpper = char.IsUpper(ch);
            letterScore = (ch - 'a') +  1; //a=1 z=26 'a'-'a' = 0 'b'-'a'=1
            if(isUpper)
            {
                letterScore += 58; //A should be 27 'A'-'a'+1 =-31
            }
            return letterScore;
        }
    }
}