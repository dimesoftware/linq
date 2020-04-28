using System.Collections.Generic;

namespace System.Linq
{
    public static class ExceptionHandler
    {
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
    }
}