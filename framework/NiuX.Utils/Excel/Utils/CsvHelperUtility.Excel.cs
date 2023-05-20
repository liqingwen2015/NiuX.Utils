using CsvHelper;
using CsvHelper.Excel;
using System.Globalization;
using NiuX.Extensions;
using NiuX.Consts;
using NiuX.Utils;

// ReSharper disable once CheckNamespace
namespace NiuX.Csv.Utils;

/// <summary>
/// Excel 助手
/// </summary>
public static partial class CsvHelperUtility
{
    /// <summary>
    /// Gets or sets the default excel postfix.
    /// </summary>
    /// <value>
    /// The default excel postfix.
    /// </value>
    public static string DefaultExcelPostfix { get; set; } = DefaultPostfixConsts.Excel;

    #region ReadExcelAsList

    /// <summary>
    /// Reads the excel as list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <returns></returns>
    public static List<T> ReadExcelAsList<T>(string path) => ReadExcel<T, List<T>>(path, x => x.ToList());

    /// <summary>
    /// Reads the excel as list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <returns></returns>
    public static List<T> ReadExcelAsList<T>(string path, string sheetName) => ReadExcel<T, List<T>>(path, sheetName, x => x.ToList());

    /// <summary>
    /// Reads the excel as list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    public static List<T> ReadExcelAsList<T>(string path, CultureInfo culture) => ReadExcel<T, List<T>>(path, culture, x => x.ToList());

    /// <summary>
    /// Reads the excel as list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    public static List<T> ReadExcelAsList<T>(string path, string sheetName, CultureInfo culture) => ReadExcel<T, List<T>>(path, sheetName, culture, x => x.ToList());

    /// <summary>
    /// Reads the excel as list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream">The stream.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="leaveOpen">if set to <c>true</c> [leave open].</param>
    /// <returns></returns>
    public static List<T> ReadExcelAsList<T>(Stream stream, CultureInfo culture, bool leaveOpen = false) => ReadExcel<T, List<T>>(stream, culture, leaveOpen, x => x.ToList());

    /// <summary>
    /// Reads the excel as list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream">The stream.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="leaveOpen">if set to <c>true</c> [leave open].</param>
    /// <returns></returns>
    public static List<T> ReadExcelAsList<T>(Stream stream, string sheetName, CultureInfo culture,
        bool leaveOpen = false) =>
        ReadExcel<T, List<T>>(stream, sheetName, culture, leaveOpen, x => x.ToList());

    #endregion ReadExcelAsList

    #region ReadExcelAsArray

    /// <summary>
    /// Reads the excel as array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <returns></returns>
    public static T[] ReadExcelAsArray<T>(string path) => ReadExcel<T, T[]>(path, x => x.ToArray());

    /// <summary>
    /// Reads the excel as array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <returns></returns>
    public static T[] ReadExcelAsArray<T>(string path, string sheetName)
    => ReadExcel<T, T[]>(path, sheetName, x => x.ToArray());

    /// <summary>
    /// Reads the excel as array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    public static T[] ReadExcelAsArray<T>(string path, CultureInfo culture)
    => ReadExcel<T, T[]>(path, culture, x => x.ToArray());

    /// <summary>
    /// Reads the excel as array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    public static T[] ReadExcelAsArray<T>(string path, string sheetName, CultureInfo culture)
    => ReadExcel<T, T[]>(path, sheetName, culture, x => x.ToArray());

    /// <summary>
    /// Reads the excel as array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream">The stream.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="leaveOpen">if set to <c>true</c> [leave open].</param>
    /// <returns></returns>
    public static T[] ReadExcelAsArray<T>(Stream stream, CultureInfo culture, bool leaveOpen = false)
    => ReadExcel<T, T[]>(stream, culture, leaveOpen, x => x.ToArray());

    /// <summary>
    /// Reads the excel as array.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream">The stream.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="leaveOpen">if set to <c>true</c> [leave open].</param>
    /// <returns></returns>
    public static T[] ReadExcelAsArray<T>(Stream stream, string sheetName, CultureInfo culture, bool leaveOpen = false)
    => ReadExcel<T, T[]>(stream, sheetName, culture, leaveOpen, x => x.ToArray());

    #endregion ReadExcelAsArray

    #region ReadExcel

    /// <summary>
    /// Reads the excel.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <param name="path">The path.</param>
    /// <param name="func">The function.</param>
    /// <returns></returns>
    private static TReturn ReadExcel<TItem, TReturn>(string path, Func<IEnumerable<TItem>, TReturn> func) => ReadExcel(new ExcelParser(FileHelper.FormatePostfix(path, DefaultExcelPostfix)), func);

    /// <summary>
    /// Reads the excel.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <param name="path">The path.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="func">The function.</param>
    /// <returns></returns>
    private static TReturn ReadExcel<TItem, TReturn>(string path, string sheetName,
        Func<IEnumerable<TItem>, TReturn> func)
    => ReadExcel(new ExcelParser(FileHelper.FormatePostfix(path, DefaultExcelPostfix), sheetName), func);

    /// <summary>
    /// Reads the excel.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <param name="path">The path.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="func">The function.</param>
    /// <returns></returns>
    private static TReturn ReadExcel<TItem, TReturn>(string path, CultureInfo culture,
        Func<IEnumerable<TItem>, TReturn> func)
    => ReadExcel(new ExcelParser(FileHelper.FormatePostfix(path, DefaultExcelPostfix), culture), func);

    /// <summary>
    /// Reads the excel.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <param name="path">The path.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="func">The function.</param>
    /// <returns></returns>
    private static TReturn ReadExcel<TItem, TReturn>(string path, string sheetName, CultureInfo culture,
        Func<IEnumerable<TItem>, TReturn> func)
    => ReadExcel(new ExcelParser(FileHelper.FormatePostfix(path, DefaultExcelPostfix), sheetName, culture), func);

    /// <summary>
    /// Reads the excel.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <param name="stream">The stream.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="leaveOpen">if set to <c>true</c> [leave open].</param>
    /// <param name="func">The function.</param>
    /// <returns></returns>
    private static TReturn ReadExcel<TItem, TReturn>(Stream stream, CultureInfo culture, bool leaveOpen,
        Func<IEnumerable<TItem>, TReturn> func)
    => ReadExcel(new ExcelParser(stream, culture, leaveOpen), func);

    /// <summary>
    /// Reads the excel.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <param name="stream">The stream.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="leaveOpen">if set to <c>true</c> [leave open].</param>
    /// <param name="func">The function.</param>
    /// <returns></returns>
    private static TReturn ReadExcel<TItem, TReturn>(Stream stream, string sheetName, CultureInfo culture,
        bool leaveOpen, Func<IEnumerable<TItem>, TReturn> func)
    => ReadExcel(new ExcelParser(stream, sheetName, culture, leaveOpen), func);

    /// <summary>
    /// Reads the excel.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <param name="excelParser">The excel parser.</param>
    /// <param name="func">The function.</param>
    /// <returns></returns>
    private static TReturn ReadExcel<TItem, TReturn>(ExcelParser excelParser, Func<IEnumerable<TItem>, TReturn> func)
    {
        using var reader = excelParser;
        using var csv = new CsvReader(reader);
        return func(csv.GetRecords<TItem>());
    }

    #endregion ReadExcel

    #region ReadExcelAsListAsync

    /// <summary>
    /// Reads the excel as list asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <returns></returns>
    public static async Task<List<T>> ReadExcelAsListAsync<T>(string path) => await ReadExcelAsync<T, List<T>>(path, x => x.ToList());

    /// <summary>
    /// Reads the excel as list asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <returns></returns>
    public static async Task<List<T>> ReadExcelAsListAsync<T>(string path, string sheetName) => await ReadExcelAsync<T, List<T>>(path, sheetName, x => x.ToList());

    /// <summary>
    /// Reads the excel as list asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    public static async Task<List<T>> ReadExcelAsListAsync<T>(string path, CultureInfo culture) => await ReadExcelAsync<T, List<T>>(path, culture, x => x.ToList());

    /// <summary>
    /// Reads the excel as list asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    public static async Task<List<T>> ReadExcelAsListAsync<T>(string path, string sheetName, CultureInfo culture) => await ReadExcelAsync<T, List<T>>(path, sheetName, culture, x => x.ToList());

    /// <summary>
    /// Reads the excel as list asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream">The stream.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="leaveOpen">if set to <c>true</c> [leave open].</param>
    /// <returns></returns>
    public static async Task<List<T>> ReadExcelAsListAsync<T>(Stream stream, CultureInfo culture,
        bool leaveOpen = false) =>
        await ReadExcelAsync<T, List<T>>(stream, culture, leaveOpen, x => x.ToList());

    /// <summary>
    /// Reads the excel as list asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream">The stream.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="leaveOpen">if set to <c>true</c> [leave open].</param>
    /// <returns></returns>
    public static async Task<List<T>> ReadExcelAsListAsync<T>(Stream stream, string sheetName, CultureInfo culture,
        bool leaveOpen = false) =>
        await ReadExcelAsync<T, List<T>>(stream, sheetName, culture, leaveOpen, x => x.ToList());

    #endregion ReadExcelAsListAsync

    #region ReadExcelAsArrayAsync

    /// <summary>
    /// Reads the excel as array asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <returns></returns>
    public static async Task<T[]> ReadExcelAsArrayAsync<T>(string path) => await ReadExcelAsync<T, T[]>(path, x => x.ToArray());

    /// <summary>
    /// Reads the excel as array asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <returns></returns>
    public static async Task<T[]> ReadExcelAsArrayAsync<T>(string path, string sheetName) => await ReadExcelAsync<T, T[]>(path, sheetName, x => x.ToArray());

    /// <summary>
    /// Reads the excel as array asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    public static async Task<T[]> ReadExcelAsArrayAsync<T>(string path, CultureInfo culture) => await ReadExcelAsync<T, T[]>(path, culture, x => x.ToArray());

    /// <summary>
    /// Reads the excel as array asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    public static async Task<T[]> ReadExcelAsArrayAsync<T>(string path, string sheetName, CultureInfo culture) => await ReadExcelAsync<T, T[]>(path, sheetName, culture, x => x.ToArray());

    /// <summary>
    /// Reads the excel as array asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream">The stream.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="leaveOpen">if set to <c>true</c> [leave open].</param>
    /// <returns></returns>
    public static async Task<T[]> ReadExcelAsArrayAsync<T>(Stream stream, CultureInfo culture, bool leaveOpen = false) => await ReadExcelAsync<T, T[]>(stream, culture, leaveOpen, x => x.ToArray());

    /// <summary>
    /// Reads the excel as array asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream">The stream.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="leaveOpen">if set to <c>true</c> [leave open].</param>
    /// <returns></returns>
    public static async Task<T[]> ReadExcelAsArrayAsync<T>(Stream stream, string sheetName, CultureInfo culture,
        bool leaveOpen = false) =>
        await ReadExcelAsync<T, T[]>(stream, sheetName, culture, leaveOpen, x => x.ToArray());

    #endregion ReadExcelAsArrayAsync

    #region ReadExcelAsync

    /// <summary>
    /// Reads the excel asynchronous.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <param name="path">The path.</param>
    /// <param name="func">The function.</param>
    /// <returns></returns>
    private static async Task<TReturn> ReadExcelAsync<TItem, TReturn>(string path,
        Func<IEnumerable<TItem>, TReturn> func) =>
        await ReadExcelAsync(new ExcelParser(FileHelper.FormatePostfix(path, DefaultExcelPostfix)), func);

    /// <summary>
    /// Reads the excel asynchronous.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <param name="path">The path.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="func">The function.</param>
    /// <returns></returns>
    private static async Task<TReturn> ReadExcelAsync<TItem, TReturn>(string path, string sheetName,
        Func<IEnumerable<TItem>, TReturn> func)
    => await ReadExcelAsync(new ExcelParser(FileHelper.FormatePostfix(path, DefaultExcelPostfix), sheetName), func);

    /// <summary>
    /// Reads the excel asynchronous.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <param name="path">The path.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="func">The function.</param>
    /// <returns></returns>
    private static async Task<TReturn> ReadExcelAsync<TItem, TReturn>(string path, CultureInfo culture,
        Func<IEnumerable<TItem>, TReturn> func)
    => await ReadExcelAsync(new ExcelParser(FileHelper.FormatePostfix(path, DefaultExcelPostfix), culture), func);

    /// <summary>
    /// Reads the excel asynchronous.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <param name="path">The path.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="func">The function.</param>
    /// <returns></returns>
    private static async Task<TReturn> ReadExcelAsync<TItem, TReturn>(string path, string sheetName,
        CultureInfo culture, Func<IEnumerable<TItem>, TReturn> func)
    => await ReadExcelAsync(new ExcelParser(FileHelper.FormatePostfix(path, DefaultExcelPostfix), sheetName, culture), func);

    /// <summary>
    /// Reads the excel asynchronous.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <param name="stream">The stream.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="leaveOpen">if set to <c>true</c> [leave open].</param>
    /// <param name="func">The function.</param>
    /// <returns></returns>
    private static async Task<TReturn> ReadExcelAsync<TItem, TReturn>(Stream stream, CultureInfo culture,
        bool leaveOpen, Func<IEnumerable<TItem>, TReturn> func)
    => await ReadExcelAsync(new ExcelParser(stream, culture, leaveOpen), func);

    /// <summary>
    /// Reads the excel asynchronous.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <param name="stream">The stream.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="leaveOpen">if set to <c>true</c> [leave open].</param>
    /// <param name="func">The function.</param>
    /// <returns></returns>
    private static async Task<TReturn> ReadExcelAsync<TItem, TReturn>(Stream stream, string sheetName,
        CultureInfo culture, bool leaveOpen, Func<IEnumerable<TItem>, TReturn> func)
    => await ReadExcelAsync(new ExcelParser(stream, sheetName, culture, leaveOpen), func);

    /// <summary>
    /// Reads the excel asynchronous.
    /// </summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <typeparam name="TReturn">The type of the return.</typeparam>
    /// <param name="excelParser">The excel parser.</param>
    /// <param name="func">The function.</param>
    /// <returns></returns>
    private static async Task<TReturn> ReadExcelAsync<TItem, TReturn>(ExcelParser excelParser,
        Func<List<TItem>, TReturn> func)
    {
        using var reader = excelParser;
        using var csv = new CsvReader(reader);
        var list = new List<TItem>();
        await foreach (var item in csv.GetRecordsAsync<TItem>()) list.Add(item);
        return func(list);
    }

    #endregion ReadExcelAsync

    #region WriteToExcel

    /// <summary>
    /// Writes to excel.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="records">The records.</param>
    public static void WriteToExcel<T>(string path, IEnumerable<T> records) => WriteToExcel(records, () => new ExcelWriter(path));

    /// <summary>
    /// Writes to excel.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="records">The records.</param>
    /// <param name="culture">The culture.</param>
    public static void WriteToExcel<T>(string path, IEnumerable<T> records, CultureInfo culture) => WriteToExcel(records, () => new ExcelWriter(path, culture));

    /// <summary>
    /// Writes to excel.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="records">The records.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    public static void WriteToExcel<T>(string path, IEnumerable<T> records, string sheetName) => WriteToExcel(records, () => new ExcelWriter(path, sheetName));

    /// <summary>
    /// Writes to excel.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="records">The records.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="culture">The culture.</param>
    public static void WriteToExcel<T>(string path, IEnumerable<T> records, string sheetName, CultureInfo culture)
    => WriteToExcel(records, () => new ExcelWriter(path, sheetName, culture));

    /// <summary>
    /// Writes to excel.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream">The stream.</param>
    /// <param name="records">The records.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="leaveOpen">if set to <c>true</c> [leave open].</param>
    public static void WriteToExcel<T>(Stream stream, IEnumerable<T> records, CultureInfo culture,
        bool leaveOpen = false) =>
        WriteToExcel(records, () => new ExcelWriter(stream, culture, leaveOpen));

    /// <summary>
    /// Writes to excel.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream">The stream.</param>
    /// <param name="records">The records.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="leaveOpen">if set to <c>true</c> [leave open].</param>
    public static void WriteToExcel<T>(Stream stream, IEnumerable<T> records, string sheetName, CultureInfo culture,
        bool leaveOpen = false)
    => WriteToExcel(records, () => new ExcelWriter(stream, sheetName, culture, leaveOpen));

    /// <summary>
    /// Writes to excel.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="records">The records.</param>
    /// <param name="func">The function.</param>
    private static void WriteToExcel<T>(IEnumerable<T> records, Func<ExcelWriter> func)
    {
        using var writeToExcelr = func();
        writeToExcelr.WriteRecords(records);
    }

    #endregion WriteToExcel

    #region WriteToExcelAsync

    /// <summary>
    /// Writes to excel asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="records">The records.</param>
    public static async Task WriteToExcelAsync<T>(string path, IEnumerable<T> records) => await WriteToExcelAsync(records, () => new ExcelWriter(path));

    /// <summary>
    /// Writes to excel asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="records">The records.</param>
    /// <param name="culture">The culture.</param>
    public static async Task WriteToExcelAsync<T>(string path, IEnumerable<T> records, CultureInfo culture) => await WriteToExcelAsync(records, () => new ExcelWriter(path, culture));

    /// <summary>
    /// Writes to excel asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="records">The records.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    public static async Task WriteToExcelAsync<T>(string path, IEnumerable<T> records, string sheetName) => await WriteToExcelAsync(records, () => new ExcelWriter(path, sheetName));

    /// <summary>
    /// Writes to excel asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="records">The records.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="culture">The culture.</param>
    public static async Task WriteToExcelAsync<T>(string path, IEnumerable<T> records, string sheetName,
        CultureInfo culture) =>
        await WriteToExcelAsync(records, () => new ExcelWriter(path, sheetName, culture));

    /// <summary>
    /// Writes to excel asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream">The stream.</param>
    /// <param name="records">The records.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="leaveOpen">if set to <c>true</c> [leave open].</param>
    public static async Task WriteToExcelAsync<T>(Stream stream, IEnumerable<T> records, CultureInfo culture,
        bool leaveOpen = false) =>
        await WriteToExcelAsync(records, () => new ExcelWriter(stream, culture, leaveOpen));

    /// <summary>
    /// Writes to excel asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="stream">The stream.</param>
    /// <param name="records">The records.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="leaveOpen">if set to <c>true</c> [leave open].</param>
    public static async Task WriteToExcelAsync<T>(Stream stream, IEnumerable<T> records, string sheetName,
        CultureInfo culture, bool leaveOpen = false) =>
        await WriteToExcelAsync(records, () => new ExcelWriter(stream, sheetName, culture, leaveOpen));

    /// <summary>
    /// Writes to excel asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="records">The records.</param>
    /// <param name="func">The function.</param>
    private static async Task WriteToExcelAsync<T>(IEnumerable<T> records, Func<ExcelWriter> func)
    {
        using var writeToExcelr = func();
        await writeToExcelr.WriteRecordsAsync(records);
    }

    #endregion WriteToExcelAsync
}

public static partial class CsvHelperUtility
{
    /// <summary>
    /// 写入并打开
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="records">The records.</param>
    public static async Task WriteToExcelAndOpenAsync<T>(string path, IEnumerable<T> records)
    {
        await WriteToExcelAsync(path, records);
        path.AsOpen();
    }
}