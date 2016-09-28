using System.Collections.Generic;

namespace Hfk.Felles
{
    /// <summary>
    ///     Extention methods.
    /// </summary>
    public static class Stacks
    {
        /// <summary>
        ///     Returns whether the provided stack is empty or not.
        /// </summary>
        /// <typeparam name="T">The type of items in the stack.</typeparam>
        /// <param name="input">The string to convert.</param>
        /// <returns>Whether the stack has items or not.</returns>
        public static bool IsEmpty<T>(this Stack<T> input)
        {
            return input.Count == 0;
        }
    }
}