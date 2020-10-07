using System;
using System.Linq;

namespace FakerLibrary.generators
{
    public class StringGenerator : IPrimitiveGenerator<string>
    {
        private readonly Random _random;
        private const int StringLength = 10;
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public StringGenerator()
        {
            _random = new Random();
        }

        public string Generate()
        {
            return new string(Enumerable.Repeat(Chars, StringLength)
                .Select(finalString => finalString[_random.Next(finalString.Length)]).ToArray());
        }
    }
}