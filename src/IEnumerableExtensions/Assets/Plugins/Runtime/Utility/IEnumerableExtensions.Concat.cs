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
        /// Concatenates a sequence with an array of elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the sequences.</typeparam>
        /// <param name="first">The first sequence to concatenate.</param>
        /// <param name="second">An array of elements to concatenate to the first sequence.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> that contains the concatenated elements of the input sequence and the array.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="first"/> is <c>null</c>.
        /// </exception>
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> first, params T[] second)
        {
            if(first == null) throw new ArgumentNullException(nameof(first));

            return Enumerable.Concat(first, second);
        }

        /// <summary>
        /// Concatenates multiple sequences into a single sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the sequences.</typeparam>
        /// <param name="source">A sequence of sequences to concatenate.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> that contains the concatenated elements of the input sequences.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="source"/> is <c>null</c>.
        /// </exception>
        public static IEnumerable<T> Concat<T>(this IEnumerable<IEnumerable<T>> source)
        {
            if(source == null) throw new ArgumentNullException(nameof(source));

            foreach(var list in source)
                if(list != null)
                    foreach(var value in list)
                        yield return value;
        }
    }
}
