using System;

namespace Hfk.Felles.Identifikasjon
{
    /// <summary>
    ///     A base class for all Norwegian identity numbers that carry information (birth, sex, etc).
    /// </summary>
    public abstract class Informasjonsb�rendeNorskIdentitetsNummer : NorskIdentitetsNummer
    {
        /// <summary>
        ///     Protected constructor.
        /// </summary>
        /// <param name="identNr"></param>
        protected Informasjonsb�rendeNorskIdentitetsNummer(string identNr) : base(identNr)
        {
        }

        /// <summary>
        ///     Returns the section of the identity number which relates to birthdates.
        /// </summary>
        protected string F�dselsSiffre
        {
            get { return F�dselsDatoFraNummeret().ToString("ddMMyy"); }
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
        protected string Kj�nnsTall
        {
            get { return identNr.Substring(8, 1); }
        }

        /// <summary>
        ///     Returns the sex represented by the current identity number, M = Mann, K = Kvinne.
        /// </summary>
        public string Kj�nn
        {
            get
            {
                // Det tredje sifferet i personnummeret angir kj�nn: kvinner har partall, menn har oddetall. 
                // Kilde Wikipedia: http://no.wikipedia.org/wiki/F%C3%B8dselsnummer
                var erKvinne = int.Parse(Kj�nnsTall)%2 == 0;
                return erKvinne ? "K" : "M";
            }
        }

        /// <summary>
        ///     Returns the birthday that the current identity number represents.
        /// </summary>
        public DateTime F�dselsDato
        {
            get
            {
                // Kilde Wikipedia: http://no.wikipedia.org/wiki/F%C3%B8dselsnummer
                // 000�499 omfatter personer f�dt i perioden 1900�1999. 
                // 500�749 omfatter personer f�dt i perioden 1855�1899. 
                // 500�999 omfatter personer f�dt i perioden 2000�2039. 
                // 900�999 omfatter personer f�dt i perioden 1940�1999. 

                var f�dselsDel = F�dselsSiffre;

                var fAar = f�dselsDel.Substring(4, 2);
                var fDag = int.Parse(f�dselsDel.Substring(0, 2));
                var fMaaned = int.Parse(f�dselsDel.Substring(2, 2));
                var individsifre = IndividSiffre;

                if (individsifre > 899)
                {
                    // 1940 - 1999 eller 2000�2039
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
                    // 1855�1899 eller 2000�2039
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
                    // 1900�1999
                    fAar = "19" + fAar;
                }

                var fodselsdato = new DateTime(int.Parse(fAar), fMaaned, fDag);
                return fodselsdato;
            }
        }

        /// <summary>
        ///     Les f�dselsnummeret ut i fra den underliggende nummer.
        /// </summary>
        /// <returns></returns>
        protected abstract DateTime F�dselsDatoFraNummeret();
    }
}