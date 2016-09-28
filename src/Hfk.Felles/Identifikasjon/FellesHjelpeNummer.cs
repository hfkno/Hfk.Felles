using System;

namespace Hfk.Felles.Identifikasjon
{
    /// <summary>
    ///     Et felles hjelpenummer (FH-nummer) er et ellevesifret med to kontrollsiffer, som i ordinære
    ///     fødselsnummer, men med første siffer 8 eller 9. De resterende sifrene (posisjon to til ni)
    ///     skal være et tilfeldig valgt nummer. Nummeret skal ikke være informasjonsbærende
    /// </summary>
    public class FellesHjelpeNummer : NorskIdentitetsNummer
    {

        /// <summary>
        ///     Lag et nytt felleshjelpenummer.
        /// </summary>
        /// <param name="fhnr"></param>
        public FellesHjelpeNummer(string fhnr) : base(fhnr)
        {
            if (!fhnr.ErGyldigFHNummer())
                throw new FormatException("Ugyldig FH-nummer format.");
        }
    }

    /// <summary>
    ///     Et felles hjelpenummer (FH-nummer) er et ellevesifret med to kontrollsiffer, som i ordinære
    ///     fødselsnummer, men med første siffer 8 eller 9. De resterende sifrene (posisjon to til ni)
    ///     skal være et tilfeldig valgt nummer. Nummeret skal ikke være informasjonsbærende
    /// </summary>
    public class FHNummer : FellesHjelpeNummer
    {
        /// <summary>
        ///     Lag et nytt felleshjelpenummer.
        /// </summary>
        /// <param name="fhnr"></param>
        public FHNummer(string fhnr) : base(fhnr)
        {
        }
    }
}