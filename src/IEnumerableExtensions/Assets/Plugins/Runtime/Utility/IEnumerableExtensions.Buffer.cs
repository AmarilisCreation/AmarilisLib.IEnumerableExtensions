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
        /// Splits the elements of a sequence into chunks of a specified size.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="source">The sequence to split into chunks.</param>
        /// <param name="count">The size of each chunk.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> where each element is an <see cref="IEnumerable{T}"/> containing a chunk of elements from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than or equal to zero.</exception>
        /// <remarks>
        /// The last chunk may contain fewer elements if the number of elements in the source sequence is not evenly divisible by <paramref name="count"/>.
        /// </remarks>
        public static IEnumerable<IEnumerable<T>> Buffer<T>(this IEnumerable<T> source, int count)
        {
            if(source == null) throw new ArgumentNullException(nameof(source));
            if(count <= 0) throw new ArgumentOutOfRangeException(nameof(count), "Count must be greater than zero.");

            var result = new List<T>(count);
            foreach(var item in source)
            {
                result.Add(item);
                if(result.Count == count)
                {
                    yield return result;
                    result = new List<T>(count);
                }
            }

            if(result.Count != 0) yield return result;
        }
    }
}
