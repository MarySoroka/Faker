using System;
using FakerLibrary.faker;
using FakerLibrary.generators;

namespace CharGenerator
{
    public class CharGenerator : Generator<char>
    {

        protected override char Generate(FakerContext context)
        {
            return (char) context.Random.Next(255);
        }
    }
}