namespace EquationParser
{
    public class Term : TreeElement
    {
        private string term = "";
        private EquationParser parser;

        public Term(EquationParser p)
        {
            parser = p;
        }

        public override TreeElement AddEquation(string equation)
        {
            string[] equationParts = parser.ParseEquation(equation, out bool hasOperation);
            if (hasOperation)
            {
                term = equation;
                return this;
            }
            else if(equationParts != null)
            {
                Operation operation = new Operation(parser,equationParts);
                return operation;
            }
            return this;    
        }

        public override double Calculate()
        {
            if (double.TryParse(term,out double value))
            {
                return value;
            }
            else
            {
                if(term == "")
                {
                    return 0;
                }
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
