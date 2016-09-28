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
        ///     Backwards compat.
        /// </summary>
        protected string fnr { get { return identNr; } }

        /// <summary>
        ///     Hent fødselsdatoen fra den angitt fødselsnummer.
        /// </summary>
        /// <returns></returns>
        protected override DateTime FødselsDatoFraNummeret()
        {
            return (fnr.Substring(0, 2) + "." + fnr.Substring(2, 2) + "." + fnr.Substring(4, 2))
                   .ToDateFromNorwegian();
        }

        /// <summary>
        ///     Backwards compat.
        /// </summary>
        [Obsolete("Utdatert: Bruk heller Fødselsnummer().Kjønn")]
        public static string GetKjonnFromFnr(string fnr)
        {
            return new FødselsNummer(fnr).Kjønn;
        }

        /// <summary>
        ///     Backwards compat.
        /// </summary>
        [Obsolete("Utdatert: Bruk heller Fødselsnummer().FødselsDato")]
        public static DateTime GetFodselsdatoFromFnr(string fnr)
        {
            return new FødselsNummer(fnr).FødselsDato;
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