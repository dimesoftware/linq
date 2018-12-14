using System.Collections.Generic;
using System.Linq.Expressions;

namespace System.Linq
{
    public static partial class LinqUtilities
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="obj"></param>
        /// <param name="selector"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TResult IfNotNull<TSource, TResult>(this TSource obj, Func<TSource, TResult> selector, TResult defaultValue) 
            => obj != null ? selector(obj) : defaultValue;

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="one"></param>
        /// <returns></returns>
        [Obsolete("Will be moved to the expressions library")]
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> one)
        {
            ParameterExpression candidateExpr = one.Parameters[0];
            UnaryExpression body = Expression.Not(one.Body);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

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
                if (executor(selector(s), out var r))
                    yield return r;
        }

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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> CatchExceptions<T>(this IEnumerable<T> src, Action<Exception> action = null)
        {
            using (IEnumerator<T> enumerator = src.GetEnumerator())
            {
                bool next = true;
                while (next)
                {
                    try
                    {
                        next = enumerator.MoveNext();
                    }
                    catch (Exception ex)
                    {
                        action?.Invoke(ex);

                        continue;
                    }

                    if (next)
                    {
                        yield
                        return enumerator.Current;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the <paramref name="enumerable"/> collection is null or empty
        /// </summary>
        /// <typeparam name="T">The type of the collection</typeparam>
        /// <param name="enumerable">The collection to check</param>
        /// <returns>True if any of the conditions are valid: either null or empty</returns>        
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) => !enumerable?.Any() ?? true;        
    }
}