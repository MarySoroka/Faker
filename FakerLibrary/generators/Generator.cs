using System;
using FakerLibrary.faker;

namespace FakerLibrary.generators
{
    public abstract class Generator<T> : IGenerator
    {
        protected abstract T Generate(FakerContext context);
        object IGenerator.Generate(FakerContext context)
        {
            return Generate(context);
        }

        bool IGenerator.CanGenerate(Type type)
        {
            return type == typeof(T);
        }
    }
}