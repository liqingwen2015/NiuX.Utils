using NiuX.Excel.Abstractions;
using NiuX.Excel.Utils;

namespace NiuX.Excel.Extensions;

/// <summary>
/// Epplus Extensions
/// </summary>
public static class EpplusExtensions
{
    /// <summary>
    /// Gets the bytes asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data">The data.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <returns></returns>
    public static Task<byte[]> GetBytesAsync<T>(this List<T> data, string sheetName = "sheet")
    {
        return EpplusUtility.GetBytesAsync(data, sheetName);
    }

    /// <summary>
    /// Gets the bytes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data">The data.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <returns></returns>
    public static byte[] GetBytes<T>(this List<T> data, string sheetName = "sheet")
    {
        return EpplusUtility.GetBytes(data, sheetName);
    }

    /// <summary>
    /// Gets the bytes asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="operation">The operation.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <returns></returns>
    public static Task<byte[]> GetBytesAsync<T>(this IEPPlusOperation<T> operation, string sheetName = "sheet")
    {
        return EpplusUtility.GetBytesAsync(operation.Data, sheetName);
    }

    /// <summary>
    /// Gets the bytes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="operation">The operation.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <returns></returns>
    public static byte[] GetBytes<T>(this IEPPlusOperation<T> operation, string sheetName = "sheet")
    {
        return EpplusUtility.GetBytes(operation.Data, sheetName);
    }
}