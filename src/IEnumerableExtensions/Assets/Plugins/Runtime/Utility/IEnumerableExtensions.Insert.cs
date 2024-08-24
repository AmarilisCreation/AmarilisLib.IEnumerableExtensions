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
        /// Inserts a single item into a sequence at the specified index.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="src">The source sequence into which the item will be inserted.</param>
        /// <param name="insertItem">The item to insert into the sequence.</param>
        /// <param name="index">The zero-based index at which the item should be inserted.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> that contains the elements of the source sequence with the specified item inserted at the specified index.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="src"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="index"/> is less than zero.</exception>
        public static IEnumerable<T> Insert<T>(this IEnumerable<T> src, T insertItem, int index)
        {
            if(src == null) throw new ArgumentNullException(nameof(src));
            if(index < 0) throw new ArgumentOutOfRangeException(nameof(index), "Index must be non-negative.");

            var i = 0;
            foreach(var item in src)
            {
                if(i == index) yield return insertItem;
                yield return item;
                i++;
            }

            if(i == index) yield return insertItem;
        }

        /// <summary>
        /// Inserts a sequence of items into another sequence at the specified index.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="src">The source sequence into which the items will be inserted.</param>
        /// <param name="insertSrc">The sequence of items to insert into the source sequence.</param>
        /// <param name="index">The zero-based index at which the sequence of items should be inserted.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> that contains the elements of the source sequence with the specified sequence of items inserted at the specified index.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="src"/> or <paramref name="insertSrc"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="index"/> is less than zero.</exception>
        public static IEnumerable<T> Insert<T>(this IEnumerable<T> src, IEnumerable<T> insertSrc, int index)
        {
            if(src == null) throw new ArgumentNullException(nameof(src));
            if(insertSrc == null) throw new ArgumentNullException(nameof(insertSrc));
            if(index < 0) throw new ArgumentOutOfRangeException(nameof(index), "Index must be non-negative.");

            var i = 0;
            foreach(var item in src)
            {
                if(i == index)
                    foreach(var insertItem in insertSrc) yield return insertItem;

                yield return item;
                i++;
            }

            if(i == index)
                foreach(var insertItem in insertSrc) yield return insertItem;
        }
    }
}
