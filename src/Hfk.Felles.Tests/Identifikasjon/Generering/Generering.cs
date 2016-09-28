using System;
using System.Collections.Generic;
using System.Linq;
using Hfk.Felles.Identifikasjon;
using Hfk.Felles.Identifikasjon.Generering;
using NUnit.Framework;

namespace Hfk.Felles.Tests.Identifikasjon
{
    [TestFixture]
    public class FødselsnummerGenerering
    {
        protected static FødselsNummerGenerator generator = new FødselsNummerGenerator();

        [Test]
        public void basic_generation()
        {
            var fnr = generator.Generer();
            Assert.That(fnr, Is.Not.Null);

            const int maxTriesToMakeNumber = 12345;
            var fnrMedRetry = generator.Generer(maxTriesToMakeNumber);
            Assert.That(fnrMedRetry, Is.Not.Null);
        }


        [Test]
        public void generation_without_patterns_works_as_expected()
        {
            var fnr = generator.Generer(new DateTime(2010, 10, 10), new DateTime(2010, 10, 10), Kjønn.Alle);
            Assert.That(fnr, Is.Not.Null);
        }


        [Test]
        public void generation_with_no_retries_returns_null()
        {
            const int noRetries = 0;
            var fnr = generator.Generer(noRetries);
            Assert.That(fnr, Is.Null);
        }


        [Test]
        public void generation_with_negative_retries_returns_null()
        {
            const int negativeRetries = -1000;
            var fnr = generator.Generer(negativeRetries);
            Assert.That(fnr, Is.Null);
        }


        [TestCase("???????????")]
        [TestCase("31?????????")]
        [TestCase("??12???????")]
        [TestCase("????99?????")]
        [TestCase("??????999??")]
        [TestCase("?????????0?")]
        [TestCase("??????????0")]
        [TestCase("?????????99")]
        public void patterns_matched_without_causing_exceptions(string pattern)
        {
            // FNr generation also checks for validity of the number
            Assert.DoesNotThrow(() =>
                generator.Generer(pattern).Nummer.WriteToConsole()
            );
        }
        

        [TestCase("")]
        [TestCase("31???????")]
        [TestCase("??12??PP???")]
        [TestCase("12345678901")]
        public void bad_patterns_cause_exceptions(string pattern)
        {
            // FNr generation also checks for validity of the number
            Assert.Throws<ArgumentException>(() =>
                generator.Generer(pattern).Nummer.WriteToConsole()
            );
        }


        [Test]
        public void dates_before_fødselsnummre_were_generated_will_create_an_exception()
        {
            var startDate = FødselsNummerGenerator.EarliestFødselsnummerDate.AddDays(-1);
            var endDate = FødselsNummerGenerator.LastFødselsnummerDate;
            Assert.Throws<ArgumentException>(() => generator.Generer(startDate, endDate, Kjønn.Alle));
        }

        [Test]
        public void dates_after_fødselsnummre_are_phased_out_will_create_an_exception()
        {
            var startDate = FødselsNummerGenerator.EarliestFødselsnummerDate;
            var endDate = FødselsNummerGenerator.LastFødselsnummerDate.AddDays(1);
            Assert.Throws<ArgumentException>(() => generator.Generer(startDate, endDate, Kjønn.Alle));
        }


        [Test]
        public void bad_date_ranges_create_exception()
        {
            var badStartDate = FødselsNummerGenerator.LastFødselsnummerDate;
            var badEndDate = FødselsNummerGenerator.EarliestFødselsnummerDate;
            Assert.Throws<ArgumentException>(() => generator.Generer(badStartDate, badEndDate, Kjønn.Alle));
        }


        [TestCase(Kjønn.Kvinne, 0)]
        [TestCase(Kjønn.Mann, 1)]
        public void specified_genders_are_created(Kjønn gender, int expectedOddOrEven)
        {
            for (int index = 0; index < 10; ++index)
            {
                FødselsNummer birthNo = generator.Generer(FødselsNummerGenerator.EarliestFødselsnummerDate, FødselsNummerGenerator.LastFødselsnummerDate, gender);
                Assert.IsNotNull(birthNo);
                int oddOrEven = birthNo.Nummer[8] & 1;
                Assert.AreEqual(expectedOddOrEven, oddOrEven);
            }
        }
    }
}
