using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hfk.Felles;

namespace Hfk.Felles.Identifikasjon
{
    /// <summary>
    ///     Identifikasjon og identifikatore.
    /// </summary>
    public static class Identifikasjon
    {
        /// <summary>
        ///     Finner identifikasjonstypen for en angitt identifikasjonsnummer.
        /// </summary>
        /// <param name="nummer">Nummeret å sjekke</param>
        /// <returns></returns>
        public static IdentifikasjonsTyper TypeAv(string nummer)
        {
            return  nummer.ErGyldigFødselsNummer() ?
                        IdentifikasjonsTyper.FødselsNummer

                    : nummer.ErGyldigDNummer() ? 
                        IdentifikasjonsTyper.DirektoratNummer

                    : nummer.ErGyldigHNummer() ?
                        IdentifikasjonsTyper.HjelpeNummer 

                    : nummer.ErGyldigDUFNummer() ?
                        IdentifikasjonsTyper.DufNummer 

                    : nummer.ErGyldigFHNummer() ?
                        IdentifikasjonsTyper.FellesHjelpeNummer 

                    :   IdentifikasjonsTyper.Annet;
        }

        /// <summary>
        ///     Finner koden for en gitt identifikasjonstype.
        ///     Kodeverket forholder seg til "ID-type for personer (OID=8116)" 
        /// </summary>
        /// <param name="type">Identifikasjonstypen.</param>
        /// <returns>Koden til den angitte identifikasjonstypen, feiler ved oppslag av ukjente IdentifikasjonsTyper.</returns>
        public static string KodeFor(IdentifikasjonsTyper type)
        {
            var koder = new Dictionary<IdentifikasjonsTyper, string>()
            {
                { IdentifikasjonsTyper.FødselsNummer, "FNR" },
                { IdentifikasjonsTyper.DirektoratNummer, "DNR" },
                { IdentifikasjonsTyper.HjelpeNummer, "HNR" },
                { IdentifikasjonsTyper.DufNummer, "DUF" },
                { IdentifikasjonsTyper.FellesHjelpeNummer, "FHN" },
                { IdentifikasjonsTyper.Annet, "XXX" },
            };

            return koder[type];
        }

        /// <summary>
        ///     Finner koden for en gitt identifikasjonstype.
        ///     Kodeverket forholder seg til "ID-type for personer (OID=8116)" 
        /// </summary>
        /// <param name="nummer">Identifikasjonsnummeret som skal sjekkes.</param>
        /// <returns>Koden for den angitte identifikasjonsnummeret.</returns>
        public static string KodeFor(string nummer)
        {
            return KodeFor(TypeAv(nummer));
        }
    }

}
