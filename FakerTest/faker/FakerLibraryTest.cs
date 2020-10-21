using System.Collections.Generic;
using FakerLibrary.faker;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FakerTest.faker
{
    [TestClass]
    public class FakerLibraryTest
    {
        private static Faker _faker;
       

        [ClassInitialize]
        public static void Init(TestContext testContext)
        {
            _faker = new Faker();
        }


        [TestMethod]
        public void TestWithDefStruct()
        {
            var created = _faker.Create<FakerTestUtil.Structure2>();
            Assert.AreNotEqual(0, created.field1);
        }

        [TestMethod]
        public void TestMultipleConstrStruct()
        {
            var actual = _faker.Create<FakerTestUtil.Structure4>();
            var notExpected = new FakerTestUtil.Structure4();
            Assert.AreNotEqual(notExpected.field3, actual.field3);
        }

        [TestMethod]
        public void TestDefaultClass()
        {
            var actual = _faker.Create<FakerTestUtil.Class1>();
            var notExpected = new FakerTestUtil.Class1();
            Assert.AreNotEqual(notExpected.field, actual.field);
            Assert.AreNotEqual(notExpected.field2, actual.field2);
        }

        [TestMethod]
        public void TestPrivateConstrClass()
        {
            var actual = _faker.Create<FakerTestUtil.Class2>();
            Assert.AreEqual(null, actual);
        }

        [TestMethod]
        public void CreateNestedClasses()
        {
            var actual = _faker.Create<FakerTestUtil.Class4>();
            var notExpected = new FakerTestUtil.Class4();
            Assert.AreNotEqual(notExpected.a, actual.a);
            Assert.AreNotEqual(notExpected.sClass, actual.sClass);
        }

        [TestMethod]
        public void CreateMultCtorClass()
        {
            var actual = _faker.Create<FakerTestUtil.Class5>();
            var notExpected = new FakerTestUtil.Class5();
            Assert.AreEqual(notExpected.c, actual.c);
            Assert.AreNotEqual(notExpected.i, actual.i);
            Assert.AreNotEqual(notExpected.t, actual.t);
        }

        [TestMethod]
        public void CreateListClass()
        {
            var actual = _faker.Create<FakerTestUtil.Class6>();
            var notExpected = new FakerTestUtil.Class6(new List<double>());
            CollectionAssert.AreNotEqual(notExpected.ints, actual.ints);
            CollectionAssert.AreNotEqual(notExpected.times, actual.times);
        }
        [TestMethod]
        public void CreateCircularClass()
        {
            var actual = _faker.Create<FakerTestUtil.Circular1>();
            Assert.AreNotEqual(0, actual.i);
        }
    }
}