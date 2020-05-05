using System.Collections.Generic;

namespace System.Linq
{
    public static class AggregateExtensions
    {
        /// <summary>
        /// Applies an accumulator function over a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TDest">The target type</typeparam>
        /// <param name="source"> An System.Collections.Generic.IEnumerable`1 to aggregate over.</param>
        /// <param name="func">An accumulator function to be invoked on each element.</param>
        /// <param name="separator"></param>
        /// <returns> The final accumulator value.</returns>
        /// <exception cref="System.ArgumentNullException">source or func is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static string Aggregate<TSource, TDest>(this IEnumerable<TSource> source, Func<TSource, TDest> func, string separator = ",")
            => source.Select(func).Pipe(x => string.Join(separator, x));

    }
}