using System;
using System.Collections;
using System.Collections.Generic;
using FakerLibrary.faker;

namespace FakerLibrary.generators
{
    public class ListGenerator : ICollectionGenerator<IList>
    {
        private readonly Random _random;

        public ListGenerator()
        {
            _random = new Random();
        }

        public IList Generate(Type baseType, Faker faker)
        {
            var listType = typeof(List<>);
            var elementType = baseType.GetGenericArguments()[0];
            var constructedListType = listType.MakeGenericType(elementType);
            var obj = (IList) Activator.CreateInstance(constructedListType);
            var count = _random.Next(1, 16);

            for (var i = 0; i < count; i++)
            {
                switch (elementType.Name)
                {
                    case "Double":
                        obj?.Add(faker.Create<double>());
                        break;
                    case "Long":
                        obj?.Add(faker.Create<long>());
                        break;
                    case "Short":
                        obj?.Add(faker.Create<short>());
                        break;
                    case "String":
                        obj?.Add(faker.Create<string>());
                        break;
                    case "Boolean":
                        obj?.Add(faker.Create<bool>());
                        break;
                    case "Char":
                        obj?.Add(faker.Create<char>());
                        break;
                    case "DateTime":
                        obj?.Add(faker.Create<DateTime>());
                        break;
                    case "Integer":
                        obj?.Add(faker.Create<int>());
                        break;
                    case "Float":
                        obj?.Add(faker.Create<float>());
                        break;
                }

                ;
            }

            return obj;
        }
    }
}