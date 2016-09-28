using System;

namespace Hfk.Felles.Identifikasjon
{
    /// <summary>
    ///     Et felles hjelpenummer (FH-nummer) er et ellevesifret med to kontrollsiffer, som i ordin�re
    ///     f�dselsnummer, men med f�rste siffer 8 eller 9. De resterende sifrene (posisjon to til ni)
    ///     skal v�re et tilfeldig valgt nummer. Nummeret skal ikke v�re informasjonsb�rende
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
    ///     Et felles hjelpenummer (FH-nummer) er et ellevesifret med to kontrollsiffer, som i ordin�re
    ///     f�dselsnummer, men med f�rste siffer 8 eller 9. De resterende sifrene (posisjon to til ni)
    ///     skal v�re et tilfeldig valgt nummer. Nummeret skal ikke v�re informasjonsb�rende
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