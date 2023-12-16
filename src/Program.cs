namespace Main
{
    using EquationParser;

    public class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Equation: ");
                string input = Console.ReadLine();
                if(input != null && input != "")
                {
                    SyntaxTree tree = new SyntaxTree(input);
                    tree.PreOrderTraversal();
                    Console.WriteLine("Result: " + tree.CalculateTree());
                }
            } 
        }
    }
}