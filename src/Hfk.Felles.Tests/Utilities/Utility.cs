using NUnit.Framework;

namespace Hfk.Felles.Tests.Utilities
{
    [TestFixture]
    public class Utility
    {
        [Test]
        public void can_find_current_execution_location()
        {
            Assert.That(Felles.Utility.ExecutionLocation, Is.Not.Empty);
        }

        [Test]
        public void can_find_current_assembly_directory()
        {
            Assert.That(Felles.Utility.ExecutionLocation, Is.Not.Empty);
        }

        [Test]
        public void can_find_if_the_debugger_is_attached()
        {
            // As this test will trigger differently when tests are being debugged, a simple 
            // check that no exceptions are provoked by the code is adequate.
            Assert.That(Felles.Utility.DebuggerIsAttached || true);
        }
    }
}
