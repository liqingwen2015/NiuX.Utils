// ReSharper disable once CheckNamespace

namespace NiuX.System.Collections.Generic;

public static class IgnoreCaseDictionaryOptions<TKey>
{
    public static IEqualityComparer<TKey> DefaultEqualityComparer { get; set; } =
        (IEqualityComparer<TKey>)StringComparer.OrdinalIgnoreCase;
}