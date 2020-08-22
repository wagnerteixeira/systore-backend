using Bogus;

namespace Systore.Tests.Common.Factories
{
    public static class IntBuilder
    {
        public static int Positive(int max = int.MaxValue) => new Faker().Random.Int(0, max);

        public static int Negative(int min = int.MinValue) => new Faker().Random.Int(min, -1);
    }
}
