using System.Text.Json;
using NiuX.Extensions;

namespace NiuX.Utils;

public static class JsonHelper
{
    public static string DefaultPostfix = ".json";

    public static void Write(string path, object obj)
    {
        using var file = File.CreateText(FormatePostfix(path));
        file.Write(obj.ToJson());
    }

#if NETSTANDARD2_1_OR_GREATER
    public static async Task WriteAsync(string path, object obj)
    {
        await using var file = File.CreateText(FormatePostfix(path));
        await file.WriteAsync(obj.ToJson());
    }

#elif NETSTANDARD2_0_OR_GREATER

    public static async Task WriteAsync(string path, object obj)
    {
        using var file = File.CreateText(FormatePostfix(path));
        await file.WriteAsync(obj.ToJson());
    }

#endif

    /// <summary>
    /// 写入并打开
    /// </summary>
    /// <param name="path"></param>
    /// <param name="obj"></param>
    public static void WriteAndOpen(string path, object obj)
    {
        path = FormatePostfix(path);
        Write(path, obj);
        path.AsOpen();
    }

    /// <summary>
    /// 格式化后缀
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private static string FormatePostfix(string path)
    {
        if (path.Contains(".")) return path;
        path += DefaultPostfix;

        return path;
    }
}