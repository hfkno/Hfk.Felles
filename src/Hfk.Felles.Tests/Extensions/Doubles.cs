using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hfk.Felles.Tests.Extensions
{
    [TestFixture]
    public class Doubles
    {
        [Test]
        public void can_calculate_a_distribution_based_on_a_series()
        {
            var numbers = new[] { 10.0, 5.0, 10.0, 3.0 };

            Assert.That(numbers.Fordeling()[0], Is.EqualTo(36.0));
            Assert.That(numbers.Fordeling()[1], Is.EqualTo(18.0));
            Assert.That(numbers.Fordeling()[2], Is.EqualTo(36.0));
            Assert.That(numbers.Fordeling()[3], Is.EqualTo(11.0));
        }
    }
}
