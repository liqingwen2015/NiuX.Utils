namespace NiuX.Extensions;

public static class FileExtensions
{
    /// <summary>
    /// 以文件流的形式复制大文件(异步方式)
    /// </summary>
    /// <param name="fs">源</param>
    /// <param name="destPath">目标地址</param>
    /// <param name="bufferSize">缓冲区大小，默认8MB</param>
    public static async Task CopyToFileAsync(this Stream fs, string destPath, int bufferSize = 1024 * 1024 * 8)
    {
        using var fsWrite = new FileStream(destPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        var buf = new byte[bufferSize];
        int len;

        while ((len = await fs.ReadAsync(buf, 0, buf.Length)) != 0) 
            await fsWrite.WriteAsync(buf, 0, len);
    }

    public static void AsOpen(this string filePath) => filePath.AsStartProccess();
}