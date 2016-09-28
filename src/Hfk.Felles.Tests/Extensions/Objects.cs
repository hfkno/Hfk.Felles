using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hfk.Felles.Tests.Extensions
{
    [TestFixture]
    public class Objects
    {
        [Test]
        public void exist_when_initialized()
        {
            const string o = "foo";
            Assert.That(o.Exists());
        }

        [Test]
        public void do_not_exist_without_initialization()
        {
            Object o = null;
            Assert.That(o.Exists(), Is.False);
        }


        [Test]
        public void can_be_written_to_the_console()
        {
            object o = "hello";
            o.WriteToConsole();
            Assert.That(true);
        }
    }
}
