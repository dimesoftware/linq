using System.Collections.Generic;

namespace System.Linq
{
    /// <summary>
    ///
    /// </summary>
    public static class GroupExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <param name="elements"></param>
        /// <param name="groupSelectors"></param>
        /// <returns></returns>
        public static IEnumerable<IGrouping<object, TElement>> GroupByMany<TElement>(this IEnumerable<TElement> elements, params Func<TElement, object>[] groupSelectors)
            => groupSelectors.Length == 0 ? null : elements.GroupBy(item => groupSelectors.Select(sel => sel.Invoke(item)).ToArray(), new ColumnComparer());
    }
}