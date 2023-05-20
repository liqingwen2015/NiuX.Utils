using NiuX.Excel.Abstractions;

namespace NiuX.Excel.Extensions;

/// <summary>
/// Excel Extensions
/// </summary>
public static class ExcelExtensions
{
    /// <summary>
    /// Ases the ep plus operation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    public static IEPPlusOperation<T> AsEPPlusOperation<T>(this List<T> data) => new EPPlusOperation<T>(data);
}