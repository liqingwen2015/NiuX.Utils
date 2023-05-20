using System.Globalization;

namespace NiuX.Logging.Simple;

public class SimpleLogger : IDisposable
{
    private readonly string _dateTimeFormat;

    private readonly TimeSpan? _deleteOldFiles;

    private readonly string _directory;

    private readonly string[] _enabledLabels;

    private readonly object _lock;

    private readonly OpenStreams _openStreams;

    private readonly bool _useUtcTime;

    private bool _disposed;

    private int _longestLabel;

    public SimpleLogger(bool useUtcTime = false, TimeSpan? deleteOldFiles = null,
        string dateTimeFormat = "yyyy-MM-dd HH:mm:ss",
        string directory = null, params string[] enabledLabels)
    {
        _useUtcTime = useUtcTime;
        _deleteOldFiles = deleteOldFiles;
        _dateTimeFormat = dateTimeFormat;
        _directory = directory ?? Path.Combine(AppContext.BaseDirectory, "logs");
        _enabledLabels = (enabledLabels ?? Array.Empty<string>()).Select(Normalize).ToArray();
        _lock = new object();
        _openStreams = new OpenStreams(_directory);

        //if (_deleteOldFiles.HasValue)
        //{
        //    var min = TimeSpan.FromSeconds(5);
        //    var max = TimeSpan.FromHours(8);

        //    var cleanUpTime = new TimeSpan(_deleteOldFiles.Value.Ticks / 5);
        //    if (cleanUpTime < min)
        //    {
        //        cleanUpTime = min;
        //    }

        //    if (cleanUpTime > max)
        //    {
        //        cleanUpTime = max;
        //    }
        //}

        _longestLabel = _enabledLabels.Any() ? _enabledLabels.Select(x => x.Length).Max() : 5;
        _disposed = false;
    }

    private DateTime Now => _useUtcTime ? DateTime.UtcNow : DateTime.Now;

    public void Dispose()
    {
        lock (_lock)
        {
            if (_disposed) return;

            _openStreams?.Dispose();
            _disposed = true;
        }
    }

    public void Log(Enum label, string content)
    {
        Log(label.ToString(), content);
    }

    public void Log(string label, object content)
    {
        if (label.IsNullOrEmpty()) throw new ArgumentNullException(nameof(label));

        if (content == null) throw new ArgumentNullException(nameof(content));

        label = Normalize(label);

        if (_enabledLabels.Any() && !_enabledLabels.Contains(label)) return;

        _longestLabel = Math.Max(_longestLabel, label.Length);

        var date = Now;

        var formattedDate = date.ToString(_dateTimeFormat, CultureInfo.InvariantCulture);
        var padding = new string(' ', _longestLabel - label.Length);
        var line = $"{formattedDate} {label} {padding}{content}";

        lock (_lock)
        {
            if (_disposed) throw new ObjectDisposedException("Cannot access a disposed object.");

            _openStreams.Append(date, line);
        }
    }

    /// <summary>
    /// Logs the given information with DEBUG label.
    /// </summary>
    /// <param name="content">A string with a message or an object to call ToString() on it</param>
    public void Debug(object content)
    {
        Log(nameof(LogLevel.Debug), content);
    }

    /// <summary>
    /// Logs the given information with INFO label.
    /// </summary>
    /// <param name="content">A string with a message or an object to call ToString() on it</param>
    public void Info(object content)
    {
        Log(nameof(Info), content);
    }

    /// <summary>
    /// Logs the given information with WARN label.
    /// </summary>
    /// <param name="content">A string with a message or an object to call ToString() on it</param>
    public void Warn(object content)
    {
        Log(nameof(Warn), content);
    }

    /// <summary>
    /// Logs the given information with ERROR label.
    /// </summary>
    /// <param name="content">A string with a message or an object to call ToString() on it</param>
    public void Error(object content)
    {
        Log(nameof(LogLevel.Error), content);
    }

    /// <summary>
    /// Logs the given information with Trace label.
    /// </summary>
    /// <param name="content">A string with a message or an object to call ToString() on it</param>
    public void Trace(object content)
    {
        Log(nameof(LogLevel.Trace), content);
    }

    /// <summary>
    /// Logs the given information with Trace label.
    /// </summary>
    /// <param name="content">A string with a message or an object to call ToString() on it</param>
    public void Critical(object content)
    {
        Log(nameof(LogLevel.Critical), content);
    }

    private static string Normalize(string label)
    {
        return label.Trim().ToUpperInvariant();
    }
}