using System;
using FakerLibrary.faker;

namespace FakerLibrary.generators
{
    public interface ICollectionGenerator<out T>: IGenerator
    {
        T Generate(Type baseType, Faker faker);
    }
}