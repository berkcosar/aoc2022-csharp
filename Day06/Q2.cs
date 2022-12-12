
namespace Day06
{
    using System.Text;
    public class Q2 : BaseQuestion
    {
        public override object Run()
        {
            string input = ReadAllLines()[0];
            int returnVal = -1;
            Dictionary<char, int> dict = new();
            int sequenceStartIndex = 0;
            dict.Add(input[0], 0);

            for (int i = 1; i < input.Length; i++)
            {
                if (dict.ContainsKey(input[i]))
                {
                    if (dict[input[i]] >= sequenceStartIndex)
                    {
                        sequenceStartIndex = dict[input[i]]+1;
                        dict[input[i]] = i;
                    }
                    else
                    {
                        if (i + 1 - sequenceStartIndex == 14)
                        {
                            returnVal = i+1;
                            break;
                        }
                        dict[input[i]] = i;
                    }
                }
                else
                {
                    if (i + 1 - sequenceStartIndex == 14)
                    {
                        returnVal = i+1;
                        break;
                    }
                    dict.Add(input[i],i);
                }
            }

            return returnVal;
        }



    }
}