using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    public class ReplaceParamsExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression _expression;
        private IDictionary<string, object> _dic;

        public ReplaceParamsExpressionVisitor(Expression expression, IDictionary<string, object> dic)
        {
            _expression = expression;
            _dic = dic;
        }

        public ReplaceParamsExpressionVisitor(Expression<Func<int, string, string>> expression, params Expression<Func<string, object>>[] dicFunc)
        {
            _expression = expression;

            IDictionary<string, object> dic = new Dictionary<string, object>();
            foreach (var func in dicFunc) {
                dic.Add(func.Parameters[0].Name, func.Compile().Invoke(""));
            }

            _dic = dic;
        }

        public LambdaExpression ReplaceWithConstant()
        {
            return (LambdaExpression)Visit(_expression);
        }
      
        protected override Expression VisitParameter(ParameterExpression node)
        {

            if (node.NodeType == ExpressionType.Parameter)
            {
                var paramNode = node as ParameterExpression;
                if (_dic.ContainsKey(paramNode.Name))
                {
                    object obj = _dic[paramNode.Name];
                    return Expression.Constant(obj);
                }
            }

            return base.VisitParameter(node);
        }

        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            var parameters = node.Parameters.Where(p => !_dic.ContainsKey(p.Name)).ToList();

            return Expression.Lambda(Visit(node.Body), parameters);
        }
    }
}
