using System.Collections.Generic;
using FakerLibrary.faker;
using FakerTest.faker;

namespace FakerConsoleApp.application
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var faker = new Faker();
            var structure1 = faker.Create<FakerTestUtil.Structure1>();
            var structure2 = faker.Create<FakerTestUtil.Structure2>();
            var structure3 = faker.Create<FakerTestUtil.Structure3>();
            var structure4 = faker.Create<FakerTestUtil.Structure4>();
            var class1 = faker.Create<FakerTestUtil.Class1>();
            var class2 = faker.Create<FakerTestUtil.Class2>();
            var class3 = faker.Create<FakerTestUtil.Class3>();
            var class4 = faker.Create<FakerTestUtil.Class4>();
            var class5 = faker.Create<FakerTestUtil.Class5>();
            var class6 = faker.Create<FakerTestUtil.Class6>();
            var someClasses = faker.Create<List<FakerTestUtil.Class1>>();

        }
    }
}