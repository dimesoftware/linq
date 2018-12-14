using System.Collections.Generic;

namespace System.Linq
{
    /// <summary>
    ///
    /// </summary>
    public static partial class LinqUtilities
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="tuples"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<T2, T1>> DistinctBy<T1, T2, TKey>(this IEnumerable<Tuple<T2, T1>> tuples, Func<T1, TKey> filter)
        {
            HashSet<TKey> set = new HashSet<TKey>();
            List<Tuple<T2, T1>> list = new List<Tuple<T2, T1>>();

            foreach (Tuple<T2, T1> tuple in tuples)
            {
                TKey key = filter(tuple.Item2);
                if (set.Contains(key))
                    continue;

                list.Add(tuple);
                set.Add(key);
            }
            return list;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="items"></param>
        /// <param name="keyer"></param>
        /// <returns></returns>
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> keyer)
        {
            HashSet<TKey> set = new HashSet<TKey>();
            List<T> list = new List<T>();

            foreach (T item in items)
            {
                TKey key = keyer(item);
                if (set.Contains(key))
                    continue;

                list.Add(item);
                set.Add(key);
            }
            return list;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="items">The items to de-duplicate</param>
        /// <param name="keyer">The property on which to calculate the distinct</param>
        /// <param name="condition">Extra condition </param>
        /// <returns></returns>
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> keyer, Func<T, bool> condition)
        {
            HashSet<TKey> set = new HashSet<TKey>();
            List<T> list = new List<T>();

            foreach (T item in items)
            {
                TKey key = keyer(item);
                if (set.Contains(key))
                    continue;
                if (!condition(item))
                    continue;

                list.Add(item);
                set.Add(key);
            }
            return list;
        }
    }
}