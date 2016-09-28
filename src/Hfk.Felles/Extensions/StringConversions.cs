using System;
using System.Globalization;
using Hfk.Felles.Identifikasjon;

namespace Hfk.Felles
{

    /// <summary>
    ///     Extention methods.
    /// </summary>
    public static class StringConversions
    {



        #region Basic Conversions



        /// <summary>
        ///     Converts a given string to a boolean value, allowing 'true', 'false', '0', and '1' as logical values.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <returns>A bolean value.</returns>
        public static bool ToBool(this string input)
        {
            if (!input.Exists())
                return false;

            if (input == "0")
                return false;

            if (input == "1" || input == "-1")
                return true;

            return Boolean.Parse(input);
        }

        /// <summary>
        ///     Converts a given string to an integer.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <returns>A valid integer, 0 if the input is not a usable number.</returns>
        public static int ToInt(this string input)
        {
            var ret = 0;

            if (input.Exists())
                Int32.TryParse(input, out ret);

            return ret;
        }

        /// <summary>
        ///     Converts a given string to a long.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <returns>A valid long, 0 if the input is not a usable number.</returns>
        public static long ToLong(this string input)
        {
            long ret = 0;

            if (input.Exists())
                Int64.TryParse(input, out ret);

            return ret;
        }

        /// <summary>
        ///     Converts a given string to a float.
        /// </summary>
        /// <param name="input">The string to convert.</param>
        /// <returns>A valid float or 0, if the input is not a usable number.</returns>
        public static float ToFloat(this string input)
        {
            float ret = 0;

            if (input.Exists())
                Single.TryParse(input.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture, out ret);

            return ret;
        }

        /// <summary>
        ///     Converts a given string to a double.
        /// </summary>
        /// <param name="input">THe string to convert.</param>
        /// <returns>A valid double, 0 if the input is not a usable number.</returns>
        public static double ToDouble(this string input)
        {
            double ret = 0;

            if (input.Exists())
                Double.TryParse(input.Replace(",", "."), NumberStyles.Float, CultureInfo.InvariantCulture, out ret);

            return ret;
        }

        /// <summary>
        ///     Converts a given date string to a DateTime using the current culture.
        /// </summary>
        /// <param name="dateString">The string to convert.</param>
        /// <returns>A DateTime based on the provided string.</returns>
        public static DateTime ToDate(this string dateString)
        {
            var date = DateTime.Parse(dateString, CultureInfo.InvariantCulture);

            return date;
        }

        /// <summary>
        ///     Converts a given string to a DateTime using the provided format to parse the string.
        /// </summary>
        /// <param name="dateString">The string to convert.</param>
        /// <param name="format">The datetime format (ie ddMMyy), used in dateString.</param>
        /// <returns>The converted date.</returns>
        public static DateTime ToDate(this string dateString, string format)
        {
            var dt = new DateTime();

            var wasConverted = DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture,
                DateTimeStyles.NoCurrentDateDefault, out dt);

            if (!wasConverted)
                throw new FormatException(
                    "Invalid date format or date string: {0} was not a match for date format '{1}'".FormatWith(
                        dateString, format));

            return dt;
        }

        /// <summary>
        ///     Converts a given date string to a DateTime using the nb-NO culture.
        /// </summary>
        /// <param name="dateString">The string to convert.</param>
        /// <returns>A DateTime based on the provided string converted using the nb-NO culture.</returns>
        public static DateTime ToDateFromNorwegian(this string norwegianDateString)
        {
            return Convert.ToDateTime(norwegianDateString, new CultureInfo("nb-NO"));
        }

        #endregion



        #region Guid Conversions



        /// <summary>
        ///     Converts a GUID string to a GUID object.
        /// </summary>
        /// <param name="guid">The GUID to convert.</param>
        /// <returns>A GUID based on the provided string.</returns>
        public static Guid ToGuid(this string guid)
        {
            return new Guid(guid);
        }

        /// <summary>
        ///     Converts a string to a GUID compressed by a conversion to Base64.
        /// </summary>
        /// <param name="guid">The guid to convert in string form.</param>
        /// <returns>The compressed GUID in Base64.</returns>
        public static string ToGuidBase64(this string guid)
        {
            return guid.ToGuid().ToBase64();
        }

        /// <summary>
        /// </summary>
        /// <param name="base64Guid"></param>
        /// <returns></returns>
        public static Guid ToGuidFromBase64(this string base64Guid)
        {
            var guid = default(Guid);
            base64Guid = base64Guid.Replace("-", "/").Replace("_", "+") + "==";

            try
            {
                guid = new Guid(Convert.FromBase64String(base64Guid));
            }
            catch (Exception ex)
            {
                throw new FormatException("Bad Base64 conversion to GUID from string '{0}'".FormatWith(base64Guid), ex);
            }

            return guid;
        }



        #endregion



        #region Identifikasjonsnummer conversions



        /// <summary>
        ///     Converts a fødselsnummer string to a Fødselsnummer object.
        /// </summary>
        /// <param name="fnr">The number to convert.</param>
        /// <returns>A valid fødselsnummer object, or a FormatException.</returns>
        public static FødselsNummer ToFNummer(this string fnr)
        {
            return new FødselsNummer(fnr);
        }

        /// <summary>
        ///     Converts a D-nummer string to a Fødselsnummer object.
        /// </summary>
        /// <param name="dnr">The number to convert.</param>
        public static DirektoratNummer ToDNummer(this string dnr)
        {
            return new DirektoratNummer(dnr);
        }

        /// <summary>
        ///     Converts an H-nummer string to a Fødselsnummer object.
        /// </summary>
        /// <param name="hnr">The number to convert.</param>
        public static HjelpeNummer ToHNummer(this string hnr)
        {
            return new HjelpeNummer(hnr);
        }

        /// <summary>
        ///     Converts an FH-nummer string to a FellesHjelpeNummer object.
        /// </summary>
        /// <param name="fhnr">The number to convert.</param>
        public static FellesHjelpeNummer ToFHNummer(this string fhnr)
        {
            return new FellesHjelpeNummer(fhnr);
        }



        #endregion



    }
}