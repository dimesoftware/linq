using System.Collections.Generic;

namespace System.Linq
{
    public static class SelectExtensions
    {
        /// <summary>
        /// Loops through each item in the source list and converts the value to the specified result type
        /// </summary>
        /// <typeparam name="TSource">The type of the source list</typeparam>
        /// <typeparam name="TResult">The struct to which to convert to</typeparam>
        /// <param name="src">The source list to convert</param>
        /// <returns>The same list as the source but with a different type</returns>
        public static IEnumerable<TResult> ConvertTo<TSource, TResult>(this IEnumerable<TSource> src) where TResult : struct
        {
            // Get the enumerator
            using (IEnumerator<TSource> enumerator = src.GetEnumerator())
            {
                // Loop through each item
                while (enumerator.MoveNext())
                {
                    // Convert the source value to an instance of the TResult type
                    TResult parsedValue = (TResult)Convert.ChangeType(enumerator.Current, typeof(TResult));
                    yield return parsedValue;
                }
            }
        }

        public delegate bool TryFunc<in TSource, TResult>(TSource arg, out TResult result);

        ///  <summary>
        ///
        ///  </summary>
        ///  <typeparam name="TSource"></typeparam>
        ///  <typeparam name="TResult"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source"></param>
        ///  <param name="selector"></param>
        /// <param name="executor"></param>
        /// <returns></returns>
        public static IEnumerable<TResult> SelectTry<TSource, TValue, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TValue> selector,
            TryFunc<TValue, TResult> executor)
        {
            foreach (TSource s in source)
                if (executor(selector(s), out TResult r))
                    yield return r;
        }
    }
}