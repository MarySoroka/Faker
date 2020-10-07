using System;

namespace FakerLibrary.generators
{
    public class DateGenerator : IPrimitiveGenerator<DateTime>
    {
        private readonly Random _random;

        public DateGenerator()
        {
            _random = new Random();
        }

        public DateTime Generate()
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(_random.Next(range));
        }
    }
}