namespace Day4
{
    public class Q2:BaseQuestion
    {
        public override object Run()
        {
            string[] lines = ReadAllLines();
            long overlappingPairCount= 0;
            foreach(var line in lines)
            {
                string[] elves = line.Split(',');
                string[] elf1Parts = elves[0].Split('-');
                string[] elf2Parts = elves[1].Split('-');
                (int start,int end) elf1 =(Convert.ToInt32(elf1Parts[0]),Convert.ToInt32(elf1Parts[1]));
                (int start,int end) elf2 =(Convert.ToInt32(elf2Parts[0]),Convert.ToInt32(elf2Parts[1]));
                if(DoIntervalsOverlap(elf1,elf2))
                {
                    overlappingPairCount++;
                }
            }

            return overlappingPairCount; 
        }
        private bool DoIntervalsOverlap((int start,int end) first, (int start,int end) second)
        {
            if(first.start >= second.start && first.start <= second.end)
            {
                return true;
            }
            if(first.end >= second.start && first.end <= second.end)
            {
                return true;
            }
            if(second.start >= first.start && second.start <= first.end)
            {
               return true;
            }
            if(second.end >= first.start && second.end <= first.end)
            {
                return true;
            }
            return false;

        }
    }
}