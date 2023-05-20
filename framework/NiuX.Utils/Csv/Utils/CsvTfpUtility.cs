using NotVisualBasic.FileIO;
using System.Data;
using System.Text.Json;

namespace NiuX.Csv.Utils;

/// <summary>
/// Csv Tfp Utility
/// </summary>
public static class CsvTfpUtility
{
    /// <summary>
    /// 读取，包含头部
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns></returns>
    /// <remarks>
    /// https://github.com/22222/CsvTextFieldParser
    /// </remarks>
    public static IEnumerable<IDictionary<string, string>> ReadWithHeader(string filePath)
    {
        using var csvReader = new StringReader(filePath);
        using var parser = new CsvTextFieldParser(csvReader);

        if (parser.EndOfData) yield break;

        string[] headerFields = parser.ReadFields();

        while (!parser.EndOfData)
        {
            string[] fields = parser.ReadFields();
            var fieldCount = Math.Min(headerFields.Length, fields.Length);
            IDictionary<string, string> fieldDictionary = new Dictionary<string, string>(fieldCount);

            for (var i = 0; i < fieldCount; i++)
            {
                var headerField = headerFields[i];
                fieldDictionary[headerField] = fields[i];
            }

            yield return fieldDictionary;
        }
    }

    /// <summary>
    /// Reads as json.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <returns></returns>
    public static string ReadAsJson(string filePath)
    {
        var csvData = new DataTable();

        try
        {
            using var csvReader = new CsvTextFieldParser(filePath);
            csvReader.SetDelimiter(',');
            csvReader.HasFieldsEnclosedInQuotes = true;
            var tableCreated = false;
            while (tableCreated == false)
            {
                string[] colFields = csvReader.ReadFields();
                foreach (var column in colFields)
                {
                    var datecolumn = new DataColumn(column);
                    datecolumn.AllowDBNull = true;
                    csvData.Columns.Add(datecolumn);
                }

                tableCreated = true;
            }

            while (!csvReader.EndOfData) csvData.Rows.Add(csvReader.ReadFields());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return "Error:Parsing CSV";
        }

        //if everything goes well, serialize csv to json
        return csvData.ToJson();
    }

    /// <summary>
    /// Reads as list.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath">The file path.</param>
    /// <returns></returns>
    public static List<T> ReadAsList<T>(string filePath)
    {
        return ReadAsJson(filePath).FromJson<List<T>>();
    }

    /// <summary>
    /// Reads as list with header.
    /// </summary>
    /// <param name="csvInput">The CSV input.</param>
    /// <returns></returns>
    public static List<IDictionary<string, string>> ReadAsListWithHeader(string csvInput)
    {
        return ReadWithHeader(csvInput).ToList();
    }

    /// <summary>
    /// Reads as array with header.
    /// </summary>
    /// <param name="csvInput">The CSV input.</param>
    /// <returns></returns>
    public static IDictionary<string, string>[] ReadAsArrayWithHeader(string csvInput)
    {
        return ReadWithHeader(csvInput).ToArray();
    }
}