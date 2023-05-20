using System.Linq.Expressions;
using System.Text;

namespace NiuX.Linq.SqlServer;

internal class CustomTranslator : ExpressionVisitor
{
    private StringBuilder _stringBuilder;

    internal string Translate(Expression expression)
    {
        _stringBuilder = new StringBuilder();
        Visit(expression);
        return _stringBuilder.ToString();
    }

    private static Expression StripQuotes(Expression e)
    {
        while (e.NodeType == ExpressionType.Quote) e = ((UnaryExpression)e).Operand;
        return e;
    }

    protected override Expression VisitMethodCall(MethodCallExpression methodCallExpression)
    {
        if (methodCallExpression.Method.DeclaringType != typeof(Queryable) ||
            methodCallExpression.Method.Name != "Where")
            throw new NotSupportedException($"不支持 '{methodCallExpression.Method.Name}' 方法");

        _stringBuilder.Append("SELECT * FROM (");
        Visit(methodCallExpression.Arguments[0]);
        _stringBuilder.Append(") AS t WHERE ");
        var lambda = (LambdaExpression)StripQuotes(methodCallExpression.Arguments[1]);
        Visit(lambda.Body);
        return methodCallExpression;
    }

    protected override Expression VisitUnary(UnaryExpression expression)
    {
        switch (expression.NodeType)
        {
            case ExpressionType.Not:
                _stringBuilder.Append(" NOT ");
                Visit(expression.Operand);
                break;

            default:
                throw new NotSupportedException($"不支持 '{expression.NodeType}' 操作符");
        }

        return expression;
    }

    protected override Expression VisitBinary(BinaryExpression b)
    {
        _stringBuilder.Append("(");
        Visit(b.Left);
        switch (b.NodeType)
        {
            case ExpressionType.And:
                _stringBuilder.Append(" AND ");
                break;

            case ExpressionType.Or:
                _stringBuilder.Append(" OR");
                break;

            case ExpressionType.Equal:
                _stringBuilder.Append(" = ");
                break;

            case ExpressionType.NotEqual:
                _stringBuilder.Append(" <> ");
                break;

            case ExpressionType.LessThan:
                _stringBuilder.Append(" < ");
                break;

            case ExpressionType.LessThanOrEqual:
                _stringBuilder.Append(" <= ");
                break;

            case ExpressionType.GreaterThan:
                _stringBuilder.Append(" > ");
                break;

            case ExpressionType.GreaterThanOrEqual:
                _stringBuilder.Append(" >= ");
                break;

            default:
                throw new NotSupportedException($"不支持 '{b.NodeType}' 运算符");
        }

        Visit(b.Right);
        _stringBuilder.Append(")");
        return b;
    }

    protected override Expression VisitConstant(ConstantExpression c)
    {
        if (!(c.Value is IQueryable q))
        {
            if (c.Value == null)
                _stringBuilder.Append("NULL");
            else
                switch (Type.GetTypeCode(c.Value.GetType()))
                {
                    case TypeCode.Boolean:
                        _stringBuilder.Append((bool)c.Value ? 1 : 0);
                        break;

                    case TypeCode.String:
                        _stringBuilder.Append("'");
                        _stringBuilder.Append(c.Value);
                        _stringBuilder.Append("'");
                        break;

                    case TypeCode.Object:
                        throw new NotSupportedException($"不支持 '{c.Value}' 类型");
                    default:
                        _stringBuilder.Append(c.Value);
                        break;
                }
        }
        else
        {
            _stringBuilder.Append("SELECT * FROM ");
            _stringBuilder.Append(q.ElementType.Name);
        }

        return c;
    }

    protected override Expression VisitMember(MemberExpression m)
    {
        if (m.Expression == null || m.Expression.NodeType != ExpressionType.Parameter)
            throw new NotSupportedException($"不支持 '{m.Member.Name}' 成员");
        _stringBuilder.Append(m.Member.Name);
        return m;
    }
}