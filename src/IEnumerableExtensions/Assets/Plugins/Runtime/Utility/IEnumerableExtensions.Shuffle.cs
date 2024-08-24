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
        /// Returns a new sequence with the elements of the source sequence in random order using Fisher-Yates shuffle.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">The sequence of elements to shuffle.</param>
        /// <returns>A new <see cref="IEnumerable{T}"/> that contains the elements of the source sequence in random order.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <c>null</c>.</exception>
        public static IEnumerable<TSource> Shuffle<TSource>(this IEnumerable<TSource> source)
            => source.Shuffle(ThreadSafeRandom.Instance);

        /// <summary>
        /// Returns a new sequence with the elements of the source sequence in random order using Fisher-Yates shuffle and a specified random number generator.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">The sequence of elements to shuffle.</param>
        /// <param name="random">The random number generator to use.</param>
        /// <returns>A new <see cref="IEnumerable{T}"/> that contains the elements of the source sequence in random order.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="source"/> or <paramref name="random"/> is <c>null</c>.
        /// </exception>
        public static IEnumerable<TSource> Shuffle<TSource>(this IEnumerable<TSource> source, Random random)
        {
            if(source == null) throw new ArgumentNullException(nameof(source));
            if(random == null) throw new ArgumentNullException(nameof(random));

            var list = new List<TSource>(source);
            int n = list.Count;

            for(int i = n - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                var temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }

            return list;
        }
    }
}
