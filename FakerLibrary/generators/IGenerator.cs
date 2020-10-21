using System;
using FakerLibrary.faker;

namespace FakerLibrary.generators
{
    public interface IGenerator
    {
        object Generate(FakerContext context);
        bool CanGenerate(Type type);
    }
}