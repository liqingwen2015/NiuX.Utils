using MiniExcelLibs;
using System.Data;

namespace NiuX.Excel.Utils;

/// <summary>
/// Mini Excel Utility
/// </summary>
public static class MiniExcelUtility
{
    /// <summary>
    /// Saves as asynchronous.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="value">The value.</param>
    /// <param name="printHeader">if set to <c>true</c> [print header].</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="excelType">Type of the excel.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="overwriteFile">if set to <c>true</c> [overwrite file].</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    public static Task SaveAsAsync(
        string path,
        object value,
        bool printHeader = true,
        string sheetName = "Sheet1",
        ExcelType excelType = ExcelType.UNKNOWN,
        IConfiguration configuration = null,
        bool overwriteFile = false,
        CancellationToken cancellationToken = default)
    {
        return MiniExcel.SaveAsAsync(path, value, printHeader, sheetName, excelType, configuration, overwriteFile,
            cancellationToken);
    }

    /// <summary>
    /// Queries the asynchronous.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="useHeaderRow">if set to <c>true</c> [use header row].</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="excelType">Type of the excel.</param>
    /// <param name="startCell">The start cell.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static Task<IEnumerable<object>> QueryAsync(
        string path,
        bool useHeaderRow = false,
        string sheetName = null,
        ExcelType excelType = ExcelType.UNKNOWN,
        string startCell = "A1",
        IConfiguration configuration = null)
    {
        return MiniExcel.QueryAsync(path, useHeaderRow, sheetName, excelType, startCell, configuration);
    }

    /// <summary>
    /// Queries the asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="excelType">Type of the excel.</param>
    /// <param name="startCell">The start cell.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static Task<IEnumerable<T>> QueryAsync<T>(
        string path,
        string sheetName = null,
        ExcelType excelType = ExcelType.UNKNOWN,
        string startCell = "A1",
        IConfiguration configuration = null)
        where T : class, new()
    {
        return MiniExcel.QueryAsync<T>(path, sheetName, excelType, startCell, configuration);
    }

    /// <summary>
    /// Saves as by template asynchronous.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="templatePath">The template path.</param>
    /// <param name="value">The value.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static Task SaveAsByTemplateAsync(
        string path,
        string templatePath,
        object value,
        IConfiguration configuration = null)
    {
        return MiniExcel.SaveAsByTemplateAsync(path, templatePath, value, configuration);
    }

    /// <summary>
    /// Saves as by template asynchronous.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="templateBytes">The template bytes.</param>
    /// <param name="value">The value.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static Task SaveAsByTemplateAsync(
        string path,
        byte[] templateBytes,
        object value,
        IConfiguration configuration = null)
    {
        return MiniExcel.SaveAsByTemplateAsync(path, templateBytes, value, configuration);
    }

    /// <summary>
    /// Queries as data table asynchronous.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="useHeaderRow">if set to <c>true</c> [use header row].</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="excelType">Type of the excel.</param>
    /// <param name="startCell">The start cell.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static Task<DataTable> QueryAsDataTableAsync(
        string path,
        bool useHeaderRow = true,
        string sheetName = null,
        ExcelType excelType = ExcelType.UNKNOWN,
        string startCell = "A1",
        IConfiguration configuration = null)
    {
        return MiniExcel.QueryAsDataTableAsync(path, useHeaderRow, sheetName, excelType, startCell, configuration);
    }

    /// <summary>
    /// Gets the reader.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="useHeaderRow">if set to <c>true</c> [use header row].</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="excelType">Type of the excel.</param>
    /// <param name="startCell">The start cell.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static MiniExcelDataReader GetReader(
        string path,
        bool useHeaderRow = false,
        string sheetName = null,
        ExcelType excelType = ExcelType.UNKNOWN,
        string startCell = "A1",
        IConfiguration configuration = null)
    {
        return MiniExcel.GetReader(path, useHeaderRow, sheetName, excelType, startCell, configuration);
    }

    /// <summary>
    /// Saves as.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="value">The value.</param>
    /// <param name="printHeader">if set to <c>true</c> [print header].</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="excelType">Type of the excel.</param>
    /// <param name="configuration">The configuration.</param>
    /// <param name="overwriteFile">if set to <c>true</c> [overwrite file].</param>
    public static void SaveAs(
        string path,
        object value,
        bool printHeader = true,
        string sheetName = "Sheet1",
        ExcelType excelType = ExcelType.UNKNOWN,
        IConfiguration configuration = null, bool overwriteFile = false)
    {
        MiniExcel.SaveAs(path, value, printHeader, sheetName, excelType, configuration, overwriteFile);
    }

    /// <summary>
    /// Queries the specified path.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="excelType">Type of the excel.</param>
    /// <param name="startCell">The start cell.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static IEnumerable<T> Query<T>(
        string path,
        string sheetName = null,
        ExcelType excelType = ExcelType.UNKNOWN,
        string startCell = "A1",
        IConfiguration configuration = null)
        where T : class, new()
    {
        return MiniExcel.Query<T>(path, sheetName, excelType, startCell, configuration);
    }

    /// <summary>
    /// Queries the specified path.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="useHeaderRow">if set to <c>true</c> [use header row].</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="excelType">Type of the excel.</param>
    /// <param name="startCell">The start cell.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static IEnumerable<object> Query(
        string path,
        bool useHeaderRow = false,
        string sheetName = null,
        ExcelType excelType = ExcelType.UNKNOWN,
        string startCell = "A1",
        IConfiguration configuration = null)
    {
        return MiniExcel.Query(path, useHeaderRow, sheetName, excelType, startCell, configuration);
    }

    /// <summary>
    /// Saves as by template.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="templatePath">The template path.</param>
    /// <param name="value">The value.</param>
    /// <param name="configuration">The configuration.</param>
    public static void SaveAsByTemplate(
        string path,
        string templatePath,
        object value,
        IConfiguration configuration = null)
    {
        MiniExcel.SaveAsByTemplate(path, templatePath, value, configuration);
    }

    /// <summary>
    /// Saves as by template.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="templateBytes">The template bytes.</param>
    /// <param name="value">The value.</param>
    /// <param name="configuration">The configuration.</param>
    public static void SaveAsByTemplate(
        string path,
        byte[] templateBytes,
        object value,
        IConfiguration configuration = null)
    {
        MiniExcel.SaveAsByTemplate(path, templateBytes, value, configuration);
    }

    /// <summary>
    /// Queries as data table.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="useHeaderRow">if set to <c>true</c> [use header row].</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="excelType">Type of the excel.</param>
    /// <param name="startCell">The start cell.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static DataTable QueryAsDataTable(
        string path,
        bool useHeaderRow = true,
        string sheetName = null,
        ExcelType excelType = ExcelType.UNKNOWN,
        string startCell = "A1",
        IConfiguration configuration = null)
    {
        return MiniExcel.QueryAsDataTable(path, useHeaderRow, sheetName, excelType, startCell, configuration);
    }

    /// <summary>
    /// Gets the sheet names.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <returns></returns>
    public static List<string> GetSheetNames(string path)
    {
        return MiniExcel.GetSheetNames(path);
    }

    /// <summary>
    /// Gets the columns.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="useHeaderRow">if set to <c>true</c> [use header row].</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="excelType">Type of the excel.</param>
    /// <param name="startCell">The start cell.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns></returns>
    public static ICollection<string> GetColumns(
        string path,
        bool useHeaderRow = false,
        string sheetName = null,
        ExcelType excelType = ExcelType.UNKNOWN,
        string startCell = "A1",
        IConfiguration configuration = null)
    {
        return MiniExcel.GetColumns(path, useHeaderRow, sheetName, excelType, startCell, configuration);
    }

    /// <summary>
    /// Converts the CSV to XLSX.
    /// </summary>
    /// <param name="csv">The CSV.</param>
    /// <param name="xlsx">The XLSX.</param>
    public static void ConvertCsvToXlsx(string csv, string xlsx)
    {
        MiniExcel.ConvertCsvToXlsx(csv, xlsx);
    }

    /// <summary>
    /// Converts the CSV to XLSX.
    /// </summary>
    /// <param name="csv">The CSV.</param>
    /// <param name="xlsx">The XLSX.</param>
    public static void ConvertCsvToXlsx(Stream csv, Stream xlsx)
    {
        MiniExcel.ConvertCsvToXlsx(csv, xlsx);
    }

    /// <summary>
    /// Converts the XLSX to CSV.
    /// </summary>
    /// <param name="xlsx">The XLSX.</param>
    /// <param name="csv">The CSV.</param>
    public static void ConvertXlsxToCsv(string xlsx, string csv)
    {
        MiniExcel.ConvertXlsxToCsv(xlsx, csv);
    }

    /// <summary>
    /// Converts the XLSX to CSV.
    /// </summary>
    /// <param name="xlsx">The XLSX.</param>
    /// <param name="csv">The CSV.</param>
    public static void ConvertXlsxToCsv(Stream xlsx, Stream csv)
    {
        MiniExcel.ConvertXlsxToCsv(xlsx, csv);
    }
}