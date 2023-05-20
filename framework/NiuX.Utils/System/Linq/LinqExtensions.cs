using NiuX.System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace NiuX.System.Linq;

public static class LinqExtensions
{
    #region ToIgnoreCaseDictionary

    /// <summary>
    /// Converts to ignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <returns></returns>
    public static IgnoreCaseDictionary<TKey, TSource> ToIgnoreCaseDictionary<TSource, TKey>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey : notnull
    {
        var capacity = 0;
        if (source is ICollection<TSource> sources)
        {
            capacity = sources.Count;
            if (capacity == 0)
                return new IgnoreCaseDictionary<TKey, TSource>();
            switch (sources)
            {
                case TSource[] source1:
                    return source1.ToIgnoreCaseDictionary(keySelector);

                case List<TSource> source2:
                    return source2.ToIgnoreCaseDictionary(keySelector);
            }
        }

        var dictionary = new IgnoreCaseDictionary<TKey, TSource>(capacity);
        foreach (var source3 in source)
            dictionary.Add(keySelector(source3), source3);
        return dictionary;
    }

    /// <summary>
    /// Converts to ignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="elementSelector">The element selector.</param>
    /// <returns></returns>
    public static IgnoreCaseDictionary<TKey, TElement> ToIgnoreCaseDictionary<TSource, TKey, TElement>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector) where TKey : notnull
    {
        //if (source == null)
        //    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
        //if (keySelector == null)
        //    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.keySelector);
        //if (elementSelector == null)
        //    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.elementSelector);
        var capacity = 0;
        if (source is ICollection<TSource> sources)
        {
            capacity = sources.Count;
            if (capacity == 0)
                return new IgnoreCaseDictionary<TKey, TElement>();
            switch (sources)
            {
                case TSource[] source1:
                    return source1.ToIgnoreCaseDictionary(keySelector, elementSelector);

                case List<TSource> source2:
                    return source2.ToIgnoreCaseDictionary(keySelector, elementSelector);
            }
        }

        var dictionary = new IgnoreCaseDictionary<TKey, TElement>(capacity);
        foreach (var source3 in source)
            dictionary.Add(keySelector(source3), elementSelector(source3));
        return dictionary;
    }

    /// <summary>
    /// Converts to ignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <returns></returns>
    public static IgnoreCaseDictionary<TKey, TSource> ToIgnoreCaseDictionary<TSource, TKey>(this TSource[] source,
        Func<TSource, TKey> keySelector)
    {
        return ToIgnoreCaseDictionary(source, keySelector,
            () => new IgnoreCaseDictionary<TKey, TSource>(source.Length));
    }

    /// <summary>
    /// Converts to ignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <returns></returns>
    public static IgnoreCaseDictionary<TKey, TSource> ToIgnoreCaseDictionary<TSource, TKey>(this List<TSource> source,
        Func<TSource, TKey> keySelector)
    {
        return ToIgnoreCaseDictionary(source, keySelector, () => new IgnoreCaseDictionary<TKey, TSource>(source.Count));
    }

    /// <summary>
    /// Converts to ignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="elementSelector">The element selector.</param>
    /// <returns></returns>
    public static IgnoreCaseDictionary<TKey, TElement> ToIgnoreCaseDictionary<TSource, TKey, TElement>(
        this TSource[] source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
    {
        return ToIgnoreCaseDictionary(source, keySelector, elementSelector,
            () => new IgnoreCaseDictionary<TKey, TElement>(source.Length));
    }

    /// <summary>
    /// Converts to ignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="elementSelector">The element selector.</param>
    /// <returns></returns>
    public static IgnoreCaseDictionary<TKey, TElement> ToIgnoreCaseDictionary<TSource, TKey, TElement>(
        this List<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
    {
        return source.ToIgnoreCaseDictionary(keySelector, elementSelector,
            () => new IgnoreCaseDictionary<TKey, TElement>(source.Count));
    }

    #endregion ToIgnoreCaseDictionary

    #region ToCurrentCultureIgnoreCaseDictionary

    /// <summary>
    /// Converts to ignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <returns></returns>
    public static CurrentCultureIgnoreCaseDictionary<TKey, TSource> ToCurrentCultureIgnoreCaseDictionary<TSource, TKey>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey : notnull
    {
        var capacity = 0;
        if (source is ICollection<TSource> sources)
        {
            capacity = sources.Count;
            if (capacity == 0)
                return new CurrentCultureIgnoreCaseDictionary<TKey, TSource>();
            switch (sources)
            {
                case TSource[] source1:
                    return source1.ToCurrentCultureIgnoreCaseDictionary(keySelector);

                case List<TSource> source2:
                    return source2.ToCurrentCultureIgnoreCaseDictionary(keySelector);
            }
        }

        var dictionary = new CurrentCultureIgnoreCaseDictionary<TKey, TSource>(capacity);
        foreach (var source3 in source)
            dictionary.Add(keySelector(source3), source3);
        return dictionary;
    }

    /// <summary>
    /// Converts to ignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="elementSelector">The element selector.</param>
    /// <returns></returns>
    public static CurrentCultureIgnoreCaseDictionary<TKey, TElement> ToCurrentCultureIgnoreCaseDictionary<TSource, TKey,
        TElement>(this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector) where TKey : notnull
    {
        //if (source == null)
        //    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
        //if (keySelector == null)
        //    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.keySelector);
        //if (elementSelector == null)
        //    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.elementSelector);
        var capacity = 0;
        if (source is ICollection<TSource> sources)
        {
            capacity = sources.Count;
            if (capacity == 0)
                return new CurrentCultureIgnoreCaseDictionary<TKey, TElement>();
            switch (sources)
            {
                case TSource[] source1:
                    return source1.ToCurrentCultureIgnoreCaseDictionary(keySelector, elementSelector);

                case List<TSource> source2:
                    return source2.ToCurrentCultureIgnoreCaseDictionary(keySelector, elementSelector);
            }
        }

        var dictionary = new CurrentCultureIgnoreCaseDictionary<TKey, TElement>(capacity);
        foreach (var source3 in source)
            dictionary.Add(keySelector(source3), elementSelector(source3));
        return dictionary;
    }

    /// <summary>
    /// Converts to currentcultureignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <returns></returns>
    public static CurrentCultureIgnoreCaseDictionary<TKey, TSource> ToCurrentCultureIgnoreCaseDictionary<TSource, TKey>(
        this TSource[] source, Func<TSource, TKey> keySelector)
    {
        return ToIgnoreCaseDictionary(source, keySelector,
            () => new CurrentCultureIgnoreCaseDictionary<TKey, TSource>(source.Length));
    }

    /// <summary>
    /// Converts to currentcultureignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <returns></returns>
    public static CurrentCultureIgnoreCaseDictionary<TKey, TSource> ToCurrentCultureIgnoreCaseDictionary<TSource, TKey>(
        this List<TSource> source, Func<TSource, TKey> keySelector)
    {
        return ToIgnoreCaseDictionary(source, keySelector,
            () => new CurrentCultureIgnoreCaseDictionary<TKey, TSource>(source.Count));
    }

    /// <summary>
    /// Converts to currentcultureignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="elementSelector">The element selector.</param>
    /// <returns></returns>
    public static CurrentCultureIgnoreCaseDictionary<TKey, TElement>
        ToCurrentCultureIgnoreCaseDictionary<TSource, TKey, TElement>(this TSource[] source,
            Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
    {
        return ToIgnoreCaseDictionary(source, keySelector, elementSelector,
            () => new CurrentCultureIgnoreCaseDictionary<TKey, TElement>(source.Length));
    }

    /// <summary>
    /// Converts to currentcultureignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="elementSelector">The element selector.</param>
    /// <returns></returns>
    public static CurrentCultureIgnoreCaseDictionary<TKey, TElement>
        ToCurrentCultureIgnoreCaseDictionary<TSource, TKey, TElement>(this List<TSource> source,
            Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
    {
        return source.ToIgnoreCaseDictionary(keySelector, elementSelector,
            () => new CurrentCultureIgnoreCaseDictionary<TKey, TElement>(source.Count));
    }

    #endregion ToCurrentCultureIgnoreCaseDictionary

    #region ToInvariantCultureIgnoreCaseDictionary

    /// <summary>
    /// Converts to ignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <returns></returns>
    public static InvariantCultureIgnoreCaseDictionary<TKey, TSource>
        ToInvariantCultureIgnoreCaseDictionary<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector) where TKey : notnull
    {
        var capacity = 0;
        if (source is ICollection<TSource> sources)
        {
            capacity = sources.Count;
            if (capacity == 0)
                return new InvariantCultureIgnoreCaseDictionary<TKey, TSource>();
            switch (sources)
            {
                case TSource[] source1:
                    return source1.ToInvariantCultureIgnoreCaseDictionary(keySelector);

                case List<TSource> source2:
                    return source2.ToInvariantCultureIgnoreCaseDictionary(keySelector);
            }
        }

        var dictionary = new InvariantCultureIgnoreCaseDictionary<TKey, TSource>(capacity);
        foreach (var source3 in source)
            dictionary.Add(keySelector(source3), source3);
        return dictionary;
    }

    /// <summary>
    /// Converts to ignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="elementSelector">The element selector.</param>
    /// <returns></returns>
    public static InvariantCultureIgnoreCaseDictionary<TKey, TElement> ToInvariantCultureIgnoreCaseDictionary<TSource,
        TKey, TElement>(this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector) where TKey : notnull
    {
        //if (source == null)
        //    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
        //if (keySelector == null)
        //    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.keySelector);
        //if (elementSelector == null)
        //    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.elementSelector);
        var capacity = 0;
        if (source is ICollection<TSource> sources)
        {
            capacity = sources.Count;
            if (capacity == 0)
                return new InvariantCultureIgnoreCaseDictionary<TKey, TElement>();
            switch (sources)
            {
                case TSource[] source1:
                    return source1.ToInvariantCultureIgnoreCaseDictionary(keySelector, elementSelector);

                case List<TSource> source2:
                    return source2.ToInvariantCultureIgnoreCaseDictionary(keySelector, elementSelector);
            }
        }

        var dictionary = new InvariantCultureIgnoreCaseDictionary<TKey, TElement>(capacity);
        foreach (var source3 in source)
            dictionary.Add(keySelector(source3), elementSelector(source3));
        return dictionary;
    }

    /// <summary>
    /// Converts to invariantcultureignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="elementSelector">The element selector.</param>
    /// <returns></returns>
    public static InvariantCultureIgnoreCaseDictionary<TKey, TElement>
        ToInvariantCultureIgnoreCaseDictionary<TSource, TKey, TElement>(this TSource[] source,
            Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
    {
        return ToIgnoreCaseDictionary(source, keySelector, elementSelector,
            () => new InvariantCultureIgnoreCaseDictionary<TKey, TElement>(source.Length));
    }

    /// <summary>
    /// Converts to invariantcultureignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="elementSelector">The element selector.</param>
    /// <returns></returns>
    public static InvariantCultureIgnoreCaseDictionary<TKey, TElement>
        ToInvariantCultureIgnoreCaseDictionary<TSource, TKey, TElement>(this List<TSource> source,
            Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
    {
        return source.ToIgnoreCaseDictionary(keySelector, elementSelector,
            () => new InvariantCultureIgnoreCaseDictionary<TKey, TElement>(source.Count));
    }

    /// <summary>
    /// Converts to invariantcultureignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <returns></returns>
    public static InvariantCultureIgnoreCaseDictionary<TKey, TSource>
        ToInvariantCultureIgnoreCaseDictionary<TSource, TKey>(this TSource[] source, Func<TSource, TKey> keySelector)
    {
        return ToIgnoreCaseDictionary(source, keySelector,
            () => new InvariantCultureIgnoreCaseDictionary<TKey, TSource>(source.Length));
    }

    /// <summary>
    /// Converts to invariantcultureignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <returns></returns>
    public static InvariantCultureIgnoreCaseDictionary<TKey, TSource>
        ToInvariantCultureIgnoreCaseDictionary<TSource, TKey>(this List<TSource> source,
            Func<TSource, TKey> keySelector)
    {
        return ToIgnoreCaseDictionary(source, keySelector,
            () => new InvariantCultureIgnoreCaseDictionary<TKey, TSource>(source.Count));
    }

    #endregion ToInvariantCultureIgnoreCaseDictionary

    #region ToOrdinalIgnoreCaseDictionary

    /// <summary>
    /// Converts to ignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <returns></returns>
    public static OrdinalIgnoreCaseDictionary<TKey, TSource> ToOrdinalIgnoreCaseDictionary<TSource, TKey>(
        this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) where TKey : notnull
    {
        var capacity = 0;
        if (source is ICollection<TSource> sources)
        {
            capacity = sources.Count;
            if (capacity == 0)
                return new OrdinalIgnoreCaseDictionary<TKey, TSource>();
            switch (sources)
            {
                case TSource[] source1:
                    return source1.ToOrdinalIgnoreCaseDictionary(keySelector);

                case List<TSource> source2:
                    return source2.ToOrdinalIgnoreCaseDictionary(keySelector);
            }
        }

        var dictionary = new OrdinalIgnoreCaseDictionary<TKey, TSource>(capacity);
        foreach (var source3 in source)
            dictionary.Add(keySelector(source3), source3);
        return dictionary;
    }

    /// <summary>
    /// Converts to ignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="elementSelector">The element selector.</param>
    /// <returns></returns>
    public static OrdinalIgnoreCaseDictionary<TKey, TElement> ToOrdinalIgnoreCaseDictionary<TSource, TKey, TElement>(
        this IEnumerable<TSource> source,
        Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector) where TKey : notnull
    {
        //if (source == null)
        //    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
        //if (keySelector == null)
        //    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.keySelector);
        //if (elementSelector == null)
        //    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.elementSelector);
        var capacity = 0;
        if (source is ICollection<TSource> sources)
        {
            capacity = sources.Count;
            if (capacity == 0)
                return new OrdinalIgnoreCaseDictionary<TKey, TElement>();
            switch (sources)
            {
                case TSource[] source1:
                    return source1.ToOrdinalIgnoreCaseDictionary(keySelector, elementSelector);

                case List<TSource> source2:
                    return source2.ToOrdinalIgnoreCaseDictionary(keySelector, elementSelector);
            }
        }

        var dictionary = new OrdinalIgnoreCaseDictionary<TKey, TElement>(capacity);
        foreach (var source3 in source)
            dictionary.Add(keySelector(source3), elementSelector(source3));
        return dictionary;
    }

    /// <summary>
    /// Converts to ordinalignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <returns></returns>
    public static OrdinalIgnoreCaseDictionary<TKey, TSource> ToOrdinalIgnoreCaseDictionary<TSource, TKey>(
        this TSource[] source, Func<TSource, TKey> keySelector)
    {
        return ToIgnoreCaseDictionary(source, keySelector,
            () => new OrdinalIgnoreCaseDictionary<TKey, TSource>(source.Length));
    }

    /// <summary>
    /// Converts to ordinalignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <returns></returns>
    public static OrdinalIgnoreCaseDictionary<TKey, TSource> ToOrdinalIgnoreCaseDictionary<TSource, TKey>(
        this List<TSource> source, Func<TSource, TKey> keySelector)
    {
        return ToIgnoreCaseDictionary(source, keySelector,
            () => new OrdinalIgnoreCaseDictionary<TKey, TSource>(source.Count));
    }

    /// <summary>
    /// Converts to ordinalignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="elementSelector">The element selector.</param>
    /// <returns></returns>
    public static OrdinalIgnoreCaseDictionary<TKey, TElement> ToOrdinalIgnoreCaseDictionary<TSource, TKey, TElement>(
        this List<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
    {
        return source.ToIgnoreCaseDictionary(keySelector, elementSelector,
            () => new OrdinalIgnoreCaseDictionary<TKey, TElement>(source.Count));
    }

    /// <summary>
    /// Converts to ordinalignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="elementSelector">The element selector.</param>
    /// <returns></returns>
    public static OrdinalIgnoreCaseDictionary<TKey, TElement> ToOrdinalIgnoreCaseDictionary<TSource, TKey, TElement>(
        this TSource[] source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
    {
        return ToIgnoreCaseDictionary(source, keySelector, elementSelector,
            () => new OrdinalIgnoreCaseDictionary<TKey, TElement>(source.Length));
    }

    #endregion ToOrdinalIgnoreCaseDictionary

    #region private methods

    /// <summary>
    /// Converts to ignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="creator">The create.</param>
    /// <returns></returns>
    private static TResult ToIgnoreCaseDictionary<TSource, TKey, TResult>(TSource[] source,
        Func<TSource, TKey> keySelector, Func<TResult> creator)
        where TResult : IgnoreCaseDictionary<TKey, TSource>
    {
        var dictionary = creator();
        for (var index = 0; index < source.Length; ++index)
            dictionary.Add(keySelector(source[index]), source[index]);
        return dictionary;
    }

    /// <summary>
    /// Converts to ignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="creator">The create.</param>
    /// <returns></returns>
    private static TResult ToIgnoreCaseDictionary<TSource, TKey, TResult>(List<TSource> source,
        Func<TSource, TKey> keySelector, Func<TResult> creator)
        where TResult : IgnoreCaseDictionary<TKey, TSource>
    {
        var dictionary = creator();
        foreach (var source1 in source)
            dictionary.Add(keySelector(source1), source1);
        return dictionary;
    }

    /// <summary>
    /// Converts to ignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="elementSelector">The element selector.</param>
    /// <param name="creator">The function.</param>
    /// <returns></returns>
    private static TResult ToIgnoreCaseDictionary<TSource, TKey, TElement, TResult>(TSource[] source,
        Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TResult> creator)
        where TResult : IgnoreCaseDictionary<TKey, TElement>
    {
        var dictionary = creator();
        for (var index = 0; index < source.Length; ++index)
            dictionary.Add(keySelector(source[index]), elementSelector(source[index]));
        return dictionary;
    }

    /// <summary>
    /// Converts to ignorecasedictionary.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TElement">The type of the element.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="keySelector">The key selector.</param>
    /// <param name="elementSelector">The element selector.</param>
    /// <param name="creator"></param>
    /// <returns></returns>
    private static TResult ToIgnoreCaseDictionary<TSource, TKey, TElement, TResult>(this List<TSource> source,
        Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TResult> creator)
        where TResult : IgnoreCaseDictionary<TKey, TElement>
    {
        var dictionary = creator();
        foreach (var source1 in source)
            dictionary.Add(keySelector(source1), elementSelector(source1));
        return dictionary;
    }

    #endregion private methods
}