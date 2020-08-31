using System.Collections.Generic;

namespace System.Linq
{
    public static class ForkExtensions
    {
        /// <summary>
        /// Splits up the collection into a selection of records that match the filter and one pile of rejects.
        /// </summary>
        /// <typeparam name="T">The collection type</typeparam>
        /// <param name="source">The records to filter</param>
        /// <param name="predicate">The filter to evaluate on the records</param>
        /// <returns>A tuple with a success and reject pile</returns>
        public static (IEnumerable<T> matches, IEnumerable<T> nonMatches) Fork<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            ILookup<bool, T> groupedByMatching = source.ToLookup(predicate);
            return (groupedByMatching[true], groupedByMatching[false]);
        }
    }
}