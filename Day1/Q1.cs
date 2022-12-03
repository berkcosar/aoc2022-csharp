namespace Day1
{
    public class Q1:BaseQuestion
    {
        public override object Run()
        {
            string[] lines = ReadAllLines();
            List<long> cals = new();

            long currentSum = 0;

            foreach(var line in lines)
            {
                if(String.IsNullOrEmpty(line))
                {
                    cals.Add(currentSum);
                    currentSum = 0;
                }
                else
                {
                    currentSum += Convert.ToInt64(line);
                }
            }

            if(currentSum != 0)
            {
                cals.Add(currentSum);
            }
            cals.Sort();
            return cals[cals.Count-1];
        }
    }
}