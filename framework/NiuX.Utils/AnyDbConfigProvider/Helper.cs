namespace NiuX.AnyDbConfigProvider;

/// <summary>
/// 助手
/// </summary>
public static class Helper
{
    /// <summary>
    /// Clones the specified dictionary.
    /// </summary>
    /// <param name="dict">The dictionary.</param>
    /// <returns></returns>
    public static IDictionary<string, string> Clone(this IDictionary<string, string> dict)
    {
        IDictionary<string, string> newDict = new Dictionary<string, string>();
        foreach (var kv in dict) newDict[kv.Key] = kv.Value;
        return newDict;
    }

    /// <summary>
    /// Determines whether the specified old dictionary is changed.
    /// </summary>
    /// <param name="oldDict">The old dictionary.</param>
    /// <param name="newDict">The new dictionary.</param>
    /// <returns>
    ///   <c>true</c> if the specified old dictionary is changed; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsChanged(IDictionary<string, string> oldDict,
        IDictionary<string, string> newDict) =>
        oldDict.Count != newDict.Count || (from oldKv in oldDict let oldKey = oldKv.Key let oldValue = oldKv.Value let newValue = newDict[oldKey] where oldValue != newValue select oldValue).Any();
}