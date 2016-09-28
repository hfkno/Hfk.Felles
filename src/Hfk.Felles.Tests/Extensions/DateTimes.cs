using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hfk.Felles.Tests.Extensions
{
    [TestFixture]
    public class DateTimes
    {

        [Test]
        public void can_show_if_they_have_recieved_a_value()
        {
            var dt = new DateTime(2020, 10, 10);
            Assert.That(dt.Exists());

            var emptyDt = new DateTime();
            Assert.That(emptyDt.Exists(), Is.False);
        }

        [Test]
        public void can_show_if_nullable_variables_have_recieved_a_value()
        {
            DateTime? dt = new DateTime(2020, 10, 10);
            Assert.That(dt.Exists());

            DateTime? emptyDt = null;
            Assert.That(emptyDt.Exists(), Is.False);

            DateTime? emptyDt2 = new DateTime();
            Assert.That(emptyDt2.Exists(), Is.False);
        }

        [Test]
        public void are_within_time_spans_that_contain_them()
        {
            var checkPoint = new DateTime(2012, 10, 10, 10, 10, 10);
            Assert.That(checkPoint.IsBetween(new DateTime(2012, 10, 10, 10, 10, 9), 
                                             new DateTime(2012, 10, 10, 10, 10, 11)));

            Assert.That(checkPoint.IsBetween(new DateTime(2012, 1, 1),
                                             new DateTime(2020, 1, 1)));
        }

        [Test]
        public void are_not_within_time_spans_that_dont_contain_them()
        {
            var checkPoint = new DateTime(2012, 10, 10);
            Assert.That(checkPoint.IsBetween(new DateTime(2020, 10, 10),
                                             new DateTime(2000, 10, 10)), 
                        Is.False);
        }

        [Test]
        public void can_tell_the_first_day_of_the_month()
        {
            Assert.That(new DateTime(2012, 10, 10).FirstOfMonth(), Is.EqualTo(new DateTime(2012, 10, 01)) );
            Assert.That(new DateTime(2010, 1, 1).FirstOfMonth(), Is.EqualTo(new DateTime(2010, 1, 1)) );
        }

        [Test]
        public void can_tell_the_last_day_of_the_month()
        {
            Assert.That(new DateTime(2012, 10, 10).LastOfMonth(), Is.EqualTo(new DateTime(2012, 10, 31)));
            Assert.That(new DateTime(2010, 1, 1).LastOfMonth(), Is.EqualTo(new DateTime(2010, 1, 31)));
        }

        [Test]
        public void can_find_the_first_instance_of_a_weekday_in_a_given_month()
        {
            Assert.That(new DateTime(2012, 5, 5).FirstDayInMonth(DayOfWeek.Saturday), 
                        Is.EqualTo(new DateTime(2012, 5, 5)));
            Assert.That(new DateTime(2012, 5, 5).FirstDayInMonth(DayOfWeek.Sunday), 
                        Is.EqualTo(new DateTime(2012, 5, 6)));
            Assert.That(new DateTime(2012, 5, 5).FirstDayInMonth(DayOfWeek.Monday),
                        Is.EqualTo(new DateTime(2012, 5, 7)));
        }

        [Test]
        public void can_find_the_last_instance_of_a_weekday_in_a_given_month()
        {
            Assert.That(new DateTime(2012, 5, 5).LastDayInMonth(DayOfWeek.Saturday),
                        Is.EqualTo(new DateTime(2012, 5, 26)));
            Assert.That(new DateTime(2012, 5, 5).LastDayInMonth(DayOfWeek.Sunday), 
                        Is.EqualTo(new DateTime(2012, 5, 27)));
            Assert.That(new DateTime(2012, 5, 5).LastDayInMonth(DayOfWeek.Monday),
                        Is.EqualTo(new DateTime(2012, 5, 28)));
            Assert.That(new DateTime(2012, 5, 5).LastDayInMonth(DayOfWeek.Tuesday),
                        Is.EqualTo(new DateTime(2012, 5, 29)));
            Assert.That(new DateTime(2012, 5, 5).LastDayInMonth(DayOfWeek.Wednesday),
                        Is.EqualTo(new DateTime(2012, 5, 30)));
            Assert.That(new DateTime(2012, 5, 5).LastDayInMonth(DayOfWeek.Thursday),
                        Is.EqualTo(new DateTime(2012, 5, 31)));
        }

        [Test]
        public void can_find_the_next_day_of_week_from_a_given_time()
        {
            Assert.That(new DateTime(2012, 8, 18).Next(DayOfWeek.Saturday), 
                        Is.EqualTo(new DateTime(2012, 8, 25)));
            Assert.That(new DateTime(2012, 3, 20).Next(DayOfWeek.Monday), 
                        Is.EqualTo(new DateTime(2012, 3, 26)));
        }

        [Test]
        public void can_find_their_week_number()
        {
            Assert.That(new DateTime(2012, 8, 18).WeekOfYear(), Is.EqualTo(33));
            Assert.That(new DateTime(2012, 1, 18).WeekOfYear(), Is.EqualTo(3));
        }

        [Test]
        public void can_generate_an_sqlserver_friendly_date()
        {
            Assert.That(new DateTime(2012, 8, 18).ToSqlDate(), Is.EqualTo("20120818"));
        }

        [Test]
        public void can_generate_an_sqlserver_friendly_time()
        {
            Assert.That(new DateTime(2012, 8, 18, 20, 01, 08).ToSqlTime(), Is.EqualTo("20:01:08"));
        }

        [Test]
        public void can_generate_an_sqlserver_friendly_datetime()
        {
            Assert.That(new DateTime(2012, 8, 18, 20, 01, 08).ToSqlDateTime(), Is.EqualTo("20120818 20:01:08"));
        }

        [Test]
        public void can_generate_user_friendly_descriptions_of_elapsed_time()
        {
            var dt = DateTime.Now;
            Assert.That(dt.AddSeconds(-2).PrettyTimeSince(), Is.EqualTo("just now"));
            Assert.That(dt.AddMinutes(-1).PrettyTimeSince(), Is.EqualTo("1 minute ago"));
            Assert.That(dt.AddMinutes(-2).PrettyTimeSince(), Is.EqualTo("2 minutes ago"));
            Assert.That(dt.AddHours(-1.5).PrettyTimeSince(), Is.EqualTo("1 hour ago"));
            Assert.That(dt.AddHours(-2).PrettyTimeSince(), Is.EqualTo("2 hours ago"));
            Assert.That(dt.AddDays(-1).PrettyTimeSince(), Is.EqualTo("yesterday"));
            Assert.That(dt.AddDays(-2).PrettyTimeSince(), Is.EqualTo("2 days ago"));
            Assert.That(dt.AddDays(-7).PrettyTimeSince(), Is.EqualTo("1 week ago"));
            Assert.That(dt.AddDays(-14).PrettyTimeSince(), Is.EqualTo("2 weeks ago"));
            Assert.That(dt.AddDays(-34).PrettyTimeSince(), Is.EqualTo("several weeks ago"));
        }

        [Test]
        public void can_generate_user_friendly_descriptions_of_elapsed_time_in_norwegian()
        {
            var dt = DateTime.Now;
            Assert.That(dt.AddSeconds(-2).PrettyTimeSinceInNorwegian(), Is.EqualTo("nå nylig"));
            Assert.That(dt.AddMinutes(-1).PrettyTimeSinceInNorwegian(), Is.EqualTo("1 minutt siden"));
            Assert.That(dt.AddMinutes(-2).PrettyTimeSinceInNorwegian(), Is.EqualTo("2 minutter siden"));
            Assert.That(dt.AddHours(-1.5).PrettyTimeSinceInNorwegian(), Is.EqualTo("1 time siden"));
            Assert.That(dt.AddHours(-2).PrettyTimeSinceInNorwegian(), Is.EqualTo("2 timer siden"));
            Assert.That(dt.AddDays(-1).PrettyTimeSinceInNorwegian(), Is.EqualTo("i går"));
            Assert.That(dt.AddDays(-2).PrettyTimeSinceInNorwegian(), Is.EqualTo("2 dager siden"));
            Assert.That(dt.AddDays(-7).PrettyTimeSinceInNorwegian(), Is.EqualTo("1 uke siden"));
            Assert.That(dt.AddDays(-14).PrettyTimeSinceInNorwegian(), Is.EqualTo("2 uker siden"));
            Assert.That(dt.AddDays(-34).PrettyTimeSinceInNorwegian(), Is.EqualTo("flere uker siden"));
        }

        [Test]
        public void can_report_the_previous_months()
        {
            var dates = DateTime.Now.PrecedingMonths(5);
            Assert.That(dates.Count, Is.EqualTo(5));

            Assert.That(DateTime.Now.PrecedingMonths(10).Count(), Is.EqualTo(10));
            Assert.That(DateTime.Now.PrecedingMonths(0).Count(), Is.EqualTo(0));
            Assert.That(DateTime.Now.PrecedingMonths(-10).Count(), Is.EqualTo(0));
        }

        [Test]
        public void can_report_the_previous_twelve_months()
        {
            var dates = DateTime.Now.Preceding12Months();
            Assert.That(dates.Count, Is.EqualTo(12));
        }

        [Test]
        public void can_report_the_months_between_two_dates()
        {
            var dt = DateTime.Now;
            var dateSeries = DateTime.Now.EnsuingMonthsTo(DateTime.Now.AddYears(2));
            Assert.That(dateSeries.Count, Is.EqualTo(25));

            var invalidDateSeries = DateTime.Now.EnsuingMonthsTo(DateTime.Now.AddYears(-1));
            Assert.That(invalidDateSeries, Is.Empty);
        }
    }
}
