namespace EquationParser
{
    public abstract class TreeElement
    {
        public abstract TreeElement AddEquation(string equation);
        public abstract void PreOrderTraversal(int depth, string spacer);
        public abstract double Calculate();
    }
}
