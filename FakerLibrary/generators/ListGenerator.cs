using System;
using System.Collections;
using System.Collections.Generic;
using FakerLibrary.faker;

namespace FakerLibrary.generators
{
    public class ListGenerator : IGenerator
    {

        public object Generate(FakerContext context)
        {
            var listType = typeof(List<>);
            var elementType = context.TargetType.GetGenericArguments()[0];
            var constructedListType = listType.MakeGenericType(elementType);
            var instance = (IList) Activator.CreateInstance(constructedListType);
            for (var i = 0; i < 5; i++)
            {
                instance?.Add(context.Faker.Create(elementType));
            }

            return instance;
        }

        public bool CanGenerate(Type type)
        {
            if (type.IsGenericType)
            {
                return type.GetGenericTypeDefinition() == typeof(List<>);
            }

            return false;
        }
    }
}