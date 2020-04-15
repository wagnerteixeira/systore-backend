using System;
using System.Collections.Generic;
using System.Text;
using Bogus;

namespace Systore.Tests.Common.Factories
{
    public static class DecimalFactory
    {
        public static decimal Positive(decimal max = decimal.MaxValue)
        {
            return new Faker().Random.Decimal(0, max);
        }

        public static decimal Negative(decimal min = decimal.MinValue)
        {
            return new Faker().Random.Decimal(min, -1);
        }
    }
}
