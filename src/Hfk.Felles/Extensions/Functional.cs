using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hfk.Felles
{

    /// <summary>
    ///     Extention methods.
    /// </summary>
    public static class Functional
    {



        #region Folding



        /// <summary>
        ///     <see href="http://en.wikipedia.org/wiki/Fold_%28higher-order_function%29">Folds</see>
        ///     Folds a given collection.
        /// </summary>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <param name="enumerable">The collection.</param>
        /// <param name="func">A folding function.</param>
        /// <returns>The sum of the folding function across all collection elements.</returns>
        public static T Fold<T>(this IEnumerable<T> enumerable, Func<T, T, T> func)
        {
            return enumerable.Aggregate(func);
        }

        /// <summary>
        ///     <see href="http://en.wikipedia.org/wiki/Fold_%28higher-order_function%29">Folds</see>
        ///     Folds a given collection using a seed as its original value.
        /// </summary>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <typeparam name="TAccumulate">The type of value to accumulate.</typeparam>
        /// <param name="enumerable">The collection.</param>
        /// <param name="seed">The initial value for folding</param>
        /// <param name="func">A folding function.</param>
        /// <returns>The sum of the folding function across all collection elements.</returns>
        public static TAccumulate Fold<T, TAccumulate>(this IEnumerable<T> enumerable, TAccumulate seed,
            Func<TAccumulate, T, TAccumulate> func)
        {
            return enumerable.Aggregate(seed, func);
        }

        /// <summary>
        ///     <see href="http://en.wikipedia.org/wiki/Fold_%28higher-order_function%29">Folds</see>
        ///     Folds a given collection using a seed as its original value.
        /// </summary>
        /// <typeparam name="T">The collection type.</typeparam>
        /// <typeparam name="TAccumulate">The type of value to accumulate.</typeparam>
        /// <typeparam name="TResult">The type of result selected.</typeparam>
        /// <param name="enumerable">The collection.</param>
        /// <param name="seed">The initial value for folding</param>
        /// <param name="func">A folding function.</param>
        /// <param name="resultSelector">A function that 'selects' the result of the fold operation.</param>
        /// <returns>The sum of the folding function across all collection elements.</returns>
        public static TResult Fold<T, TAccumulate, TResult>(this IEnumerable<T> enumerable, TAccumulate seed,
            Func<TAccumulate, T, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
        {
            return enumerable.Aggregate(seed, func, resultSelector);
        }



        #endregion



        #region Mapping



        /// <summary>
        ///     <see href="http://en.wikipedia.org/wiki/Map_(higher-order_function)">Maps</see>
        ///     Maps a given function across a collection.
        /// </summary>
        /// <typeparam name="T">Collection type.</typeparam>
        /// <param name="array">The array to iterate over.</param>
        /// <param name="action">The function to execute on each item of the collection.</param>
        /// <returns>The original collection.</returns>
        public static T[] ForEach<T>(this T[] array, Action<T> action)
        {
            foreach (var item in array)
                action(item);

            return array;
        }

        /// <summary>
        ///     <see href="http://en.wikipedia.org/wiki/Map_(higher-order_function)">Maps</see>
        ///     Maps a given function across a collection with an index.
        /// </summary>
        /// <typeparam name="T">Collection type.</typeparam>
        /// <param name="array">The array to iterate over.</param>
        /// <param name="action">The function to execute on each item of the collection.</param>
        /// <returns>The original collection.</returns>
        public static T[] ForEach<T>(this T[] array, Action<T, int> action)
        {
            var i = 0;
            foreach (var item in array)
                action(item, i++);

            return array;
        }

        /// <summary>
        ///     <see href="http://en.wikipedia.org/wiki/Map_(higher-order_function)">Maps</see>
        ///     Maps a given function across a collection.
        /// </summary>
        /// <typeparam name="T">Collection type.</typeparam>
        /// <param name="enumerable">The enumerable collection to iterate over.</param>
        /// <param name="action">The function to execute on each item of the collection.</param>
        /// <returns>The original collection.</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
                action(item);

            return enumerable;
        }

        /// <summary>
        ///     <see href="http://en.wikipedia.org/wiki/Map_(higher-order_function)">Maps</see>
        ///     Maps a given function across a collection with an index.
        /// </summary>
        /// <typeparam name="T">Collection type.</typeparam>
        /// <param name="enumerable">The enumerable collection to iterate over.</param>
        /// <param name="action">The function to execute on each item of the collection.</param>
        /// <returns>The original collection.</returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            var i = 0;
            foreach (var item in enumerable)
                action(item, i++);

            return enumerable;
        }

        /// <summary>
        ///     <see href="http://en.wikipedia.org/wiki/Map_(higher-order_function)">Maps</see>
        ///     Maps a given function across a collection with an index.
        /// </summary>
        /// <typeparam name="T">Collection type.</typeparam>
        /// <param name="enumerable">The enumerable collection to iterate over.</param>
        /// <param name="action">The function to execute on each item of the collection.</param>
        /// <returns>The original collection.</returns>
        public static IEnumerable ForEach<T>(this IEnumerable enumerable, Action<T, int> action)
        {
            var i = 0;
            foreach (var item in enumerable)
                action((T) item, i++);

            return enumerable;
        }

        /// <summary>
        ///     <see href="http://en.wikipedia.org/wiki/Map_(higher-order_function)">Maps</see>
        ///     Maps a given function across a collection with an index.
        /// </summary>
        /// <typeparam name="T">Collection type.</typeparam>
        /// <param name="enumerable">The enumerable collection to iterate over.</param>
        /// <param name="action">The function to execute on each item of the collection.</param>
        /// <returns>The original collection.</returns>
        public static IEnumerable ForEach<T>(this IEnumerable enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
                action((T) item);

            return enumerable;
        }


        /// <summary>
        ///     <see href="http://en.wikipedia.org/wiki/Map_(higher-order_function)">Maps</see>
        ///     Maps a given function across a collection with an index.
        /// </summary>
        /// <typeparam name="T">Collection type.</typeparam>
        /// <param name="array">The enumerable collection to iterate over.</param>
        /// <param name="action">The function to execute on each item of the collection.</param>
        /// <returns>The original collection.</returns>
        public static T[] Map<T>(this T[] array, Action<T> action)
        {
            foreach (var item in array)
                action(item);

            return array;
        }

        /// <summary>
        ///     <see href="http://en.wikipedia.org/wiki/Map_(higher-order_function)">Maps</see>
        ///     Maps a given function across a collection with an index.
        /// </summary>
        /// <typeparam name="T">Collection type.</typeparam>
        /// <param name="list">The enumerable collection to iterate over.</param>
        /// <param name="action">The function to execute on each item of the collection.</param>
        /// <returns>The original collection.</returns>
        public static List<T> Map<T>(this List<T> list, Action<T> action)
        {
            list.ForEach(action);

            return list;
        }

        /// <summary>
        ///     <see href="http://en.wikipedia.org/wiki/Map_(higher-order_function)">Maps</see>
        ///     Maps a given function across a collection with an index.
        /// </summary>
        /// <typeparam name="T">Collection type.</typeparam>
        /// <param name="enumerable">The enumerable collection to iterate over.</param>
        /// <param name="action">The function to execute on each item of the collection.</param>
        /// <returns>The original collection.</returns>
        public static IEnumerable<T> Map<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            enumerable.ForEach(action);

            return enumerable;
        }

        /// <summary>
        ///     <see href="http://en.wikipedia.org/wiki/Map_(higher-order_function)">Maps</see>
        ///     Maps a given function across a collection with an index.
        /// </summary>
        /// <typeparam name="T">Collection type.</typeparam>
        /// <param name="enumerable">The enumerable collection to iterate over.</param>
        /// <param name="action">The function to execute on each item of the collection.</param>
        /// <returns>The original collection.</returns>
        public static IEnumerable Map<T>(this IEnumerable enumerable, Action<T> action)
        {
            enumerable.ForEach(action);

            return enumerable;
        }



        #endregion



        #region Filtering



        /// <summary>
        ///     <see href="http://en.wikipedia.org/wiki/Filter_(higher-order_function)">Filters</see>
        ///     Filters a given collection through a function.
        /// </summary>
        /// <typeparam name="T">Collection type.</typeparam>
        /// <param name="enumerable">The enumerable collection to iterate over.</param>
        /// <param name="predicate">The function to use to filter items out of the collection.</param>
        /// <returns>The original collection filtered through the selection function.</returns>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            return from l in enumerable where predicate(l) select l;
        }

        /// <summary>
        ///     <see href="http://en.wikipedia.org/wiki/Filter_(higher-order_function)">Filters</see>
        ///     Filters the items of a provided collection out of the given collection.
        /// </summary>
        /// <typeparam name="T">Collection type.</typeparam>
        /// <param name="enumerable">The enumerable collection to iterate over.</param>
        /// <param name="elementsToFilterAway">A collection of items to filter from the primary collection.</param>
        /// <returns>The original collection filtered through the selection function.</returns>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> enumerable, IEnumerable<T> elementsToFilterAway)
        {
            return from l in enumerable where !elementsToFilterAway.Contains(l) select l;
        }

        /// <summary>
        ///     <see href="http://en.wikipedia.org/wiki/Filter_(higher-order_function)">Filters</see>
        ///     Filters a given collection through a function.
        /// </summary>
        /// <param name="enumerable">The enumerable collection to iterate over.</param>
        /// <param name="elementsToKeep"></param>
        /// <returns>The original collection filtered through the selection function.</returns>
        public static IEnumerable Filter(this IEnumerable enumerable, Func<object, bool> elementsToKeep)
        {
            return from object l in enumerable where elementsToKeep(l) select l;
        }

        /// <summary>
        ///     <see href="http://en.wikipedia.org/wiki/Filter_(higher-order_function)">Filters</see>
        ///     Filters a given collection with the items provided from a function.
        /// </summary>
        /// <typeparam name="T">Collection type.</typeparam>
        /// <param name="enumerable">The enumerable collection to iterate over.</param>
        /// <param name="getElementsAction">A function which returns the items for filter from the primary collection.</param>
        /// <returns>The original collection filtered through the selection function.</returns>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> enumerable,
            Func<IEnumerable<T>, IEnumerable<T>> getElementsAction)
        {
            return from l in enumerable where !getElementsAction(enumerable).Contains(l) select l;
        }



        #endregion



    }
}