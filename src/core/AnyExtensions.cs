using System.Collections.Generic;

namespace System.Linq
{
    public static partial class AnyExtensions
    {
        /// <summary>
        /// Checks if the <paramref name="enumerable"/> collection is null or empty
        /// </summary>
        /// <typeparam name="T">The type of the collection</typeparam>
        /// <param name="enumerable">The collection to check</param>
        /// <returns>True if any of the conditions are valid: either null or empty</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable) => !enumerable?.Any() ?? true;
    }
}