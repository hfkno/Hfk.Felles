using System;
using System.Globalization;

namespace Hfk.Felles.Identifikasjon
{
    /// <summary>
    ///     Et D-nummer er ellevesifret, som ordin�re f�dselsnummer, og best�r av en
    ///     modifisert sekssifret f�dselsdato og et femsifret personnummer. F�dselsdatoen
    ///     modifiseres ved at det legges til 4 p� det f�rste sifferet: en person f�dt
    ///     1. januar 1980 f�r dermed f�dselsdato 410180, mens en som er f�dt
    ///     31. januar 1980 f�r 710180.
    /// </summary>
    public class DirektoratNummer : Informasjonsb�rendeNorskIdentitetsNummer
    {

        /// <summary>
        ///     Lag et nytt direktoratnummer.
        /// </summary>
        /// <param name="dnr"></param>
        public DirektoratNummer(string dnr) : base(dnr)
        {
            if (!dnr.ErGyldigDNummer())
                throw new FormatException("Ugyldig d-nummer format.");
        }

        /// <summary>
        ///     Underliggende direktoratnummer.
        /// </summary>
        protected string dnr { get { return identNr; } }


        /// <summary>
        ///     Finn f�dselsdatoen i fra den angitte nummeret.
        /// </summary>
        /// <returns></returns>
        protected override DateTime F�dselsDatoFraNummeret()
        {
            var firstDigit = int.Parse(dnr.Substring(0, 1));
            firstDigit -= 4;

            return
                (firstDigit + dnr.Substring(1, 1) + "." + dnr.Substring(2, 2) + "." + dnr.Substring(4, 2))
                .ToDateFromNorwegian();
        }
    }

    /// <summary>
    ///     Et D-nummer er ellevesifret, som ordin�re f�dselsnummer, og best�r av en
    ///     modifisert sekssifret f�dselsdato og et femsifret personnummer. F�dselsdatoen
    ///     modifiseres ved at det legges til 4 p� det f�rste sifferet: en person f�dt
    ///     1. januar 1980 f�r dermed f�dselsdato 410180, mens en som er f�dt
    ///     31. januar 1980 f�r 710180.
    /// </summary>
    public class DNummer : DirektoratNummer
    {
        /// <summary>
        ///     Lag et nytt direktoratnummer.
        /// </summary>
        /// <param name="dnr"></param>
        public DNummer(string dnr) : base(dnr)
        {
        }
    }
}