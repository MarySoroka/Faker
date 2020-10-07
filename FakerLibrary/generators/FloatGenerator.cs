using System;

namespace FakerLibrary.generators
{
    public class FloatGenerator: IPrimitiveGenerator<float>
    {
        private readonly Random _random;

        public FloatGenerator()
        {
            _random = new Random();
        }

        public float Generate()
        {
            return (float) _random.NextDouble();
        }
    }
}