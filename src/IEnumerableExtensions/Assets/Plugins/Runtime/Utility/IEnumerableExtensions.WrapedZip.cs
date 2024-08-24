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
        /// Represents a pair of elements from two sequences.
        /// </summary>
        /// <typeparam name="TFirst">The type of the elements in the first sequence.</typeparam>
        /// <typeparam name="TSecond">The type of the elements in the second sequence.</typeparam>
        public class WrapedZipItem<TFirst, TSecond>
        {
            /// <summary>
            /// Gets the element from the first sequence.
            /// </summary>
            public TFirst First { get; private set; }

            /// <summary>
            /// Gets the element from the second sequence.
            /// </summary>
            public TSecond Second { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="WrapedZipItem{TFirst, TSecond}"/> class with the specified elements.
            /// </summary>
            /// <param name="firstItem">The element from the first sequence.</param>
            /// <param name="secondItem">The element from the second sequence.</param>
            public WrapedZipItem(TFirst firstItem, TSecond secondItem)
            {
                First = firstItem;
                Second = secondItem;
            }
        }

        /// <summary>
        /// Merges two sequences by creating a sequence of pairs, where each pair consists of corresponding elements from the two input sequences.
        /// </summary>
        /// <typeparam name="TFirst">The type of the elements in the first input sequence.</typeparam>
        /// <typeparam name="TSecond">The type of the elements in the second input sequence.</typeparam>
        /// <param name="first">The first sequence to merge.</param>
        /// <param name="second">The second sequence to merge.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> that contains pairs of elements, where each pair consists of an element from the first sequence and an element from the second sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="first"/> or <paramref name="second"/> is <c>null</c>.</exception>
        public static IEnumerable<WrapedZipItem<TFirst, TSecond>> WrapedZip<TFirst, TSecond>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second)
        {
            if(first == null) throw new ArgumentNullException(nameof(first));
            if(second == null) throw new ArgumentNullException(nameof(second));

            using(var firstSeq = first.GetEnumerator())
            using(var secondSeq = second.GetEnumerator())
            {
                while(firstSeq.MoveNext() && secondSeq.MoveNext())
                {
                    yield return new WrapedZipItem<TFirst, TSecond>(firstSeq.Current, secondSeq.Current);
                }
            }
        }
    }
}
