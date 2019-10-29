/*
 * Create a class based on ExpressionVisitor, which makes expression tree transformation:
 * 1. converts expressions like <variable> + 1 to increment operations, <variable> - 1 - into decrement operations.
 * 2. changes parameter values in a lambda expression to constants, taking the following as transformation parameters:
 *    - source expression;
 *    - dictionary: <parameter name: value for replacement>
 * The results could be printed in console or checked via Debugger using any Visualizer.
 */
using System;
using System.Linq.Expressions;

namespace ExpressionTrees.Task1.ExpressionsTransformer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Expression Visitor for increment/decrement.");
            Console.WriteLine();

            // todo: feel free to add your code here

            Expression<Func<int, int>> exp = (a) => a + (a + 1) * (a + 5) * (a + 1) + a + (a - 1);
            Expression<Func<int, string, string>> exp2 = (a, Name) => a + (a - 1) * (a + 5) * (a + 1) + Name;

            var result = new IncDecExpressionVisitor().VisitAndConvert(exp, "");

            Console.WriteLine(exp + " " + exp.Compile().Invoke(3));
            Console.WriteLine(result + " " + result.Compile().Invoke(3));

            Console.WriteLine();
            Console.WriteLine();
            var replacedParamsExpression = (new ReplaceParamsExpressionVisitor(exp2, a => 3, Name => " Raccoons forever").ReplaceWithConstant());
            Console.WriteLine(replacedParamsExpression + " result: " + replacedParamsExpression.Compile().DynamicInvoke());

            Console.ReadLine();
        }
    }
}
