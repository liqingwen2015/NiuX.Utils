using System.Reflection.Emit;

namespace NiuX.Csv.Quantum;

/// <summary>
/// 量子 Csv
/// </summary>
/// <remarks>量子阅读，超快，比流行的类库至少快几倍</remarks>
public class QuantumCsv
{
    /// <summary>
    /// 转对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <param name="columns"></param>
    /// <returns></returns>
    public delegate bool ToObject<in T>(T obj, QuantumColumn columns);

    /// <summary>
    /// 列数量
    /// </summary>
    private const int ColumnCount = 50;

    /// <summary>
    /// 量子阅读
    /// </summary>
    public static List<T> Read<T>(string path, bool hasheader, char delimiter, ToObject<T> mapper)
    {
        return Read(File.OpenText(path), hasheader, ColumnCount, delimiter, mapper);
    }

    /// <summary>
    /// 量子阅读
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sr"></param>
    /// <param name="hasheader"></param>
    /// <param name="colcount"></param>
    /// <param name="delimiter"></param>
    /// <param name="mapper"></param>
    /// <returns></returns>
    private static List<T> Read<T>(TextReader sr, bool hasheader, int colcount, char delimiter, ToObject<T> mapper)
    {
        QuantumSpan[] cols;
        var list = new List<T>(10000);

        var lineNum = 0;
        var createInstance = CreateInstance<T>();
        var br = new QuantumReader(sr, 64 * 1024);
        var line = new QuantumSpan();
        var names = new string[colcount];

        if (hasheader)
        {
            line = br.ReadLine();

            if (line.Count == 0) return list;

            var occurence = CountOccurence(line, delimiter);

            if (occurence == 0) throw new Exception($"文件没有包含 '{delimiter}'");

            names = GetNames(line, delimiter);
            cols = new QuantumSpan[occurence + 1];
        }
        else
        {
            cols = new QuantumSpan[colcount];
        }

        while (true)
            try
            {
                line = br.ReadLine();
                lineNum++;

                if (line.Count == 0) break;

                var spans = ParseLine(line, delimiter, cols);
                var obj = (T)createInstance();

                if (mapper(obj, new QuantumColumn(spans, names))) list.Add(obj);
            }
            catch (Exception ex)
            {
                sr.Close();
                throw new Exception($"出错行： {lineNum}\r\n{line}", ex);
            }

        sr.Close();
        return list;
    }

    /// <summary>
    /// 创建实例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    private static CreateObject CreateInstance<T>()
    {
        CreateObject obj = null;
        var objtype = typeof(T);

        try
        {
            if (objtype.IsClass)
            {
                var dynMethod = new DynamicMethod("_fcic", objtype, null, true);
                var ilGen = dynMethod.GetILGenerator();

                ilGen.Emit(OpCodes.Newobj, objtype.GetConstructor(Type.EmptyTypes));
                ilGen.Emit(OpCodes.Ret);

                obj = (CreateObject)dynMethod.CreateDelegate(typeof(CreateObject));
            }
        }
        catch (Exception exc)
        {
            throw new Exception(
                $"Failed to fast create instance for type '{objtype.FullName}' from assembly '{objtype.AssemblyQualifiedName}'",
                exc);
        }

        return obj;
    }

    /// <summary>
    /// 统计频率，出现的次数
    /// </summary>
    /// <param name="text"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    private static unsafe int CountOccurence(QuantumSpan text, char c)
    {
        var count = 0;
        var length = text.Count + text.Start;
        var index = text.Start;

        fixed (char* s = text.buf)
        {
            while (index++ < length)
            {
                var ch = *(s + index);

                if (ch == c) count++;
            }
        }

        return count;
    }

    /// <summary>
    /// 获取名称
    /// </summary>
    /// <param name="text"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    private static unsafe string[] GetNames(QuantumSpan text, char c)
    {
        var length = text.Count + text.Start;
        var index = text.Start;
        var list = new List<string>();

        fixed (char* s = text.buf)
        {
            while (index++ < length)
            {
                var ch = *(s + index);

                if (ch == c)
                {
                    text.Count = index - text.Start;
                    list.Add(text.ToString());
                    text.Start = index + 1;
                }

                if (c is '\r' or '\n') break;
            }

            text.Count = index - text.Start - 1;
            list.Add(text.ToString());
        }

        return list.ToArray();
    }

    /// <summary>
    /// 转换行
    /// </summary>
    /// <param name="line"></param>
    /// <param name="delimiter"></param>
    /// <param name="columns"></param>
    /// <returns></returns>
    private static unsafe QuantumSpan[] ParseLine(QuantumSpan line, char delimiter, QuantumSpan[] columns)
    {
        var col = 0;
        var lineLength = line.Count + line.Start;
        var index = line.Start;

        fixed (char* l = line.buf)
        {
            while (index < lineLength)
                if (*(l + index) != '\"')
                {
                    var next = -1;
                    for (var i = index; i < lineLength; i++)
                    {
                        if (*(l + i) != delimiter) continue;

                        next = i;
                        break;
                    }

                    if (next < 0)
                    {
                        columns[col++] = new QuantumSpan(line.buf, index, lineLength - index);
                        break;
                    }

                    columns[col++] = new QuantumSpan(line.buf, index, next - index);
                    index = next + 1;
                }
                else
                {
                    var qc = 1;
                    var start = index + 1;
                    var end = index + 1;
                    var c = *(l + ++index);

                    while (index++ < lineLength)
                    {
                        if (c == '\"') qc++;

                        if (c != '\r' && c != '\n') end = index - 1;
                        if (c == delimiter && qc % 2 == 0)
                        {
                            end = index - 2;
                            break;
                        }

                        c = *(l + index);
                    }

                    var s = new string(line.buf, start, end - start).Replace("\"\"", "\"");
                    columns[col++] = new QuantumSpan(s.ToCharArray(), 0, s.Length);
                }

            while (col < columns.Length) columns[col++] = new QuantumSpan();
        }

        return columns;
    }

    /// <summary>
    /// 创建对象
    /// </summary>
    /// <returns></returns>
    private delegate object CreateObject();
}