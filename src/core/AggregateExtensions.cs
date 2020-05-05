using System.Collections.Generic;

namespace System.Linq
{
    public static class AggregateExtensions
    {
        /// <summary>
        /// Applies an accumulator function over a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source"> An System.Collections.Generic.IEnumerable`1 to aggregate over.</param>
        /// <param name="func">An accumulator function to be invoked on each element.</param>
        /// <returns> The final accumulator value.</returns>
        /// <exception cref="System.ArgumentNullException">source or func is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static string Aggregate<TSource>(this IEnumerable<TSource> source, Func<IEnumerable<TSource>, string> func)
            => source.Pipe(func);

    }
}