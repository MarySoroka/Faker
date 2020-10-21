using System;
using System.Linq;
using FakerLibrary.faker;

namespace FakerLibrary.generators
{
    public class StringGenerator : Generator<string>
    {
        private const int StringLength = 10;
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        protected override string Generate(FakerContext context)
        {
            return new string(Enumerable.Repeat(Chars, StringLength)
                .Select(finalString => finalString[context.Random.Next(finalString.Length)]).ToArray());
        }
    }
}