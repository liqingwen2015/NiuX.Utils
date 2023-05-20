using System.Text;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace NiuX.Csv.Utils;

/// <summary>
/// Tiny Csv Utility
/// </summary>
public static class TinyCsvUtility
{
    /// <summary>
    /// Gets or sets the default CSV parser options.
    /// </summary>
    /// <value>
    /// The default CSV parser options.
    /// </value>
    public static CsvParserOptions DefaultCsvParserOptions { get; set; } = new(true, ',');

    /// <summary>
    /// Gets or sets the default encoding.
    /// </summary>
    /// <value>
    /// The default encoding.
    /// </value>
    public static Encoding DefaultEncoding { get; set; } = Encoding.UTF8;

    /// <summary>
    /// 读取并转换成 List
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath">The file path.</param>
    /// <param name="mapping">The mapping.</param>
    /// <returns></returns>
    /// <remarks>
    /// https://www.thecodebuzz.com/read-csv-file-in-net-core/
    /// </remarks>
    public static List<T> ReadAsList<T>(string filePath, CsvMapping<T> mapping) where T : class, new()
    {
        return new CsvParser<T>(DefaultCsvParserOptions, mapping).ReadFromFile(filePath, DefaultEncoding)
            .Select(x => x.Result).ToList();
    }
}