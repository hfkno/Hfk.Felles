using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hfk.Felles.Tests.Extensions
{
    [TestFixture]
    public class Conversions
    {

        [Test]
        public void can_convert_to_between_value_types()
        {
            Assert.That("123".To<int>(), Is.EqualTo(123));
            Assert.That("123z".To<int>(), Is.EqualTo(0));

            Assert.That("123,4".To<double>() > 123.0);
            Assert.That("123.4".To<double>() > 123.0);
            Assert.That("123.4z".To<double>(), Is.EqualTo(0));

            Assert.That(1234.To<double>(), Is.EqualTo(1234));
            Assert.That(1234.34.To<int>(), Is.EqualTo(1234));

            Assert.That("2014-10-10".To<DateTime>(), Is.EqualTo(new DateTime(2014, 10, 10)));
            Assert.That("5.5.14".To<DateTime>(), Is.EqualTo(new DateTime(2014, 5, 5)));
            Assert.That("3/10/12".To<DateTime>(), Is.EqualTo(new DateTime(2012, 3, 10)));

            Assert.That("true".To<bool>());
            //Assert.That("1".To<bool>());
            //Assert.That("-1".To<bool>());
            Assert.That("0".To<bool>(), Is.False);
        }
    }
}
