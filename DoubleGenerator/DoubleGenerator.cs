using System;
using FakerLibrary.faker;
using FakerLibrary.generators;

namespace DoubleGenerator
{
    public class DoubleGenerator : Generator<double>
    {
        protected override double Generate(FakerContext context)
        {
            return context.Random.NextDouble();
        }
    }
}