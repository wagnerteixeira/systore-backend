using Bogus;
using Systore.Domain.Dtos;

namespace Systore.Tests.Common.Builders
{
    public class FilterPaginateDtoBuilder
    {
        private readonly FilterPaginateDto _instance;
        public FilterPaginateDtoBuilder()
        {
            _instance = GetFaker().Generate();
        }

        private Faker<FilterPaginateDto> GetFaker()
        {
            return new Faker<FilterPaginateDto>()
                .RuleFor(o => o.Limit, 10)
                .RuleFor(o => o.Order, Domain.Enums.Order.Asc)
                .RuleFor(o => o.Skip, 0)
                .RuleFor(o => o.SortPropertyName, "id")
                .RuleFor(o => o.filters, new FilterDtoBuilder().BuildList(3));

        }

        public FilterPaginateDto Build() => _instance;
    }
}
