namespace NiuX.Logging.Simple;

public static class SimpleLogHelper
{
    public static SimpleLogger Logger { get; } = new();

    public static void Log(Enum label, string content)
    {
        Logger.Log(label, content);
    }

    public static void Log(string label, object content)
    {
        Logger.Log(label, content);
    }

    /// <summary>
    /// Logs the given information with DEBUG label.
    /// </summary>
    /// <param name="content">A string with a message or an object to call ToString() on it</param>
    public static void Debug(object content)
    {
        Logger.Debug(content);
    }

    /// <summary>
    /// Logs the given information with INFO label.
    /// </summary>
    /// <param name="content">A string with a message or an object to call ToString() on it</param>
    public static void Info(object content)
    {
        Logger.Info(content);
    }

    /// <summary>
    /// Logs the given information with WARN label.
    /// </summary>
    /// <param name="content">A string with a message or an object to call ToString() on it</param>
    public static void Warn(object content)
    {
        Logger.Warn(content);
    }

    /// <summary>
    /// Logs the given information with ERROR label.
    /// </summary>
    /// <param name="content">A string with a message or an object to call ToString() on it</param>
    public static void Error(object content)
    {
        Logger.Error(content);
    }

    /// <summary>
    /// Logs the given information with Trace label.
    /// </summary>
    /// <param name="content">A string with a message or an object to call ToString() on it</param>
    public static void Trace(object content)
    {
        Logger.Trace(content);
    }

    /// <summary>
    /// Logs the given information with Trace label.
    /// </summary>
    /// <param name="content">A string with a message or an object to call ToString() on it</param>
    public static void Critical(object content)
    {
        Logger.Critical(content);
    }

    public static void Dispose()
    {
        Logger.Dispose();
    }
}