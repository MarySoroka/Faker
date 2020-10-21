using System;
using FakerLibrary.faker;

namespace FakerLibrary.generators
{
    public class LongGenerator : Generator<long>
    {
        protected override long Generate(FakerContext context)
        {
            return context.Random.Next() << 31 | context.Random.Next();
        }
    }
}