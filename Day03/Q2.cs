namespace Day03
{
    public class Q2:BaseQuestion
    {
        public override object Run()
        {
            string[] lines = ReadAllLines();
            long totalScore= 0;
            for(int i = 0; i < lines.Length-2; i+=3)
            {
                int first = i;
                int second = i+1;
                int third = i+2;
                Dictionary<char,int> dict = new();
                for(int j = 0; j < lines[first].Length; j++)
                {
                    //add all chars in first group to dictionary with value of 1.
                    if(!dict.ContainsKey(lines[first][j]))
                    {
                        dict.Add(lines[first][j],1);
                    }
                }
                for(int j = 0; j < lines[second].Length;j++)
                {
                    //incremenet value of chars in dictionary if they exist in second group.
                    if((dict.ContainsKey(lines[second][j])))
                    {
                        dict[lines[second][j]]++;
                    }
                }
                for(int j = 0; j < lines[third].Length;j++)
                {
                    //if current char in third group exists in dictionary 
                    //and its value is >1 (found in first and second group)
                    //calculate score and stop searching in group third.
                    if(dict.ContainsKey(lines[third][j]) && dict[lines[third][j]] > 1)
                    {
                        totalScore += GetLetterScore(lines[third][j]);
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