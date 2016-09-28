﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Hfk.Felles
{

    /// <summary>
    ///     Extention methods for validation.
    /// </summary>
    public static class Validation
    {

        /// <summary>
        ///     Validates a given fødselsnummer.
        /// </summary>
        /// <param name="fnr">The fødselsnummer to validate.</param>
        /// <returns>Whether the fødselsnummer was valid or not.</returns>
        public static bool ErGyldigFødselsNummer(this string fnr)
        {
            return fnr.ErGyldigFNummer();
        }

        /// <summary>
        ///     Validates a given fødselsnummer.
        /// </summary>
        /// <param name="fnr">The fødselsnummer to validate.</param>
        /// <returns>Whether the fødselsnummer was valid or not.</returns>
        public static bool ErGyldigFNummer(this string fnr)
        {
            if (string.IsNullOrEmpty(fnr) || !(fnr.Length == 11 && fnr.IsNumeric()))
            {
                return false;
            }

            try
            {
                (fnr.Substring(0, 2) + "." + fnr.Substring(2, 2) + "." + fnr.Substring(4, 2))
                .ToDateFromNorwegian();
            }
            catch (FormatException)
            {
                return false;
            }

            return CheckKontrollSiffre(fnr);
        }

        /// <summary>
        ///     Validates a given d-nummer.
        /// </summary>
        /// <param name="dnr">D-nummer to validate.</param>
        /// <returns>Whether the provided d-nummer was valid or not</returns>
        /// <remarks>
        ///     Et D-nummer er ellevesifret, som ordinære fødselsnummer, og består av en
        ///     modifisert sekssifret fødselsdato og et femsifret personnummer.
        ///     Fødselsdatoen modifiseres ved at det legges til 4 på det første sifferet:
        ///     en person født 1. januar 1980 får dermed fødselsdato 410180, mens en som er født 31. januar
        ///     1980 får 710180.
        /// </remarks>
        public static bool ErGyldigDNummer(this string dnr)
        {
            if (string.IsNullOrEmpty(dnr) || !(dnr.Length == 11 && dnr.IsNumeric()))
            {
                return false;
            }

            try
            {
                long.Parse(dnr);
            }
            catch (FormatException)
            {
                return false;
            }

            var firstDigit = int.Parse(dnr.Substring(0, 1));

            if (firstDigit > 7 || (firstDigit - 4) < 0)
            {
                return false;
            }

            firstDigit = firstDigit - 4;
            try
            {
                (firstDigit + dnr.Substring(1, 1) + "." + dnr.Substring(2, 2) + "." + dnr.Substring(4, 2))
                .ToDateFromNorwegian();
            }
            catch (FormatException)
            {
                return false;
            }

            return CheckKontrollSiffre(dnr);
        }

        /// <summary>
        ///     Validates a given h-nummer.
        /// </summary>
        /// <param name="hnr">H-nummer to validate.</param>
        /// <returns>Whther the h-nummer is valid or not.</returns>
        /// <remarks>
        ///     Et H-nummer er ellevesifret, som ordinære fødselsnummer, og består av en
        ///     modifisert sekssifret fødselsdato og et femsifret personnummer. Fødselsdatoen
        ///     modifiseres ved at det legges til 4 på det tredje sifferet: en person født 1. januar 1980
        ///     får dermed fødselsdato 014180, mens en som er født 31. januar 1980 får 314180.
        /// </remarks>
        public static bool ErGyldigHNummer(this string hnr)
        {
            if (string.IsNullOrEmpty(hnr) || !(hnr.Length == 11 && hnr.IsNumeric()))
            {
                return false;
            }

            try
            {
                long.Parse(hnr);
            }
            catch (FormatException)
            {
                return false;
            }

            var thirdDigit = int.Parse(hnr.Substring(2, 1));

            if (thirdDigit > 7 || (thirdDigit - 4) < 0)
            {
                return false;
            }

            thirdDigit = thirdDigit - 4;

            try
            {
                (hnr.Substring(0, 2) + "." + thirdDigit + hnr.Substring(3, 1) + "." + hnr.Substring(4, 2))
                .ToDateFromNorwegian();
            }
            catch (FormatException)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///     Validates a given FH-nummer
        /// </summary>
        /// <param name="fhnr"></param>
        /// <remarks>
        ///     Et FH-nummer skal bestå av et ni-siffret tall mellom 800.000.000 og 999.999.999.
        ///     Siffer 10 og 11, kontrollsifrene, beregnes på samme måte som for fødselsnummer.
        /// </remarks>
        /// <returns></returns>
        public static bool ErGyldigFHNummer(this string fhnr)
        {
            if (string.IsNullOrEmpty(fhnr) || !(fhnr.Length == 11 && fhnr.IsNumeric()))
            {
                return false;
            }

            var fhNummer = fhnr.Substring(0, 9);

            var nr = fhNummer.ToInt();
            var harRiktigTallSerie = 800000000 <= nr && nr <= 999999999;

            return harRiktigTallSerie && CheckKontrollSiffre(fhnr);
        }


        private static bool CheckKontrollSiffre(string fnr)
        {
            var v1 = new [] { 3, 7, 6, 1, 8, 9, 4, 5, 2 } ;
            var v2 = new [] { 5, 4, 3, 2, 7, 6, 5, 4, 3, 2 } ;

            var s1 = 0;
            var s2 = 0;

            for (var i = 0; i < 9; i++)
            {
                s1 += Convert.ToInt16(fnr.Substring(i, 1)) * v1[i];
            }

            var r1 = s1 % 11;
            var k1 = 11 - r1;

            if (k1 == 11)
            {
                k1 = 0;
            }
            else if (k1 == 10)
            {
                return false;
            }

            for (var i = 0; i < 10; i++)
            {
                s2 += Convert.ToInt16(fnr.Substring(i, 1)) * v2[i];
            }

            var r2 = s2 % 11;
            var k2 = 11 - r2;

            if (k2 == 11)
            {
                k2 = 0;
            }
            else if (k2 == 10)
            {
                return false;
            }

            if ((Convert.ToInt16(fnr.Substring(9, 1)) == k1 && Convert.ToInt16(fnr.Substring(10, 1)) == k2))
            {
                return true;
            }

            return false;
        }


        /// <summary>
        ///     Validates a given DUF-nummer
        /// </summary>
        /// <param name="dufnr"></param>
        /// <remarks>
        ///     DUF-nummeret er eit tolvsifra nummer som vert brukt om lag på same måte som eit fødselsnummer, 
        ///     men inneheld ikkje informasjon om til dømes fødselsdato.
        /// </remarks>
        /// <returns></returns>
        public static bool ErGyldigDUFNummer(this string dufnr)
        {
            // TODO: implement
            // http://lovas.info/2013/12/01/identitetsnummer-i-norge/
            //Hvis DUFNR ikke har lengde 12->ikke gyldig

            //Hvis DUFNR ikke er et tall->ikke gyldig

            //int kontroll = 2 siste siffer i DUFNR
            //int[] d_siffer = LØPENR + ÅRSTALL(altså array lengde 10)

            //int[] da = new int[] { 4, 6, 3, 2, 7, 5, 4, 6, 3, 2 }

            //int tmp = 0;

            //            for (int i = 9; i >= 0; i–)
            //            {
            //                tmp = da[i] * d_siffer[i];
            //            }

            //            tmp = tmp mod 11

            //if (tmp == kontroll) –> duf gyldig
            //else –> ikke gyldig

            return false;
        }
    }
}