using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Hfk.Felles.Tests.Extensions
{
    [TestFixture]
    public class Enumerables
    {
        IEnumerable<int> testTypedEnum = new List<int>() { 1, 2, 3, 4, 5 };
        IEnumerable<string> testStringEnum = new List<string>() { "1:{0}", "2:{0}" };
        IEnumerable<int> emptyTypedEnum = new List<int>() { };
        IEnumerable<int> nullTypedEnum = null;
        IEnumerable testEnum = new List<int>() { 1, 2, 3, 4, 5 };
        IEnumerable emptyEnum = new List<int>() { };
        IEnumerable nullEnum = null;
        IEnumerable<int> testSet = new List<int>() { 1, 2, 3, 4, 5 };


        [Test]
        public void can_write_themselves_to_the_console()
        {
            testTypedEnum.WriteMembersToConsole();
            testTypedEnum.WriteMembersToConsole(x => "i:{0}".FormatWith(x));

            testStringEnum.WriteMembersToConsole("hello");
            testStringEnum.WriteMembersToConsole(x => "{0}".FormatWith(x), "hey");

            testTypedEnum.WriteMembersToConsoleWhere(i => i > 3);
        }


        [Test]
        public void can_report_if_they_have_items()
        {
            Assert.That(testStringEnum.HasItems());
        }

        [Test]
        public void can_report_if_they_are_empty()
        {
            Assert.That(emptyTypedEnum.IsEmpty());
            
        }

        [Test]
        public void can_report_if_that_they_are_initialized_and_populated()
        {
            Assert.That(testStringEnum.IsInitializedAndPopulated());
            Assert.That(emptyTypedEnum.IsInitializedAndPopulated(), Is.False);
            Assert.That(nullTypedEnum.IsInitializedAndPopulated(), Is.False);
        }

        [Test]
        public void can_generate_ranges()
        {
            var numbers = 5.To(20);
            Assert.That(numbers.Count(), Is.EqualTo(16));
            Assert.That(numbers.HasAnyWhere(n => n == 5));
            Assert.That(numbers.HasAnyWhere(n => n == 20));
        }


        [Test]
        public void can_convert_themselves_to_typed_enumerables()
        {
            var typedOutput = testEnum.ToIEnumerableOf<int>();
            Assert.That(typedOutput.FirstOrDefault(), Is.EqualTo(1));
        }

        [Test]
        public void can_convert_themselves_to_typed_lists()
        {
            var typedOutput = testEnum.ToListOf<int>();
            Assert.That(typedOutput[2], Is.EqualTo(3));

            var nullListOutput = nullEnum.ToListOf<int>();
            Assert.That(nullListOutput.Exists());
            Assert.That(nullListOutput.HasItems(), Is.False);
        }

        [Test]
        public void can_convert_themselves_to_typed_list_based_on_a_generation_function()
        {
            var typedOutput = testTypedEnum.ToSafeListFrom(i => i.ToString());
            Assert.That(typedOutput[2], Is.EqualTo("3"));

            IList<int> nullListOutput = nullTypedEnum.ToSafeListFrom(i => i);
            Assert.That(nullListOutput.Exists());
            Assert.That(nullListOutput.HasItems(), Is.False);
        }

        [Test]
        public void can_find_child_values_matching_a_criteria()
        {
            Assert.That(testEnum.HasAnyWhere((int n) => n == 2));
            Assert.That(testTypedEnum.HasAnyWhere(n => n == 2));
            Assert.That(testTypedEnum.IsTrueForAny(n => n == 2));
        }

        [Test]
        public void can_find_if_all_child_values_match_a_criteria()
        {
            Assert.That(testTypedEnum.IsTrueForAll(n => n > 0));
            Assert.That(testTypedEnum.IsTrueForAll(n => n > 100), Is.False);
        }

        [Test]
        public void can_find_if_all_child_values_fail_to_match_a_criteria()
        {
            Assert.That(testTypedEnum.IsFalseForAll(n => n > 100));
            Assert.That(testTypedEnum.IsFalseForAll(n => n > 0), Is.False);
        }

        [Test]
        public void can_find_if_any_child_values_fail_to_match_a_criteria()
        {
            Assert.That(testTypedEnum.IsFalseForAny(n => n > 2));
            Assert.That(testTypedEnum.IsFalseForAny(n => n > 0), Is.False);
        }

        [Test]
        public void can_determine_if_they_are_a_proper_subset()
        {
            var x = new List<int>(new[] { 1, 2, 3 });
            Assert.That(x.IsProperSubsetOf(testSet));

            var y = new List<int>(new[] { 0, 1, 2, 3 });
            Assert.That(y.IsProperSubsetOf(testSet), Is.False);
            Assert.That(testSet.IsProperSubsetOf(testSet), Is.False);
        }

        [Test]
        public void can_determine_if_they_are_a_proper_superset()
        {
            var x = new List<int>(new[] { 1, 2, 3, 4, 5, 6 });
            Assert.That(x.IsProperSupersetOf(testSet));

            Assert.That(testSet.IsProperSupersetOf(testSet), Is.False);
        }

        [Test]
        public void can_determine_if_they_are_a_subset()
        {
            var x = new List<int>(new[] { 1, 2, 3 });
            Assert.That(x.IsSubsetOf(testSet));

            Assert.That(testSet.IsSubsetOf(testSet));
        }

        [Test]
        public void can_determine_if_they_are_a_superset()
        {
            var x = new List<int>(new[] { 1, 2, 3, 4, 5, 12345 });
            Assert.That(x.IsSupersetOf(testSet));

            Assert.That(testSet.IsSupersetOf(testSet));
        }

        [Test]
        public void can_find_overlapping_sets()
        {
            var x = new List<int>(new[] { 1, 2, 3 });
            Assert.That(x.Overlaps(testSet));
        }

        [Test]
        public void can_be_converted_to_a_set()
        {
            var s = testSet.ToSet();
            Assert.That(s.Contains(2));
        }

        [Test]
        public void can_be_converted_to_a_sorted_set()
        {
            var s = testSet.ToSortedSet();
            Assert.That(s.Contains(2));
            Assert.That(s.Skip(2).Take(1).FirstOrDefault(), Is.EqualTo(3), "Set order not preserved");
        }

        [Test]
        public void can_check_set_equality()
        {
            var x = new List<int>(new[] { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 });
            Assert.That(x.IsSetEqualTo(testSet));
            
            var y = new List<int>(new[] { 5, 4, 3, 2, 1 });
            Assert.That(y.IsSetEqualTo(testSet));

            var z = new List<int>(new[] { 1, 2, 3, 4 });
            Assert.That(z.IsSetEqualTo(testSet), Is.False);
        }


        [Test]
        public void can_check_sequence_equality()
        {
            var x = new List<int>(new[] { 1, 2, 3, 4, 5 });
            Assert.That(x.IsSequenceEqualTo(testSet));

            var y = new List<int>(new[] { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 });
            Assert.That(y.IsSequenceEqualTo(testSet), Is.False);

            var m = new List<int>(new[] { 1, 2, 3, 4, 6 });
            Assert.That(m.IsSequenceEqualTo(testSet), Is.False);

            var q = new List<int>(new[] { 1, 2, 3, 4 });
            Assert.That(q.IsSequenceEqualTo(testSet), Is.False);

            var r = new List<int>(new[] { 1, 2, 3, 4, 5, 5 });
            Assert.That(r.IsSequenceEqualTo(testSet), Is.False);

            var z = new List<int>(new[] { 5, 4, 3, 2, 1 });
            Assert.That(z.IsSequenceEqualTo(testSet), Is.False);

        }
    }
}
