
namespace Day16
{

    public class Valve
    {
        public static Dictionary<string, Valve> AllValves;

        static Valve()
        {
            AllValves = new();
        }
        private Valve(string valveNo, int flowRate, string[] leadToValveNos)
        {
            ValveNo = valveNo;
            FlowRate = flowRate;
            LeadToValveNos = leadToValveNos;
        }
        public static Valve CreateValve(string line)
        {

            string[] lineParts = line.Split(';');
            string valveNo = line.Substring(6, 2);
            int flowRate = Convert.ToInt32(lineParts[0].Substring(23));
            int leadToValvePartStartIndex = lineParts[1].Length > 25 ? 24 : 23;
            string[] targetParts = lineParts[1].Substring(leadToValvePartStartIndex).Split(", ");
            Valve v = new(valveNo, flowRate, targetParts);
            AllValves.Add(v.ValveNo, v);
            return v;
        }

        public string ValveNo { get; set; }

        public int FlowRate { get; set; }

        public string[] LeadToValveNos { get; set; }
    }

    public class Q1 : BaseQuestion
    {
        Dictionary<(string valveNo, int remainingMinutes,string openedValvesString),int> memo = new();
        int NumberOfValvesHasValue;
        public override object Run()
        {
            string[] commands = ReadAllLines();
            Valve v = Valve.CreateValve(commands[0]);

            string firstValveNo = "AA";
            for (int i = 1; i < commands.Length; i++)
            {
                Valve.CreateValve(commands[i]);
            }
            NumberOfValvesHasValue = Valve.AllValves.Where(p=>p.Value.FlowRate > 0).Count();
            int retVal = GetMaxReleasedPressure(firstValveNo, 30, new List<string>());

            return retVal;
        }
        
        public int GetMaxReleasedPressure(string valveNo, int remainingMinutes, List<string> openedValves)
        {
            string orderedOpenedValvesString = string.Join("-", openedValves.Order());
            if(openedValves.Count == NumberOfValvesHasValue || remainingMinutes < 1)
            {
                return 0;
            }
            if(memo.ContainsKey((valveNo,remainingMinutes,orderedOpenedValvesString)))
            {
                return memo[(valveNo,remainingMinutes,orderedOpenedValvesString)];
            }
            
            Valve currentValve = Valve.AllValves[valveNo];
            if (!openedValves.Contains(valveNo) && currentValve.FlowRate > 0)
            {
                int remMinutes = remainingMinutes;
                int currentValvePressureReleasable = currentValve.FlowRate * (remainingMinutes-1);
                if (remainingMinutes == 1)
                {
                    memo.Add((valveNo,remainingMinutes,orderedOpenedValvesString),currentValvePressureReleasable);
                    return currentValvePressureReleasable;
                }

                List<int> currentIncludedPathPressureResults = new();
                List<string> currentIncludedPathOpenedValues = new(openedValves);
                currentIncludedPathOpenedValues.Add(valveNo);
                foreach (var target in currentValve.LeadToValveNos)
                {
                    currentIncludedPathPressureResults.Add(
                        currentValvePressureReleasable + GetMaxReleasedPressure(target, remainingMinutes - 2, currentIncludedPathOpenedValues));
                }

                List<int> currentNotIncludedPathPressureResults = new();
                List<string> currentNotIncludedPathOpenedValues = new(openedValves);
                foreach (var target in currentValve.LeadToValveNos)
                {
                    currentNotIncludedPathPressureResults.Add(
                        GetMaxReleasedPressure(target, remainingMinutes - 1, currentNotIncludedPathOpenedValues));
                }
                int retVal = Math.Max(currentIncludedPathPressureResults.DefaultIfEmpty(0).Max(), currentNotIncludedPathPressureResults.DefaultIfEmpty(0).Max());

                memo.Add((valveNo,remainingMinutes,orderedOpenedValvesString),retVal);

                return retVal;
            }
            else
            {
                if (remainingMinutes < 2)
                {
                    return 0;
                }
                List<int> currentNotIncludedPathPressureResults = new();
                List<string> currentNotIncludedPathOpenedValues = new(openedValves);
                foreach (var target in currentValve.LeadToValveNos)
                {
                    currentNotIncludedPathPressureResults.Add(
                        GetMaxReleasedPressure(target, remainingMinutes - 1, currentNotIncludedPathOpenedValues));
                }
                int retVal =currentNotIncludedPathPressureResults.Max();
                memo.Add((valveNo,remainingMinutes,orderedOpenedValvesString),retVal);
                return retVal;
            }
        }
    }
}