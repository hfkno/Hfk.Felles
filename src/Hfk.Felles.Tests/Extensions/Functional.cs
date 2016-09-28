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
    public class Functional
    {
        ICollection<int> testColl = new List<int>() { 1, 2, 3, 4, 5 };
        IEnumerable testEnum = new List<int>() { 1, 2, 3, 4, 5 };
        IEnumerable<int> testTypedEnum = new List<int>() { 1, 2, 3, 4, 5 };
        List<int> typedList = new List<int>() { 1, 2, 3, 4, 5 };
        IDictionary<string, string>  testDict = new Dictionary<string, string>()
                                                    {
                                                        {"foo", "bar"},
                                                        {"oi", "df"},
                                                        {"re", "hghg"},
                                                    };
 
        int[] testArr = new[] { 1, 2, 3, 4, 5 };


        [Test]
        public void can_be_folded()
        {
            var result = testColl.Fold((x, y) => x + y);
            Assert.That(result, Is.EqualTo(15));
        }

        [Test]
        public void can_be_folded_with_accumulation()
        {
            var result = testColl.Fold(5, (x, y) => x + y);
            Assert.That(result, Is.EqualTo(20));
        }

        [Test]
        public void can_be_folded_with_accumulation_and_a_result_selector()
        {
            var result = testColl.Fold(5, (x, y) => x + y, (x) => x == 20);
            Assert.That(result, Is.True);
        }

        [Test]
        public void can_be_iterated()
        {
            var sum = 10;
            testColl.ForEach(x => sum += x);
            Assert.That(sum, Is.EqualTo(25));

            var arrSum = 10;
            testArr.ForEach(x => arrSum += x);
            Assert.That(arrSum, Is.EqualTo(25));

            var enumSum = 20;
            testEnum.ForEach((int x) => enumSum += x);
            Assert.That(enumSum, Is.EqualTo(35));

            var typedEnumSum = 30;
            testTypedEnum.ForEach((int x) => typedEnumSum += x);
            Assert.That(typedEnumSum, Is.EqualTo(45));
        }

        [Test]
        public void can_be_mapped()
        {
            // The mapping methods mostly wrap ForEach calls...
            var sum = 0;
            testArr.Map(x => sum += x);
            testEnum.Map((int x) => sum += x);
            testTypedEnum.Map((int x) => sum += x);
            typedList.Map((int x) => sum += x);

            Assert.That(sum, Is.EqualTo(60));
        }

        [Test]
        public void can_be_iterated_with_an_index()
        {
            var sum = 10;
            testColl.ForEach((x, y) => sum += x + y);
            Assert.That(sum, Is.EqualTo(35));

            var arrSum = 10;
            testArr.ForEach((x, y) => arrSum += x + y);
            Assert.That(arrSum, Is.EqualTo(35));

            var enumSum = 20;
            testEnum.ForEach((int x, int y) => enumSum += x + y);
            Assert.That(enumSum, Is.EqualTo(45));
        }


        [Test]
        public void can_be_filtered()
        {
            var result = testColl.Filter(x => x >= 4);
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void can_be_filtered_with_other_lists()
        {
            var filterItems = new List<int>() {3, 4, 5};
            var result = testTypedEnum.Filter(filterItems);

            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First(), Is.EqualTo(1));
            Assert.That(result.Skip(1).Take(1).First(), Is.EqualTo(2));
        }

        [Test]
        public void can_be_filtered_with_a_function()
        {
            var result = testEnum.Filter((o) => o.Exists());
            var itemCount = result.Cast<object>().Count();
            Assert.That(itemCount, Is.EqualTo(5));
        }

        [Test]
        public void can_be_filtered_from_a_list_provided_by_a_function()
        {
            var result = testTypedEnum.Filter(x => typedList);
            Assert.That(result.Count(), Is.EqualTo(0));
        }

    }
}
