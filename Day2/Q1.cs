namespace Day2
{
    public class Q1:BaseQuestion
    {
        public override object Run()
        {
            Dictionary<string,int> scores = new(){
                {"A X", 4},
                {"A Y", 8},
                {"A Z", 3},
                {"B X", 1},
                {"B Y", 5},
                {"B Z", 9},
                {"C X", 7},
                {"C Y", 2},
                {"C Z", 6},
                };
            string[] lines = ReadAllLines();
            int totalScore= 0;
            foreach(var line in lines)
            {
                totalScore +=scores[line];
            }

            return totalScore;
        }
    }
}