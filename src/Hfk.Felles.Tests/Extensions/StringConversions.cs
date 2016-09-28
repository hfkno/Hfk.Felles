using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Hfk.Felles.Tests.Extensions
{
    [TestFixture]
    public class StringConversions
    {
        private readonly string validFnr = "17054026641";
        private readonly string validDnr = "51106297510";
        private readonly string validHnr = "22495314442";
        private readonly string validFHnr = "81212121223";

        [Test]
        public void can_convert_a_string_to_fødselsnummer()
        {
            Assert.That(validFnr.ToFNummer().Kjønn, Is.EqualTo("K"));
        }

        [Test]
        public void will_error_when_converting_an_invalid_string_to_fødselsnummer()
        {
            Assert.Throws<FormatException>(() => "notanumber".ToFNummer());
        }

        [Test]
        public void can_convert_a_string_to_dnummer()
        {
            Assert.That(validDnr.ToDNummer().Kjønn, Is.EqualTo("M"));
        }

        [Test]
        public void will_error_when_converting_an_invalid_string_to_dnummer()
        {
            Assert.Throws<FormatException>(() => "notanumber".ToDNummer());
        }

        [Test]
        public void can_convert_a_string_to_hnummer()
        {
            Assert.That(validHnr.ToHNummer().Kjønn, Is.EqualTo("K"));
        }

        [Test]
        public void will_error_when_converting_an_invalid_string_to_hnummer()
        {
            Assert.Throws<FormatException>(() => "notanumber".ToHNummer());
        }


        [Test]
        public void can_convert_a_string_to_fhnummer()
        {
            Assert.That(validFHnr.ToFHNummer(), Is.Not.Null);
        }

        [Test]
        public void will_error_when_converting_an_invalid_string_to_fhnummer()
        {
            Assert.Throws<FormatException>(() => "notanumber".ToFHNummer());
        }


        [Test]
        public void can_convert_to_an_integer()
        {
            Assert.That("unf".ToInt(), Is.EqualTo(0));
            Assert.That(((string)null).ToInt(), Is.EqualTo(0));
            Assert.That("".ToInt(), Is.EqualTo(0));
            Assert.That("123 45".ToInt(), Is.EqualTo(0));
            Assert.That("12345".ToInt(), Is.EqualTo(12345));
        }

        [Test]
        public void can_convert_to_a_long()
        {
            Assert.That("unf".ToLong(), Is.EqualTo(0));
            Assert.That(((string)null).ToLong(), Is.EqualTo(0));
            Assert.That("".ToLong(), Is.EqualTo(0));
            Assert.That("123 45".ToLong(), Is.EqualTo(0));

            Assert.That("1234567891234".ToLong(), Is.EqualTo(1234567891234));
        }

        [Test]
        public void can_convert_to_a_double()
        {
            Assert.That("unf".ToDouble(), Is.EqualTo(0));
            Assert.That(((string)null).ToDouble(), Is.EqualTo(0));
            Assert.That("".ToDouble(), Is.EqualTo(0));

            Assert.That("10.2".ToDouble(), Is.EqualTo(10.2));
            Assert.That("10,2".ToDouble(), Is.EqualTo(10.2));
        }

        [Test]
        public void can_convert_to_a_float()
        {
            Assert.That("unf".ToFloat(), Is.EqualTo(0));
            Assert.That(((string)null).ToFloat(), Is.EqualTo(0));
            Assert.That("".ToFloat(), Is.EqualTo(0));

            Assert.That("10.2".ToFloat(), Is.EqualTo(10.2f));
            Assert.That("10,2".ToFloat(), Is.EqualTo(10.2f));
        }

        [Test]
        public void can_convert_to_a_datetime()
        {
            Assert.That("10.10.2011".ToDate().Year, Is.EqualTo(2011));
            Assert.That("10.10.11".ToDate().Year, Is.EqualTo(2011));
            Assert.That("2011-10-10".ToDate().Year, Is.EqualTo(2011)); 
            Assert.That(@"12/22/18".ToDate().Year, Is.EqualTo(2018)); 
        }

        [Test]
        public void can_convert_to_a_datetime_with_a_specific_format()
        {
            Assert.That("050511".ToDate("ddMMyy").Year, Is.EqualTo(2011));
            Assert.That("050511".ToDate("ddMMyy").Year, Is.EqualTo(2011));
            Assert.That("2011-10-10".ToDate("yyyy-MM-dd").Year, Is.EqualTo(2011)); 
        }

        [Test]
        public void can_convert_to_a_boolean_value()
        {
            Assert.That("1".ToBool());
            Assert.That("-1".ToBool());
            Assert.That("true".ToBool());

            Assert.That("false".ToBool(), Is.False);
            Assert.That("0".ToBool(), Is.False);
            Assert.That(((string)null).ToBool(), Is.False);
        }

        [Test]
        public void will_error_when_unable_to_convert_to_a_boolean()
        {
            Assert.Throws<FormatException>(() => "!".ToBool());
        }

        [Test]
        public void will_error_when_unable_to_convert_to_a_specific_date_format()
        {
            Assert.Throws<FormatException>(() => "bzzzzz".ToDate("ddMMyy"));
        }

        [Test]
        public void can_convert_to_a_guid()
        {
            Assert.That("(FB48E38F-A360-4A8B-AFE7-16FD4C1572CA)".ToGuid().IsEmpty(), Is.False);
        }

        [Test]
        public void can_convert_to_a_base64guid()
        {
            var g = "{6259d0fe-b9d2-4a4a-83cf-f8b1e31f3bfd}";
            var g64 = g.ToGuidBase64();

            var gOut = g64.ToGuidFromBase64();

            Assert.That(g.ToGuid(), Is.EqualTo(gOut));
        }

        [Test]
        public void will_error_when_unable_to_convert_from_a_base64_guid()
        {
            Assert.Throws<FormatException>(() => "bzzzzz".ToGuidFromBase64());
        }
    }
}
