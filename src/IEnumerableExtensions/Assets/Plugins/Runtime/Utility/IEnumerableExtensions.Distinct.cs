using System;
using System.Linq;
using System.Collections.Generic;

namespace AmarilisLib
{
    /// <summary>
    /// Provides extension methods for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static partial class IEnumerableExtensions
    {
        /// <summary>
        /// Returns distinct elements from a sequence by using a specified key selector function to determine uniqueness.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the sequence.</typeparam>
        /// <typeparam name="TKey">The type of the key used to determine uniqueness.</typeparam>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <param name="selector">A function to extract the key for each element.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains distinct elements from the input sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="source"/> or <paramref name="selector"/> is <c>null</c>.
        /// </exception>
        public static IEnumerable<T> Distinct<T, TKey>(this IEnumerable<T> source, Func<T, TKey> selector)
        {
            if(source == null) throw new ArgumentNullException(nameof(source));
            if(selector == null) throw new ArgumentNullException(nameof(selector));

            return source.Distinct(new LambdaEqualityComparer<T, TKey>(selector));
        }
    }
}
