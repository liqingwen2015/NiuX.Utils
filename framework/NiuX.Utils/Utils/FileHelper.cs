using NiuX.Extensions;

namespace NiuX.Utils;

/// <summary>
/// 文件工具
/// </summary>
public static class FileHelper
{
    public static string DefaultPostfix = ".txt";

    public static void Delete(string path)
    {
        if (File.Exists(path)) File.Delete(path);
    }

    public static void Write(string path, string content)
    {
        var fileInfo = new FileInfo(path);

        if (fileInfo.Directory is { Exists: false }) Directory.CreateDirectory(fileInfo.Directory.Name);

        File.WriteAllText(path, content);
    }

    public static void WriteAndOpen(string path, string content)
    {
        Write(path, content);
        path.AsOpen();
    }

#if NETSTANDARD2_1_OR_GREATER
    public static Task WriteAsync(string filePath, string content)
    {
        var fileInfo = new FileInfo(filePath);

        if (fileInfo.Directory is { Exists: false })
        {
            Directory.CreateDirectory(fileInfo.Directory.Name);
        }

        return File.WriteAllTextAsync(filePath, content);
    }

    public static async Task WriteAndOpenAsync(string path, string content)
    {
        await WriteAsync(path, content);
        path.AsOpen();
    }

#endif

    /// <summary>
    /// 格式化后缀
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string FormatePostfix(string path)
    {
        return FormatePostfix(path, DefaultPostfix);
    }

    /// <summary>
    /// 格式化后缀
    /// </summary>
    /// <returns></returns>
    public static string FormatePostfix(string path, string postfix)
    {
        if (path.Contains('.')) return path;
        path += postfix;

        return path;
    }
}