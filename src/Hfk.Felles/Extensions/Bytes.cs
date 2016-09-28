namespace Hfk.Felles
{
    /// <summary>
    ///     Extention methods.
    /// </summary>
    public static class Bytes
    {
        /// <summary>
        ///     Converts a byte to a boolean value.
        /// </summary>
        /// <param name="input">The byte to check.</param>
        /// <returns>A boolean value based on the given byte.</returns>
        public static bool ToBool(this byte input)
        {
            return input != 0;
        }
    }
}