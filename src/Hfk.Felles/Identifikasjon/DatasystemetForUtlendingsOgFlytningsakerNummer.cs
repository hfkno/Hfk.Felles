using System;

namespace Hfk.Felles.Identifikasjon
{
    /// <summary>
    ///     Eit DUF-nummer er eit nummer som vert gjeve til alle som s�kjer om opphald i Noreg. 
    ///     Det er s�kjaren sitt registreringsnummer i Datasystemet for utlendings- og flyktningsaker. 
    ///     Alle innvandrarar som ikkje kjem fr� eit E�S-land, skal i utgangspunktet registrerast i 
    ///     dette registeret n�r dei kjem til Noreg. DUF-nummeret er eit tolvsifra nummer som vert brukt 
    ///     om lag p� same m�te som eit f�dselsnummer, men inneheld ikkje informasjon om til d�mes f�dselsdato. 
    ///     Dersom utlendingen f�r opphald, og skal vera i Noreg i meir enn eit halvt �r, vert han registrert 
    ///     med f�dselsnummer i Folkeregisteret. D� vert DUF-nummeret �g registrert i Folkeregisteret.
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
        ///     Angir hvilket s�knads�r DUF nummeret gjelder.
        /// </summary>
        public int S�knads�r
        {
            get
            {
                return identNr.Left(4).ToInt();
            }
        }
    }

    /// <summary>
    ///     Eit DUF-nummer er eit nummer som vert gjeve til alle som s�kjer om opphald i Noreg. 
    ///     Det er s�kjaren sitt registreringsnummer i Datasystemet for utlendings- og flyktningsaker. 
    ///     Alle innvandrarar som ikkje kjem fr� eit E�S-land, skal i utgangspunktet registrerast i 
    ///     dette registeret n�r dei kjem til Noreg. DUF-nummeret er eit tolvsifra nummer som vert brukt 
    ///     om lag p� same m�te som eit f�dselsnummer, men inneheld ikkje informasjon om til d�mes f�dselsdato. 
    ///     Dersom utlendingen f�r opphald, og skal vera i Noreg i meir enn eit halvt �r, vert han registrert 
    ///     med f�dselsnummer i Folkeregisteret. D� vert DUF-nummeret �g registrert i Folkeregisteret.
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