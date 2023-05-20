using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using System.Text;

namespace NiuX.Csv.Utils;

/// <summary>
/// 简单 Csv 工具类
/// </summary>
public static class SimpleCsvUtility
{
    /// <summary>
    /// Reads the specified path.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <returns></returns>
    public static List<T> Read<T>(string path) where T : new()
    {
        var fields = new List<string>();
        var fieldMaps = new Dictionary<string, PropertyInfo>();

        foreach (var s in File.ReadLines(path).First().Split(','))
        {
            fields.Add(s);

            foreach (var property in typeof(T).GetProperties())
            {
                var columnAttribute = property.GetCustomAttribute<ColumnAttribute>();

                if (columnAttribute != null)
                {
                    if (s != columnAttribute.Name) continue;

                    fieldMaps[s] = property;
                    break;
                }

                if (property.Name.IsEqual(s))
                {
                    fieldMaps[s] = property;
                    break;
                }
            }
        }

        var items = new List<T>();

        foreach (var line in File.ReadLines(path).Skip(1))
        {
            var item = new T();
            var arr = line.Split(',');

            for (var i = 0; i < arr.Length; i++)
            {
                var val = arr[i];

                if (!fieldMaps.TryGetValue(fields[i], out var prop)) continue;

                //var prop = fieldMaps[fields[i]];

                if (prop.PropertyType == typeof(int?))
                {
                    prop.SetValue(item, val.TryParseNullableInt());
                }
                else if (prop.PropertyType == typeof(decimal?))
                {
                    prop.SetValue(item, val.TryParseNullableDecimal());
                }
                else if (prop.PropertyType == typeof(long?))
                {
                    prop.SetValue(item, val.TryParseNullableLong());
                }
                else if (prop.PropertyType == typeof(bool?))
                {
                    prop.SetValue(item, val.TryParseNullableBoolean());
                }
                else if (prop.PropertyType == typeof(DateTime?))
                {
                    prop.SetValue(item, val.TryParseNullableDateTime());
                }
                else if (prop.PropertyType == typeof(byte?))
                {
                    prop.SetValue(item, val.TryParseNullableByte());
                }
                else if (prop.PropertyType == typeof(bool))
                {
                    prop.SetValue(item, val.TryParseBoolean());
                }
                else if (prop.PropertyType == typeof(byte[]))
                {
                    // TODO

                    //prop.SetValue(item, val.tryby());
                }
                else
                {
                    prop.SetValue(item, Convert.ChangeType(val, prop.PropertyType));
                }
            }

            items.Add(item);
        }

        return items;
    }

    /// <summary>
    /// Writes the specified path.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="texts">The texts.</param>
    public static void Write(string path, List<string> texts)
    {
        File.WriteAllLines(path, texts);
    }

    /// <summary>
    /// Writes the specified path.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="header">The header.</param>
    /// <param name="texts">The texts.</param>
    public static void Write(string path, string header, List<string> texts)
    {
        texts.Insert(0, header);
        File.WriteAllLines(path, texts);
    }

    /// <summary>
    /// Writes the specified path.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="headers">The headers.</param>
    /// <param name="texts">The texts.</param>
    public static void Write(string path, List<string> headers, List<string> texts)
    {
        texts.Insert(0, string.Join(",", headers));
        File.WriteAllLines(path, texts);
    }

    /// <summary>
    /// Writes the specified path.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="items">The items.</param>
    public static void Write<T>(string path, IEnumerable<T> items) where T : new()
    {
        File.WriteAllText(path, WriteInner(items));
    }

    /// <summary>
    /// Writes the inner.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The items.</param>
    /// <returns></returns>
    private static string WriteInner<T>(IEnumerable<T> items) where T : new()
    {
        var sb = new StringBuilder();
        var props = typeof(T).GetProperties();

        foreach (var property in props) sb.Append(property.Name).Append(',');

        sb.Remove(sb.Length - 1, 1).AppendLine();

        foreach (var item in items)
        {
            foreach (var property in props) sb.Append(property.GetValue(item)).Append(',');

            sb.Remove(sb.Length - 1, 1).AppendLine();
        }

        return sb.ToString();
    }

    /// <summary>
    /// Writes the specified path.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">The path.</param>
    /// <param name="items">The items.</param>
    public static void Write<T>(string path, List<T> items) where T : new()
    {
        File.WriteAllText(path, WriteInner(items));
    }

    /// <summary>
    /// Writes the specified path.
    /// </summary>
    /// <param name="path">The path.</param>
    /// <param name="table">The table.</param>
    public static void Write(string path, DataTable table)
    {
        File.WriteAllText(path, WriteInner(table));
    }

    /// <summary>
    /// Writes the inner.
    /// </summary>
    /// <param name="table">The table.</param>
    /// <returns></returns>
    private static string WriteInner(DataTable table)
    {
        var sb = new StringBuilder();

        foreach (DataColumn column in table.Columns) sb.Append(column.ColumnName).Append(',');

        sb.Remove(sb.Length - 1, 1).AppendLine();

        foreach (DataRow row in table.Rows)
        {
            for (var i = 0; i < table.Columns.Count; i++) sb.Append(row[i]).Append(',');

            sb.AppendLine();
        }

        return sb.ToString();
    }

#if NETSTANDARD2_1_OR_GREATER
    public static Task WriteAsync<T>(string path, IEnumerable<T> items) where T : new()
    {
        return File.WriteAllTextAsync(path, WriteInner(items));
    }

    public static Task WriteAsync<T>(string path, List<T> items) where T : new()
    {
        return File.WriteAllTextAsync(path, WriteInner(items));
    }

    public static Task WriteAsync(string path, DataTable table)
    {
        return File.WriteAllTextAsync(path, WriteInner(table));
    }

#endif
}