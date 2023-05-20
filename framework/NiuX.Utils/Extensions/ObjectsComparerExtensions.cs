using ObjectsComparer;

namespace NiuX.Extensions;

/// <summary>
/// 对象比较器扩展
/// </summary>
public static class ObjectsComparerExtensions
{
    /// <summary>
    /// 比较差异
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj1"></param>
    /// <param name="obj2"></param>
    /// <param name="differences"></param>
    /// <returns></returns>
    public static bool CompareDifferences<T>(this T obj1, T obj2, out IEnumerable<Difference> differences) => new ObjectsComparer.Comparer<T>().Compare(obj1, obj2, out differences);

    /// <summary>
    /// 比较差异
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj1"></param>
    /// <param name="obj2"></param>
    /// <returns></returns>
    public static bool CompareDifferences<T>(this T obj1, T obj2) => new ObjectsComparer.Comparer<T>().Compare(obj1, obj2);

    /// <summary>
    /// 比较差异
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj1"></param>
    /// <param name="obj2"></param>
    /// <returns></returns>
    public static IEnumerable<Difference> GetDifferences<T>(this T obj1, T obj2)
    {
        new ObjectsComparer.Comparer<T>().Compare(obj1, obj2, out var differences);
        return differences;
    }
}