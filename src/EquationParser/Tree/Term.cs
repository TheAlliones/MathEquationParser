namespace EquationParser
{
    public class Term : TreeElement
    {
        string term = "";

        public override TreeElement AddEquation(string equation)
        {
            EquationParser parser = new EquationParser();
            parser.AddOperations(new string[] { "+", "-" });
            parser.AddOperations(new string[] { "*", "/" });
            string[] equationParts = parser.ParseEquation(equation, out bool hasOperation);
            if (hasOperation)
            {
                term = equation;
                return this;
            }
            else
            {
                Operation operation = new Operation(equationParts);
                return operation;
            }
        }

        public override double Calculate()
        {
            if (double.TryParse(term,out double value))
            {
                return value;
            }
            else
            {
                Console.WriteLine("Error: Number was in wrong format! : " + term);
                return 0;
            }
        }

        public override void PreOrderTraversal(int depth, string spacer)
        {
            Console.WriteLine(spacer + term);
        }

        
    }
}
