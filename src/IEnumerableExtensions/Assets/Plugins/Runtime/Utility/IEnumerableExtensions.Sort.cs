using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmarilisLib
{
    /// <summary>
    /// Provides extension methods for <see cref="IEnumerable{T}"/> to sort collections using various sorting algorithms.
    /// </summary>
    public static partial class IEnumerableExtensions
    {
        /// <summary>
        /// Sorts the collection using the quicksort algorithm and returns a sorted list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="collection">The collection to sort.</param>
        /// <returns>A sorted list of elements.</returns>
        public static IList<T> ToQuickSortedList<T>(this IEnumerable<T> collection)
            where T : IComparable<T>
            => ToQuickSortedList(collection, null);

        /// <summary>
        /// Sorts the collection using the quicksort algorithm with a custom selector and returns a sorted list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="collection">The collection to sort.</param>
        /// <param name="selector">A function to extract a key for each element.</param>
        /// <returns>A sorted list of elements.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> is <c>null</c>.</exception>
        public static IList<T> ToQuickSortedList<T>(this IEnumerable<T> collection, Func<T, IComparable> selector)
        {
            if(collection == null) throw new ArgumentNullException(nameof(collection));

            var list = collection as IList<T> ?? collection.ToList();
            ParallelQuickSort(list, 0, list.Count - 1, selector);
            return list;
        }

        /// <summary>
        /// Sorts the collection using the mergesort algorithm and returns a sorted list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="collection">The collection to sort.</param>
        /// <returns>A sorted list of elements.</returns>
        public static IList<T> ToMergeSortedList<T>(this IEnumerable<T> collection)
            where T : IComparable<T>
            => ToMergeSortedList(collection, null);

        /// <summary>
        /// Sorts the collection using the mergesort algorithm with a custom selector and returns a sorted list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="collection">The collection to sort.</param>
        /// <param name="selector">A function to extract a key for each element.</param>
        /// <returns>A sorted list of elements.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> is <c>null</c>.</exception>
        public static IList<T> ToMergeSortedList<T>(this IEnumerable<T> collection, Func<T, IComparable> selector)
        {
            if(collection == null) throw new ArgumentNullException(nameof(collection));

            var list = collection as IList<T> ?? collection.ToList();
            if(list.Count <= 1)
                return list;

            var mid = list.Count / 2;
            var left = list.Take(mid).ToMergeSortedList(selector).ToList();
            var right = list.Skip(mid).ToMergeSortedList(selector).ToList();

            return Merge(left, right, selector);
        }

        /// <summary>
        /// Sorts the collection using the heapsort algorithm and returns a sorted list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="collection">The collection to sort.</param>
        /// <returns>A sorted list of elements.</returns>
        public static IList<T> ToHeapSortedList<T>(this IEnumerable<T> collection)
            where T : IComparable<T>
            => ToHeapSortedList(collection, null);

        /// <summary>
        /// Sorts the collection using the heapsort algorithm with a custom selector and returns a sorted list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="collection">The collection to sort.</param>
        /// <param name="selector">A function to extract a key for each element.</param>
        /// <returns>A sorted list of elements.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> is <c>null</c>.</exception>
        public static IList<T> ToHeapSortedList<T>(this IEnumerable<T> collection, Func<T, IComparable> selector)
        {
            if(collection == null) throw new ArgumentNullException(nameof(collection));

            var list = collection as IList<T> ?? collection.ToList();
            var n = list.Count;

            for(var i = n / 2 - 1; i >= 0; i--)
                Heapify(list, n, i, selector);

            for(var i = n - 1; i > 0; i--)
            {
                Swap(list, 0, i);
                Heapify(list, i, 0, selector);
            }

            return list;
        }

        /// <summary>
        /// Sorts the collection using the shellsort algorithm and returns a sorted list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="collection">The collection to sort.</param>
        /// <returns>A sorted list of elements.</returns>
        public static IList<T> ToShellSortedList<T>(this IEnumerable<T> collection)
            where T : IComparable<T>
            => ToShellSortedList(collection, null);

        /// <summary>
        /// Sorts the collection using the shellsort algorithm with a custom selector and returns a sorted list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="collection">The collection to sort.</param>
        /// <param name="selector">A function to extract a key for each element.</param>
        /// <returns>A sorted list of elements.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> is <c>null</c>.</exception>
        public static IList<T> ToShellSortedList<T>(this IEnumerable<T> collection, Func<T, IComparable> selector)
        {
            if(collection == null) throw new ArgumentNullException(nameof(collection));

            var list = collection as IList<T> ?? collection.ToList();
            var n = list.Count;

            for(var gap = n / 2; gap > 0; gap /= 2)
            {
                for(var i = gap; i < n; i++)
                {
                    var temp = list[i];
                    var j = i;
                    for(; j >= gap && Compare(list[j - gap], temp, selector) > 0; j -= gap)
                        list[j] = list[j - gap];
                    list[j] = temp;
                }
            }

            return list;
        }

        /// <summary>
        /// Sorts the collection using the insertion sort algorithm and returns a sorted list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="collection">The collection to sort.</param>
        /// <returns>A sorted list of elements.</returns>
        public static IList<T> ToInsertionSortedList<T>(this IEnumerable<T> collection)
            where T : IComparable<T>
            => ToInsertionSortedList(collection, null);

        /// <summary>
        /// Sorts the collection using the insertion sort algorithm with a custom selector and returns a sorted list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="collection">The collection to sort.</param>
        /// <param name="selector">A function to extract a key for each element.</param>
        /// <returns>A sorted list of elements.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> is <c>null</c>.</exception>
        public static IList<T> ToInsertionSortedList<T>(this IEnumerable<T> collection, Func<T, IComparable> selector)
        {
            if(collection == null) throw new ArgumentNullException(nameof(collection));

            var list = collection as IList<T> ?? collection.ToList();
            for(var i = 1; i < list.Count; i++)
            {
                var key = list[i];
                var j = i - 1;
                while(j >= 0 && Compare(list[j], key, selector) > 0)
                {
                    list[j + 1] = list[j];
                    j--;
                }
                list[j + 1] = key;
            }
            return list;
        }

        /// <summary>
        /// Sorts the collection using the selection sort algorithm and returns a sorted list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="collection">The collection to sort.</param>
        /// <returns>A sorted list of elements.</returns>
        public static IList<T> ToSelectionSortedList<T>(this IEnumerable<T> collection)
            where T : IComparable<T>
            => ToSelectionSortedList(collection, null);

        /// <summary>
        /// Sorts the collection using the selection sort algorithm with a custom selector and returns a sorted list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="collection">The collection to sort.</param>
        /// <param name="selector">A function to extract a key for each element.</param>
        /// <returns>A sorted list of elements.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> is <c>null</c>.</exception>
        public static IList<T> ToSelectionSortedList<T>(this IEnumerable<T> collection, Func<T, IComparable> selector)
        {
            if(collection == null) throw new ArgumentNullException(nameof(collection));

            var list = collection as IList<T> ?? collection.ToList();
            for(var i = 0; i < list.Count - 1; i++)
            {
                var minIndex = i;
                for(var j = i + 1; j < list.Count; j++)
                {
                    if(Compare(list[j], list[minIndex], selector) < 0)
                        minIndex = j;
                }
                if(minIndex != i)
                    Swap(list, i, minIndex);
            }
            return list;
        }

        /// <summary>
        /// Sorts the collection using the bubble sort algorithm and returns a sorted list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="collection">The collection to sort.</param>
        /// <returns>A sorted list of elements.</returns>
        public static IList<T> ToBubbleSortedList<T>(this IEnumerable<T> collection)
            where T : IComparable<T>
            => ToBubbleSortedList(collection, null);

        /// <summary>
        /// Sorts the collection using the bubble sort algorithm with a custom selector and returns a sorted list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="collection">The collection to sort.</param>
        /// <param name="selector">A function to extract a key for each element.</param>
        /// <returns>A sorted list of elements.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> is <c>null</c>.</exception>
        public static IList<T> ToBubbleSortedList<T>(this IEnumerable<T> collection, Func<T, IComparable> selector)
        {
            if(collection == null) throw new ArgumentNullException(nameof(collection));

            var list = collection as IList<T> ?? collection.ToList();
            for(var i = 0; i < list.Count - 1; i++)
            {
                var swapped = false;
                for(var j = 0; j < list.Count - 1 - i; j++)
                {
                    if(Compare(list[j], list[j + 1], selector) > 0)
                    {
                        Swap(list, j, j + 1);
                        swapped = true;
                    }
                }
                if(!swapped) break;
            }
            return list;
        }

        /// <summary>
        /// Sorts the collection using the counting sort algorithm with a custom integer selector and returns a sorted list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="collection">The collection to sort.</param>
        /// <param name="selector">A function to extract an integer key for each element.</param>
        /// <returns>A sorted list of elements.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> is <c>null</c>.</exception>
        public static IList<T> ToCountingSortedList<T>(this IEnumerable<T> collection, Func<T, int> selector)
        {
            if(collection == null) throw new ArgumentNullException(nameof(collection));

            var list = collection as IList<T> ?? collection.ToList();
            if(!list.Any()) return list;

            var min = list.Min(x => Convert.ToInt64(selector(x)));
            var max = list.Max(x => Convert.ToInt64(selector(x)));
            var range = max - min + 1;

            var count = new int[range];
            var result = new T[list.Count];

            foreach(var item in list)
                count[Convert.ToInt64(selector(item)) - min]++;

            for(var i = 1; i < count.Length; i++)
                count[i] += count[i - 1];

            for(var i = list.Count - 1; i >= 0; i--)
            {
                long value = Convert.ToInt64(selector(list[i]));
                result[count[value - min] - 1] = list[i];
                count[value - min]--;
            }

            return result;
        }

        /// <summary>
        /// Sorts the collection using the radix sort algorithm with a custom key selector and returns a sorted list.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <typeparam name="TKey">The type of the key for sorting, must implement IComparable and IConvertible.</typeparam>
        /// <param name="collection">The collection to sort.</param>
        /// <param name="selector">A function to extract a key for each element.</param>
        /// <returns>A sorted list of elements.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="collection"/> is <c>null</c>.</exception>
        public static IList<T> ToRadixSortedList<T, TKey>(this IEnumerable<T> collection, Func<T, TKey> selector)
            where TKey : IComparable<TKey>, IConvertible
        {
            if(collection == null) throw new ArgumentNullException(nameof(collection));

            var list = collection as IList<T> ?? collection.ToList();
            if(!list.Any()) return list;

            var max = list.Max(x => Convert.ToInt64(selector(x)));
            for(var exp = 1; max / exp > 0; exp *= 10)
                list = CountSortByDigit(list, selector, exp).ToList();

            return list;
        }

        private static IList<T> CountSortByDigit<T, TKey>(IList<T> list, Func<T, TKey> selector, long exp)
            where TKey : IComparable<TKey>, IConvertible
        {
            var count = new int[10];
            var result = new T[list.Count];

            foreach(var item in list)
            {
                int digit = (int)(Convert.ToInt64(selector(item)) / exp % 10);
                count[digit]++;
            }

            for(var i = 1; i < 10; i++)
                count[i] += count[i - 1];

            for(var i = list.Count - 1; i >= 0; i--)
            {
                long value = Convert.ToInt64(selector(list[i]));
                int digit = (int)(value / exp % 10);
                result[count[digit] - 1] = list[i];
                count[digit]--;
            }

            return result;
        }

        private static void ParallelQuickSort<T>(IList<T> list, int low, int high, Func<T, IComparable> selector)
        {
            if(low < high)
            {
                var pi = Partition(list, low, high, selector);

                Parallel.Invoke(
                    () => ParallelQuickSort(list, low, pi - 1, selector),
                    () => ParallelQuickSort(list, pi + 1, high, selector)
                );
            }
        }

        private static int Partition<T>(IList<T> list, int low, int high, Func<T, IComparable> selector)
        {
            var pivot = list[high];
            var i = low - 1;

            for(var j = low; j < high; j++)
            {
                if(Compare(list[j], pivot, selector) <= 0)
                {
                    i++;
                    Swap(list, i, j);
                }
            }
            Swap(list, i + 1, high);
            return i + 1;
        }

        private static void Heapify<T>(IList<T> list, int n, int i, Func<T, IComparable> selector)
        {
            var largest = i;
            var left = 2 * i + 1;
            var right = 2 * i + 2;

            if(left < n && Compare(list[left], list[largest], selector) > 0)
                largest = left;

            if(right < n && Compare(list[right], list[largest], selector) > 0)
                largest = right;

            if(largest != i)
            {
                Swap(list, i, largest);
                Heapify(list, n, largest, selector);
            }
        }

        private static IList<T> Merge<T>(IList<T> left, IEnumerable<T> right, Func<T, IComparable> selector)
        {
            var result = new List<T>();
            var leftEnumerator = left.GetEnumerator();
            var rightEnumerator = right.GetEnumerator();

            var leftHasNext = leftEnumerator.MoveNext();
            var rightHasNext = rightEnumerator.MoveNext();

            while(leftHasNext && rightHasNext)
            {
                if(Compare(leftEnumerator.Current, rightEnumerator.Current, selector) <= 0)
                {
                    result.Add(leftEnumerator.Current);
                    leftHasNext = leftEnumerator.MoveNext();
                }
                else
                {
                    result.Add(rightEnumerator.Current);
                    rightHasNext = rightEnumerator.MoveNext();
                }
            }

            while(leftHasNext)
            {
                result.Add(leftEnumerator.Current);
                leftHasNext = leftEnumerator.MoveNext();
            }

            while(rightHasNext)
            {
                result.Add(rightEnumerator.Current);
                rightHasNext = rightEnumerator.MoveNext();
            }

            return result;
        }

        private static void Swap<T>(IList<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }

        private static int Compare<T>(T x, T y, Func<T, IComparable> selector)
        {
            if(selector == null)
            {
                return Comparer<T>.Default.Compare(x, y);
            }
            else
            {
                return selector(x).CompareTo(selector(y));
            }
        }
    }
}

