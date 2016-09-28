using System.Collections.Generic;
using System.IO;

namespace Hfk.Felles
{

    /// <summary>
    ///     Extention methods.
    /// </summary>
    public static class Streams
    {
        /// <summary>
        ///     Reads all content in a stream and outputs them line-by-line.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns>A string with each read operation aded as a line.</returns>
        public static List<string> ReadLines(this Stream stream)
        {
            var lines = new List<string>();
            using (var sr = new StreamReader(stream))
            {
                while (sr.Peek() >= 0)
                {
                    lines.Add(sr.ReadLine());
                }
            }
            return lines;
        }

        /// <summary>
        ///     Reads all content in the stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        /// <returns>A string with all stream content.</returns>
        public static string ReadAll(this Stream stream)
        {
            using (var sr = new StreamReader(stream))
            {
                return sr.ReadToEnd();
            }
        }
    }
}