namespace Hfk.Felles
{
    /// <summary>
    ///     Extention methods.
    /// </summary>
    public static class Arrays
    {
        /// <summary>
        ///     Returns the number of items in the Array.
        /// </summary>
        /// <typeparam name="T">The array type.</typeparam>
        /// <param name="array">The array to count.</param>
        /// <returns>The number of items in the array.</returns>
        public static int Count<T>(this T[] array)
        {
            return array.Length;
        }

        /// <summary>
        ///     Checks to see if the provided array has items.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The array to check.</param>
        /// <returns>Whether the provided array has items.</returns>
        public static bool HasItems<T>(this T[] array)
        {
            return array.Exists() && array.Length > 0;
        }

        /// <summary>
        ///     Checks to see if the provided array is empty.
        /// </summary>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <param name="array">The array to check.</param>
        /// <returns>Whether the provided array is empty.</returns>
        public static bool IsEmpty<T>(this T[] array)
        {
            return !array.HasItems();
        }

        /// <summary>
        ///     Check to see if the provided array has an uneven number of elements or not.
        ///     Typically used when passing paramaters or key/value pairs.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool IsUneven<T>(this T[] array)
        {
            return array.Length%2 != 0;
        }
    }
}