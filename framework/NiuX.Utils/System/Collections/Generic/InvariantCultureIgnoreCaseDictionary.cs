using JetBrains.Annotations;

namespace NiuX.System.Collections.Generic;

public sealed class InvariantCultureIgnoreCaseDictionary<TKey, TValue> : IgnoreCaseDictionary<TKey, TValue>
{
    private static readonly IEqualityComparer<TKey> EqualityComparer =
        (IEqualityComparer<TKey>)StringComparer.InvariantCultureIgnoreCase;

    public InvariantCultureIgnoreCaseDictionary() : this(EqualityComparer)
    {
    }

    public InvariantCultureIgnoreCaseDictionary([NotNull] IDictionary<TKey, TValue> dictionary) : this(dictionary,
        EqualityComparer)
    {
    }

    private InvariantCultureIgnoreCaseDictionary([NotNull] IDictionary<TKey, TValue> dictionary,
        [NotNull] IEqualityComparer<TKey> comparer) : base(dictionary, comparer)
    {
    }

    private InvariantCultureIgnoreCaseDictionary([NotNull] IEqualityComparer<TKey> comparer) : base(comparer)
    {
    }

    public InvariantCultureIgnoreCaseDictionary(int capacity) : this(capacity, EqualityComparer)
    {
    }

    private InvariantCultureIgnoreCaseDictionary(int capacity, [NotNull] IEqualityComparer<TKey> comparer) : base(
        capacity, comparer)
    {
    }
}