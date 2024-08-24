using System;
using System.Collections.Generic;
using System.Linq;

namespace AmarilisLib
{
    /// <summary>
    /// Provides extension methods for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static partial class IEnumerableExtensions
    {
        /// <summary>
        /// Groups elements in a sequence that have duplicate keys, as determined by a specified key selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TSelector">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
        /// <param name="source">The sequence of elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> of <see cref="IGrouping{TKey, TElement}"/> where each grouping contains elements that have duplicate keys.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="source"/> or <paramref name="keySelector"/> is <c>null</c>.
        /// </exception>
        public static IEnumerable<IGrouping<TSelector, TSource>> DuplicateGroup<TSource, TSelector>(this IEnumerable<TSource> source, Func<TSource, TSelector> keySelector)
        {
            if(source == null) throw new ArgumentNullException(nameof(source));
            if(keySelector == null) throw new ArgumentNullException(nameof(keySelector));

            return source.GroupBy(keySelector).Where(g => g.Count() > 1);
        }
    }
}
