namespace NiuX.Csv.Quantum;

/// <summary>
/// 列信息
/// </summary>
public readonly struct QuantumColumn
{
    private readonly QuantumSpan[] _cols;
    private readonly string[] _names;

    public QuantumColumn(QuantumSpan[] cols, string[] names)
    {
        _cols = cols;
        _names = names;
    }

    /// <summary>
    /// 索引
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string this[int index] => _cols[index].ToString();

    /// <summary>
    /// 数量
    /// </summary>
    public int Count => _cols.Length;

    /// <summary>
    /// 获取列名
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string GetColumnName(int index)
    {
        return _names[index];
    }
}