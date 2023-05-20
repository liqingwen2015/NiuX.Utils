using NiuX.Extensions;
using System.Data;
using CsvHelperUtility = NiuX.Csv.Utils.CsvHelperUtility;

namespace NiuX.Excel.Utils;

/// <summary>
/// Excel 工具
/// </summary>
public static class ExcelUtils
{
    /// <summary>
    /// 生成临时文件名，不是文件，是名称
    /// </summary>
    /// <returns></returns>
    public static string GenerateTempFileName() => "_tmp_" + DateTime.Now.ToString("yyyyMMdd_HHmmss_fff") + ".xlsx";

    private static void CreateDirectoryIfNotExists(string filePath)
    {
        var directory = new FileInfo(filePath).Directory!.FullName;

        if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);
    }

    #region Read

    public static List<T> ReadAsListWithCsvHelper<T>(string filePath) => CsvHelperUtility.ReadExcelAsList<T>(filePath);

    public static Task<List<T>> ReadAsListWithCsvHelperAsync<T>(string filePath) => CsvHelperUtility.ReadExcelAsListAsync<T>(filePath);

    public static List<T> ReadAsListWithEPPlus<T>(string filePath) => EpplusUtility.ReadExcel<List<T>>(filePath);

    public static Task<List<T>> ReadAsListWithEPPlusAsync<T>(string filePath)
    => EpplusUtility.ReadAsync<List<T>>(filePath);

    public static List<T> ReadAsListWithMiniExcel<T>(string filePath) where T : class, new() => MiniExcelUtility.Query<T>(filePath).ToList();

    public static async Task<List<T>> ReadAsListWithMiniExcelAsync<T>(string filePath) where T : class, new() => (await MiniExcelUtility.QueryAsync<T>(filePath)).ToList();

    #endregion Read

    #region Write

    #region CsvHelper

    public static void WriteWithCsvHelper<T>(string filePath, List<T> items) => CsvHelperUtility.Write(filePath, items);

    public static void WriteWithCsvHelper<T>(string filePath, IEnumerable<T> items) => CsvHelperUtility.Write(filePath, items);

    public static Task WriteWithCsvHelperAsync<T>(string filePath, List<T> items) => CsvHelperUtility.WriteAsync(filePath, items);

    public static Task WriteWithCsvHelperAsync<T>(string filePath, IEnumerable<T> items) => CsvHelperUtility.WriteAsync(filePath, items);

    #endregion CsvHelper

    #region Epplus

    public static void WriteWithEpplus(string filePath, DataTable items) => EpplusUtility.Write(filePath, items);

    public static void WriteWithEpplus<T>(string filePath, List<T> items) => EpplusUtility.Write(filePath, items);

    public static void WriteWithEpplus<T>(string filePath, IEnumerable<T> items) => EpplusUtility.Write(filePath, items);

    public static Task WriteWithEpplusAsync(string filePath, DataTable items) => EpplusUtility.WriteAsync(filePath, items);

    public static Task WriteWithEpplusAsync<T>(string filePath, List<T> items) => EpplusUtility.WriteAsync(filePath, items);

    public static Task WriteWithEpplusAsync<T>(string filePath, IEnumerable<T> items) => EpplusUtility.WriteAsync(filePath, items);

    #endregion Epplus

    #region MiniExcel

    public static void WriteWithMiniExcel<T>(string filePath, List<T> items) => MiniExcelUtility.SaveAs(filePath, items, overwriteFile: true);

    public static void WriteWithMiniExcel<T>(string filePath, IEnumerable<T> items) => MiniExcelUtility.SaveAs(filePath, items, overwriteFile: true);

    public static Task WriteWithMiniExcelAsync<T>(string filePath, List<T> items) => MiniExcelUtility.SaveAsAsync(filePath, items, overwriteFile: true);

    public static Task WriteWithMiniExcelAsync<T>(string filePath, IEnumerable<T> items) => MiniExcelUtility.SaveAsAsync(filePath, items, overwriteFile: true);

    #endregion MiniExcel

    #endregion Write

    #region WriteAndOpen

    public static void WriteAndOpenWithEpplus<T>(List<T> items) => WriteAndOpenWithEpplus(GenerateTempFileName(), items);

    public static void WriteAndOpenWithEpplus<T>(string filePath, List<T> items)
    {
        EpplusUtility.Write(filePath, items);
        filePath.AsOpen();
    }

    public static void WriteAndOpenWithEpplus<T>(IEnumerable<T> items)
    => WriteAndOpenWithEpplus(GenerateTempFileName(), items);

    public static void WriteAndOpenWithEpplus<T>(string filePath, IEnumerable<T> items)
    {
        EpplusUtility.Write(filePath, items);
        filePath.AsOpen();
    }

    public static Task WriteAndOpenWithEpplusAsync<T>(List<T> items) => WriteAndOpenWithEpplusAsync(GenerateTempFileName(), items);

    public static async Task WriteAndOpenWithEpplusAsync<T>(string filePath, List<T> items)
    {
        await EpplusUtility.WriteAsync(filePath, items);
        filePath.AsOpen();
    }

    public static Task WriteAndOpenWithEpplusAsync<T>(IEnumerable<T> items)
    => WriteAndOpenWithEpplusAsync(GenerateTempFileName(), items);

    public static async Task WriteAndOpenWithEpplusAsync<T>(string filePath, IEnumerable<T> items)
    {
        await EpplusUtility.WriteAsync(filePath, items);
        filePath.AsOpen();
    }

    #endregion WriteAndOpen
}