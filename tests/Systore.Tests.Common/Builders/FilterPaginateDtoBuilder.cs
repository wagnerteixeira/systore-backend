using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Systore.Domain.Dtos;

namespace Systore.Tests.Common.Builders
{
    public class FilterPaginateDtoBuilder
    {
        private readonly FilterPaginateDto _instance;
        public FilterPaginateDtoBuilder()
        {
            _instance = new FilterPaginateDto()
            {
                Limit = 10,
                Order = Domain.Enums.Order.Asc,
                Skip = 0,
                SortPropertyName = "id",
                filters = Enumerable.Empty<FilterDto>().Append(new FilterDtoBuilder().Build())
            };
        }

        public FilterPaginateDto Build() => _instance;
    }
}
