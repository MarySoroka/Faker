using System;
using FakerLibrary.faker;

namespace FakerLibrary.generators
{
    public class BooleanGenerator : Generator<bool>
    {
        protected override bool Generate(FakerContext context)
        {
            return context.Random.Next(0, 2) != 0;
        }
    }
}