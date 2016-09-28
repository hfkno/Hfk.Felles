using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hfk.Felles.Tests.Extensions
{
    [TestFixture]
    public class Guids
    {
        [Test]
        public void can_report_if_they_are_empty()
        {
            var g = new Guid();
            Assert.That(g.IsEmpty());

            var g2 = new Guid("e46ad7c6-609c-4276-a540-077f124733b1");
            Assert.That(g2.IsEmpty(), Is.False);
        }


        [Test]
        public void can_be_converted_to_base_64()
        {
            var g = new Guid("{1ceb553f-b489-408c-a922-1cc0b35e857d}");
            var g64 = g.ToBase64();

            Assert.That(g64.ToGuidFromBase64(), Is.EqualTo(g));
        }
    }
}
