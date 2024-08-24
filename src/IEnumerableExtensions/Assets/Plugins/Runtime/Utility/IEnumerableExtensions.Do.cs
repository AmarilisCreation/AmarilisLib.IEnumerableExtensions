using System;
using System.Collections.Generic;

namespace AmarilisLib
{
    /// <summary>
    /// Provides extension methods for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static partial class IEnumerableExtensions
    {
        /// <summary>
        /// Performs the specified action on each element of the sequence and yields the elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">The sequence of elements to iterate over.</param>
        /// <param name="action">The action to perform on each element.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains the elements of the input sequence.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="source"/> or <paramref name="action"/> is <c>null</c>.
        /// </exception>
        public static IEnumerable<T> Do<T>(this IEnumerable<T> source, Action<T> action)
        {
            if(source == null) throw new ArgumentNullException(nameof(source));
            if(action == null) throw new ArgumentNullException(nameof(action));

            foreach(var item in source)
            {
                action.Invoke(item);
                yield return item;
            }
        }
    }
}
