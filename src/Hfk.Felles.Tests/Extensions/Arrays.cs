using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hfk.Felles.Tests.Extensions
{
    [TestFixture]
    public class Arrays
    {

        [Test]
        public void can_count_their_items()
        {
            var s = new[] {"h", "e", "l", "l", "o"};
            Assert.That(s.Count(), Is.EqualTo(5));

            var s2 = new string[] {};
            Assert.That(s2.Count(), Is.EqualTo(0));
        }

        [Test]
        public void can_report_if_they_have_items()
        {
            var s = new[] { "key", "val", "key", "val", "key" };
            Assert.That(s.HasItems());

            var s2 = new string[] { };
            Assert.That(s2.HasItems(), Is.False);

            Assert.That(((string[])null).HasItems(), Is.False);
        }

        [Test]
        public void can_report_if_they_are_empty()
        {
            var s = new string[] { };
            Assert.That(s.IsEmpty());

            var s2 = new[] { "key", "val", "key", "val", "key" };
            Assert.That(s2.IsEmpty(), Is.False);

        }

        [Test]
        public void can_report_if_they_are_uneven()
        {
            var s = new[] { "key", "val", "key", "val", "key" };
            Assert.That(s.IsUneven());

            var s2 = new[] { "key", "val", "key", "val", "key", "val" };
            Assert.That(s.IsUneven());
        }
    }
}
