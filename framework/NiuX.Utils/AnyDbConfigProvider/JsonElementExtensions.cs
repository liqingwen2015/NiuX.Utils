using System.Text.Json;

namespace NiuX.AnyDbConfigProvider;

/// <summary>
/// Json Element Extensions
/// </summary>
internal static class JsonElementExtensions
{
    /// <summary>
    /// Gets the value for configuration.
    /// </summary>
    /// <param name="e">The e.</param>
    /// <returns></returns>
    public static string? GetValueForConfig(this JsonElement e)
    {
        switch (e.ValueKind)
        {
            //remove the quotes, "ab"-->ab
            case JsonValueKind.String:
                return e.GetString();
            case JsonValueKind.Null:
            //remove the quotes, "null"-->null
            case JsonValueKind.Undefined:
                return null;
            default:
                return e.GetRawText();
        }
    }
}