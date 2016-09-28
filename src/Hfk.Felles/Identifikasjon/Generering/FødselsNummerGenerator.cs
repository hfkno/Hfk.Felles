using System;
using System.Collections.Generic;
using System.Linq;

namespace Hfk.Felles.Identifikasjon.Generering
{
    /// <summary>
    ///     Genererer fødselsnummre til testing.
    ///     Generatoren lager tildfeldige datoer innenfor angitt datoer, eller etter en angitt mønster.
    ///     Et mønster av "??3????????" lager en fødselsnummer med 3 som tredje siffre, f. eks.
    /// </summary>
    public class FødselsNummerGenerator
    {



        #region Constants and internal fields



        private static readonly Random Rand = new Random();

        private const char Wildcard = '?';
        private const int DefaultAntallForsøk = 1000;

        private static readonly int[] WeightsForCheckDigit1 = {3, 7, 6, 1, 8, 9, 4, 5, 2};
        private static readonly int[] WeightsForCheckDigit2 = {5, 4, 3, 2, 7, 6, 5, 4, 3, 2};

        /// <summary>
        ///     Tidligest fødselsnummer.
        /// </summary>
        public static readonly DateTime EarliestFødselsnummerDate = new DateTime(1854, 1, 1);

        /// <summary>
        ///     Senest fødselsnummer.
        /// </summary>
        public static readonly DateTime LastFødselsnummerDate = new DateTime(2039, 12, 31);



        #endregion



        #region Public API, generation



        /// <summary>
        ///     Generer et gyldig norsk fødselnummer med et begrenset antall forsøk å gjette på gyldige tall.
        /// </summary>
        /// <param name="antallForsøk">Hvor mangen forsøk generatoren skal ta for å finne et gyldig nummer.</param>
        /// <returns>Et gyldig norsk fødselnummer eller null.</returns>
        public FødselsNummer Generer(int antallForsøk = DefaultAntallForsøk)
        {
            const int fnrLength = 11;
            return Generer(new string(Wildcard, fnrLength), antallForsøk);
        }

        /// <summary>
        ///     Generer et gyldig norsk fødselnummer ut i fra et angitt nummer med et begrenset antall
        ///     forsøk å gjette på gyldige tall.  Et mønster av "1??????????" genererer et nummer med 1 som første siffer, f. eks.
        /// </summary>
        /// <param name="mønster">
        ///     En 11 sifret mønster som styrer nummergenerering.  "???????????" = helt tilfeldig,
        ///     "2??????????" = et tall som begynner med 2 sifferet, "????80?????" = et fødselnummer for en født i 1980, osv.
        /// </param>
        /// <param name="antallForsøk">Hvor mangen forsøk generatoren skal ta for å finne et gyldig nummer.</param>
        /// <returns>Et gyldig norsk fødselnummer eller null.</returns>
        public FødselsNummer Generer(string mønster, int antallForsøk = DefaultAntallForsøk)
        {
            ValidatePattern(mønster, 11);

            for (var tryCounter = 0; tryCounter < antallForsøk; ++tryCounter)
            {
                var number = MakeDate(mønster) + MakeIndividualNo(mønster);
                var checkDigit1 = MakeFirstCheckDigit(number, mønster);
                if (checkDigit1 != '-')
                {
                    number += checkDigit1;
                    var checkDigit2 = MakeSecondCheckDigit(number, mønster);
                    if (checkDigit2 != '-')
                    {
                        number += checkDigit2;
                        var result = Create(number);
                        if (result != null)
                        {
                            return result;
                        }
                    }
                }
            }
            return null;
        }

        /// <summary>
        ///     Generer et gyldig norsk fødselnummer innenfor de angitte datoene, med den angitt kjønn.
        /// </summary>
        /// <param name="fraDato">Tidligest fødselnummer man ønsker.</param>
        /// <param name="tilDato">Senest fødselnummer man ønsker.</param>
        /// <param name="kjønn">Hvilken kjønn fødselsnummeret skal representere.</param>
        /// <returns>Et gyldig norsk fødselnummer, eller null.</returns>
        public FødselsNummer Generer(DateTime fraDato, DateTime tilDato, Kjønn kjønn)
        {
            var number = GenerateRandom(fraDato, tilDato, kjønn);
            return Create(number);
        }

        private static FødselsNummer Create(string number)
        {
            FødselsNummer result;
            try
            {
                result = new FødselsNummer(number);
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }



        #endregion



        #region Generation and validation



        private static char Modulo11(IList<int> weights, string number)
        {
            var sum = CalculateSum(weights, number);
            return Modulo11(sum);
        }

        private static char Modulo11(int value)
        {
            var rest = 11 - value%11;
            if (rest == 11)
            {
                rest = 0;
            }
            return rest < 10 ? (char) ('0' + rest) : '-';
        }

        private static int CalculateSum(IList<int> weights, string number)
        {
            var sum = 0;

            for (var position = 0; position < weights.Count; ++position)
            {
                var character = number[position];
                var value = character - '0';
                sum += value*weights[position];
            }

            return sum;
        }

        private static void ValidatePattern(string pattern, int length)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentException("Mønster mangler.");
            }
            if (pattern.Length != length)
            {
                var msg = string.Format("Mønster '{0}' må bestå av akkurat {1} tegn.", pattern, length);
                throw new ArgumentException(msg);
            }
            if (pattern.Any(ch => (Wildcard != ch) && !char.IsDigit(ch)))
            {
                var msg = string.Format("Mønster '{0}' kan bare inneholde siffre 0-9 og wildcard ({1}).", pattern,
                    Wildcard);
                throw new ArgumentException(msg);
            }
            if (pattern.IndexOf(Wildcard) < 0)
            {
                var msg = string.Format("Mønster '{0}' inneholder ingen wildcard ({1}).", pattern, Wildcard);
                throw new ArgumentException(msg);
            }
        }

        private static string GenerateRandom(DateTime dateFrom, DateTime dateTo, Kjønn gender)
        {
            EnsureValidDates(dateFrom, dateTo);

            var date = DateInRange(dateFrom, dateTo);

            Func<int> getNumber;
            switch (gender)
            {
                case Kjønn.Kvinne:
                    getNumber = () => Rand.Next(500)*2;
                    break;
                case Kjønn.Mann:
                    getNumber = () => Rand.Next(500)*2 + 1;
                    break;
                default:
                    getNumber = () => Rand.Next(1000);
                    break;
            }
            var number = GenerateRandom(date, getNumber);
            return number;
        }

        private static void EnsureValidDates(DateTime dateFrom, DateTime dateTo)
        {
            if (dateFrom < EarliestFødselsnummerDate)
            {
                var msg = String.Format("Fra-dato ({0}) kan ikke være tidligere enn {1}.", dateFrom,
                    EarliestFødselsnummerDate);
                throw new ArgumentException(msg);
            }
            if (dateTo > LastFødselsnummerDate)
            {
                var msg = String.Format("Til-dato ({0}) kan ikke være senere enn {1}.", dateTo, LastFødselsnummerDate);
                throw new ArgumentException(msg);
            }
            if (dateFrom > dateTo)
            {
                var msg = String.Format("Fra-dato ({0}) kan ikke være senere enn til-dato ({1}).", dateFrom, dateTo);
                throw new ArgumentException(msg);
            }
        }

        private static string MakeDate(string pattern)
        {
            string result;
            if (pattern.StartsWith("??????"))
            {
                result = DateInRange(EarliestFødselsnummerDate, LastFødselsnummerDate);
            }
            else
            {
                result = String.Empty;
                for (var index = 0; index < 6; ++index)
                {
                    if (Wildcard == pattern[index])
                    {
                        result += (char) ('0' + Rand.Next(10));
                    }
                    else
                    {
                        result += pattern[index];
                    }
                }
            }
            return result;
        }

        private static string MakeIndividualNo(string pattern)
        {
            var result = String.Empty;
            for (var index = 0; index < 3; ++index)
            {
                if (Wildcard == pattern[index])
                {
                    result += (char) ('0' + Rand.Next(10));
                }
                else
                {
                    result += pattern[index];
                }
            }
            return result;
        }

        private static char MakeFirstCheckDigit(string number, string pattern)
        {
            return Wildcard == pattern[9] ? Modulo11(WeightsForCheckDigit1, number) : pattern[9];
        }

        private static char MakeSecondCheckDigit(string number, string pattern)
        {
            return Wildcard == pattern[10] ? Modulo11(WeightsForCheckDigit2, number) : pattern[10];
        }

        private static string GenerateRandom(string date, Func<int> getNumber)
        {
            string number;
            char checkDigit2;
            do
            {
                char checkDigit1;
                do
                {
                    var individualNo = getNumber();
                    number = String.Format("{0}{1:000}", date, individualNo);
                    checkDigit1 = Modulo11(WeightsForCheckDigit1, number);
                } while ('-' == checkDigit1);
                number += checkDigit1;
                checkDigit2 = Modulo11(WeightsForCheckDigit2, number);
            } while ('-' == checkDigit2);
            number += checkDigit2;
            return number;
        }

        private static string DateInRange(DateTime from, DateTime to)
        {
            var days = (to - @from).Days + 1;
            var date = @from.AddDays(Rand.Next(days));
            return String.Format("{0:ddMMyy}", date);
        }



        #endregion



    }
}