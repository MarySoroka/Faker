using System;

namespace FakerLibrary.generators
{
    public class DoubleGenerator:IPrimitiveGenerator<double>
    {
        private readonly Random _random;

        public DoubleGenerator()
        {
            _random = new Random();
        }

        public double Generate()
        {
            return _random.NextDouble();
        }
    }
}