using System.Collections;
using System.Collections.Generic;

namespace Hfk.Felles
{

    /// <summary>
    ///     Extention methods.
    /// </summary>
    public static class Collections
    {
        /// <summary>
        ///     Finds the first index of a collection.
        /// </summary>
        /// <param name="input">The collection to inspect.</param>
        /// <returns>-1 or 0</returns>
        public static int FirstIndex(this ICollection input)
        {
            if (input == null)
                return -1;

            return input.Count == 0 ? -1 : 0;
        }

        /// <summary>
        ///     Finds the last index of a collection.
        /// </summary>
        /// <param name="input">The collection to inspect.</param>
        /// <returns>The last index available in the collection.</returns>
        public static int LastIndex(this ICollection input)
        {
            if (input == null)
                return -1;

            return input.Count == 0 ? -1 : input.Count - 1;
        }

        /// <summary>
        ///     Finds the last index of a collection.
        /// </summary>
        /// <param name="input">The collection to inspect.</param>
        /// <returns>The last index available in the collection.</returns>
        public static int LastIndex<T>(this ICollection<T> input)
        {
            return input.Exists() ? input.Count - 1 : -1;
        }

        /// <summary>
        ///     Returns whether the provided collection has items or not.
        /// </summary>
        /// <param name="input">The collection to inspect.</param>
        /// <returns>-1 or 0</returns>
        public static bool HasItems(this ICollection input)
        {
            if (input == null)
                return false;

            return 0 < input.Count;
        }

        /// <summary>
        ///     Returns whether the provided collection has items or not.
        /// </summary>
        /// <param name="input">The collection to inspect.</param>
        /// <returns>Whether the collection has items.</returns>
        public static bool HasItems<T>(this ICollection<T> input)
        {
            return input.Exists() && 0 < input.Count;
        }

        /// <summary>
        ///     Returns whether the provided index is valid within the provided collection.
        /// </summary>
        /// <param name="input">The collection to inspect.</param>
        /// <param name="index">The index to check for.</param>
        /// <returns>Whether the collection contains the index.</returns>
        public static bool ContainsIndex<T>(this ICollection<T> input, int index)
        {
            return 0 <= index && index <= input.LastIndex();
        }
    }
}