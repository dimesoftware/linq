using System.Collections.Generic;

namespace System.Linq
{
    /// <summary>
    ///
    /// </summary>
    public static class DistinctExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="tuples"></param>
        /// <param name="qualifier"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<T2, T1>> DistinctBy<T1, T2, TKey>(this IEnumerable<Tuple<T2, T1>> tuples, Func<T1, TKey> qualifier)
        {
            HashSet<TKey> set = new HashSet<TKey>();
            foreach (Tuple<T2, T1> tuple in tuples)
            {
                TKey key = qualifier(tuple.Item2);
                if (set.Contains(key))
                    continue;

                yield return tuple;
                set.Add(key);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="items"></param>
        /// <param name="qualifier"></param>
        /// <returns></returns>
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> qualifier)
        {
            HashSet<TKey> set = new HashSet<TKey>();

            foreach (T item in items)
            {
                TKey key = qualifier(item);
                if (set.Contains(key))
                    continue;

                yield return item;
                set.Add(key);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="items">The items to de-duplicate</param>
        /// <param name="qualifier">The property on which to calculate the distinct</param>
        /// <param name="condition">Extra condition </param>
        /// <returns></returns>
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> qualifier, Func<T, bool> condition)
        {
            HashSet<TKey> set = new HashSet<TKey>();
            foreach (T item in items)
            {
                TKey key = qualifier(item);
                if (set.Contains(key))
                    continue;
                if (!condition(item))
                    continue;

                yield return item;
                set.Add(key);
            }
        }
    }
}