using NiuX.Csv.Quantum;
using System.Data;
using TinyCsvParser.Mapping;

namespace NiuX.Csv.Utils;

/// <summary>
/// Csv 工具类
/// </summary>
public static class CsvUtils
{
    #region Read

    #region QuantumRead

    /// <summary>
    /// 量子阅读
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="hasheader">if set to <c>true</c> [hasheader].</param>
    /// <param name="delimiter">The delimiter.</param>
    /// <param name="mapper">The mapper.</param>
    /// <returns></returns>
    public static List<T> QuantumRead<T>(string path, bool hasheader, char delimiter, QuantumCsv.ToObject<T> mapper)
    {
        return QuantumCsv.Read(path, hasheader, delimiter, mapper);
    }

    /// <summary>
    /// 量子阅读
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="mapper">The mapper.</param>
    /// <returns></returns>
    public static List<T> QuantumRead<T>(string path, QuantumCsv.ToObject<T> mapper)
    {
        return QuantumRead(path, true, ',', mapper);
    }

    #endregion QuantumRead

    /// <summary>
    /// Reads as list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath">The file path.</param>
    /// <returns></returns>
    public static List<T> ReadAsList<T>(string filePath) where T : new()
    {
        return SimpleCsvUtility.Read<T>(filePath);
    }

    /// <summary>
    /// Reads as list with CSV helper.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath">The file path.</param>
    /// <returns></returns>
    public static List<T> ReadAsListWithCsvHelper<T>(string filePath)
    {
        return CsvHelperUtility.ReadAsList<T>(filePath);
    }

    /// <summary>
    /// Reads as list with CSV helper asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath">The file path.</param>
    /// <returns></returns>
    public static Task<List<T>> ReadAsListWithCsvHelperAsync<T>(string filePath)
    {
        return CsvHelperUtility.ReadAsListAsync<T>(filePath);
    }

    /// <summary>
    /// Reads as list with CSV TFP.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath">The file path.</param>
    /// <returns></returns>
    public static List<T> ReadAsListWithCsvTfp<T>(string filePath)
    {
        return CsvTfpUtility.ReadAsList<T>(filePath);
    }

    /// <summary>
    /// Reads as list with CSV TFP asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath">The file path.</param>
    /// <returns></returns>
    public static Task<List<T>> ReadAsListWithCsvTfpAsync<T>(string filePath)
    {
        return Task.Run(() => CsvTfpUtility.ReadAsList<T>(filePath));
    }

    /// <summary>
    /// Reads as list with tiny CSV.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath">The file path.</param>
    /// <param name="mapping">The mapping.</param>
    /// <returns></returns>
    public static List<T> ReadAsListWithTinyCsv<T>(string filePath, CsvMapping<T> mapping) where T : class, new()
    {
        return TinyCsvUtility.ReadAsList(filePath, mapping);
    }

    /// <summary>
    /// Reads as list with tiny CSV asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath">The file path.</param>
    /// <param name="mapping">The mapping.</param>
    /// <returns></returns>
    public static Task<List<T>> ReadAsListWithTinyCsvAsync<T>(string filePath, CsvMapping<T> mapping)
        where T : class, new()
    {
        return Task.Run(() => TinyCsvUtility.ReadAsList(filePath, mapping));
    }

    #endregion Read

    #region Write

    /// <summary>
    /// Writes the specified file path.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath">The file path.</param>
    /// <param name="items">The items.</param>
    public static void Write<T>(string filePath, IEnumerable<T> items) where T : new()
    {
        SimpleCsvUtility.Write(filePath, items);
    }

    /// <summary>
    /// Writes the specified file path.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="table">The table.</param>
    public static void Write(string filePath, DataTable table)
    {
        SimpleCsvUtility.Write(filePath, table);
    }

#if NETSTANDARD2_1_OR_GREATER
    public static Task WriteAsync<T>(string filePath, IEnumerable<T> items) where T : new() => SimpleCsvUtility.WriteAsync(filePath, items);

    public static Task WriteAsync(string filePath, DataTable table) => SimpleCsvUtility.WriteAsync(filePath, table);

#endif

    /// <summary>
    /// Writes the with CSV helper.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath">The file path.</param>
    /// <param name="items">The items.</param>
    public static void WriteWithCsvHelper<T>(string filePath, IEnumerable<T> items)
    {
        CsvHelperUtility.Write(filePath, items);
    }

    /// <summary>
    /// Writes the with CSV helper asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath">The file path.</param>
    /// <param name="items">The items.</param>
    /// <returns></returns>
    public static Task WriteWithCsvHelperAsync<T>(string filePath, IEnumerable<T> items)
    {
        return CsvHelperUtility.WriteAsync(filePath, items);
    }

    #endregion Write
}