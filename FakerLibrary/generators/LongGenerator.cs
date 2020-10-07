using System;

namespace FakerLibrary.generators
{
    public class LongGenerator: IPrimitiveGenerator<long>
    {
        private readonly Random _random;

        public LongGenerator()
        {
            _random = new Random();
        }

        public long Generate()
        {
            return _random.Next();
        }
    }
}