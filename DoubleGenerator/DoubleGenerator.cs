using System;
using FakerLibrary.generators;

namespace DoubleGenerator
{
    public class DoubleGenerator : IPrimitiveGenerator<double>
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