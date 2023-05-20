using ExpressionTreeToString;
using System.Linq.Expressions;
using ZSpitz.Util;

namespace NiuX.Extensions;

/// <summary>
/// 表达式树扩展方法
/// </summary>
/// <remarks>https://github.com/zspitz/ExpressionTreeToString/wiki</remarks>
public static class ExpressionTreeExtensions
{
    public static string ToExpressionString(this Expression expression)
    {
        return expression.ToString();
    }

    public static string ToExpressionCSharpString(this Expression expression)
    {
        return expression.ToString("C#");
    }

    public static string ToExpressionVBString(this Expression expression)
    {
        return expression.ToString("Visual Basic");
    }

    public static string ToExpressionTextualTreeString(this Expression expression, Language language = Language.CSharp)
    {
        return expression.ToString("Textual tree", language);
    }

    public static string ToExpressionFactoryMethodsString(this Expression expression,
        Language language = Language.CSharp)
    {
        return expression.ToString("Factory methods", language);
    }
}