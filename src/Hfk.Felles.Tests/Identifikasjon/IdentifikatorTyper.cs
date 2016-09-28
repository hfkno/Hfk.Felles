using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Ident = Hfk.Felles.Identifikasjon;


namespace Hfk.Felles.Tests.IdentifikatorTyper
{
    public class IdentifikatorTyper
    {
        [Test]
        public void can_get_ident_types()
        {
            var ann = Ident.Identifikasjon.KodeFor(Ident.IdentifikasjonsTyper.Annet);
            var dnr = Ident.Identifikasjon.KodeFor(Ident.IdentifikasjonsTyper.DirektoratNummer);

            Assert.That(ann, Is.EqualTo("XXX"));
            Assert.That(dnr, Is.EqualTo("DNR"));
        }

        private readonly string validFnr   = "17054026641";
        private readonly string validDnr   = "51106297510";
        private readonly string validHnr   = "22495314442";
        private readonly string validFHnr  = "81212121223";
        private readonly string validDUFnr = "200816832910";

        [Test]
        public void can_get_identity_type_from_identity()
        {
            Assert.That(Ident.Identifikasjon.KodeFor(validFnr), Is.EqualTo("FNR"));
            Assert.That(Ident.Identifikasjon.KodeFor(validDnr), Is.EqualTo("DNR"));
            Assert.That(Ident.Identifikasjon.KodeFor(validHnr), Is.EqualTo("HNR"));
            Assert.That(Ident.Identifikasjon.KodeFor(validFHnr), Is.EqualTo("FHN"));
            Assert.That(Ident.Identifikasjon.KodeFor(validDUFnr), Is.EqualTo("DUF"));
            Assert.That(Ident.Identifikasjon.KodeFor("nothing"), Is.EqualTo("XXX"));
        }
    }
}
