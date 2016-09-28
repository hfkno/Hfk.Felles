using System;

namespace Hfk.Felles
{

    /// <summary>
    ///     Extention methods.
    /// </summary>
    public static class Guids
    {
        /// <summary>
        ///     Determines if a given GUID has recieved a value or not.
        /// </summary>
        /// <param name="input">The GUID to inspect.</param>
        /// <returns>Whether the GUID has a value or not.</returns>
        public static bool IsEmpty(this Guid input)
        {
            return Guid.Empty.Equals(input);
        }

        /// <summary>
        ///     Compresses the provided GUID to a smaller representation in Base 64.
        /// </summary>
        /// <param name="guid">The GUID to convert.</param>
        /// <returns>A Base 64 representation of the provided GUID in a string.</returns>
        public static string ToBase64(this Guid guid)
        {
            return Convert.ToBase64String(guid.ToByteArray()).Replace("/", "-").Replace("+", "_").Replace("=", "");
        }
    }
}