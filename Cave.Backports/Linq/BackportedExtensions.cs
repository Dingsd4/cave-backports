﻿using System.Collections;
using System.Collections.Generic;

namespace System.Linq
{
    /// <summary>Some backported linq extensions.</summary>
    public static class BackportedExtensions
    {
#if NET20
        /// <summary>
        /// Creates an array from a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="items">An <see cref="IEnumerable{T}"/> to create an array from.</param>
        /// <returns>An array that contains the elements from the input sequence.</returns>
        public static T[] ToArray<T>(this IEnumerable<T> items)
        {
            var list = items.ToList();
            T[] result = new T[list.Count];
            list.CopyTo(result, 0);
            return result;
        }

        /// <summary>
        /// Creates a <see cref="List{T}"/> from an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of source.</typeparam>
        /// <param name="items">The <see cref="IEnumerable{T}"/> to create a <see cref="List{T}"/> from.</param>
        /// <returns>A <see cref="List{T}"/> that contains elements from the input sequence.</returns>
        public static List<T> ToList<T>(this IEnumerable<T> items)
        {
            return new List<T>(items);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to filter.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input sequence that satisfy the condition.</returns>
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            List<TSource> items = new List<TSource>();
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    items.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate. Each element's index is used in the logic of the predicate function.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to filter.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input sequence that satisfy the condition.</returns>
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            List<TSource> items = new List<TSource>();
            int i = 0;
            foreach (var item in source)
            {
                if (predicate(item, i++))
                {
                    items.Add(item);
                }
            }
            return items;
        }

        /// <summary>
        /// Gets a <see cref="IEnumerable{TTarget}"/> of elements of {TSource} satisfying a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the <see cref="IEnumerable"/>.</typeparam>
        /// <typeparam name="TTarget">The return type of the list.</typeparam>
        /// <param name="source">The list of elements.</param>
        /// <param name="selector">The condition implementation.</param>
        /// <returns>A list of items.</returns>
        public static IEnumerable<TTarget> Select<TSource, TTarget>(this IEnumerable<TSource> source, Func<TSource, TTarget> selector)
        {
            List<TTarget> items = new List<TTarget>();
            foreach (var item in source)
            {
                items.Add(selector(item));
            }
            return items;
        }

        /// <summary>
        /// Computes the sum of a sequence of values.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        public static int Sum(this IEnumerable<int> source)
        {
            int result = 0;
            foreach (var item in source)
            {
                result += item;
            }

            return result;
        }

        /// <summary>
        /// Computes the sum of a sequence of values.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        public static long Sum(this IEnumerable<long> source)
        {
            long result = 0;
            foreach (var item in source)
            {
                result += item;
            }

            return result;
        }

        /// <summary>
        /// Returns the maximum value in a sequence of values.
        /// </summary>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <returns>The maximum value in the sequence.</returns>
        public static int Max(this IEnumerable<int> source)
        {
            int result = source.First();
            foreach (var item in source)
            {
                if (item > result)
                {
                    result = item;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the maximum value in a sequence of values.
        /// </summary>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <returns>The maximum value in the sequence.</returns>
        public static long Max(this IEnumerable<long> source)
        {
            long result = source.First();
            foreach (var item in source)
            {
                if (item > result)
                {
                    result = item;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the minimum value in a sequence of values.
        /// </summary>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <returns>The minimum value in the sequence.</returns>
        public static int Min(this IEnumerable<int> source)
        {
            int result = source.First();
            foreach (var item in source)
            {
                if (item > result)
                {
                    result = item;
                }
            }

            return result;
        }

        /// <summary>
        /// Returns the minimum value in a sequence of values.
        /// </summary>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <returns>The minimum value in the sequence.</returns>
        public static long Min(this IEnumerable<long> source)
        {
            long result = source.First();
            foreach (var item in source)
            {
                if (item > result)
                {
                    result = item;
                }
            }

            return result;
        }

        /// <typeparam name="T">The type of elements in the <see cref="IEnumerable"/>.</typeparam>
        /// <param name="source">The list of elements.</param>
        /// <returns>True if source has any elements.</returns>
        public static bool Any<T>(this IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                return true;
            }

            return false;
        }

        /// <typeparam name="T">The type of elements in the <see cref="IEnumerable"/>.</typeparam>
        /// <param name="source">The list of elements.</param>
        /// <param name="predicate">The condition implementation.</param>
        /// <returns>True if source has any elements that satisfy the condition.</returns>
        public static bool Any<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                if (predicate(item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <typeparam name="T">The type of elements in the <see cref="IEnumerable"/>.</typeparam>
        /// <param name="source">The list of elements.</param>
        /// <param name="index">The index.</param>
        /// <returns>An element of the source from a specific position.</returns>
        public static T ElementAt<T>(this IEnumerable<T> source, int index)
        {
            int i = 0;
            foreach (var item in source)
            {
                if (index == i++)
                {
                    return item;
                }
            }
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        /// <typeparam name="T">The type of elements in the <see cref="IEnumerable"/>.</typeparam>
        /// <param name="source">The list of elements.</param>
        /// <param name="value">The value.</param>
        /// <returns>True if source contains value.</returns>
        public static bool Contains<T>(this IEnumerable<T> source, T value)
        {
            foreach (var item in source)
            {
                if (Equals(item, value))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Gets a <see cref="IEnumerable{T}"/> of elements of source in reverse.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="IEnumerable"/>.</typeparam>
        /// <param name="source">The list of elements.</param>
        /// <returns>The list in reverse.</returns>
        public static IEnumerable<T> Reverse<T>(this IEnumerable<T> source)
        {
            var list = source.ToList();
            list.Reverse();
            return list;
        }

        /// <summary>
        /// Gets the first element of source.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="IEnumerable"/>.</typeparam>
        /// <param name="source">The list of elements.</param>
        /// <returns>The first element.</returns>
        public static T First<T>(this IEnumerable<T> source)
        {
            foreach (T item in source)
            {
                return item;
            }

            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// Gets the first or a default item of the source.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="IEnumerable"/>.</typeparam>
        /// <param name="source">The list of elements.</param>
        /// <returns>The first or a default item.</returns>
        public static T FirstOrDefault<T>(this IEnumerable<T> source)
        {
            foreach (T item in source)
            {
                return item;
            }

            return default(T);
        }

        /// <summary>
        /// Gets the first item of source satisfying a condition.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="IEnumerable"/>.</typeparam>
        /// <param name="source">The list of elements.</param>
        /// <param name="predicate">The condition implementation.</param>
        /// <returns>The first element that satisfy the condition.</returns>
        public static T First<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (T item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }
            throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// Gets the forst or a default item of source satisfying a condition.
        /// </summary>
        /// <typeparam name="T">The type of elements in the <see cref="IEnumerable"/>.</typeparam>
        /// <param name="source">The list of elements.</param>
        /// <param name="predicate">The condition implementation.</param>
        /// <returns>The first element that satisfy the condition or a default value.</returns>
        public static T FirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach (T item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }
            return default(T);
        }

        /// <typeparam name="T">The type of elements in the <see cref="IEnumerable"/>.</typeparam>
        /// <param name="source">The list of elements.</param>
        /// <returns>The last element.</returns>
        public static T Last<T>(this IEnumerable<T> source)
        {
            object last = null;
            foreach (T item in source)
            {
                last = item;
            }

            if (last is T)
            {
                return (T)last;
            }

            throw new ArgumentOutOfRangeException();
        }

        /// <typeparam name="T">The type of elements in the <see cref="IEnumerable"/>.</typeparam>
        /// <param name="source">The list of elements.</param>
        /// <returns>The last element or a default value.</returns>
        public static T LastOrDefault<T>(this IEnumerable<T> source)
        {
            T last = default(T);
            foreach (T item in source)
            {
                last = item;
            }

            return last;
        }

        /// <typeparam name="T">The type of elements in the <see cref="IEnumerable"/>.</typeparam>
        /// <param name="source">The list of elements.</param>
        /// <param name="predicate">The condition implementation.</param>
        /// <returns>The last element that satisfy the condition.</returns>
        public static T Last<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            object last = null;
            foreach (T item in source)
            {
                if (predicate(item))
                {
                    last = item;
                }
            }
            if (last is T)
            {
                return (T)last;
            }

            throw new ArgumentOutOfRangeException();
        }

        /// <typeparam name="T">The type of elements in the <see cref="IEnumerable"/>.</typeparam>
        /// <param name="source">The list of elements.</param>
        /// <param name="predicate">The condition implementation.</param>
        /// <returns>The last element that satisfy the condition or a default value.</returns>
        public static T LastOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            T last = default(T);
            foreach (T item in source)
            {
                if (predicate(item))
                {
                    last = item;
                }
            }
            return last;
        }

        /// <summary>
        /// Returns the total number of elements in a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of elements in the <see cref="IEnumerable"/>.</typeparam>
        /// <param name="source">The list of elements.</param>
        /// <returns>The number of elements.</returns>
        public static int Count<TSource>(this IEnumerable<TSource> source)
        {
            int i = 0;
            foreach (var item in source)
            {
                i++;
            }

            return i;
        }

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
        /// </summary>
        /// <typeparam name="TSource">The type of elements.</typeparam>
        /// <param name="source">The lements to count.</param>
        /// <param name="predicate">The condition implementation.</param>
        /// <returns>The number of elements that satisfy a condition.</returns>
        public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return Count(source.Where(predicate));
        }

        /// <summary>
        /// Returns distinct elements from a sequence by using the default equality comparer to compare values.
        /// </summary>
        /// <typeparam name="TSource">The type of elements.</typeparam>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <param name="comparer">The comparer to use.</param>
        /// <returns>The distinct elements.</returns>
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer = null)
        {
            var dict = new Dictionary<TSource, object>(comparer);
            bool nullFound = false;

            foreach (var item in source)
            {
                if (item == null)
                {
                    if (nullFound)
                    {
                        continue;
                    }

                    nullFound = true;
                }
                else
                {
                    if (dict.ContainsKey(item))
                    {
                        continue;
                    }

                    dict.Add(item, null);
                }
                yield return item;
            }
        }
#endif
    }
}
