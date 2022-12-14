
using System.Text;
namespace Day13
{
    public class Q1 : BaseQuestion
    {
        public override object Run()
        {
            string[] commands = ReadAllLines();
            List<(List<object> first, List<object> second)> inputs = new();
            int sumOfCorrectOrderIndices = 0;
            for(int i =0; i< commands.Length-1; i = i+3)
            {
                inputs.Add((ParseRow(commands[i]),ParseRow(commands[i+1])));
            }

            for(int i = 0; i < inputs.Count;i++)
            {
                bool? res = AreInCorrectOrder(inputs[i].first, inputs[i].second);
                if(res.HasValue && res.Value)
                {
                    sumOfCorrectOrderIndices += (i+1);
                }
            }

            return sumOfCorrectOrderIndices;
        }

        private bool? AreInCorrectOrder(List<object> first, List<object> second)
        {
            var firstEnumerator = first.GetEnumerator();
            var secondEnumerator = second.GetEnumerator();
            firstEnumerator.MoveNext();
            secondEnumerator.MoveNext();
            while(firstEnumerator.Current != null && secondEnumerator.Current != null)
            {
                bool? result = AreObjectsAreInCorrectOrder(firstEnumerator.Current,secondEnumerator.Current);
                if(result.HasValue)
                {
                    return result.Value;
                }
                firstEnumerator.MoveNext();
                secondEnumerator.MoveNext();
            }
            if(firstEnumerator.Current == null && secondEnumerator.Current == null)
            {
                return null;
            }
            else if(firstEnumerator.Current == null)
            {
                return true;
            }
            return false;
        }

        private bool? AreObjectsAreInCorrectOrder(object first, object second)
        {
            if(first is Int32 && second is Int32)
            {
                int firstInt = (int)first;
                int secondInt = (int)second;
                return firstInt == secondInt ? null : firstInt< secondInt;
            }
            else if(first is List<object> && second is List<object>)
            {
                return AreInCorrectOrder(first as List<object>, second as List<object>);
            }
            else
            {
                if(first is List<object>)
                {
                    return AreInCorrectOrder(first as List<object>, new List<object>(){second});
                }
                else{
                    return AreInCorrectOrder(new List<object>(){first}, second as List<object>);
                }
            }
        }

        private List<object> ParseRow(string input)
        {
            Stack<List<object>> stack = new();
            List<object> returnVal = new();
            stack.Push(returnVal);
            StringBuilder numBuilder= new();
            for(int i =1; i < input.Length; i++)
            {
                char currentChar = input[i];

                while(char.IsNumber(currentChar))
                {
                    numBuilder.Append(currentChar);
                    currentChar = input[++i];
                }
                if(numBuilder.Length > 0)
                {
                    stack.Peek().Add(Convert.ToInt32(numBuilder.ToString()));
                    numBuilder.Clear();
                }
                if(currentChar == '[')
                {
                    stack.Push(new List<object>());
                }
                else if(currentChar == ']')
                {
                    if(stack.Count > 1)
                    {
                        var currentStack = stack.Pop();
                        stack.Peek().Add(currentStack);
                    }
                }
            }

            return returnVal;
        }
    }
}