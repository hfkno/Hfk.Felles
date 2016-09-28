using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Hfk.Felles.Tests.Extensions
{
    [TestFixture]
    public class Lists
    {
        IList<int> intList = new List<int>() { 1, 2, 3, 4, 5 };
        IList<string> stringList = new List<string>() { "hey", "there", "now" };
        IList<string> emptyList = new List<string>() {  };
        IList untypedList = new List<int>() { 1, 2, 3, 4, 5 };
        IList nullList = null;

        [Test]
        public void can_find_their_last_index()
        {
            Assert.That(untypedList.LastIndex(), Is.EqualTo(4));
            Assert.That(nullList.LastIndex(), Is.EqualTo(-1));
        }

        [Test]
        public void can_find_their_first_item()
        {
            Assert.That(stringList.FirstItem(), Is.EqualTo("hey"));
            Assert.That(emptyList.FirstItem(), Is.EqualTo(null));
        }

        [Test]
        public void can_find_their_last_item()
        {
            Assert.That(stringList.LastItem(), Is.EqualTo("now"));
            Assert.That(emptyList.LastItem(), Is.EqualTo(null));
        }

        [Test]
        public void can_produce_a_safe_list()
        {
            Assert.That(untypedList.SafeList(), Is.EquivalentTo(untypedList));
            Assert.That(nullList.SafeList(), Is.Not.Null);
        }

        [Test]
        public void can_find_a_union_of_two_lists()
        {
            var a = new List<string>() { "one", "two", "three", "four" };
            var b = new List<string>() { "no", "no", "one", "no", "four" };

            var union = a.FindUnionWith(b);


            var x = new[] {1, 2, 3, 4};
            var y = new[] {1, 2, 3, 4};

            var z = new HashSet<int>(x);

            //Assert.That(x.set);


            //Assert.That(union.Count(), Is.EqualTo(2));
            //Assert.That(union.Contains("four"));
            //Assert.That(union.Contains("one"));
            //Assert.That(union.Contains("one"), Is.False);

        }
    }

    public static class stringsszzzz
    {
        public static string FindUnionWith<T>(this List<T> input, List<T> unionWith) where T : IEquatable<T>
        {
            return "hry";
        }
    }
}
