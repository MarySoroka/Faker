using System;

namespace FakerLibrary.faker
{
    public class FakerContext
    {
        public FakerContext(Faker faker, Random random, Type targetType)
        {
            Faker = faker;
            Random = random;
            TargetType = targetType;
        }

        public Random Random { get; }

        public Type TargetType { get; }

        public Faker Faker { get; }
    }
}