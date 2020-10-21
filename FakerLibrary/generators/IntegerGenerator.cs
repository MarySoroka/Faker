using System;
using FakerLibrary.faker;

namespace FakerLibrary.generators
{
    public class IntegerGenerator : Generator<int>
    {
        protected override int Generate(FakerContext context)
        {
            return context.Random.Next();
        }
    }
}