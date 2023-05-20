namespace NiuX.Csv.Quantum;

/// <summary>
/// 范围
/// </summary>
public struct QuantumSpan
{
    public QuantumSpan(char[] line, int start, int count)
    {
        buf = line;
        Start = start;
        Count = count;
    }

    public char[] buf;
    public int Start;
    public int Count;

    /// <summary>
    /// 转字符串
    /// </summary>
    /// <returns></returns>
    public new string ToString()
    {
        return buf != null ? new string(buf, Start, Count) : "";
    }
}