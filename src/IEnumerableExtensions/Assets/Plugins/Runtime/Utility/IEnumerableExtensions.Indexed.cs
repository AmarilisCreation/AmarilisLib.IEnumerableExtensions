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
        /// Represents an item in a sequence along with its index.
        /// </summary>
        /// <typeparam name="T">The type of the item in the sequence.</typeparam>
        public class IndexedItem<T>
        {
            /// <summary>
            /// Gets the value of the item.
            /// </summary>
            public T Value { get; private set; }

            /// <summary>
            /// Gets the index of the item in the sequence.
            /// </summary>
            public int Index { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="IndexedItem{T}"/> class with the specified value and index.
            /// </summary>
            /// <param name="value">The value of the item.</param>
            /// <param name="index">The index of the item in the sequence.</param>
            public IndexedItem(T value, int index)
            {
                Value = value;
                Index = index;
            }
        }

        /// <summary>
        /// Enumerates a sequence and provides each element's value and index.
        /// </summary>
        /// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
        /// <param name="source">The sequence of elements to enumerate.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> of <see cref="IndexedItem{T}"/> objects containing the elements of the source sequence and their indices.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> is <c>null</c>.</exception>
        public static IEnumerable<IndexedItem<T>> Indexed<T>(this IEnumerable<T> source)
        {
            if(source == null) throw new ArgumentNullException(nameof(source));

            var index = 0;
            foreach(var item in source)
                yield return new IndexedItem<T>(item, index++);
        }
    }
}
