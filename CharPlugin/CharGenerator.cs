using System;
using FakerLibrary.generators;

namespace CharPlugin
{
    public class CharGenerator : IGenerator
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