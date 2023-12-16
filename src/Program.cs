namespace Main
{
    using EquationParser;

    public class Program
    {
        public static void Main(string[] args)
        {
            string equation = "1*2+3*4+5/6";
            SyntaxTree tree = new SyntaxTree(equation);
            tree.PreOrderTraversal();
            Console.WriteLine("Result: " + tree.CalculateTree());
            while (true);
        }
    }
}