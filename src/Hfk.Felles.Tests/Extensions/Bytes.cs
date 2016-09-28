using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hfk.Felles.Tests.Extensions
{
    [TestFixture]
    public class Bytes
    {

        [Test]
        public void can_be_converted_to_boolean()
        {
            byte fb = 0;
            byte tb = 1;

            Assert.That(tb.ToBool());
            Assert.That(fb.ToBool(), Is.False);
        }
    }
}
