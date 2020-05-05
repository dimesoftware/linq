namespace System.Linq
{
    public static class GenericExtensions
    {
        /// <summary>
        /// Wraps the item in a pipe
        /// </summary>
        /// <typeparam name="TIn">The target type</typeparam>
        /// <typeparam name="TOut">The destination type</typeparam>
        /// <param name="item">The item</param>
        /// <param name="func">The condition</param>
        /// <returns>An item of <see cref="TOut"/></returns>
        public static TOut Pipe<TIn, TOut>(this TIn item, Func<TIn, TOut> func) => func(item);
    }
}