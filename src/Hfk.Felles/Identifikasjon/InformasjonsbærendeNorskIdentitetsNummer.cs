using System;

namespace Hfk.Felles.Identifikasjon
{
    /// <summary>
    ///     A base class for all Norwegian identity numbers that carry information (birth, sex, etc).
    /// </summary>
    public abstract class InformasjonsbærendeNorskIdentitetsNummer : NorskIdentitetsNummer
    {
        /// <summary>
        ///     Protected constructor.
        /// </summary>
        /// <param name="identNr"></param>
        protected InformasjonsbærendeNorskIdentitetsNummer(string identNr) : base(identNr)
        {
        }

        /// <summary>
        ///     Returns the section of the identity number which relates to birthdates.
        /// </summary>
        protected string FødselsSiffre
        {
            get { return FødselsDatoFraNummeret().ToString("ddMMyy"); }
        }

        /// <summary>
        ///     Checks the number which identifies the person.
        /// </summary>
        protected int IndividSiffre
        {
            get { return int.Parse(identNr.Substring(6, 3)); }
        }

        /// <summary>
        ///     Returns the number identifying the sex in the provided identity number.
        /// </summary>
        protected string KjønnsTall
        {
            get { return identNr.Substring(8, 1); }
        }

        /// <summary>
        ///     Returns the sex represented by the current identity number, M = Mann, K = Kvinne.
        /// </summary>
        public string Kjønn
        {
            get
            {
                // Det tredje sifferet i personnummeret angir kjønn: kvinner har partall, menn har oddetall. 
                // Kilde Wikipedia: http://no.wikipedia.org/wiki/F%C3%B8dselsnummer
                var erKvinne = int.Parse(KjønnsTall)%2 == 0;
                return erKvinne ? "K" : "M";
            }
        }

        /// <summary>
        ///     Returns the birthday that the current identity number represents.
        /// </summary>
        public DateTime FødselsDato
        {
            get
            {
                // Kilde Wikipedia: http://no.wikipedia.org/wiki/F%C3%B8dselsnummer
                // 000–499 omfatter personer født i perioden 1900–1999. 
                // 500–749 omfatter personer født i perioden 1855–1899. 
                // 500–999 omfatter personer født i perioden 2000–2039. 
                // 900–999 omfatter personer født i perioden 1940–1999. 

                var fødselsDel = FødselsSiffre;

                var fAar = fødselsDel.Substring(4, 2);
                var fDag = int.Parse(fødselsDel.Substring(0, 2));
                var fMaaned = int.Parse(fødselsDel.Substring(2, 2));
                var individsifre = IndividSiffre;

                if (individsifre > 899)
                {
                    // 1940 - 1999 eller 2000–2039
                    if (int.Parse(fAar) > 39)
                    {
                        fAar = "19" + fAar;
                    }
                    else
                    {
                        fAar = "20" + fAar;
                    }
                }
                else if (individsifre > 749)
                {
                    // 2000 - 2039
                    fAar = "20" + fAar;
                }
                else if (individsifre > 499)
                {
                    // 1855–1899 eller 2000–2039
                    if (int.Parse(fAar) > 54)
                    {
                        fAar = "18" + fAar;
                    }
                    else
                    {
                        fAar = "20" + fAar;
                    }
                }
                else
                {
                    // 1900–1999
                    fAar = "19" + fAar;
                }

                var fodselsdato = new DateTime(int.Parse(fAar), fMaaned, fDag);
                return fodselsdato;
            }
        }

        /// <summary>
        ///     Les fødselsnummeret ut i fra den underliggende nummer.
        /// </summary>
        /// <returns></returns>
        protected abstract DateTime FødselsDatoFraNummeret();
    }
}