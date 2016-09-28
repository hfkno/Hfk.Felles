using System;
using System.Globalization;

namespace Hfk.Felles
{

    /// <summary>
    ///     Extention methods.
    /// </summary>
    public static class Conversions
    {
        /// <summary>
        ///     Converts a given reference or value type to another type.
        /// </summary>
        /// <typeparam name="T">The type to convert to.</typeparam>
        /// <param name="input">The value to convert from.</param>
        /// <returns>A converted value.</returns>
        public static T To<T>(this IConvertible input)
        {
            try
            {
                return (T)Convert.ChangeType(input, typeof(T), CultureInfo.InvariantCulture);
            }
            catch
            {
                return default(T);
            }
        }
    }
}