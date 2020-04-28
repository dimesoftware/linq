using System.Collections.Generic;

namespace System.Linq
{
    public static class JoinExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TA"></typeparam>
        /// <typeparam name="TB"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="selectKeyA"></param>
        /// <param name="selectKeyB"></param>
        /// <param name="projection"></param>
        /// <param name="defaultA"></param>
        /// <param name="defaultB"></param>
        /// <param name="cmp"></param>
        /// <returns></returns>
        /// <remarks>Courtesy of https://stackoverflow.com/a/13503860/1842261 </remarks>
        public static IEnumerable<TResult> FullOuterJoin<TA, TB, TKey, TResult>(
        this IEnumerable<TA> a,
        IEnumerable<TB> b,
        Func<TA, TKey> selectKeyA,
        Func<TB, TKey> selectKeyB,
        Func<TA, TB, TKey, TResult> projection,
        TA defaultA = default(TA),
        TB defaultB = default(TB),
        IEqualityComparer<TKey> cmp = null)
        {
            cmp = cmp ?? EqualityComparer<TKey>.Default;
            ILookup<TKey, TA> alookup = a.ToLookup(selectKeyA, cmp);
            ILookup<TKey, TB> blookup = b.ToLookup(selectKeyB, cmp);

            HashSet<TKey> keys = new HashSet<TKey>(alookup.Select(p => p.Key), cmp);
            keys.UnionWith(blookup.Select(p => p.Key));

            return from key in keys
                   from xa in alookup[key].DefaultIfEmpty(defaultA)
                   from xb in blookup[key].DefaultIfEmpty(defaultB)
                   select projection(xa, xb, key);          
        }

        /// <summary>
        /// Zips the (uneven) collections together
        /// </summary>
        /// <typeparam name="T">The collection type</typeparam>
        /// <param name="first">The first collection to join</param>
        /// <param name="second">The second collection to join</param>
        /// <param name="operation">The action to take on the joined collection</param>
        /// <returns>Collection of merged items</returns>
        public static IEnumerable<T> Merge<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, T> operation)
        {
            using (IEnumerator<T> iterator1 = first.GetEnumerator())
            using (IEnumerator<T> iterator2 = second.GetEnumerator())
            {
                while (iterator1.MoveNext())
                    yield return iterator2.MoveNext() ?
                        operation(iterator1.Current, iterator2.Current) :
                        iterator1.Current;

                while (iterator2.MoveNext())
                    yield return iterator2.Current;
            }
        }

        /// <summary>
        /// Zips the (uneven) collections together
        /// </summary>
        /// <typeparam name="TFirst">The type of the first collection</typeparam>
        /// <typeparam name="TSecond">The type of the second collection</typeparam>
        /// <typeparam name="TResult">The type that is returned from the zipped collection</typeparam>
        /// <param name="first">The first collection to join</param>
        /// <param name="second">The second collection to join</param>
        /// <param name="operation">The action to take on the joined collection</param>
        /// <returns>Collection of merged items</returns>
        public static IEnumerable<TResult> Merge<TFirst, TSecond, TResult>(
            this IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            Func<TFirst, TSecond, TResult> operation)
        {
            using (IEnumerator<TFirst> iterator1 = first.GetEnumerator())
            using (IEnumerator<TSecond> iterator2 = second.GetEnumerator())
            {
                while (iterator1.MoveNext())
                    yield return iterator2.MoveNext() ?
                        operation(iterator1.Current, iterator2.Current) :
                        operation(iterator1.Current, default(TSecond));

                while (iterator2.MoveNext())
                    yield return operation(default(TFirst), iterator2.Current);
            }
        }
    }
}