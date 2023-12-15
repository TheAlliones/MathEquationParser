namespace Main
{
    using EquationParser;

    public class Program
    {
        public static void Main(string[] args)
        {
            string equation = "((5+4)×2)-6×8×(4-9)";
            EquationParser parser = new EquationParser();
            parser.AddOperations(new string[] { "×", "/" });
            parser.AddOperations(new string[] { "+", "-" });
            parser.ParseEquation(equation);
            while (true);
        }
    }
}