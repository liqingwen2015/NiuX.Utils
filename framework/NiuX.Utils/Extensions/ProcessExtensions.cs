using System.Diagnostics;

namespace NiuX.Extensions;

public static class ProcessExtensions
{
    /// <summary>
    /// 打开
    /// </summary>
    /// <param name="str"></param>
    public static void AsStartProccess(this string str)
    {
        using var p = new Process();
        p.StartInfo = new ProcessStartInfo
        {
            UseShellExecute = true,
            FileName = str
        };
        p.Start();
    }

    /// <summary>
    /// 打开
    /// </summary>
    public static void AsSaveAndStartProccess(this string str, string content)
    {
        File.WriteAllText(str, content);
        str.AsStartProccess();
    }

#if NETSTANDARD2_1_OR_GREATER
    /// <summary>
    /// 打开
    /// </summary>
    public static async Task AsSaveAndStartProccessAsync(this string str, string content)
    {
        await File.WriteAllTextAsync(str, content);
        str.AsStartProccess();
    }

#endif
}