using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquationParser
{
    public class EquationParser
    {
        private static List<string[]> operations;

        public EquationParser()
        {
            operations = new List<string[]>();
        }

        public void AddOperations(string[] ops)
        {
            operations.Add(ops);
        }

        public void ParseEquation(string equation)
        {
            string[] parts = SplitAtHighestOperation(equation);
            foreach (string part in parts)
            {
                Console.WriteLine(part);
            }
        }
        private string[] SplitAtHighestOperation(string equation)
        {
            equation = RemoveOuterBrackets(equation);
            int splitIndex = -1;
            int tokenLength = -1;
            if(operations.Count < 1) Console.WriteLine("Error: No Operations Given!");
            foreach (string[] ops in operations) { 
                if(GetIndexOfFirstHighestOccurenc(equation, ops, out splitIndex, out tokenLength))
                {
                    break;
                }
            }
            if(splitIndex == -1)
            {
                Console.WriteLine("Error: Equation was in wrong format!");
            }
            return GetPartsSplittedAt(equation, splitIndex, tokenLength);
        }


        private string[] GetPartsSplittedAt(string equation, int splitIndex, int tokenLength)
        {
            List<string> result = new List<string>();
            if (splitIndex != 0)
            {
                result.Add(equation.Substring(0,splitIndex));
            }
            result.Add(equation.Substring(splitIndex, tokenLength));
            result.Add(equation.Substring(splitIndex+tokenLength));
            return result.ToArray();
        }

        private bool GetIndexOfFirstHighestOccurenc(string equation, string[] tokens, out int index, out int tokenLength)
        {
            char[] equationChars = equation.ToCharArray();
            int bracket = 0;
            for (int i = 0; i < equation.Length; i++)
            {
                char c = equationChars[i];
                if (c == '(')
                {
                    bracket++;
                }
                if (c == ')')
                {
                    bracket--;
                }
                if (bracket == 0)
                {
                    foreach (String token in tokens)
                    {
                        if (MatchCharArrayWithToken(equationChars, i, token))
                        {
                            tokenLength = token.Length;
                            index = i;
                            return true;
                        }
                    }
                }
            }
            tokenLength = -1;
            index = -1;
            return false;
        }

        private bool MatchCharArrayWithToken(char[] array, int startIndex, string token)
        {
            char[] tokenArray = token.ToCharArray();
            if (tokenArray.Length > array.Length - startIndex)
            {
                return false;
            }
            for (int i = 0; i < tokenArray.Length; i++)
            {
                if (tokenArray[i] != array[i + startIndex])
                {
                    return false;
                }
            }
            return true;
        }

        private bool HasOuterBrackets(string equation)
        {
            char[] equationChars = equation.ToCharArray();
            if (equationChars[0] == '(' && equationChars[equationChars.Length - 1] == ')')
            {
                int bracket = 0;
                for (int i = 0; i < equation.Length - 1; i++)
                {
                    char c = equationChars[i];
                    if (c == '(')
                    {
                        bracket++;
                    }
                    if (c == ')')
                    {
                        bracket--;
                        if (bracket == 0)
                        {
                            return false;
                        }
                    }
                }
                return true;
            }
            return false;
        }
        private string RemoveOuterBrackets(string equation)
        {

            while (HasOuterBrackets(equation))
            {
                equation = equation.Substring(1, equation.Length - 2);
            }
            return equation;
        }
    }
}
