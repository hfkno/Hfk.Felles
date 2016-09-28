using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hfk.Felles.Tests.Extensions
{
    [TestFixture]
    public class Stacks
    {
        [Test]
        public void can_report_if_they_are_empty()
        {
            var s = new Stack<string>();
            Assert.That(s.IsEmpty());

            s.Push("one");
            Assert.That(s.IsEmpty(), Is.False);
        }
    }
}
