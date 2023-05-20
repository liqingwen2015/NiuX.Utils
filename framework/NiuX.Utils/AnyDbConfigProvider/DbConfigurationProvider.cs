using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Text.Json;

namespace NiuX.AnyDbConfigProvider;

/// <summary>
/// DB 配置提供器
/// </summary>
/// <seealso cref="ConfigurationProvider" />
/// <seealso cref="System.IDisposable" />
public class DbConfigurationProvider : ConfigurationProvider, IDisposable
{
    //allow multi reading and single writing
    /// <summary>
    /// The lock object
    /// </summary>
    private readonly ReaderWriterLockSlim _lockObj = new();

    /// <summary>
    /// The options
    /// </summary>
    private readonly DbConfigOptions _options;
    /// <summary>
    /// The is disposed
    /// </summary>
    private bool _isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="DbConfigurationProvider"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public DbConfigurationProvider(DbConfigOptions options)
    {
        _options = options;
        var interval = TimeSpan.FromSeconds(3);
        if (options.ReloadInterval != null) interval = options.ReloadInterval.Value;
        if (options.ReloadOnChange)
            _ = Task.Factory.StartNew(() =>
            {
                while (!_isDisposed)
                {
                    Load();
                    Thread.Sleep(interval);
                }
            }, TaskCreationOptions.LongRunning);
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        _isDisposed = true;
    }

    /// <summary>
    /// Returns the list of keys that this provider has.
    /// </summary>
    /// <param name="earlierKeys">The earlier keys that other providers contain.</param>
    /// <param name="parentPath">The path for the parent IConfiguration.</param>
    /// <returns>
    /// The list of keys for this provider.
    /// </returns>
    public override IEnumerable<string> GetChildKeys(IEnumerable<string> earlierKeys, string parentPath)
    {
        _lockObj.EnterReadLock();

        try
        {
            return base.GetChildKeys(earlierKeys, parentPath);
        }
        finally
        {
            _lockObj.ExitReadLock();
        }
    }

    /// <summary>
    /// Attempts to find a value with the given key, returns true if one is found, false otherwise.
    /// </summary>
    /// <param name="key">The key to lookup.</param>
    /// <param name="value">The value found at key if one is found.</param>
    /// <returns>
    /// True if key has a value, false otherwise.
    /// </returns>
    public override bool TryGet(string key, out string? value)
    {
        _lockObj.EnterReadLock();
        try
        {
            return base.TryGet(key, out value);
        }
        finally
        {
            _lockObj.ExitReadLock();
        }
    }

    /// <summary>
    /// Loads (or reloads) the data for this provider.
    /// </summary>
    public override void Load()
    {
        base.Load();
        IDictionary<string, string> clonedData = null;
        try
        {
            _lockObj.EnterWriteLock();
            clonedData = Data.Clone();
            var tableName = _options.TableName;
            Data.Clear();
            using var conn = _options.CreateDbConnection();
            conn.Open();
            DoLoad(tableName, conn);
        }
        catch (DbException)
        {
            //if DbException is thrown, restore to the original data.
            Data = clonedData;
            throw;
        }
        finally
        {
            _lockObj.ExitWriteLock();
        }

        //OnReload cannot be between EnterWriteLock and ExitWriteLock, or "A read lock may not be acquired with the write lock held in this mode" will be thrown.
        if (Helper.IsChanged(clonedData, Data)) OnReload();
    }

    /// <summary>
    /// Does the load.
    /// </summary>
    /// <param name="tableName">Name of the table.</param>
    /// <param name="conn">The connection.</param>
    private void DoLoad(string tableName, IDbConnection conn)
    {
        using var cmd = conn.CreateCommand();
        cmd.CommandText = $@"SELECT TheKey, Value
                            FROM AppConfig
                            where Id in(select Max(Id) from {tableName} group by TheKey)";
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            var name = reader.GetString(0);
            var value = reader.GetString(1);
            if (value == null)
            {
                Data[name] = value;
                continue;
            }

            value = value.Trim();
            //if the value is like [...] or {} , it may be a json array value or json object value,
            //so try to parse it as json
            if (value.StartsWith("[") && value.EndsWith("]")
                || value.StartsWith("{") && value.EndsWith("}"))
                TryLoadAsJson(name, value);
            else
                Data[name] = value;
        }
    }

    /// <summary>
    /// Loads the json element.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="jsonRoot">The json root.</param>
    private void LoadJsonElement(string name, JsonElement jsonRoot)
    {
        switch (jsonRoot.ValueKind)
        {
            case JsonValueKind.Array:
                {
                    var index = 0;
                    foreach (var item in jsonRoot.EnumerateArray())
                    {
                        //https://andrewlock.net/creating-a-custom-iconfigurationprovider-in-asp-net-core-to-parse-yaml/
                        //parse as "a:b:0"="hello";"a:b:1"="world"
                        var path = name + ConfigurationPath.KeyDelimiter + index;
                        LoadJsonElement(path, item);
                        index++;
                    }

                    break;
                }
            case JsonValueKind.Object:
                {
                    foreach (var jsonObj in jsonRoot.EnumerateObject())
                    {
                        var pathOfObj = name + ConfigurationPath.KeyDelimiter + jsonObj.Name;
                        LoadJsonElement(pathOfObj, jsonObj.Value);
                    }

                    break;
                }
            default:
                //if it is not json array or object, parse it as plain string value
                Data[name] = jsonRoot.GetValueForConfig();
                break;
        }
    }

    /// <summary>
    /// Tries the load as json.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="value">The value.</param>
    private void TryLoadAsJson(string name, string value)
    {
        var jsonOptions = new JsonDocumentOptions
        { AllowTrailingCommas = true, CommentHandling = JsonCommentHandling.Skip };
        try
        {
            var jsonRoot = JsonDocument.Parse(value, jsonOptions).RootElement;
            LoadJsonElement(name, jsonRoot);
        }
        catch (JsonException ex)
        {
            //if it is not valid json, parse it as plain string value
            Data[name] = value;
            Debug.WriteLine($"When trying to parse {value} as json object, exception was thrown. {ex}");
        }
    }
}