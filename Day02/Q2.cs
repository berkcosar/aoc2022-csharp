namespace Day02
{
    public class Q2:BaseQuestion
    {
        public override object Run()
        {
            Dictionary<string,int> scores = new(){
                {"A X", 3},//C
                {"A Y", 4},//A
                {"A Z", 8},//B
                {"B X", 1},//A
                {"B Y", 5},//B
                {"B Z", 9},//C
                {"C X", 2},//B
                {"C Y", 6},//C
                {"C Z", 7},//A
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