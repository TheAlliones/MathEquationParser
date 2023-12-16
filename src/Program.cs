using EquationParser;

namespace Main
{
    public class Program
    {
        public static void Main(string[] args) {
            Console.WriteLine("Loading and Compiling Tokens!");
            TokenManager.LoadTokensFromFile("tokens.json");
            TokenManager.CompileTokenMethods();
            TokenManager.Log();
            while (true)
            {
                Console.WriteLine("Equation: ");
                string input = Console.ReadLine();
                if (input != null && input != "")
                {
                    SyntaxTree tree = new SyntaxTree(input);
                    tree.PreOrderTraversal();
                    Console.WriteLine("Result: " + tree.CalculateTree());
                }
            }
        }
    }
}

