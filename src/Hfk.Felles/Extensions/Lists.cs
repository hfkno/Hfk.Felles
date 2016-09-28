using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Hfk.Felles
{

    /// <summary>
    ///     Extention methods.
    /// </summary>
    public static class Lists
    {
        /// <summary>
        ///     Retrieves the last index available in the list.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int LastIndex(this IList input)
        {
            if (input.Exists())
                return input.Count - 1;
            return -1;
        }

        /// <summary>
        ///     Retrieves the first item in the list.
        /// </summary>
        /// <typeparam name="T">The type of the list.</typeparam>
        /// <param name="list">The list to find items in.</param>
        /// <returns>The first item found in the list.</returns>
        public static T FirstItem<T>(this IList<T> list) where T : class
        {
            return list.FirstOrDefault();
        }

        /// <summary>
        ///     Retrieves the last item in the list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static T LastItem<T>(this IList<T> list) where T : class
        {
            if (list.HasItems())
                return list[list.LastIndex()];

            return null;
        }

        /// <summary>
        ///     Retreives a 'safe' copy of a list, guaranteed to exist and be empty if no list is provided.
        /// </summary>
        /// <param name="listToCheck">The list to check for existence.</param>
        /// <returns>A safe copy of the list, ensuring that no null reference operations will occur.</returns>
        public static IList SafeList(this IList listToCheck)
        {
            if (!listToCheck.Exists())
                return new List<object>();
            return listToCheck;
        }
    }
}