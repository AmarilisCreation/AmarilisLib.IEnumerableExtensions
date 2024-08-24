using System;
using System.Collections;
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
        /// Groups adjacent elements of a sequence based on a key selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by the key selector function.</typeparam>
        /// <param name="source">The sequence whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> where each element is an <see cref="IGrouping{TKey, TElement}"/> containing a sequence of adjacent elements that share the same key.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="source"/> or <paramref name="keySelector"/> is <c>null</c>.
        /// </exception>
        public static IEnumerable<IGrouping<TKey, TSource>> ChunkBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return source.ChunkBy(keySelector, EqualityComparer<TKey>.Default);
        }

        /// <summary>
        /// Groups adjacent elements of a sequence based on a key selector function, using a specified equality comparer for keys.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by the key selector function.</typeparam>
        /// <param name="source">The sequence whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="comparer">An equality comparer to compare keys.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> where each element is an <see cref="IGrouping{TKey, TElement}"/> containing a sequence of adjacent elements that share the same key.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="source"/>, <paramref name="keySelector"/>, or <paramref name="comparer"/> is <c>null</c>.
        /// </exception>
        public static IEnumerable<IGrouping<TKey, TSource>> ChunkBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if(source == null) throw new ArgumentNullException(nameof(source));
            if(keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if(comparer == null) throw new ArgumentNullException(nameof(comparer));

            var noMoreSourceElements = true;
            var enumerator = source.GetEnumerator();
            if(!enumerator.MoveNext()) yield break;

            Chunk<TKey, TSource> current = null;
            while(true)
            {
                var key = keySelector(enumerator.Current);
                current = new Chunk<TKey, TSource>(key, enumerator, value => comparer.Equals(key, keySelector(value)));
                yield return current;
                if(current.CopyAllChunkElements() == noMoreSourceElements)
                    yield break;
            }
        }

        /// <summary>
        /// Represents a chunk of elements that share the same key.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TSource">The type of the elements in the chunk.</typeparam>
        class Chunk<TKey, TSource> : IGrouping<TKey, TSource>
        {
            /// <summary>
            /// Represents an element in the chunk.
            /// </summary>
            class ChunkItem
            {
                public ChunkItem(TSource value)
                {
                    Value = value;
                }
                public readonly TSource Value;
                public ChunkItem Next = null;
            }

            private readonly TKey key;
            private IEnumerator<TSource> enumerator;
            private Func<TSource, bool> predicate;
            private readonly ChunkItem head;
            private ChunkItem tail;
            internal bool isLastSourceElement = false;
            private readonly object thisLock;

            /// <summary>
            /// Initializes a new instance of the <see cref="Chunk{TKey, TSource}"/> class.
            /// </summary>
            /// <param name="key">The key shared by all elements in the chunk.</param>
            /// <param name="enumerator">The enumerator to read elements from the source sequence.</param>
            /// <param name="predicate">A function to test each element for a condition.</param>
            public Chunk(TKey key, IEnumerator<TSource> enumerator, Func<TSource, bool> predicate)
            {
                this.key = key;
                this.enumerator = enumerator;
                this.predicate = predicate;
                head = new ChunkItem(enumerator.Current);
                tail = head;
                thisLock = new object();
            }

            private bool DoneCopyingChunk { get { return tail == null; } }

            /// <summary>
            /// Copies the next element into the chunk.
            /// </summary>
            private void CopyNextChunkElement()
            {
                isLastSourceElement = !enumerator.MoveNext();
                if(isLastSourceElement || !predicate(enumerator.Current))
                {
                    enumerator = null;
                    predicate = null;
                }
                else
                {
                    tail.Next = new ChunkItem(enumerator.Current);
                }
                tail = tail.Next;
            }

            /// <summary>
            /// Copies all elements that belong to the current chunk.
            /// </summary>
            /// <returns><c>true</c> if no more elements are available from the source sequence; otherwise, <c>false</c>.</returns>
            internal bool CopyAllChunkElements()
            {
                while(true)
                {
                    lock(thisLock)
                    {
                        if(DoneCopyingChunk)
                            return isLastSourceElement;
                        else
                            CopyNextChunkElement();
                    }
                }
            }

            /// <summary>
            /// Gets the key of the current chunk.
            /// </summary>
            public TKey Key { get { return key; } }

            /// <summary>
            /// Returns an enumerator that iterates through the elements of the chunk.
            /// </summary>
            /// <returns>An enumerator that can be used to iterate through the chunk.</returns>
            public IEnumerator<TSource> GetEnumerator()
            {
                var current = head;
                while(current != null)
                {
                    yield return current.Value;
                    lock(thisLock)
                    {
                        if(current == tail)
                            CopyNextChunkElement();
                    }

                    current = current.Next;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
                => GetEnumerator();
        }
    }
}
