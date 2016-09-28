using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Hfk.Felles
{

    /// <summary>
    ///     Extension methods.
    /// </summary>
    public static class Strings
    {



        #region General access



        /// <summary>
        ///     Returns whether the provided string contains useful content or not.
        /// </summary>
        /// <param name="input">The string to examine.</param>
        /// <returns>Whether the provided string 'Exists'.</returns>
        [DebuggerStepThrough]
        public static bool Exists(this string input)
        {
            return !(input == null || string.IsNullOrWhiteSpace(input.Trim('\0')));
        }

        /// <summary>
        ///     Returns a valid, initialized, string regardless of input.
        /// </summary>
        /// <param name="input">The string to acess in a safe manner.</param>
        /// <returns>An initialized string, or the original input.</returns>
        [DebuggerStepThrough]
        public static string SafeString(this string input)
        {
            return input.Exists() ? input : string.Empty;
        }



        #endregion



        #region Content inspection



        /// <summary>
        ///     Returns whether the provided string is comprised of numbers or not.
        /// </summary>
        /// <param name="input">The string to examine for numericity.</param>
        /// <returns>Whether the provided string is comprised of numbers.</returns>
        public static bool IsNumeric(this string input)
        {
            return input.ToCharArray().All(char.IsNumber);
        }

        /// <summary>
        ///     Returns whether the provided string is entirely uppercase or not.
        /// </summary>
        /// <param name="input">The string to examine for case.</param>
        /// <returns>Whether the provided string was entirely uppercase or not.</returns>
        public static bool IsUpperCase(this string input)
        {
            return input.Exists() && string.Compare(input.ToUpper(), input, StringComparison.CurrentCulture) == 0;
        }

        /// <summary>
        ///     Returns whether the provided string is entirely lowercase or not.
        /// </summary>
        /// <param name="input">The string to examine for case.</param>
        /// <returns>Whether the provided string was entirely lowercase or not.</returns>
        public static bool IsLowerCase(this string input)
        {
            return input.Exists() && string.Compare(input.ToLower(), input, StringComparison.CurrentCulture) == 0;
        }

        /// <summary>
        ///     Returns whether the provided string contains an instance of the comparison string provided.
        /// </summary>
        /// <param name="source">The string to check for a given substring.</param>
        /// <param name="checkFor">The substring to check for.</param>
        /// <param name="comp">The comparison rules to check for content.</param>
        /// <returns></returns>
        public static bool Contains(this string source, string checkFor, StringComparison comp)
        {
            return source.IndexOf(checkFor, comp) >= 0;
        }

        /// <summary>
        ///     Checks to see if the provided string matches the provided regular expression pattern.
        /// </summary>
        /// <param name="input">The string to compare with the pattern.</param>
        /// <param name="regularExpressionPattern">The pattern to use to check for matches.</param>
        /// <returns>Whether the provided input matches the provided pattern.</returns>
        public static bool Matches(this string input, string regularExpressionPattern)
        {
            return IsMatchWith(input, regularExpressionPattern);
        }

        /// <summary>
        ///     Checks to see if the provided string matches the provided regular expression pattern.
        /// </summary>
        /// <param name="input">The string to compare with the pattern.</param>
        /// <param name="regularExpressionPattern">The pattern to use to check for matches.</param>
        /// <returns>Whether the provided input matches the provided pattern.</returns>
        public static bool IsMatchWith(this string input, string regularExpressionPattern)
        {
            return Regex.IsMatch(input, regularExpressionPattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        ///     Checks to see if the provided string matches the provided regular expression pattern.
        /// </summary>
        /// <param name="input">The string to compare with the pattern.</param>
        /// <param name="re">The RegEx to use to check for matches.</param>
        /// <returns>Whether the provided input matches the provided RegEx.</returns>
        public static bool IsMatchWith(this string input, Regex re)
        {
            return re.IsMatch(input);
        }

        /// <summary>
        ///     Reports whether the provided string is a datetime with the provided format.
        /// </summary>
        /// <param name="dateString">The string to convert.</param>
        /// <param name="format">The datetime format (ie ddMMyy), to verify the datestring against.</param>
        /// <returns>Whether the provided string is a datetime with the provided format.</returns>
        public static bool IsDateWithFormat(this string dateString, string format)
        {
            var dt = new DateTime();
            return DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture,
                DateTimeStyles.NoCurrentDateDefault, out dt);
        }



        #endregion



        #region Content manipulation



        /// <summary>
        ///     Returns the leftmost characters of a provided string, equivilant to .Substring(0,X).
        /// </summary>
        /// <param name="input">The string to take characters from.</param>
        /// <param name="length">The number of characters to take.</param>
        /// <returns>The leftmost portion of a string.</returns>
        public static string Left(this string input, int length)
        {
            var result = input;
            if (input != null && input.Length > length)
            {
                result = input.Substring(0, length);
            }
            return result;
        }

        /// <summary>
        ///     Returns the leftmost characters of the provided string with a trailing
        ///     ellipse for GUI presentation of shortened strings.
        /// </summary>
        /// <param name="input">The string to take charcters from.</param>
        /// <param name="charactersToTake">The number of characters to take.</param>
        /// <param name="addEllipse">Whether an ellipse should be added to shortened strings or not.</param>
        /// <returns>A shortened string with or without an ellipse to show the shortening.</returns>
        public static string Left(this string input, int charactersToTake, bool addEllipse)
        {
            var clipped = input.SafeString();

            if (clipped.Length < charactersToTake)
                charactersToTake = clipped.Length;

            var needEllipse = addEllipse && charactersToTake < clipped.Length;

            clipped = clipped.Substring(0, charactersToTake);

            if (needEllipse) clipped += "...";

            return clipped;
        }

        /// <summary>
        ///     Returns the rightmost characters of a provided string, equivilant to .Substring(Length - characterCount,
        ///     characterCount).
        /// </summary>
        /// <param name="input">The string to take characters from.</param>
        /// <param name="length">The number of characters to take.</param>
        /// <returns>The rightmost porttion of a string.</returns>
        public static string Right(this string input, int length)
        {
            var result = input;
            if (input != null && input.Length > length)
            {
                result = input.Substring(input.Length - length, length);
            }
            return result;
        }

        /// <summary>
        ///     Guarantees that the returned starts with the provided string.
        /// </summary>
        /// <param name="input">The string to check.</param>
        /// <param name="forceOntoStart">The desired starting substring.</param>
        /// <returns>A copy of the input string that begins with the provided prefix.</returns>
        public static string EnsureStartsWith(this string input, string forceOntoStart)
        {
            forceOntoStart = forceOntoStart.SafeString();
            var final = input.SafeString();
            if (!final.StartsWith(forceOntoStart))
                final = forceOntoStart + final;
            return final;
        }

        /// <summary>
        ///     Guarantees that the returned ends with the provided string.
        /// </summary>
        /// <param name="input">The string to check.</param>
        /// <param name="forceOnToEnd">The desired ending substring.</param>
        /// <returns>A copy of the input string that begins with the provided suffix.</returns>
        public static string EnsureEndsWith(this string input, string forceOnToEnd)
        {
            forceOnToEnd = forceOnToEnd.SafeString();
            var final = input.SafeString();
            if (!final.EndsWith(forceOnToEnd))
                final = final + forceOnToEnd;
            return final;
        }

        /// <summary>
        ///     Removes the provided string from the source string.
        ///     Gurantees an initialized string is returned.
        /// </summary>
        /// <param name="input">The string to remove text from.</param>
        /// <param name="textToKill">The text to remove.</param>
        /// <returns>A copy of the input string with the provided text removed.</returns>
        public static string Kill(this string input, string textToKill)
        {
            if (input.Exists() && textToKill.Exists())
                return input.Replace(textToKill, String.Empty);
            return input.SafeString();
        }

        /// <summary>
        ///     Formats the provided string using the standard formatting syntax and the provided parameters.
        /// </summary>
        /// <param name="stringToFormat">The formatting string to use.</param>
        /// <param name="parameters">The paramaters to get values from.</param>
        /// <returns>A formatted copy of the string using the provided parameters and the standard formatting syntax.</returns>
        public static string Format(this string stringToFormat, params object[] parameters)
        {
            return String.Format(stringToFormat, parameters);
        }

        /// <summary>
        ///     Formats the provided string using the standard formatting syntax and the provided parameters.
        /// </summary>
        /// <param name="stringToFormat">The formatting string to use.</param>
        /// <param name="parameters">The paramaters to get values from.</param>
        /// <returns>A formatted copy of the string using the provided parameters and the standard formatting syntax.</returns>
        public static string FormatWith(this string stringToFormat, params object[] parameters)
        {
            return String.Format(stringToFormat, parameters);
        }



        #endregion



        #region Console logging



        /// <summary>
        ///     Writes the provided string to the standard output stream.
        /// </summary>
        /// <param name="input">The information to write.</param>
        public static void WriteToConsole(this string input)
        {
            WriteToConsole(input, null);
        }

        /// <summary>
        ///     Writes the provided text to the standard output stream using
        ///     the formatting specified in the input string.
        /// </summary>
        /// <param name="input">The formatting string to write to stdOut.</param>
        /// <param name="parameters">The paramaters to apply formatting to.</param>
        public static void WriteToConsole(this string input, params object[] parameters)
        {
            if (parameters.Exists())
                Console.WriteLine(input, parameters);
            else
                Console.WriteLine(input);
        }



        #endregion



    }
}