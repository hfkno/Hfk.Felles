using System;
using System.Globalization;

namespace Hfk.Felles.Identifikasjon
{
    /// <summary>
    ///     Et H-nummer er ellevesifret, som ordin�re f�dselsnummer, og best�r av en modifisert
    ///     sekssifret f�dselsdato og et femsifret personnummer. F�dselsdatoen modifiseres ved
    ///     at det legges til 4 p� det tredje sifferet: en person f�dt 1. januar 1980 f�r
    ///     dermed f�dselsdato 014180, mens en som er f�dt 31. januar 1980 f�r 314180.
    /// </summary>
    public class HjelpeNummer : Informasjonsb�rendeNorskIdentitetsNummer
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
        protected override DateTime F�dselsDatoFraNummeret()
        {
            var thirdDigit = int.Parse(hnr.Substring(2, 1));
            thirdDigit -= 4;

            return
                (hnr.Substring(0, 2) + "." + thirdDigit + hnr.Substring(3, 1) + "." + hnr.Substring(4, 2))
                .ToDateFromNorwegian();
        }
    }

    /// <summary>
    ///     Et H-nummer er ellevesifret, som ordin�re f�dselsnummer, og best�r av en modifisert
    ///     sekssifret f�dselsdato og et femsifret personnummer. F�dselsdatoen modifiseres ved
    ///     at det legges til 4 p� det tredje sifferet: en person f�dt 1. januar 1980 f�r
    ///     dermed f�dselsdato 014180, mens en som er f�dt 31. januar 1980 f�r 314180.
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