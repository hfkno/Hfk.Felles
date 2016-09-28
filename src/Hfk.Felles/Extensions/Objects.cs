using System;

namespace Hfk.Felles
{

    /// <summary>
    ///     Extention methods.
    /// </summary>
    public static class Objects
    {
        /// <summary>
        ///     Returns whether the provided object is initialized or not.
        /// </summary>
        /// <param name="o">The object to check for existence.</param>
        /// <returns>Whether the object 'Exists' or not.</returns>
        public static bool Exists(this Object o)
        {
            return (o != null);
        }

        /// <summary>
        ///     Writed the provided object to the standard output stream.
        /// </summary>
        /// <param name="input"></param>
        public static void WriteToConsole(this object input)
        {
            Console.WriteLine(input);
        }
    }
}