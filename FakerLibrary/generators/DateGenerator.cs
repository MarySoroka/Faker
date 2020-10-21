using System;
using FakerLibrary.faker;

namespace FakerLibrary.generators
{
    public class DateGenerator : Generator<DateTime>
    {
        protected override DateTime Generate(FakerContext context)
        {
            DateTime start = new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(context.Random.Next(range));        }
    }
}