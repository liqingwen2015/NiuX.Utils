using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace NiuX.Logging.Simple;

public class OpenStreams : IDisposable
{
    private readonly string _directory;

    private readonly object _lock;

    private readonly SemaphoreSlim _mutex = new(1);

    private readonly Dictionary<DateTime, StreamWriter> _streams;

    private readonly Timer _timer;

    public OpenStreams(string directory)
    {
        _directory = directory;
        _streams = new Dictionary<DateTime, StreamWriter>();
        _lock = new object();
        _timer = new Timer(ClosePastStreams, null, 0, (int)TimeSpan.FromHours(2).TotalMilliseconds);
    }

    public void Dispose()
    {
        _timer.Dispose();
        CloseAllStreams();
    }

    internal void Append(DateTime date, string content)
    {
        lock (_lock)
        {
            GetStream(date.Date).WriteLine(content);
        }
    }

    [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope",
        Justification = "It's disposed on this class Dispose.")]
    private StreamWriter GetStream(DateTime date)
    {
        if (!_streams.ContainsKey(date))
        {
            var fileName = $"{date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)}.log";
            var filePath = Path.Combine(_directory, fileName);

            Directory.CreateDirectory(_directory);

            var stream = new StreamWriter(File.Open(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                { AutoFlush = true };
            _streams[date] = stream;
        }

        return _streams[date];
    }

    private void ClosePastStreams(object ignored)
    {
        lock (_lock)
        {
            var today = DateTime.Today;
            foreach (var pair in _streams.Where(x => x.Key < today))
            {
                pair.Value.Dispose();
                _streams.Remove(pair.Key);
            }
        }
    }

    private void CloseAllStreams()
    {
        lock (_lock)
        {
            foreach (var value in _streams.Values) value.Dispose();

            _streams.Clear();
        }
    }
}