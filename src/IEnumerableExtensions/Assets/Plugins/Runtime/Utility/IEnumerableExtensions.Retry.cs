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
        /// Repeats the sequence indefinitely.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">The sequence of elements to repeat.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that repeats the elements of the source sequence indefinitely.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <c>null</c>.</exception>
        public static IEnumerable<T> Retry<T>(this IEnumerable<T> source)
        {
            if(source == null) throw new ArgumentNullException(nameof(source));

            while(true)
                foreach(var value in source)
                    yield return value;
        }

        /// <summary>
        /// Repeats the sequence a specified number of times.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">The sequence of elements to repeat.</param>
        /// <param name="count">The number of times to repeat the sequence.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that repeats the elements of the source sequence the specified number of times.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="count"/> is less than zero.</exception>
        public static IEnumerable<T> Retry<T>(this IEnumerable<T> source, int count)
        {
            if(source == null) throw new ArgumentNullException(nameof(source));
            if(count < 0) throw new ArgumentOutOfRangeException(nameof(count), "Count must be non-negative.");

            for(var i = 0; i < count; i++)
                foreach(var value in source)
                    yield return value;
        }
    }
}
