using System;
using FakerLibrary.faker;

namespace FakerLibrary.generators
{
    public class FloatGenerator: Generator<float>
    {
        protected override float Generate(FakerContext context)
        {
            return (float) context.Random.NextDouble();
        }
    }
}