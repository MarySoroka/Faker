using System;
using FakerLibrary.generators;

namespace BooleanPlugin
{
    public class BooleanGenerator:IPrimitiveGenerator<bool>
    {
        private readonly Random _random;

        public BooleanGenerator()
        {
            _random = new Random();
        }
        
        public bool Generate()
        {
            return _random.Next(0, 2) != 0;
            
        }
    }
}