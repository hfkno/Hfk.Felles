using System;
using System.Globalization;

namespace Hfk.Felles.Identifikasjon
{
    /// <summary>
    ///     Et H-nummer er ellevesifret, som ordinære fødselsnummer, og består av en modifisert
    ///     sekssifret fødselsdato og et femsifret personnummer. Fødselsdatoen modifiseres ved
    ///     at det legges til 4 på det tredje sifferet: en person født 1. januar 1980 får
    ///     dermed fødselsdato 014180, mens en som er født 31. januar 1980 får 314180.
    /// </summary>
    public class HjelpeNummer : InformasjonsbærendeNorskIdentitetsNummer
    {
        /// <summary>
        ///     Lag et nytt hjelpenummer.
        /// </summary>
        /// <param name="hnr"></param>
        public HjelpeNummer(string hnr): base(hnr)
        {
            if (!hnr.ErGyldigHNummer())
                throw new FormatException("Ugyldig H-nummer format.");
        }

        /// <summary>
        ///     Underliggende hjelpenummer.
        /// </summary>
        protected string hnr { get { return identNr; } }

        /// <summary>
        ///     Finn dato fra nummer.
        /// </summary>
        /// <returns></returns>
        protected override DateTime FødselsDatoFraNummeret()
        {
            var thirdDigit = int.Parse(hnr.Substring(2, 1));
            thirdDigit -= 4;

            return
                (hnr.Substring(0, 2) + "." + thirdDigit + hnr.Substring(3, 1) + "." + hnr.Substring(4, 2))
                .ToDateFromNorwegian();
        }
    }

    /// <summary>
    ///     Et H-nummer er ellevesifret, som ordinære fødselsnummer, og består av en modifisert
    ///     sekssifret fødselsdato og et femsifret personnummer. Fødselsdatoen modifiseres ved
    ///     at det legges til 4 på det tredje sifferet: en person født 1. januar 1980 får
    ///     dermed fødselsdato 014180, mens en som er født 31. januar 1980 får 314180.
    /// </summary>
    public class HNummer : HjelpeNummer
    {
        /// <summary>
        ///     Lag et nytt hjelpenummer.
        /// </summary>
        /// <param name="hnr"></param>
        public HNummer(string hnr) : base(hnr)
        {
        }
    }
}