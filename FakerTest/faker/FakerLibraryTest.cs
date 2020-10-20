using System;
using System.Collections.Generic;
using FakerLibrary.faker;
using FakerLibrary.generators;
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
            var fakerGeneratorRules = new List<FakerGeneratorRule>();
            var fakerConfiguration = new FakerConfiguration(fakerGeneratorRules);
            fakerConfiguration.AddFakerRule<PrivateModificationClassTest, int, CustomLongGenerator>(c => c.prop);
            _faker = new Faker(fakerConfiguration);
        }

        private struct DefaultTestStruct
        {
            public readonly int Num;
            char _charTest;

            public DefaultTestStruct(int num, char charTest)
            {
                this.Num = num;
                this._charTest = charTest;
            }
        }

        private struct PublicConstructorTestStruct
        {
            public int field1;
            char field2;

            public PublicConstructorTestStruct(char f)
            {
                field2 = f;
                field1 = 1;
            }
        }

        struct PrivateConstructorTestStruct
        {
            public int field1;
            char field2;

            PrivateConstructorTestStruct(char f)
            {
                field2 = f;
                field1 = 1;
            }
        }

        struct MultiConstructorStruct
        {
            int field1;
            char field2;
            public float field3;

            public MultiConstructorStruct(int field)
            {
                field1 = field;
                field2 = '1';
                field3 = 0.1f;
            }

            public MultiConstructorStruct(int field1, char field2, float field3)
            {
                this.field1 = field1;
                this.field2 = field2;
                this.field3 = field3;
            }
        }

        private class DefaultConstructorClass
        {
            public int i;
            float f;
            public string s;
            public DateTime t;
            long l;
            public bool b;
            double d;
        }

        class PrivateConstructorClass
        {
            public int i;
            float f;
            public string s;
            public DateTime t;
            long l;
            public char c;
            public bool b;
            double d;

            PrivateConstructorClass(float f, long l)
            {
                this.f = f;
                this.l = l;
            }
        }

        class MultiConstructorClass
        {
            public int i;
            float f;
            public string s { get; set; }
            public DateTime t;
            long l;
            public char c { get; }
            public bool b { get; }
            double d;

            public MultiConstructorClass()
            {
            }

            public MultiConstructorClass(long l, double d)
            {
                this.l = l;
                this.d = d;
            }
        }

        class ListClass
        {
            public List<int> ints;
            List<double> doubles;
            List<char> chars;
            public List<DateTime> times;

            public ListClass(List<double> doubles)
            {
                this.doubles = doubles;
            }
        }

        class MultiTypeClass
        {
            public int a;
            public List<string> s;
            public ListClass c;
            ListClass pc;

            MultiTypeClass(ListClass pc)
            {
                this.pc = pc;
            }

            public MultiTypeClass()
            {
            }
        }

        class CicleClass1
        {
            public CicleClass2 c { get; set; }
        }

        class CicleClass2
        {
            public CicleClass3 c { get; set; }
        }

        class CicleClass3
        {
            public CicleClass1 c { get; set; }
        }

        class PrivateModificationClassTest
        {
            public int prop { get; }
            public long prop2 { get; set; }

            public PrivateModificationClassTest(int prop)
            {
                this.prop = prop;
            }
        }

        class CustomLongGenerator : IPrimitiveGenerator<long>
        {
            public long Generate()
            {
                return 5;
            }
        }


        [TestMethod]
        public void CreateDefaultStructTest()
        {
            var testStruct = _faker.Create<DefaultTestStruct>();
            var defaultTestStruct = new DefaultTestStruct();
            Assert.AreNotEqual(defaultTestStruct.Num, testStruct.Num);
        }

        [TestMethod]
        public void CreatePublicStructTest()
        {
            var testStruct = _faker.Create<PublicConstructorTestStruct>();
            var publicConstructorTestStruct = new PublicConstructorTestStruct('f');
            Assert.AreEqual(publicConstructorTestStruct.field1, testStruct.field1);
        }

        [TestMethod]
        public void CreatePrivateStructTest()
        {
            var testStruct = _faker.Create<PrivateConstructorTestStruct>();
            var privateConstructorTestStruct = new PrivateConstructorTestStruct();
            Assert.AreNotEqual(privateConstructorTestStruct.field1, testStruct.field1);
        }

        [TestMethod]
        public void CreateMultiStructTest()
        {
            var testStruct = _faker.Create<MultiConstructorStruct>();
            var multiConstructorStruct = new MultiConstructorStruct();
            Assert.AreNotEqual(multiConstructorStruct.field3, testStruct.field3);
        }

        [TestMethod]
        public void CreateDefaultClassTest()
        {
            var constructorClass = _faker.Create<DefaultConstructorClass>();
            var defaultConstructorClass = new DefaultConstructorClass();
            Assert.AreNotEqual(defaultConstructorClass.i, constructorClass.i);
            Assert.AreNotEqual(defaultConstructorClass.s, constructorClass.s);
            Assert.AreNotEqual(defaultConstructorClass.t, constructorClass.t);
        }

        [TestMethod]
        public void CreatePrivateClassTest()
        {
            var privateConstructorClass = _faker.Create<PrivateConstructorClass>();
            Assert.AreEqual(null, privateConstructorClass);
        }

        [TestMethod]
        public void CreateMultiClassTest()
        {
            var constructorClass = _faker.Create<MultiConstructorClass>();
            var multiConstructorClass = new MultiConstructorClass();
            Assert.AreEqual(multiConstructorClass.c, constructorClass.c);
            Assert.AreNotEqual(multiConstructorClass.i, constructorClass.i);
            Assert.AreNotEqual(multiConstructorClass.s, constructorClass.s);
            Assert.AreNotEqual(multiConstructorClass.t, constructorClass.t);
        }

        [TestMethod]
        public void CreateCollectionClassTest()
        {
            var listClass = _faker.Create<ListClass>();
            var notExpected = new ListClass(new List<double>());
            CollectionAssert.AreNotEqual(notExpected.ints, listClass.ints);
            CollectionAssert.AreNotEqual(notExpected.times, listClass.times);
        }

        [TestMethod]
        public void CreateListClassTest()
        {
            var multiTypeClass = _faker.Create<MultiTypeClass>();
            var typeClass = new MultiTypeClass();
            Assert.AreNotEqual(typeClass.a, multiTypeClass.a);
            Assert.AreNotEqual(typeClass.c, multiTypeClass.c);
            CollectionAssert.AreNotEqual(typeClass.s, multiTypeClass.s);
        }

        [TestMethod]
        public void CreateConfigurationClassTest()
        {
            var privateModificationClassTest = _faker.Create<PrivateModificationClassTest>();
            var modificationClassTest = new PrivateModificationClassTest(5);
            Assert.AreEqual(modificationClassTest.prop, privateModificationClassTest.prop);
            Assert.AreNotEqual(modificationClassTest.prop2, privateModificationClassTest.prop2);
        }
    }
}