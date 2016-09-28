using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace Hfk.Felles.Tests.Extensions
{
    [TestFixture]
    public class Collections
    {
        ICollection<int> testTypedColl = new List<int>() { 1, 2, 3, 4, 5};
        ICollection<int> emptyTypedColl = new List<int>() { };
        ICollection<int> nullTypedColl = null;
        ICollection testColl = new List<int>() { 1, 2, 3, 4, 5};
        ICollection emptyColl = new List<int>() { };
        ICollection nullColl = null;

            
        [Test]
        public void can_find_their_last_index()
        {
            Assert.That(testColl.LastIndex(), Is.EqualTo(4));
            Assert.That(testTypedColl.LastIndex(), Is.EqualTo(4));
            Assert.That(emptyTypedColl.LastIndex(), Is.EqualTo(-1));
            Assert.That(emptyColl.LastIndex(), Is.EqualTo(-1));
            Assert.That(nullColl.LastIndex(), Is.EqualTo(-1));
        }

        [Test]
        public void can_find_their_first_index()
        {
            Assert.That(testColl.FirstIndex(), Is.EqualTo(0));
            Assert.That(emptyColl.FirstIndex(), Is.EqualTo(-1));
            Assert.That(nullColl.FirstIndex(), Is.EqualTo(-1));
        }

        [Test]
        public void know_if_they_have_items()
        {
            Assert.That(testColl.HasItems());
            Assert.That(testTypedColl.HasItems());
            Assert.That(emptyTypedColl.HasItems(), Is.False);
            Assert.That(nullTypedColl.HasItems(), Is.False);
            Assert.That(emptyColl.HasItems(), Is.False);
            Assert.That(nullColl.HasItems(), Is.False);
        }

        [Test]
        public void know_if_they_have_a_given_index()
        {
            Assert.That(testTypedColl.ContainsIndex(25), Is.False);
            Assert.That(testTypedColl.ContainsIndex(-1), Is.False);
            Assert.That(testTypedColl.ContainsIndex(2));
            Assert.That(nullTypedColl.ContainsIndex(2), Is.False);
        }

    }
}
