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
        /// Represents a pair of a source item and a selected item.
        /// </summary>
        /// <typeparam name="TSource">The type of the source item.</typeparam>
        /// <typeparam name="TSelect">The type of the selected item.</typeparam>
        public class SyntheticItem<TSource, TSelect>
        {
            /// <summary>
            /// Gets the source item.
            /// </summary>
            public TSource SourceItem { get; private set; }

            /// <summary>
            /// Gets the selected item.
            /// </summary>
            public TSelect SelectItem { get; private set; }

            /// <summary>
            /// Initializes a new instance of the <see cref="SyntheticItem{TSource, TSelect}"/> class with the specified source item and selected item.
            /// </summary>
            /// <param name="sourceItem">The source item.</param>
            /// <param name="selectItem">The selected item.</param>
            public SyntheticItem(TSource sourceItem, TSelect selectItem)
            {
                SourceItem = sourceItem;
                SelectItem = selectItem;
            }
        }

        /// <summary>
        /// Projects each element of a sequence into a new form, creating a synthetic item containing the source element and the result of a specified selector function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TSelect">The type of the value returned by the selector function.</typeparam>
        /// <param name="source">The sequence of elements to project.</param>
        /// <param name="selector">A function to apply to each element.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> that contains synthetic items, each containing an element from the source sequence and the result of applying the selector function.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> or <paramref name="selector"/> is <c>null</c>.</exception>
        public static IEnumerable<SyntheticItem<TSource, TSelect>> SelectSynthetic<TSource, TSelect>(this IEnumerable<TSource> source, Func<TSource, TSelect> selector)
            => source.SelectSynthetic((value, index) => selector(value));

        /// <summary>
        /// Projects each element of a sequence into a new form, creating a synthetic item containing the source element and the result of a specified selector function that receives the element and its index.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements in the source sequence.</typeparam>
        /// <typeparam name="TSelect">The type of the value returned by the selector function.</typeparam>
        /// <param name="source">The sequence of elements to project.</param>
        /// <param name="selector">A function to apply to each element; the second parameter of the function represents the index of the source element.</param>
        /// <returns>
        /// An <see cref="IEnumerable{T}"/> that contains synthetic items, each containing an element from the source sequence and the result of applying the selector function.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="source"/> or <paramref name="selector"/> is <c>null</c>.</exception>
        public static IEnumerable<SyntheticItem<TSource, TSelect>> SelectSynthetic<TSource, TSelect>(this IEnumerable<TSource> source, Func<TSource, int, TSelect> selector)
        {
            if(source == null) throw new ArgumentNullException(nameof(source));
            if(selector == null) throw new ArgumentNullException(nameof(selector));

            var index = 0;
            foreach(var item in source)
                yield return new SyntheticItem<TSource, TSelect>(item, selector(item, index++));
        }
    }
}
