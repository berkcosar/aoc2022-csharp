
namespace Day11
{
    public class Q2 : BaseQuestion
    {
        public override object Run()
        {
            string[] commands = ReadAllLines();
            List<Monkey> monkeys = new();
            int currentIndex = 0;
            while(currentIndex + 6<= commands.Length )
            {
                monkeys.Add(new Monkey(commands, currentIndex,1));
                currentIndex +=7;
            }

            for(int i= 0; i< 10000; i++)
            {
                foreach(var monkey in monkeys)
                {
                    while(monkey.ItemExists)
                    {
                        var throwResult = monkey.ThrowItem();
                        monkeys[throwResult.monkeyIndex].AddItem(throwResult.val);
                    }
                }
            }

            var orderedMonkeys = monkeys.OrderByDescending((p)=>p.TotalThrownItemCount).ToArray();

            return orderedMonkeys[0].TotalThrownItemCount * orderedMonkeys[1].TotalThrownItemCount;
        }
    }

}