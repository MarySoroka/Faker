using System;

namespace FakerLibrary.generators
{
    public class ShortGenerator: IPrimitiveGenerator<short>
    {
        private readonly Random _random;

        public ShortGenerator()
        {
            _random = new Random();
        }
        
        public short Generate()
        {
            return (short) _random.Next(0,255);
        }
    }
}