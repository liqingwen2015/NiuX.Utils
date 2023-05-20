using JetBrains.Annotations;

namespace NiuX.System.Collections.Generic;

/// <summary>
/// Represents a collection of keys and values, ignore case.
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TValue">The type of the value.</typeparam>
/// <seealso cref="Generic.Dictionary&lt;TKey, TValue&gt;" />
public sealed class OrdinalIgnoreCaseDictionary<TKey, TValue> : IgnoreCaseDictionary<TKey, TValue>
{
    private static readonly IEqualityComparer<TKey> EqualityComparer =
        (IEqualityComparer<TKey>)StringComparer.OrdinalIgnoreCase;

    public OrdinalIgnoreCaseDictionary() : this(EqualityComparer)
    {
    }

    public OrdinalIgnoreCaseDictionary([NotNull] IDictionary<TKey, TValue> dictionary) : this(dictionary,
        EqualityComparer)
    {
    }

    private OrdinalIgnoreCaseDictionary([NotNull] IDictionary<TKey, TValue> dictionary,
        [NotNull] IEqualityComparer<TKey> comparer) : base(dictionary, comparer)
    {
    }

    private OrdinalIgnoreCaseDictionary([NotNull] IEqualityComparer<TKey> comparer) : base(comparer)
    {
    }

    public OrdinalIgnoreCaseDictionary(int capacity) : this(capacity, EqualityComparer)
    {
    }

    private OrdinalIgnoreCaseDictionary(int capacity, [NotNull] IEqualityComparer<TKey> comparer) : base(capacity,
        comparer)
    {
    }
}