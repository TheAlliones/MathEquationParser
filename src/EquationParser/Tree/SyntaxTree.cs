namespace EquationParser
{
    public class SyntaxTree
    {
        public string equation {  get; }
        private EquationParser parser;


        private TreeElement root;
        public SyntaxTree(string equ) 
        {
            equation = equ;
            parser = new EquationParser();
            parser.LoadTokens(TokenManager.GetTokens());
            root = new Term(parser);
            root = root.AddEquation(equation);
        }

        public void PreOrderTraversal()
        {
            root.PreOrderTraversal(0,"");
        }

        public double CalculateTree() {
            return root.Calculate();
        }

        public static string GetSpaces(int amount)
        {
            string s = "";
            for (int i = 0; i < amount; i++)
            {
                s += "|";
            }
            return s;
        }
    }
}
