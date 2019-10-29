using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class IncDecExpressionVisitor : ExpressionVisitor
    {
        // todo: feel free to add your code here

        protected override Expression VisitBinary(BinaryExpression node)
        {

            ParameterExpression parameter = null;
            ConstantExpression constant = null;

            if (node.NodeType == ExpressionType.Add || node.NodeType == ExpressionType.Subtract) {
                if (node.Left.NodeType == ExpressionType.Parameter) {
                    parameter = (ParameterExpression)node.Left;
                } else if (node.Left.NodeType == ExpressionType.Constant) {
                    constant = (ConstantExpression)node.Left;
                }

                if (node.Right.NodeType == ExpressionType.Parameter) {
                    parameter = (ParameterExpression)node.Right;
                } else if (node.Right.NodeType == ExpressionType.Constant) {
                    constant = (ConstantExpression)node.Right;
                }

                if (parameter != null && constant != null && constant.Type == typeof(int) && (int)constant.Value == 1) {
                    return node.NodeType == ExpressionType.Add ? Expression.Increment(parameter) : Expression.Decrement(parameter);
                }
            }

            return base.VisitBinary(node);
        }
    }
}
