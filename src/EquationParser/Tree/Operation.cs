using System.Text;

namespace EquationParser
{
    public class Operation : TreeElement
    {
        private string operation;
        private TreeElement leftChild;
        private TreeElement rightChild;
        public Operation(string[] parts)
        {
            operation = parts[1];
            leftChild = new Term().AddEquation(parts[0]);
            rightChild = new Term().AddEquation(parts[2]);
        }
        public override TreeElement AddEquation(string equation)
        {
            return this;
        }
        public override double Calculate()
        {
            double v1 = leftChild.Calculate();
            double v2 = rightChild.Calculate();
            switch (operation)
            {
                case "+":
                    return v1 + v2;
                case "-":
                    return v1 - v2;
                case "*":
                    return v1 * v2;
                case "/":
                    if(v2 == 0)
                    {
                        Console.WriteLine("Error: Division by zero is illegal!");
                        return 0;
                    }
                    return v1 / v2;
                default:
                    Console.WriteLine("Error: Operator is unknown! : " + operation);
                    break;
            }
            return 0;
        }
        public override void PreOrderTraversal(int depths, string spacer)
        {
            Console.WriteLine(spacer + "(" +operation +")");
            spacer = spacer.Replace("├", "│");
            spacer = spacer.Replace("└", " ");
            leftChild.PreOrderTraversal(depths+1,spacer + "├");
            rightChild.PreOrderTraversal(depths+1,spacer + "└");   
        }
    }
}
