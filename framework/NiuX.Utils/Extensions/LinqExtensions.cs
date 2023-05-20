namespace NiuX.Extensions;

public static class LinqExtensions
{
    /// <summary>
    /// 获取最小项
    /// </summary>
    /// <typeparam name="TSouce"></typeparam>
    /// <typeparam name="TCompareValue"></typeparam>
    /// <param name="source"></param>
    /// <param name="comparerExpression"></param>
    /// <returns></returns>
    public static TSouce GetMin<TSouce, TCompareValue>(this IEnumerable<TSouce> source,
        Func<TSouce, TCompareValue> comparerExpression)
    {
        var comparer = Comparer<TCompareValue>.Default;
        return source.Aggregate((minValue, item) =>
            comparer.Compare(comparerExpression(minValue), comparerExpression(item)) < 0 ? minValue : item);
    }

    /// <summary>
    /// 获取最大项
    /// </summary>
    /// <typeparam name="TSouce"></typeparam>
    /// <typeparam name="TCompareValue"></typeparam>
    /// <param name="source"></param>
    /// <param name="comparerExpression"></param>
    /// <returns></returns>
    public static TSouce GetMax<TSouce, TCompareValue>(this IEnumerable<TSouce> source,
        Func<TSouce, TCompareValue> comparerExpression)
    {
        var comparer = Comparer<TCompareValue>.Default;
        return source.Aggregate((minValue, item) =>
            comparer.Compare(comparerExpression(minValue), comparerExpression(item)) < 0 ? minValue : item);
    }
}