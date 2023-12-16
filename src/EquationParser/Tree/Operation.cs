using System.Runtime.Intrinsics;
using System.Text;

namespace EquationParser
{
    public class Operation : TreeElement
    {
        private string operation;
        private TreeElement leftChild;
        private TreeElement rightChild;
        public Operation(EquationParser parser, string[] parts)
        {
            operation = parts[1];
            leftChild = new Term(parser).AddEquation(parts[0]);
            rightChild = new Term(parser).AddEquation(parts[2]);
        }
        public override TreeElement AddEquation(string equation)
        {
            return this;
        }
        public override double Calculate()
        {
            double v1 = leftChild.Calculate();
            double v2 = rightChild.Calculate();
            if (!TokenManager.ContainsToken(operation))
            {
                Console.WriteLine("Error: Operator is unknown! : " + operation);
            }
            return TokenManager.CalculateToken(operation, v1, v2);
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
