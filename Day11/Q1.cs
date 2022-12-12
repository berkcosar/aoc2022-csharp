
namespace Day11
{
    public class Monkey
    {
        private Queue<ulong> Items = new();

        private static ulong ModuloToDivise = 1;
        public Monkey(string[] input, int currentIndex, ulong worryLevelDivideBy)
        {
            WorryLevelDivisor = worryLevelDivideBy;
            ParseInput(input,currentIndex);
            TestFunc = ParseTestFunc(input,currentIndex);
            Operation = ParseOperation(input[currentIndex+2]);
        }

        public int Index { get; private set; }
        public ulong TotalThrownItemCount { get; private set; }

        public ulong WorryLevelDivisor{get; private set; }
        private int TestTrueMonkeyIndex { get; set; }
        private int TestFalseMonkeyIndex { get; set; }
        public Func<ulong, ulong, ulong> Operation { get; private set; }
        public Func<ulong, int> TestFunc { get; private set; }

        public bool ItemExists => Items.Count > 0;
        public (int monkeyIndex, ulong val) ThrowItem()
        {
            ulong val = Items.Dequeue();
            ulong operationResult = Operation(val,ModuloToDivise);
            var monkeyIndex = TestFunc(operationResult);
            TotalThrownItemCount++;
            return (monkeyIndex ,operationResult);
            
        }

        public void AddItem(ulong val)
        {
            Items.Enqueue(val);
        }

        private void ParseInput(string[] input,int currentIndex)
        {
            string[] parts = input[currentIndex+ 0].Replace(":", string.Empty).Split(' ');
            Index = Convert.ToInt32(parts[1]);

            parts = input[currentIndex+ 1].Split(':')[1].Split(',');
            foreach (var item in parts)
            {
                Items.Enqueue(Convert.ToUInt64(item.Trim()));
            }
        }
        public Func<ulong, ulong,ulong> ParseOperation(string operationInput)
        {
            string[] parts = operationInput.Split(':')[1].Replace(" new = old ", string.Empty).Split(' ');
            string valToConvert = parts[1].Trim();
            Func<ulong, ulong,ulong> returnVal;
            if (parts[0] == "*")
            {
                returnVal = new Func<ulong, ulong,ulong>((ulong val,ulong moduloDivisor) =>
                {
                    return (checked(val * (valToConvert == "old" ? val : Convert.ToUInt64(valToConvert)))%moduloDivisor) / WorryLevelDivisor;
                });
            }
            else// if (parts[0] == "+")
            {
                returnVal = new Func<ulong, ulong,ulong>((ulong val,ulong moduloDivisor) =>
                                {
                                    return (checked(val + (valToConvert == "old" ? val : Convert.ToUInt64(valToConvert)))%moduloDivisor) / WorryLevelDivisor;
                                });
            }
            return returnVal;
        }

        public Func<ulong, int> ParseTestFunc(string[] input, int currentIndex)
        {
            ulong divisibleBy = Convert.ToUInt64(input[currentIndex+ 3].Replace("Test: divisible by ", string.Empty));
            TestTrueMonkeyIndex = Convert.ToInt32(input[currentIndex + 4].Replace("If true: throw to monkey ", string.Empty));
            TestFalseMonkeyIndex = Convert.ToInt32(input[currentIndex +5].Replace("If false: throw to monkey ", string.Empty));

            ModuloToDivise *= divisibleBy;
            return new Func<ulong, int>((ulong val) =>
            {
                return val % divisibleBy == 0 ? TestTrueMonkeyIndex : TestFalseMonkeyIndex;
            });
        }
    }
    public class Q1 : BaseQuestion
    {
        public override object Run()
        {
            string[] commands = ReadAllLines();
            List<Monkey> monkeys = new();
            int currentIndex = 0;
            while(currentIndex + 6<= commands.Length )
            {
                monkeys.Add(new Monkey(commands, currentIndex,3));
                currentIndex +=7;
            }

            for(int i= 0; i< 20; i++)
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