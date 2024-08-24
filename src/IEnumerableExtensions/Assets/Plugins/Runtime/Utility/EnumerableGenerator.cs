using System;
using System.Collections.Generic;
using System.Linq;

namespace AmarilisLib
{
    /// <summary>
    /// Provides static methods to generate various enumerable sequences, including infinite sequences, random sequences, ranges, and repeated elements.
    /// </summary>
    public static partial class EnumerableGenerator
    {
        /// <summary>
        /// Generates an infinite sequence of integers starting from 0.
        /// </summary>
        /// <returns>An infinite sequence of integers.</returns>
        public static IEnumerable<int> Infinite()
        {
            int i = 0;
            while(true) yield return i++;
        }

        /// <summary>
        /// Generates an infinite sequence of random integers using a default random number generator.
        /// </summary>
        /// <returns>An infinite sequence of random integers.</returns>
        public static IEnumerable<int> InfiniteRandom()
            => InfiniteRandom(ThreadSafeRandom.Instance);

        /// <summary>
        /// Generates an infinite sequence of random integers using the specified random number generator.
        /// </summary>
        /// <param name="random">The random number generator to use.</param>
        /// <returns>An infinite sequence of random integers.</returns>
        public static IEnumerable<int> InfiniteRandom(Random random)
        {
            while(true) yield return random.Next(0, int.MaxValue);
        }

        /// <summary>
        /// Generates an infinite sequence of random integers between 0 (inclusive) and a specified maximum (exclusive) value using a default random number generator.
        /// </summary>
        /// <param name="max">The exclusive upper bound of the random numbers.</param>
        /// <returns>An infinite sequence of random integers.</returns>
        public static IEnumerable<int> InfiniteRandom(int max)
            => InfiniteRandom(max, ThreadSafeRandom.Instance);

        /// <summary>
        /// Generates an infinite sequence of random integers between 0 (inclusive) and a specified maximum (exclusive) value using the specified random number generator.
        /// </summary>
        /// <param name="max">The exclusive upper bound of the random numbers.</param>
        /// <param name="random">The random number generator to use.</param>
        /// <returns>An infinite sequence of random integers.</returns>
        public static IEnumerable<int> InfiniteRandom(int max, Random random)
        {
            while(true) yield return random.Next(max);
        }

        /// <summary>
        /// Generates an infinite sequence of random integers within a specified range using a default random number generator.
        /// </summary>
        /// <param name="min">The inclusive lower bound of the random numbers.</param>
        /// <param name="max">The exclusive upper bound of the random numbers.</param>
        /// <returns>An infinite sequence of random integers.</returns>
        public static IEnumerable<int> InfiniteRandom(int min, int max)
            => InfiniteRandom(min, max, ThreadSafeRandom.Instance);

        /// <summary>
        /// Generates an infinite sequence of random integers within a specified range using the specified random number generator.
        /// </summary>
        /// <param name="min">The inclusive lower bound of the random numbers.</param>
        /// <param name="max">The exclusive upper bound of the random numbers.</param>
        /// <param name="random">The random number generator to use.</param>
        /// <returns>An infinite sequence of random integers.</returns>
        public static IEnumerable<int> InfiniteRandom(int min, int max, Random random)
        {
            while(true) yield return random.Next(min, max);
        }

        /// <summary>
        /// Generates a finite sequence of random integers with the specified count using a default random number generator.
        /// </summary>
        /// <param name="count">The number of random integers to generate.</param>
        /// <returns>A finite sequence of random integers.</returns>
        public static IEnumerable<int> Random(int count)
            => InfiniteRandom().Take(count);

        /// <summary>
        /// Generates a finite sequence of random integers with the specified count using the specified random number generator.
        /// </summary>
        /// <param name="count">The number of random integers to generate.</param>
        /// <param name="random">The random number generator to use.</param>
        /// <returns>A finite sequence of random integers.</returns>
        public static IEnumerable<int> Random(int count, Random random)
            => InfiniteRandom(random).Take(count);

        /// <summary>
        /// Generates a finite sequence of random integers between 0 (inclusive) and a specified maximum (exclusive) value with the specified count using a default random number generator.
        /// </summary>
        /// <param name="max">The exclusive upper bound of the random numbers.</param>
        /// <param name="count">The number of random integers to generate.</param>
        /// <returns>A finite sequence of random integers.</returns>
        public static IEnumerable<int> Random(int max, int count)
            => InfiniteRandom(max).Take(count);

        /// <summary>
        /// Generates a finite sequence of random integers between 0 (inclusive) and a specified maximum (exclusive) value with the specified count using the specified random number generator.
        /// </summary>
        /// <param name="max">The exclusive upper bound of the random numbers.</param>
        /// <param name="count">The number of random integers to generate.</param>
        /// <param name="random">The random number generator to use.</param>
        /// <returns>A finite sequence of random integers.</returns>
        public static IEnumerable<int> Random(int max, int count, Random random)
            => InfiniteRandom(max, random).Take(count);

        /// <summary>
        /// Generates a finite sequence of random integers within a specified range with the specified count using a default random number generator.
        /// </summary>
        /// <param name="min">The inclusive lower bound of the random numbers.</param>
        /// <param name="max">The exclusive upper bound of the random numbers.</param>
        /// <param name="count">The number of random integers to generate.</param>
        /// <returns>A finite sequence of random integers.</returns>
        public static IEnumerable<int> Random(int min, int max, int count)
            => InfiniteRandom(min, max).Take(count);

        /// <summary>
        /// Generates a finite sequence of random integers within a specified range with the specified count using the specified random number generator.
        /// </summary>
        /// <param name="min">The inclusive lower bound of the random numbers.</param>
        /// <param name="max">The exclusive upper bound of the random numbers.</param>
        /// <param name="count">The number of random integers to generate.</param>
        /// <param name="random">The random number generator to use.</param>
        /// <returns>A finite sequence of random integers.</returns>
        public static IEnumerable<int> Random(int min, int max, int count, Random random)
            => InfiniteRandom(min, max, random).Take(count);

        /// <summary>
        /// Generates a sequence of integers within a specified range.
        /// </summary>
        /// <param name="start">The starting integer of the sequence.</param>
        /// <param name="count">The number of integers to generate.</param>
        /// <returns>A sequence of integers.</returns>
        public static IEnumerable<int> Range(int start, int count)
            => Enumerable.Range(start, count);

        /// <summary>
        /// Generates a sequence of integers starting from 0 with the specified count.
        /// </summary>
        /// <param name="count">The number of integers to generate.</param>
        /// <returns>A sequence of integers.</returns>
        public static IEnumerable<int> Range(int count)
            => Range(0, count);

        /// <summary>
        /// Generates an infinite sequence where all elements are the specified value.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="element">The value to be repeated infinitely.</param>
        /// <returns>An infinite sequence of the specified value.</returns>
        public static IEnumerable<T> InfiniteRepeat<T>(T element)
        {
            while(true) yield return element;
        }

        /// <summary>
        /// Generates a sequence where the specified value is repeated a given number of times.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="element">The value to be repeated.</param>
        /// <param name="count">The number of times to repeat the value.</param>
        /// <returns>A sequence of repeated values.</returns>
        public static IEnumerable<T> Repeat<T>(T element, int count)
            => Enumerable.Repeat(element, count);

        /// <summary>
        /// Generates a sequence containing a single element.
        /// </summary>
        /// <typeparam name="T">The type of the element.</typeparam>
        /// <param name="element">The single element to include in the sequence.</param>
        /// <returns>A sequence containing the specified single element.</returns>
        public static IEnumerable<T> Return<T>(T element)
        {
            yield return element;
        }

        /// <summary>
        /// Generates a sequence containing the specified elements.
        /// </summary>
        /// <typeparam name="T">The type of the elements.</typeparam>
        /// <param name="elements">The elements to include in the sequence.</param>
        /// <returns>A sequence containing the specified elements.</returns>
        public static IEnumerable<T> Return<T>(params T[] elements)
            => elements;

        /// <summary>
        /// Returns an empty sequence of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the sequence.</typeparam>
        /// <returns>An empty sequence of the specified type.</returns>
        public static IEnumerable<T> Empty<T>()
            => Enumerable.Empty<T>();
    }
}
