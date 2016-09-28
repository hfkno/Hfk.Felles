using System;
using System.Globalization;

namespace Hfk.Felles.Identifikasjon
{
    /// <summary>
    ///     Et D-nummer er ellevesifret, som ordinære fødselsnummer, og består av en
    ///     modifisert sekssifret fødselsdato og et femsifret personnummer. Fødselsdatoen
    ///     modifiseres ved at det legges til 4 på det første sifferet: en person født
    ///     1. januar 1980 får dermed fødselsdato 410180, mens en som er født
    ///     31. januar 1980 får 710180.
    /// </summary>
    public class DirektoratNummer : InformasjonsbærendeNorskIdentitetsNummer
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
        ///     Finn fødselsdatoen i fra den angitte nummeret.
        /// </summary>
        /// <returns></returns>
        protected override DateTime FødselsDatoFraNummeret()
        {
            var firstDigit = int.Parse(dnr.Substring(0, 1));
            firstDigit -= 4;

            return
                (firstDigit + dnr.Substring(1, 1) + "." + dnr.Substring(2, 2) + "." + dnr.Substring(4, 2))
                .ToDateFromNorwegian();
        }
    }

    /// <summary>
    ///     Et D-nummer er ellevesifret, som ordinære fødselsnummer, og består av en
    ///     modifisert sekssifret fødselsdato og et femsifret personnummer. Fødselsdatoen
    ///     modifiseres ved at det legges til 4 på det første sifferet: en person født
    ///     1. januar 1980 får dermed fødselsdato 410180, mens en som er født
    ///     31. januar 1980 får 710180.
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