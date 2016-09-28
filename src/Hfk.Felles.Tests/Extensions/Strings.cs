using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Hfk.Felles.Tests.Extensions
{
    [TestFixture]
    public class Strings
    {
        [Test]
        public void exist_when_they_have_content()
        {
            Assert.That("Hello".Exists(), Is.True);
            Assert.That("i".Exists(), Is.True);
        }
        
        [Test]
        public void do_not_exist_without_content()
        {
            Assert.That("".Exists(), Is.False);
            Assert.That(" ".Exists(), Is.False);
            Assert.That("\t".Exists(), Is.False);
            Assert.That(new string(new char[0]).Exists(), Is.False);
            Assert.That("\0".Exists(), Is.False);
            Assert.That(Convert.ToChar(0x0).ToString().Exists(), Is.False);
        }

        [Test]
        public void can_be_converted_to_a_safe_string()
        {
            Assert.That(((string)null).SafeString().Equals(""));
            Assert.That("".SafeString().Equals(""));
            Assert.That("\0".SafeString().Equals(""));
            Assert.That("\t".SafeString().Equals(""));
            Assert.That("\tcontent".SafeString().Equals("\tcontent"));
        }

        [Test]
        public void are_numeric_when_composed_of_numbers()
        {
            Assert.That("12345".IsNumeric(), Is.True);
            Assert.That("1".IsNumeric(), Is.True);
        }

        [Test]
        public void are_not_numeric_when_composed_of_anything_but_numbers()
        {
            Assert.That(" 3".IsNumeric(), Is.False);
            Assert.That("text".IsNumeric(), Is.False);
        }

        [Test]
        public void are_uppercase_when_entirely_composed_of_uppercase_letters()
        {
            Assert.That("HELLO".IsUpperCase());
            Assert.That("Y".IsUpperCase());
        }

        [Test]
        public void are_not_uppercase_when_not_entirely_composed_of_uppercase_letters()
        {
            Assert.That("HELjLO".IsUpperCase(), Is.False);            
            Assert.That("".IsUpperCase(), Is.False);            
        }

        [Test]
        public void are_lowercase_when_composed_entirely_of_lowercase_letters()
        {
            Assert.That("herro".IsLowerCase());
            Assert.That("p".IsLowerCase());
            Assert.That(" p ".IsLowerCase());
            Assert.That("!".IsLowerCase());
        }

        [Test]
        public void are_not_lowercase_when_not_composed_of_lowercase_letters()
        {
            Assert.That("heLLo".IsLowerCase(), Is.False);            
            Assert.That("".IsLowerCase(), Is.False);            
        }

        [Test]
        public void can_find_substrings_they_contain()
        {
            Assert.That("https://www.rawr.com".Contains("RAWR", StringComparison.CurrentCultureIgnoreCase));
            Assert.That("foobar".Contains("foo"));

            Assert.That("foobar".Contains("nope"), Is.False);
            Assert.That("".Contains("nope"), Is.False);
        }

        [Test]
        public void can_be_matched_with_regular_expression_patterns()
        {
            Assert.That("http".Matches(".*ttp"));
            Assert.That("HTTP".IsMatchWith("http"));
            Assert.That("hTtP".IsMatchWith("http"));

            Assert.That("http".Matches("zzzz"), Is.False);
            Assert.That("http".IsMatchWith("123.*"), Is.False);
        }

        [Test]
        public void can_be_matched_with_an_existing_regular_expressions()
        {
            var re = new Regex(".*zz.*");
            Assert.That("oozzoo".IsMatchWith(re));
            Assert.That("ooZZoo".IsMatchWith(re), Is.False); // test regex is case sensitive!
            Assert.That("nnnnno".IsMatchWith(re), Is.False);
        }

        [Test]
        public void can_remove_unwanted_substrings()
        {
            Assert.That("one two three".Kill("one"), Is.EqualTo(" two three"));
            Assert.That("one two three".Kill("three"), Is.EqualTo("one two "));

            Assert.That("".Kill("three"), Is.EqualTo(""));
            Assert.That("default".Kill(null), Is.EqualTo("default"));
            Assert.That(((string)null).Kill(null), Is.EqualTo(""));
        }

        [Test]
        public void can_start_with_a_specific_prefix()
        {
            Assert.That("www.url.com".EnsureStartsWith("https://"), Is.EqualTo("https://www.url.com"));
            Assert.That("https://www.url.com".EnsureStartsWith("https://"), Is.EqualTo("https://www.url.com"));

            Assert.That("foo".EnsureStartsWith(null), Is.EqualTo("foo"));
        }

        [Test]
        public void can_start_with_a_specific_suffix()
        {
            Assert.That(@"Directory\Path".EnsureEndsWith(@"\"), Is.EqualTo(@"Directory\Path\"));
            Assert.That(@"Directory\Path\".EnsureEndsWith(@"\"), Is.EqualTo(@"Directory\Path\"));

            Assert.That("foo".EnsureEndsWith(null), Is.EqualTo("foo"));
        }

        [Test]
        public void can_yield_the_left_of_the_string()
        {
            Assert.That("hello".Left(5), Is.EqualTo("hello"));
            Assert.That("hello".Left(3), Is.EqualTo("hel"));
            Assert.That("hello".Left(1), Is.EqualTo("h"));
            Assert.That("hello".Left(0), Is.EqualTo(""));

            Assert.That("hey".Left(250), Is.EqualTo("hey"));
            Assert.That(((string)null).Left(5), Is.EqualTo(null));
        }

        [Test]
        public void can_show_when_they_have_been_shortened()
        {
            Assert.That("created some long text".Left(5, true), Is.EqualTo("creat..."));
            Assert.That("created some long text".Left(0, true), Is.EqualTo("..."));
            Assert.That("created some long text".Left(0, false), Is.EqualTo(""));
            Assert.That("created some long text".Left(3, false), Is.EqualTo("cre"));

            Assert.That("".Left(20, true), Is.EqualTo(""));
            Assert.That(((string)null).Left(20, true), Is.EqualTo(""));
        }

        [Test]
        public void will_error_from_an_invalid_selection_of_left_characters()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => "hey".Left(-250));
        }

        [Test]
        public void will_error_from_an_invalid_selection_of_left_shortening_characters()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => "hey".Left(-250, true));
        }

        [Test]
        public void will_error_from_an_invalid_selection_of_right_characters()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => "hey".Right(-250));
        }

        [Test]
        public void can_yield_the_right_of_the_string()
        {
            Assert.That("hello".Right(5), Is.EqualTo("hello"));
            Assert.That("hello".Right(3), Is.EqualTo("llo"));
            Assert.That("hello".Right(1), Is.EqualTo("o"));
            Assert.That("hello".Right(0), Is.EqualTo(""));

            Assert.That("hey".Right(250), Is.EqualTo("hey"));
            Assert.That(((string)null).Right(5), Is.EqualTo(null));
        }

        [Test]
        public void can_be_formatted()
        {
            Assert.That("hey".Format(1, 2, 3), Is.EqualTo("hey"));
            Assert.That("s{0}s{1}s{2}".Format(1, 2, 3), Is.EqualTo("s1s2s3"));
            Assert.That("".Format(1, 2, 3), Is.EqualTo(""));

            Assert.That("hey".FormatWith(1, 2, 3), Is.EqualTo("hey"));
            Assert.That("s{0}s{1}s{2}".FormatWith(1, 2, 3), Is.EqualTo("s1s2s3"));
            Assert.That("".Format(1, 2, 3), Is.EqualTo(""));
        }

        [Test]
        public void can_be_written_to_the_console()
        {
            "test output".WriteToConsole();
            "test output: #{0}".WriteToConsole(1);
            Assert.That(true);
        }

        [Test]
        public void can_report_when_they_match_a_date_format()
        {
            Assert.That("050511".IsDateWithFormat("ddMMyy"));
            Assert.That("05.05.11".IsDateWithFormat("dd.MM.yy"));
            Assert.That("20101010".IsDateWithFormat("yyyyMMdd"));

            Assert.That("0505119".IsDateWithFormat("ddMMyy"), Is.False);
            Assert.That("yo".IsDateWithFormat("ddMMyy"), Is.False);

            Assert.That("".IsDateWithFormat("yyyyMMdd"), Is.False);
            Assert.That(((string)null).IsDateWithFormat("yyyyMMdd"), Is.False);

        }

    }
}
