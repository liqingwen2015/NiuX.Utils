/* 项目“NiuX.Utils (netstandard2.1)”的未合并的更改
在此之前:
using System.Runtime.Serialization;
在此之后:
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
*/

using JetBrains.Annotations;
using System.Runtime.Serialization;

namespace NiuX.System.Collections.Generic;

public class IgnoreCaseHashSet<T> : HashSet<T>
{
    private static readonly IEqualityComparer<T> EqualityComparer =
        IgnoreCaseDictionaryOptions<T>.DefaultEqualityComparer;

    public IgnoreCaseHashSet() : this(EqualityComparer)
    {
    }

    public IgnoreCaseHashSet([NotNull] IEnumerable<T> collection) : this(collection, EqualityComparer)
    {
    }

    protected IgnoreCaseHashSet([NotNull] IEnumerable<T> collection, IEqualityComparer<T> comparer) : base(collection,
        comparer)
    {
    }

    protected IgnoreCaseHashSet(IEqualityComparer<T> comparer) : base(comparer)
    {
    }

#if NETSTANDARD2_1_OR_GREATER
    public IgnoreCaseHashSet(int capacity) : base(capacity, EqualityComparer)
    {
    }

    protected IgnoreCaseHashSet(int capacity, IEqualityComparer<T> comparer) : base(capacity, comparer)
    {
    }
#endif

    protected IgnoreCaseHashSet(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}