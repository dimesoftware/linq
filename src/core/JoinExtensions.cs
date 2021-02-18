using System.Collections.Generic;

namespace System.Linq
{
    public static class JoinExtensions
    {
        public static IEnumerable<TResult> FullOuterJoin<TA, TB, TKey, TResult>(
        this IEnumerable<TA> a,
        IEnumerable<TB> b,
        Func<TA, TKey> selectKeyA,
        Func<TB, TKey> selectKeyB,
        Func<TA, TB, TKey, TResult> projection,
        TA defaultA = default,
        TB defaultB = default,
        IEqualityComparer<TKey> cmp = null)
        {
            cmp ??= EqualityComparer<TKey>.Default;
            ILookup<TKey, TA> alookup = a.ToLookup(selectKeyA, cmp);
            ILookup<TKey, TB> blookup = b.ToLookup(selectKeyB, cmp);

            HashSet<TKey> keys = new HashSet<TKey>(alookup.Select(p => p.Key), cmp);
            keys.UnionWith(blookup.Select(p => p.Key));

            return from key in keys
                   from xa in alookup[key].DefaultIfEmpty(defaultA)
                   from xb in blookup[key].DefaultIfEmpty(defaultB)
                   select projection(xa, xb, key);
        }

        public static IEnumerable<TResult> FullOuterGroupJoin<TA, TB, TKey, TResult>(
           this IEnumerable<TA> a,
           IEnumerable<TB> b,
           Func<TA, TKey> selectKeyA,
           Func<TB, TKey> selectKeyB,
           Func<IEnumerable<TA>, IEnumerable<TB>, TKey, TResult> projection, IEqualityComparer<TKey> cmp = null)
        {
            cmp ??= EqualityComparer<TKey>.Default;
            ILookup<TKey, TA> alookup = a.ToLookup(selectKeyA, cmp);
            ILookup<TKey, TB> blookup = b.ToLookup(selectKeyB, cmp);

            HashSet<TKey> keys = new HashSet<TKey>(alookup.Select(p => p.Key), cmp);
            keys.UnionWith(blookup.Select(p => p.Key));

            return from key in keys
                   let xa = alookup[key]
                   let xb = blookup[key]
                   select projection(xa, xb, key);
        }

        public static IEnumerable<TResult> LeftOuterJoin<TLeft, TRight, TKey, TResult>(
            this IEnumerable<TLeft> left,
            IEnumerable<TRight> right,
            Func<TLeft, TKey> leftKey,
            Func<TRight, TKey> rightKey,
            Func<TLeft, TRight, TResult> result)
            => left
            .GroupJoin(right, leftKey, rightKey, (l, r) => (l, r))
            .SelectMany(o => o.r.DefaultIfEmpty(), (l, r) => new { lft = l.l, rght = r })
            .Select(o => result.Invoke(o.lft, o.rght));

        public static IEnumerable<T> Merge<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, T> operation)
        {
            using IEnumerator<T> iterator1 = first.GetEnumerator();
            using IEnumerator<T> iterator2 = second.GetEnumerator();

            while (iterator1.MoveNext())
                yield return iterator2.MoveNext() ?
                    operation(iterator1.Current, iterator2.Current) :
                    iterator1.Current;

            while (iterator2.MoveNext())
                yield return iterator2.Current;
        }

        public static IEnumerable<TResult> Merge<TFirst, TSecond, TResult>(
            this IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            Func<TFirst, TSecond, TResult> operation)
        {
            using IEnumerator<TFirst> iterator1 = first.GetEnumerator();
            using IEnumerator<TSecond> iterator2 = second.GetEnumerator();

            while (iterator1.MoveNext())
                yield return iterator2.MoveNext() ?
                    operation(iterator1.Current, iterator2.Current) :
                    operation(iterator1.Current, default);

            while (iterator2.MoveNext())
                yield return operation(default, iterator2.Current);
        }
    }
}