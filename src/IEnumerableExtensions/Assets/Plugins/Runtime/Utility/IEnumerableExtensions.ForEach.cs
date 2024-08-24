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
        /// Iterates over each element in the sequence and performs no action.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence of elements to iterate over.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="source"/> is <c>null</c>.
        /// </exception>
        public static void ForEach<T>(this IEnumerable<T> source)
            => ForEach(source, Stubs.Ignore, Stubs.Throw, Stubs.Nop);

        /// <summary>
        /// Iterates over each element in the sequence and performs the specified action.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence of elements to iterate over.</param>
        /// <param name="onNext">The action to perform on each element.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="source"/> or <paramref name="onNext"/> is <c>null</c>.
        /// </exception>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> onNext)
            => ForEach(source, onNext, Stubs.Throw, Stubs.Nop);

        /// <summary>
        /// Iterates over each element in the sequence, performs the specified action, and then calls a completion action.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence of elements to iterate over.</param>
        /// <param name="onNext">The action to perform on each element.</param>
        /// <param name="onCompleted">The action to call when iteration is complete.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="source"/>, <paramref name="onNext"/>, or <paramref name="onCompleted"/> is <c>null</c>.
        /// </exception>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> onNext, Action onCompleted)
            => ForEach(source, onNext, Stubs.Throw, onCompleted);

        /// <summary>
        /// Iterates over each element in the sequence, performs the specified action, and handles any exceptions.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence of elements to iterate over.</param>
        /// <param name="onNext">The action to perform on each element.</param>
        /// <param name="onError">The action to call if an exception occurs.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="source"/>, <paramref name="onNext"/>, or <paramref name="onError"/> is <c>null</c>.
        /// </exception>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> onNext, Action<Exception> onError)
            => ForEach(source, onNext, onError, Stubs.Nop);

        /// <summary>
        /// Iterates over each element in the sequence, performs the specified action, handles any exceptions, and calls a completion action.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">The sequence of elements to iterate over.</param>
        /// <param name="onNext">The action to perform on each element.</param>
        /// <param name="onError">The action to call if an exception occurs.</param>
        /// <param name="onCompleted">The action to call when iteration is complete.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="source"/>, <paramref name="onNext"/>, <paramref name="onError"/>, or <paramref name="onCompleted"/> is <c>null</c>.
        /// </exception>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> onNext, Action<Exception> onError, Action onCompleted)
        {
            if(source == null) throw new ArgumentNullException(nameof(source));
            if(onNext == null) throw new ArgumentNullException(nameof(onNext));
            if(onError == null) throw new ArgumentNullException(nameof(onError));
            if(onCompleted == null) throw new ArgumentNullException(nameof(onCompleted));

            try
            {
                foreach(var item in source)
                    onNext(item);
                onCompleted();
            }
            catch(Exception ex)
            {
                onError(ex);
            }
        }
    }
}
