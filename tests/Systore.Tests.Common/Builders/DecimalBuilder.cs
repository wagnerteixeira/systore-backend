using Bogus;

namespace Systore.Tests.Common.Factories
{
    public static class DecimalBuilder
    {
        public static decimal Positive(decimal max = decimal.MaxValue) => new Faker().Random.Decimal(0, max);

        public static decimal Negative(decimal min = decimal.MinValue) => new Faker().Random.Decimal(min, -1);
    }
}
