using System.Globalization;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;

namespace NiuX.Csv.Utils;

/// <summary>
/// Csv 文件助手
/// </summary>
/// <remarks>因为 CsvHelper 是第三方类库的命名空间，所以进行了改名</remarks>
public static partial class CsvHelperUtility
{
    /// <summary>
    /// 默认编码
    /// </summary>
    public static readonly Encoding DefaultEncoding = Encoding.UTF8;

    /// <summary>
    /// 默认配置
    /// </summary>
    public static readonly CsvConfiguration DefaultConfiguration = new(CultureInfo.InvariantCulture)
    {
        HeaderValidated = null,
        MissingFieldFound = null
    };

    /// <summary>
    /// 默认配置
    /// </summary>
    public static CsvConfiguration CreateConfiguration(CultureInfo cultureInfo)
    {
        return new CsvConfiguration(cultureInfo)
        {
            HeaderValidated = null,
            MissingFieldFound = null
        };
    }

    /// <summary>
    /// 是否 csv 文件
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private static void ValidateCsvFormate(string filePath)
    {
        if (!filePath.EndsWith(".csv") && !filePath.EndsWith(".txt")) throw new Exception("只支持 .csv、.txt 文件");
    }

    #region ReadAsList

    public static List<T> ReadAsList<T>(string filePath)
    {
        return ReadAsList<T>(filePath, DefaultEncoding);
    }

    public static List<T> ReadAsList<T>(string filePath, Encoding encoding)
    {
        return Read<T, List<T>>(filePath, x => x.ToList(), encoding);
    }

    public static List<T> ReadAsList<T>(TextReader textReader, CultureInfo culture)
    {
        return Read<T, List<T>>(textReader, culture, x => x.ToList());
    }

    public static List<T> ReadAsList<T>(TextReader textReader, CsvConfiguration configuration)
    {
        return Read<T, List<T>>(textReader, configuration, x => x.ToList());
    }

    #endregion ReadAsList

    #region ReadAsArray

    public static T[] ReadAsArray<T>(string filePath)
    {
        return Read<T, T[]>(filePath, x => x.ToArray(), DefaultEncoding);
    }

    public static T[] ReadAsArray<T>(string filePath, Encoding encoding)
    {
        return Read<T, T[]>(filePath, x => x.ToArray(), encoding);
    }

    public static T[] ReadAsArray<T>(TextReader textReader, CultureInfo culture)
    {
        return Read<T, T[]>(textReader, culture, x => x.ToArray());
    }

    public static T[] ReadAsArray<T>(TextReader textReader, CsvConfiguration configuration)
    {
        return Read<T, T[]>(textReader, configuration, x => x.ToArray());
    }

    #endregion ReadAsArray

    #region Read

    private static TReturn Read<TItem, TReturn>(string filePath, Func<IEnumerable<TItem>, TReturn> func,
        Encoding encoding)
    {
        ValidateCsvFormate(filePath);
        using var reader = new StreamReader(filePath, encoding);
        using var csv = new CsvReader(reader, DefaultConfiguration);
        return func(csv.GetRecords<TItem>());
    }

    private static TReturn Read<TItem, TReturn>(TextReader textReader, CultureInfo culture,
        Func<IEnumerable<TItem>, TReturn> func)
    {
        using var reader = textReader;
        using var csv = new CsvReader(reader, CreateConfiguration(culture));
        return func(csv.GetRecords<TItem>());
    }

    private static TReturn Read<TItem, TReturn>(TextReader textReader, CsvConfiguration configuration,
        Func<IEnumerable<TItem>, TReturn> func)
    {
        using var reader = textReader;
        using var csv = new CsvReader(reader, configuration);
        return func(csv.GetRecords<TItem>());
    }

    #endregion Read

    #region ReadAsListAsync

    public static Task<List<T>> ReadAsListAsync<T>(string filePath)
    {
        return ReadAsync<T, List<T>>(filePath, x => x);
    }

    public static Task<List<T>> ReadAsListAsync<T>(string filePath, Encoding encoding)
    {
        return ReadAsync<T, List<T>>(filePath, encoding, x => x);
    }

    public static Task<List<T>> ReadAsListAsync<T>(TextReader textReader, CultureInfo culture)
    {
        return ReadAsync<T, List<T>>(textReader, culture, x => x);
    }

    public static Task<List<T>> ReadAsListAsync<T>(TextReader textReader, CsvConfiguration configuration)
    {
        return ReadAsync<T, List<T>>(textReader, configuration, x => x);
    }

    #endregion ReadAsListAsync

    #region ReadAsArrayAsync

    public static Task<T[]> ReadAsArrayAsync<T>(string filePath)
    {
        return ReadAsync<T, T[]>(filePath, x => x.ToArray());
    }

    public static Task<T[]> ReadAsArrayAsync<T>(string filePath, Encoding encoding)
    {
        return ReadAsync<T, T[]>(filePath, encoding, x => x.ToArray());
    }

    public static Task<T[]> ReadAsArrayAsync<T>(TextReader textReader, CultureInfo culture)
    {
        return ReadAsync<T, T[]>(textReader, culture, x => x.ToArray());
    }

    public static Task<T[]> ReadAsArrayAsync<T>(TextReader textReader, CsvConfiguration configuration)
    {
        return ReadAsync<T, T[]>(textReader, configuration, x => x.ToArray());
    }

    #endregion ReadAsArrayAsync

    #region ReadAsync

    private static Task<TReturn> ReadAsync<TItem, TReturn>(string filePath, Func<List<TItem>, TReturn> func)
    {
        return ReadAsync(filePath, DefaultEncoding, func);
    }

    private static async Task<TReturn> ReadAsync<TItem, TReturn>(string filePath, Encoding encoding,
        Func<List<TItem>, TReturn> func)
    {
        using var reader = new StreamReader(filePath, encoding);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var list = new List<TItem>();
        await foreach (var item in csv.GetRecordsAsync<TItem>()) list.Add(item);
        return func(list);
    }

    private static async Task<TReturn> ReadAsync<TItem, TReturn>(TextReader textReader, CultureInfo culture,
        Func<List<TItem>, TReturn> func)
    {
        using var reader = textReader;
        using var csv = new CsvReader(reader, culture);
        var list = new List<TItem>();
        await foreach (var item in csv.GetRecordsAsync<TItem>()) list.Add(item);
        return func(list);
    }

    private static async Task<TReturn> ReadAsync<TItem, TReturn>(TextReader textReader, CsvConfiguration configuration,
        Func<List<TItem>, TReturn> func)
    {
        using var reader = textReader;
        using var csv = new CsvReader(reader, configuration);
        var list = new List<TItem>();
        await foreach (var item in csv.GetRecordsAsync<TItem>()) list.Add(item);
        return func(list);
    }

    #endregion ReadAsync

    #region WriteByteArray

    public static void Write<T>(string filePath, IEnumerable<T> records)
    {
        Write(filePath, records, DefaultEncoding);
    }

    public static void Write<T>(string filePath, IEnumerable<T> records, Encoding encoding)
    {
        Write(records, () => new StreamWriter(filePath, false, encoding));
    }

    private static void Write<T>(IEnumerable<T> records, Func<StreamWriter> func)
    {
        using var writer = func();
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        csv.WriteRecords(records);
    }

    #endregion WriteByteArray

    #region WriteByteArrayAsync

    public static Task WriteAsync<T>(string filePath, IEnumerable<T> records)
    {
        return WriteAsync(filePath, records, DefaultEncoding);
    }

    public static Task WriteAsync<T>(string filePath, IEnumerable<T> records, Encoding encoding)
    {
        return WriteAsync(records, () => new StreamWriter(filePath, false, encoding));
    }

    private static async Task WriteAsync<T>(IEnumerable<T> records, Func<StreamWriter> func)
    {
        using var writer = func();
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        await csv.WriteRecordsAsync(records);
    }

    #endregion WriteByteArrayAsync
}