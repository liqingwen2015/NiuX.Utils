using Newtonsoft.Json;
using OfficeOpenXml;
using System.Data;
using System.Text.Json;

namespace NiuX.Excel.Utils;

/// <summary>
/// Epplus Utility
/// </summary>
public static class EpplusUtility
{
    /// <summary>
    /// Initializes the <see cref="EpplusUtility"/> class.
    /// </summary>
    static EpplusUtility()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    /// <summary>
    /// Gets the bytes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data">The data.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <returns></returns>
    public static byte[] GetBytes<T>(List<T> data, string sheetName = "sheet")
    {
        using var excelPack = new ExcelPackage();
        excelPack.Workbook.Worksheets.Add(sheetName).Cells
            .LoadFromDataTable(data.ToJson().FromJson<DataTable>(typeof(DataTable)), true);
        return excelPack.GetAsByteArray();
    }

    /// <summary>
    /// Gets the bytes.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data">The data.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <returns></returns>
    public static byte[] GetBytes<T>(IEnumerable<T> data, string sheetName = "sheet")
    {
        using var excelPack = new ExcelPackage();
        excelPack.Workbook.Worksheets.Add(sheetName).Cells
            .LoadFromDataTable(data.ToJson().FromJson<DataTable>(typeof(DataTable)), true);
        return excelPack.GetAsByteArray();
    }

    /// <summary>
    /// Gets the bytes asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data">The data.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <returns></returns>
    public static async Task<byte[]> GetBytesAsync<T>(List<T> data, string sheetName = "sheet")
    {
        using var excelPack = new ExcelPackage();
        excelPack.Workbook.Worksheets.Add(sheetName).Cells
            .LoadFromDataTable(data.ToJson().FromJson<DataTable>(typeof(DataTable)), true);
        return await excelPack.GetAsByteArrayAsync();
    }

    /// <summary>
    /// Gets the bytes asynchronous.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data">The data.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <returns></returns>
    public static async Task<byte[]> GetBytesAsync<T>(IEnumerable<T> data, string sheetName = "sheet")
    {
        using var excelPack = new ExcelPackage();
        excelPack.Workbook.Worksheets.Add(sheetName).Cells
            .LoadFromDataTable(data.ToJson().FromJson<DataTable>(typeof(DataTable)), true);
        return await excelPack.GetAsByteArrayAsync();
    }

    /// <summary>
    /// 从 Excel 读取数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath">The file path.</param>
    /// <param name="hasHeader">if set to <c>true</c> [has header].</param>
    /// <returns></returns>
    /// <remarks>
    /// https://www.thecodebuzz.com/read-write-excel-in-dotnet-core-epplus/
    /// </remarks>
    public static T ReadExcel<T>(string filePath, bool hasHeader = true)
    {
        using var excelPack = new ExcelPackage();
        //Load excel stream
        using (var stream = File.OpenRead(filePath))
        {
            excelPack.Load(stream);
        }

        //Lets Deal with first worksheet.(You may iterate here if dealing with multiple sheets)
        var ws = excelPack.Workbook.Worksheets[0];

        //Get all details as DataTable -because Datatable make life easy :)
        var excelasTable = new DataTable();
        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
            //Get colummn details
            if (!string.IsNullOrEmpty(firstRowCell.Text))
            {
                var firstColumn = $"Column {firstRowCell.Start.Column}";
                excelasTable.Columns.Add(hasHeader ? firstRowCell.Text : firstColumn);
            }

        var startRow = hasHeader ? 2 : 1;

        //Get row details
        for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
        {
            var wsRow = ws.Cells[rowNum, 1, rowNum, excelasTable.Columns.Count];
            var row = excelasTable.Rows.Add();

            foreach (var cell in wsRow) row[cell.Start.Column - 1] = cell.Text;
        }

        //Get everything as generics and let end user decides on casting to required type
        var generatedType = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(excelasTable));
        return (T)Convert.ChangeType(generatedType, typeof(T));
    }

    /// <summary>
    /// 从 Excel 读取数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath">The file path.</param>
    /// <param name="hasHeader">if set to <c>true</c> [has header].</param>
    /// <returns></returns>
    /// <remarks>
    /// https://www.thecodebuzz.com/read-write-excel-in-dotnet-core-epplus/
    /// </remarks>
    public static async Task<T> ReadAsync<T>(string filePath, bool hasHeader = true)
    {
        using var excelPack = new ExcelPackage();
        //Load excel stream
#if NETSTANDARD2_1_OR_GREATER
        await using (var stream = File.OpenRead(filePath))
#else
        using (var stream = File.OpenRead(filePath))
#endif
        {
            await excelPack.LoadAsync(stream);
        }

        //Lets Deal with first worksheet.(You may iterate here if dealing with multiple sheets)
        var ws = excelPack.Workbook.Worksheets[0];

        //Get all details as DataTable -because Datatable make life easy :)
        var excelasTable = new DataTable();
        foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
            //Get colummn details
            if (!string.IsNullOrEmpty(firstRowCell.Text))
            {
                var firstColumn = $"Column {firstRowCell.Start.Column}";
                excelasTable.Columns.Add(hasHeader ? firstRowCell.Text : firstColumn);
            }

        var startRow = hasHeader ? 2 : 1;

        //Get row details
        for (var rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
        {
            var wsRow = ws.Cells[rowNum, 1, rowNum, excelasTable.Columns.Count];
            var row = excelasTable.Rows.Add();

            foreach (var cell in wsRow) row[cell.Start.Column - 1] = cell.Text;
        }

        //Get everything as generics and let end user decides on casting to required type
        var generatedType = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(excelasTable));
        return (T)Convert.ChangeType(generatedType, typeof(T));
    }

    /// <summary>
    /// 写入 Excel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath">The file path.</param>
    /// <param name="data">The data.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <remarks>
    /// https://www.thecodebuzz.com/read-write-excel-in-dotnet-core-epplus/
    /// </remarks>
    public static void Write<T>(string filePath, IEnumerable<T> data, string sheetName = "sheet")
    {
        using var stream = new FileInfo(filePath).Create();
        using var excelPack = WriteInner(data, sheetName, stream);
        excelPack.Save();
    }

    /// <summary>
    /// 写入 Excel
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="data">The data.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <remarks>
    /// https://www.thecodebuzz.com/read-write-excel-in-dotnet-core-epplus/
    /// </remarks>
    public static void Write(string filePath, DataTable data, string sheetName = "sheet")
    {
        using var stream = new FileInfo(filePath).Create();
        using var excelPack = WriteInner(data, sheetName, stream);
        excelPack.Save();
    }

    /// <summary>
    /// 写入 Excel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filePath">The file path.</param>
    /// <param name="data">The data.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    public static async Task WriteAsync<T>(string filePath, IEnumerable<T> data, string sheetName = "sheet")
    {
#if NETSTANDARD2_1_OR_GREATER
        await using var stream = new FileInfo(filePath).Create();
#else
        using var stream = new FileInfo(filePath).Create();
#endif

        using var excelPack = WriteInner(data, sheetName, stream);
        await excelPack.SaveAsync();
    }

    /// <summary>
    /// 写入 Excel
    /// </summary>
    /// <param name="filePath">The file path.</param>
    /// <param name="data">The data.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    public static async Task WriteAsync(string filePath, DataTable data, string sheetName = "sheet")
    {
#if NETSTANDARD2_1_OR_GREATER
        await using var stream = new FileInfo(filePath).Create();
#else
        using var stream = new FileInfo(filePath).Create();
#endif

        using var excelPack = WriteInner(data, sheetName, stream);
        await excelPack.SaveAsync();
    }

    /// <summary>
    /// Writes the inner.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data">The data.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="stream">The stream.</param>
    /// <returns></returns>
    private static ExcelPackage WriteInner<T>(IEnumerable<T> data, string sheetName, Stream stream)
    {
        return WriteInner(data.ToJson().FromJson<DataTable>(typeof(DataTable))!, sheetName, stream);
        //var excelPack = new ExcelPackage(stream);

        //var dt = data.ToJson().FromJson<DataTable>(typeof(DataTable))!;
        //var excel = excelPack.Workbook.Worksheets.Add(sheetName);
        //excel.Cells.LoadFromDataTable(dt, true);

        //for (int i = 0; i < dt.Columns.Count; i++)
        //{
        //    if (dt.Columns[i].DataType == typeof(DateTime))
        //    {
        //        excel.Column(i + 1).Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";
        //        excel.Column(i + 1).AutoFit();
        //    }
        //}

        //return excelPack;
    }

    /// <summary>
    /// Writes the inner.
    /// </summary>
    /// <param name="dt">The dt.</param>
    /// <param name="sheetName">Name of the sheet.</param>
    /// <param name="stream">The stream.</param>
    /// <returns></returns>
    private static ExcelPackage WriteInner(DataTable dt, string sheetName, Stream stream)
    {
        var excelPack = new ExcelPackage(stream);

        var excel = excelPack.Workbook.Worksheets.Add(sheetName);
        excel.Cells.LoadFromDataTable(dt, true);

        for (var i = 0; i < dt.Columns.Count; i++)
            if (dt.Columns[i].DataType == typeof(DateTime))
            {
                excel.Column(i + 1).Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";
                excel.Column(i + 1).AutoFit();
            }

        return excelPack;
    }
}