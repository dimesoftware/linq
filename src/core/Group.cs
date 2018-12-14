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
        /// <typeparam name="TElement"></typeparam>
        /// <param name="elements"></param>
        /// <param name="groupSelectors"></param>
        /// <returns></returns>
        public static IEnumerable<IGrouping<object, TElement>> GroupByMany<TElement>(this IEnumerable<TElement> elements, params Func<TElement, object>[] groupSelectors)
        {
            if (groupSelectors.Length == 0)
                return null;

            Func<TElement, object> selector = groupSelectors.First();
            return elements.GroupBy(selector);
        }
    }
}