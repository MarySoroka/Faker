using System;
using FakerLibrary.generators;

namespace CharGenerator
{
    public class CharGenerator : IPrimitiveGenerator<char>
    {
        private readonly Random _random;

        public CharGenerator()
        {
            _random = new Random();
        }

        public char Generate()
        {
            return (char) _random.Next(255);
        }
    }
}