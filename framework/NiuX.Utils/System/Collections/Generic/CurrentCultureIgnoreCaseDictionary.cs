using JetBrains.Annotations;

namespace NiuX.System.Collections.Generic;

/// <summary>
/// CurrentCultureIgnoreCaseDictionary
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TValue">The type of the value.</typeparam>
/// <seealso cref="Generic.IgnoreCaseDictionary&lt;TKey, TValue&gt;" />
public sealed class CurrentCultureIgnoreCaseDictionary<TKey, TValue> : IgnoreCaseDictionary<TKey, TValue>
{
    private static readonly IEqualityComparer<TKey> EqualityComparer =
        (IEqualityComparer<TKey>)StringComparer.CurrentCultureIgnoreCase;

    public CurrentCultureIgnoreCaseDictionary() : this(EqualityComparer)
    {
    }

    public CurrentCultureIgnoreCaseDictionary([NotNull] IDictionary<TKey, TValue> dictionary) : this(dictionary,
        EqualityComparer)
    {
    }

    private CurrentCultureIgnoreCaseDictionary([NotNull] IDictionary<TKey, TValue> dictionary,
        [NotNull] IEqualityComparer<TKey> comparer) : base(dictionary, comparer)
    {
    }

    private CurrentCultureIgnoreCaseDictionary([NotNull] IEqualityComparer<TKey> comparer) : base(comparer)
    {
    }

    public CurrentCultureIgnoreCaseDictionary(int capacity) : this(capacity, EqualityComparer)
    {
    }

    private CurrentCultureIgnoreCaseDictionary(int capacity, [NotNull] IEqualityComparer<TKey> comparer) : base(
        capacity, comparer)
    {
    }
}