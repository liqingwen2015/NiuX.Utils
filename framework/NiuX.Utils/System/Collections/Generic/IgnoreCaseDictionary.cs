using JetBrains.Annotations;
using System.Runtime.Serialization;

// ReSharper disable once CheckNamespace
namespace NiuX.System.Collections.Generic;

/// <summary>
/// Represents a collection of keys and values, ignore case.
/// </summary>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <typeparam name="TValue">The type of the value.</typeparam>
/// <seealso cref="Generic.Dictionary&lt;TKey, TValue&gt;" />
public class IgnoreCaseDictionary<TKey, TValue> : Dictionary<TKey, TValue>
{
    private static readonly IEqualityComparer<TKey> EqualityComparer =
        IgnoreCaseDictionaryOptions<TKey>.DefaultEqualityComparer;

    public IgnoreCaseDictionary() : this(EqualityComparer)
    {
    }

    public IgnoreCaseDictionary([NotNull] IDictionary<TKey, TValue> dictionary) : this(dictionary, EqualityComparer)
    {
    }

    protected IgnoreCaseDictionary([NotNull] IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer) :
        base(dictionary, comparer)
    {
    }

    protected IgnoreCaseDictionary(IEqualityComparer<TKey> comparer) : base(comparer)
    {
    }

    public IgnoreCaseDictionary(int capacity) : this(capacity, EqualityComparer)
    {
    }

    protected IgnoreCaseDictionary(int capacity, IEqualityComparer<TKey> comparer) : base(capacity, comparer)
    {
    }

    protected IgnoreCaseDictionary(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}