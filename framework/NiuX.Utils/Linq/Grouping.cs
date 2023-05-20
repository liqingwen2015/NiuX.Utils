//using System.Collections.Generic;
//using System.Linq;
//using System;
//using System.Collections;

//namespace NiuX.Utils.Linq;

//public static class LinqExtensions
//{
//    sealed class Grouping<TKey, T> : IGrouping<TKey, T>
//    {
//        private readonly List<T> _items = new();
//        public Grouping(TKey key) => Key = key ?? throw new ArgumentNullException(nameof(key));

//        public TKey Key { get; }

//        public void Add(T t) => _items.Add(t);

//        public int Count => _items.Count;

//        public IEnumerator<T> GetEnumerator()
//        {
//            return _items.GetEnumerator();
//        }

//        IEnumerator IEnumerable.GetEnumerator()
//        {
//            return GetEnumerator();
//        }
//    }

//    public static IEnumerable<IGrouping<TKey, T>> GroupByEquality<T, TKey>(this IEnumerable<T> source,
//        Func<T, TKey> keySelector,
//        Func<TKey, TKey, bool> comparer)
//    {
//        var groups = new List<Grouping<TKey, T>>();
//        foreach (var item in source)
//        {
//            var key = keySelector(item);
//            var group = groups.FirstOrDefault(x => comparer(x.Key, key));
//            if (group is null)
//            {
//                group = new Grouping<TKey, T>(key);
//                group.List.Add(item);
//                groups.Add(group);
//            }
//            else
//            {
//                keyAction?.Invoke(group.Key, item);
//                group.List.Add(item);
//            }
//        }
//        return groups;
//    }

//    public static IEnumerable<IGrouping<TKey, T>> GroupByEquality<T, TKey>(this IEnumerable<T> source,
//        Func<T, TKey> keySelector,
//        Func<TKey, TKey, bool> comparer,
//        Action<TKey, T>? keyAction = null, Action<T, TKey>? itemAction = null)
//    {
//        var groups = new List<Grouping<TKey, T>>();
//        foreach (var item in source)
//        {
//            var key = keySelector(item);
//            var group = groups.FirstOrDefault(x => comparer(x.Key, key));
//            if (group is null)
//            {
//                group = new Grouping<TKey, T>(key)
//                {
//                    item
//                };
//                groups.Add(group);
//            }
//            else
//            {
//                keyAction?.Invoke(group.Key, item);
//                group.Add(item);
//            }
//        }

//        if (itemAction != null)
//        {
//            foreach (var group in groups.Where(g => g.Count > 1))
//            {
//                foreach (var item in group)
//                    itemAction.Invoke(item, group.Key);
//            }
//        }

//        return groups;
//    }
//}