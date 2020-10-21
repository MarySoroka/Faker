using System;
using FakerLibrary.faker;

namespace FakerLibrary.generators
{
    public class ShortGenerator: Generator<short>
    {
        protected override short Generate(FakerContext context)
        {
            return (short) context.Random.Next(0,255);
        }
    }
}