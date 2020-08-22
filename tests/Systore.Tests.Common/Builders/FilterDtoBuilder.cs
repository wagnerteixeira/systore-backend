using Bogus;
using System.Collections.Generic;
using Systore.Domain.Dtos;

namespace Systore.Tests.Common.Builders
{
    public class FilterDtoBuilder
    {
        private readonly FilterDto _instance;
        public FilterDtoBuilder()
        {
            _instance = GetFaker().Generate();
        }

        private Faker<FilterDto> GetFaker()
        {
            return new Faker<FilterDto>()
                .RuleFor(o => o.Operation, r => Domain.Enums.Operation.Eq)
                .RuleFor(o => o.PropertyName, "id")
                .RuleFor(o => o.Value, "");

        }

        public FilterDto Build() => _instance;

        public IEnumerable<FilterDto> BuildList(int count) => GetFaker().GenerateLazy(count);

    }
}
