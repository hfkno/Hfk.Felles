using System;

namespace Hfk.Felles.Identifikasjon
{
    /// <summary>
    ///     Eit DUF-nummer er eit nummer som vert gjeve til alle som søkjer om opphald i Noreg. 
    ///     Det er søkjaren sitt registreringsnummer i Datasystemet for utlendings- og flyktningsaker. 
    ///     Alle innvandrarar som ikkje kjem frå eit EØS-land, skal i utgangspunktet registrerast i 
    ///     dette registeret når dei kjem til Noreg. DUF-nummeret er eit tolvsifra nummer som vert brukt 
    ///     om lag på same måte som eit fødselsnummer, men inneheld ikkje informasjon om til dømes fødselsdato. 
    ///     Dersom utlendingen får opphald, og skal vera i Noreg i meir enn eit halvt år, vert han registrert 
    ///     med fødselsnummer i Folkeregisteret. Då vert DUF-nummeret òg registrert i Folkeregisteret.
    /// </summary>
    public class DatasystemetForUtlendingsOgFlytningsakerNummer : NorskIdentitetsNummer
    {

        /// <summary>
        ///     Lag en ny DUF nummer.
        /// </summary>
        /// <param name="dufnr">DUF nummeret.</param>
        public DatasystemetForUtlendingsOgFlytningsakerNummer(string dufnr): base(dufnr)
        {
            if (!dufnr.ErGyldigDUFNummer())
                throw new FormatException("Ugyldig DUF-nummer format.");
        }

        /// <summary>
        ///     Angir hvilket søknadsår DUF nummeret gjelder.
        /// </summary>
        public int Søknadsår
        {
            get
            {
                return identNr.Left(4).ToInt();
            }
        }
    }

    /// <summary>
    ///     Eit DUF-nummer er eit nummer som vert gjeve til alle som søkjer om opphald i Noreg. 
    ///     Det er søkjaren sitt registreringsnummer i Datasystemet for utlendings- og flyktningsaker. 
    ///     Alle innvandrarar som ikkje kjem frå eit EØS-land, skal i utgangspunktet registrerast i 
    ///     dette registeret når dei kjem til Noreg. DUF-nummeret er eit tolvsifra nummer som vert brukt 
    ///     om lag på same måte som eit fødselsnummer, men inneheld ikkje informasjon om til dømes fødselsdato. 
    ///     Dersom utlendingen får opphald, og skal vera i Noreg i meir enn eit halvt år, vert han registrert 
    ///     med fødselsnummer i Folkeregisteret. Då vert DUF-nummeret òg registrert i Folkeregisteret.
    /// </summary>
    public class DufNummer : DatasystemetForUtlendingsOgFlytningsakerNummer
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="dufnr"></param>
        public DufNummer(string dufnr) : base(dufnr)
        {
        }
    }
}