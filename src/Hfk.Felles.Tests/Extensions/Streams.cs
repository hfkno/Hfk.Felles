using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hfk.Felles.Tests.Extensions
{

    [TestFixture]
    public class Streams
    {

        protected static string testFile = Path.Combine(Felles.Utility.AssemblyDirectory,
                                                    @"..\..\_testData\Streams\streamTest.txt");
        [Test]
        public void can_have_all_their_content_read_as_lines()
        {
            var fs = new FileStream(testFile, FileMode.Open);
            fs.ReadLines();
        }

        [Test]
        public void can_have_all_their_content_read()
        {
            var fs = new FileStream(testFile, FileMode.Open);
            fs.ReadLines();
        }
    }
}
