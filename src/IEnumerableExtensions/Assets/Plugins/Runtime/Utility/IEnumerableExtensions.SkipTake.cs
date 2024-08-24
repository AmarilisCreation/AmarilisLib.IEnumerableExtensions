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
        /// Skips a specified number of elements in a sequence and then takes a specified number of elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="source">The sequence to return elements from.</param>
        /// <param name="skipCount">The number of elements to skip before starting to take elements.</param>
        /// <param name="length">The number of elements to take after skipping.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains the specified range of elements from the input sequence.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="skipCount"/> or <paramref name="length"/> is less than zero.</exception>
        public static IEnumerable<T> SkipTake<T>(this IEnumerable<T> source, int skipCount, int length)
        {
            if(source == null) throw new ArgumentNullException(nameof(source));
            if(skipCount < 0) throw new ArgumentOutOfRangeException(nameof(skipCount), "Skip count must be non-negative.");
            if(length < 0) throw new ArgumentOutOfRangeException(nameof(length), "Length must be non-negative.");

            int i = 0;
            foreach(var item in source)
            {
                if(i >= skipCount)
                {
                    if(i < skipCount + length)
                        yield return item;
                    else
                        break;
                }
                i++;
            }
        }
    }
}
