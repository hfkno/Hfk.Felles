using System;
using System.Globalization;

namespace Hfk.Felles.Identifikasjon
{
    /// <summary>
    ///     Et ellevesifret registreringsnummer som tildeles av den norske stat til alle landets innbyggere.
    /// </summary>
    public class FødselsNummer : InformasjonsbærendeNorskIdentitetsNummer
    {
        /// <summary>
        ///     Creates a new fødselsnummer.
        /// </summary>
        /// <param name="fnr"></param>
        public FødselsNummer(string fnr) : base(fnr)
        {
            if (!fnr.ErGyldigFødselsNummer())
                throw new FormatException("Ugyldig fodselsnummer format.");
        }

        /// <summary>
        ///     Hent fødselsdatoen fra den angitt fødselsnummer.
        /// </summary>
        /// <returns></returns>
        protected override DateTime FødselsDatoFraNummeret()
        {
            return (identNr.Substring(0, 2) + "." + identNr.Substring(2, 2) + "." + identNr.Substring(4, 2))
                   .ToDateFromNorwegian();
        }
    }

    /// <summary>
    ///     Et ellevesifret registreringsnummer som tildeles av den norske stat til alle landets innbyggere.
    /// </summary>
    public class FNummer : FødselsNummer {
        /// <summary>
        ///     Creates a new fødselsnummer.
        /// </summary>
        /// <param name="fnr"></param>
        public FNummer(string fnr) : base(fnr)
        {
        }
    }
}