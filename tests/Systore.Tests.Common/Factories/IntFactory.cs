using System;
using System.Collections.Generic;
using System.Text;
using Bogus;

namespace Systore.Tests.Common.Factories
{
    public static class IntFactory
    {
        public static int Positive(int max = int.MaxValue)
        {
            return new Faker().Random.Int(0, max);
        }

        public static int Negative(int min = int.MinValue)
        {
            return new Faker().Random.Int(min, -1);
        }
    }
}
