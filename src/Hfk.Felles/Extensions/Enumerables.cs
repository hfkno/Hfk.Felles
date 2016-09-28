using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hfk.Felles
{

    /// <summary>
    ///     Extention methods.
    /// </summary>
    public static class Enumerables
    {
        /// <summary>
        ///     Generates a range of numbers within the provided values.
        /// </summary>
        /// <param name="first">Number to start from.</param>
        /// <param name="last">The number to end the range at.</param>
        /// <returns>An enumerable list of numbers for loops and such.</returns>
        public static IEnumerable<int> To(this int first, int last)
        {
            return Enumerable.Range(first, last - first + 1);
        }



        #region Analysis of content



        /// <summary>
        ///     Checks the enumerable for content.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns>Whether the enumerable has items or not.</returns>
        public static bool HasItems<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Exists() && enumerable.Any();
        }

        /// <summary>
        ///     Checks the enumerable for content.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns>Whether the enumerable is empty or not.</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.HasItems();
        }

        /// <summary>
        ///     Checks to see if the enumerable is initialized and has content.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns>Whether the enumerable is initialized and has items or not.</returns>
        public static bool IsInitializedAndPopulated<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Exists() && enumerable.HasItems();
        }

        /// <summary>
        ///     Checks the enumerable for any items that match the provided predicate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="predicate">A function to check against each member of the enumerable.</param>
        /// <returns>If the predicate was met.</returns>
        public static bool HasAnyWhere<T>(this IEnumerable enumerable, Predicate<T> predicate)
        {
            return enumerable.ToIEnumerableOf<T>().IsTrueForAny(predicate);
        }

        /// <summary>
        ///     Checks the enumerable for any items that match the provided predicate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="predicate">A function to check against each member of the enumerable.</param>
        /// <returns>If the predicate was met.</returns>
        public static bool HasAnyWhere<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            return enumerable.IsTrueForAny(predicate);
        }

        /// <summary>
        ///     Checks the enumerable to see if all items match the provided predicate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="predicate">A function to check against each member of the enumerable.</param>
        /// <returns>If the predicate was met by all items or not.</returns>
        public static bool IsTrueForAll<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            return enumerable.Aggregate(true, (boolAcc, item) => boolAcc && predicate(item));
        }

        /// <summary>
        ///     Checks the enumerable to see if any items match the provided predicate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="predicate">A function to check against each member of the enumerable.</param>
        /// <returns>If the predicate was met by any items or not.</returns>
        public static bool IsTrueForAny<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            return enumerable.Aggregate(false, (boolAcc, item) => boolAcc || predicate(item));
        }


        /// <summary>
        ///     Checks the enumerable to see if no items match the provided predicate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="predicate">A function to check against each member of the enumerable.</param>
        /// <returns>If the predicate was not met by all items (or not).</returns>
        public static bool IsFalseForAll<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            return enumerable.Aggregate(true, (boolAcc, item) => boolAcc && !predicate(item));
        }

        /// <summary>
        ///     Checks the enumerable to see if all items match the provided predicate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="predicate">A function to check against each member of the enumerable.</param>
        /// <returns>If the predicate was met by all items or not.</returns>
        public static bool IsFalseForAny<T>(this IEnumerable<T> enumerable, Predicate<T> predicate)
        {
            return enumerable.Aggregate(false, (boolAcc, item) => boolAcc || !predicate(item));
        }



        #endregion



        #region Conversion



        /// <summary>
        ///     Converts the enumerable object to a typed enumerable of a given type.
        /// </summary>
        /// <typeparam name="T">The type used in generating the new enumerable.</typeparam>
        /// <param name="enumerable">The enumerable object to convert.</param>
        /// <returns>A typed enumerable object.</returns>
        public static IEnumerable<T> ToIEnumerableOf<T>(this IEnumerable enumerable)
        {
            return enumerable.ToListOf<T>();
        }

        /// <summary>
        ///     Converts the enumerable object to a typed list of a given type.
        /// </summary>
        /// <typeparam name="T">The type used in generating the new enumerable.</typeparam>
        /// <param name="enumerable">The enumerable object to convert.</param>
        /// <returns>A typed list object.</returns>
        public static IList<T> ToListOf<T>(this IEnumerable enumerable)
        {
            var newList = new List<T>();

            if (!enumerable.Exists()) return newList;

            enumerable.ForEach<T>(newList.Add);
            return newList;
        }

        /// <summary>
        ///     Generates a 'safe' list from a typed enumerable, guaranteeing that empty or null enumerables will result in a
        ///     usable list.
        /// </summary>
        /// <typeparam name="TItem">The type used in generating the new enumerable.</typeparam>
        /// <typeparam name="TNew">The type of the new List.</typeparam>
        /// <param name="items">The enumerable object to convert.</param>
        /// <param name="valueToRead">A function that generates the values to add to the new list.</param>
        /// <returns>A typed enumerable list.</returns>
        public static List<TNew> ToSafeListFrom<TItem, TNew>(this IEnumerable<TItem> items,
            Func<TItem, TNew> valueToRead)
        {
            var readItems = new List<TNew>();

            if (!items.HasItems()) return readItems;

            items.ForEach(item =>
                readItems.Add(valueToRead(item))
                );

            return readItems;
        }

        /// <summary>
        ///     Converts the provided enumerable to a set.
        ///     Items in the set will not be sorted.
        /// </summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="input">The enumerable to convert to a set.</param>
        /// <returns>An unsorted set based on the provided enumerable.</returns>
        public static ISet<T> ToSet<T>(this IEnumerable<T> input)
        {
            return new HashSet<T>(input);
        }

        /// <summary>
        ///     Converts the provided enumerable to a sorted set.
        ///     Items in the set will be sorted according to their default sorting rules.
        /// </summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="input">The enumerable to convert to a set.</param>
        /// <returns>A sorted set based on the provided enumerable.</returns>
        public static ISet<T> ToSortedSet<T>(this IEnumerable<T> input)
        {
            return new SortedSet<T>(input);
        }



        #endregion



        #region Set operations



        /// <summary>
        ///     Checks that an enumerable sequence is a proper subset of another provided enumerable.
        ///     A proper subset means that at least one value exists in the superset that is not present in the subset.
        /// </summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="first">The enumerated type to compare.</param>
        /// <param name="second">The enumerated type to compare with.</param>
        /// <returns>Whether the sequence is a proper subset.</returns>
        public static bool IsProperSubsetOf<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            return first.ToSet().IsProperSubsetOf(second);
        }

        /// <summary>
        ///     Checks that an enumerable is a proper superset of another provided enumerable.
        ///     A proper superset means that at least one value exists in the superset that is not present in the subset.
        /// </summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="first">The enumerated type to compare.</param>
        /// <param name="second">The enumerated type to compare with.</param>
        /// <returns>Whether the set is a proper superset.</returns>
        public static bool IsProperSupersetOf<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            return first.ToSet().IsProperSupersetOf(second);
        }

        /// <summary>
        ///     Checks that an enumerable is a subset of another provided enumerable.
        ///     Sets with equal content are considered both subsets and supersets of themselves.
        ///     Use the .IsProper set methods to distinguish sets with equal content from strict super-/subsets.
        /// </summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="first">The enumerated type to compare.</param>
        /// <param name="second">The enumerated type to compare with.</param>
        /// <returns>Whether the enumerable is a subset of the provided enumerable.</returns>
        public static bool IsSubsetOf<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            return first.ToSet().IsSubsetOf(second);
        }

        /// <summary>
        ///     Checks that an enumerable is a superset of another provided enumerable.
        ///     Sets with equal content are considered both subsets and supersets of themselves.
        ///     Use the .IsProper set methods to distinguish sets with equal content from strict super-/subsets.
        /// </summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="first">The enumerated type to compare.</param>
        /// <param name="second">The enumerated type to compare with.</param>
        /// <returns>Whether the enumerable is a superset of the provided enumerable.</returns>
        public static bool IsSupersetOf<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            return first.ToSet().IsSupersetOf(second);
        }

        /// <summary>
        ///     Checks if there is any overlap, ie shared items, between two sets.
        /// </summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="first">The enumerated type to compare.</param>
        /// <param name="second">The enumerated type to compare with.</param>
        /// <returns>Whether the enumerable has any overlap provided enumerable.</returns>
        public static bool Overlaps<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            return first.ToSet().Overlaps(second);
        }


        /// <summary>
        ///     Checks that two enumerables have items that are equal, without accounting for ordinality or duplicates.
        ///     { 1, 2, 3 }, { 3, 2, 1}, { 1, 2, 3, 1, 2, 3 }, and { 2, 1, 3 } are all equal sets with equal values, and are therefore "SetEqual".
        /// </summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="first">The enumerated type to compare.</param>
        /// <param name="second">The enumerated type to compare with.</param>
        /// <returns>Whether the items in both enumerables, ignoring duplicates and order, are equal to one another.</returns>
        public static bool IsSetEqualTo<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            return first.ToSet().SetEquals(second);
        }


        /// <summary>
        ///     Checks that two enumerables have items that are equal, are in the same order, and have the same number of items.
        ///     This checks for equality between each element of both lists sequentially.
        /// </summary>
        /// <typeparam name="T">The enumerated type.</typeparam>
        /// <param name="first">The enumerated type to compare.</param>
        /// <param name="second">The enumerated type to compare with.</param>
        /// <returns>Whether the items in both enumerables, accounting for duplicates and order, are equal to one another.</returns>
        public static bool IsSequenceEqualTo<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            return first.SequenceEqual(second);
        }



        #endregion



        #region Console logging



        /// <summary>
        ///     Writes the members of the enumerable to the console.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <param name="enumerable">Enumerable to write.</param>
        public static void WriteMembersToConsole<T>(this IEnumerable<T> enumerable)
        {
            enumerable.ForEach(o => Console.WriteLine(o));
        }

        /// <summary>
        ///     Writes the members of the enumerable to the console.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <param name="enumerable">Enumerable to write.</param>
        /// <param name="parameters">Paramaters to apply to formatting of the enumerables members.</param>
        public static void WriteMembersToConsole<T>(this IEnumerable<T> enumerable, params object[] parameters)
        {
            enumerable.ForEach(o => Console.WriteLine(o.ToString(), parameters));
        }

        /// <summary>
        ///     Writes the members of the enumerable to the console.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <param name="enumerable">Enumerable to write.</param>
        /// <param name="formatFunction">The formatting function to apply to the enumerable members.</param>
        public static void WriteMembersToConsole<T>(this IEnumerable<T> enumerable, Func<T, string> formatFunction)
        {
            enumerable.ForEach(o => Console.WriteLine(formatFunction(o)));
        }

        /// <summary>
        ///     Writes the members of the enumerable to the console.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <param name="enumerable">Enumerable to write.</param>
        /// <param name="formatFunction">The formatting function to apply to the enumerable members.</param>
        /// <param name="parameters">Paramaters to apply to formatting of the enumerables members.</param>
        public static void WriteMembersToConsole<T>(this IEnumerable<T> enumerable, Func<T, string> formatFunction,
            params object[] parameters)
        {
            enumerable.ForEach(o => Console.WriteLine(formatFunction(o), parameters));
        }

        /// <summary>
        ///     Writes the members of the enumerable to the console.
        /// </summary>
        /// <typeparam name="T">Item type.</typeparam>
        /// <param name="enumerable">Enumerable to write.</param>
        /// <param name="predicate">A selector function to determine which elements to write.</param>
        public static void WriteMembersToConsoleWhere<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            enumerable.Where(predicate).WriteMembersToConsole();
        }



        #endregion



    }
}