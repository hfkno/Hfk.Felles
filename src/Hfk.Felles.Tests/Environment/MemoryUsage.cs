using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Hfk.Felles.Tests.Environment
{
    [TestFixture]
    public class MemoryUsage
    {
        [Test]
        public void can_report_current_memory_usage()
        {
            var mu = new Felles.Environment.MemoryUsage();

            mu.Update();
            Assert.That(mu.MemUsedMax, Is.GreaterThan(0));
            Assert.That(mu.MemUsedStart, Is.GreaterThan(0));
            Assert.That(mu.Name, Is.Not.EqualTo(string.Empty));

            Assert.That(mu.Rapport.Exists());
        }

        [Test]
        public void can_report_large_memory_usage()
        {
            var mu = new TestMemoryUsage();
            mu.Update();
            mu.Rapport.WriteToConsole();
            Assert.That(mu.Rapport.Exists());

            var tu = new TestTinyMemoryUsage();
            tu.Rapport.WriteToConsole();
            Assert.That(tu.Rapport.Exists());
        }

        
        public class TestMemoryUsage : Felles.Environment.MemoryUsage
        {
            private int callCount;

            protected override long TotalMemoryUsed
            {
                get
                {
                    callCount++;
                    return callCount % 2 == 0 ? 2000000 : 125;
                }
            }
        }
        
        public class TestTinyMemoryUsage : Felles.Environment.MemoryUsage
        {
            protected override long TotalMemoryUsed
            {
                get
                {
                    return 50;
                }
            }

            protected override long TotalMemoryWithoutGarbageCollection
            {
                get { return 50; }
            }
        }
    }
}
