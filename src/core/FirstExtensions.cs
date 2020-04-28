using System.Collections.Generic;

namespace System.Linq
{
    public static class FirstExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="filter"></param>
        /// <param name="alternateFilter"></param>
        /// <returns></returns>
        public static T First<T>(this IEnumerable<T> source, Func<T, bool> filter, Func<T, bool> alternateFilter) where T : class
            => source.FirstOrDefault(filter) ?? source.First(alternateFilter);

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="filter"></param>
        /// <param name="alternateFilter"></param>
        /// <returns></returns>
        public static T FirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> filter, Func<T, bool> alternateFilter) where T : class
            => source.FirstOrDefault(filter) ?? source.FirstOrDefault(alternateFilter);
    }
}