using System;
using Hfk.Felles.Identifikasjon;
using NUnit.Framework;

namespace Hfk.Felles.Tests.Identifikasjon
{
    [TestFixture]
    public class Identitetsnummrene
    {
        private readonly string validFnr   = "17054026641";
        private readonly string validDnr   = "51106297510";
        private readonly string validHnr   = "22495314442";
        private readonly string validFHnr  = "81212121223";
        private readonly string validDUFnr = "200816832910";


        [Test]
        public void can_create_a_fødselsnummer()
        {
            var fnr = new FødselsNummer(validFnr);
            Assert.That(fnr.Exists());
        }

        [Test]
        public void can_read_kjonn_from_a_fødselsnummer()
        {
            var fnr = new FødselsNummer(validFnr);
            Assert.That(fnr.Kjønn, Is.EqualTo("K"));
            var fnr2 = new FNummer(validFnr);
            Assert.That(fnr2.Kjønn, Is.EqualTo("K"));
        }

        [Test]
        public void can_read_fodselsdato_from_a_fødselsnummer()
        {
            var fnr = new FødselsNummer(validFnr);
            Assert.That(fnr.FødselsDato, Is.EqualTo(new DateTime(1940, 05, 17)));
            Assert.That(new FødselsNummer("01030599744").FødselsDato, Is.EqualTo(new DateTime(2005, 03, 01)));
            Assert.That(new FødselsNummer("01030550532").FødselsDato, Is.EqualTo(new DateTime(2005, 03, 01)));
            Assert.That(new FødselsNummer("01035697523").FødselsDato, Is.EqualTo(new DateTime(1956, 03, 01)));
            Assert.That(new FødselsNummer("01035650756").FødselsDato, Is.EqualTo(new DateTime(1856, 03, 01)));
            Assert.That(new FødselsNummer("01032078210").FødselsDato, Is.EqualTo(new DateTime(2020, 03, 01)));
        }

        [Test]
        public void can_create_a_dnummer()
        {
            var dnr = new DirektoratNummer(validDnr);
            Assert.That(dnr.Exists());
        }

        [Test]
        public void can_read_fodselsdato_from_a_dnummer()
        {
            var fnr = new DirektoratNummer(validDnr);
            Assert.That(fnr.FødselsDato, Is.EqualTo(new DateTime(1962, 10, 11)));
        }


        [Test]
        public void can_read_kjonn_from_a_dnummer()
        {
            var dnr = new DirektoratNummer(validDnr);
            Assert.That(dnr.Kjønn, Is.EqualTo("M"));
            var dnr2 = new DNummer(validDnr);
            Assert.That(dnr2.Kjønn, Is.EqualTo("M"));
        }


        [Test]
        public void can_create_an_hnummer()
        {
            var hnr = new HjelpeNummer(validHnr);
            Assert.That(hnr.Exists());
            var hnr2 = new HNummer(validHnr);
            Assert.That(hnr2.Exists());
        }

        [Test]
        public void can_read_fodselsdato_from_an_hnummer()
        {
            var hnr = new HjelpeNummer(validHnr);
            Assert.That(hnr.FødselsDato, Is.EqualTo(new DateTime(1953, 09, 22)));
        }

        [Test]
        public void can_read_kjonn_from_an_hnummer()
        {
            var hnr = new HjelpeNummer(validHnr);
            Assert.That(hnr.Kjønn, Is.EqualTo("K"));
        }


        [Test]
        public void can_create_an_fhnummer()
        {
            var fhnr = new FellesHjelpeNummer(validFHnr);
            Assert.That(fhnr.Exists());
            var fhnr2 = new FHNummer(validFHnr);
            Assert.That(fhnr2.Exists());
        }


        [Test]
        public void can_create_an_dufnummer()
        {
            var dufNr = new DatasystemetForUtlendingsOgFlytningsakerNummer(validDUFnr);
            Assert.That(dufNr.Exists());
            var dufNr2 = new DufNummer(validDUFnr);
            Assert.That(dufNr2.Exists());
        }

        [Test]
        public void can_read_søknadsår_from_a_dufnummer()
        {
            var dufNr = new DatasystemetForUtlendingsOgFlytningsakerNummer(validDUFnr);
            Assert.That(dufNr.Søknadsår, Is.EqualTo(2008));
        }


        [Test]
        public void will_fail_with_invalid_fnummer()
        {
            Assert.Throws<FormatException>(() => new FødselsNummer("notvalid"));
        }


        [Test]
        public void will_fail_with_invalid_dnummer()
        {
            Assert.Throws<FormatException>(() => new DirektoratNummer("notvalid"));
        }

        [Test]
        public void will_fail_with_invalid_hnummer()
        {
            Assert.Throws<FormatException>(() => new HjelpeNummer("notvalid"));
        }
        [Test]
        public void will_fail_with_invalid_dufnummer()
        {
            Assert.Throws<FormatException>(() => new DatasystemetForUtlendingsOgFlytningsakerNummer("notvalid"));
        }
    }
}
