namespace NiuX.Comparers;

/// <summary>
/// 数组比较器
/// </summary>
public class ArrayComparer : IComparer<int[]>
{
    /// <summary>
    /// 比较
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    /// <remarks>针对数组进行排序，如 [3,4] [2,3,4] 排序后会变成 [2,3,4] [3,4]</remarks>
    public int Compare(int[]? x, int[]? y)
    {
        if (x == null) throw new ArgumentNullException(nameof(x));
        if (y == null) throw new ArgumentNullException(nameof(y));

        for (var i = 0; i < x.Length; i++)
        {
            if (y.Length <= i) return 1;

            var val = x[i].CompareTo(y[i]);
            if (val != 0) return val;
        }

        return x.Length.CompareTo(y.Length);
    }
}