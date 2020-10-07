using System;

namespace FakerLibrary.generators
{
    public class IntegerGenerator : IPrimitiveGenerator<int>
    {
        private readonly Random _random;

        public IntegerGenerator()
        {
            _random = new Random();
        }
        
        public int Generate()
        {
            return _random.Next();
        }
    }
}