namespace Hfk.Felles.Identifikasjon
{
    /// <summary>
    ///     A base class for all norwegian identity numbers.
    /// </summary>
    public abstract class NorskIdentitetsNummer
    {
        /// <summary>
        /// The provided identity number.
        /// </summary>
        protected readonly string identNr;

        /// <summary>
        /// Den angitte identitetsnummer.
        /// </summary>
        public string Nummer { get { return identNr; } }

        /// <summary>
        /// Identitetsnummer setter.
        /// </summary>
        /// <param name="identNr"></param>
        protected NorskIdentitetsNummer(string identNr)
        {
            this.identNr = identNr;
        }
    }
}