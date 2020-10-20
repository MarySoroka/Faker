using System;
using FakerLibrary.faker;

namespace FakerLibrary.generators
{
    public class CollectionGeneratorUtil
    {
        private CollectionGeneratorUtil()
        {
        }

        public static object CreatePrimitive(Type baseType, Faker faker)
        {
            return baseType.Name switch
            {
                "Double" => faker.Create<double>(),
                "Long" => faker.Create<long>(),
                "Short" => faker.Create<short>(),
                "String" => faker.Create<string>(),
                "Boolean" => faker.Create<bool>(),
                "Char" => faker.Create<char>(),
                "DateTime" => faker.Create<DateTime>(),
                "Integer" => faker.Create<int>(),
                "Float" => faker.Create<float>(),
                _ => null
            };
        }
    }
}