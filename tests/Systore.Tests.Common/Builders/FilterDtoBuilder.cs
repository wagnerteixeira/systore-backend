using System;
using System.Collections.Generic;
using System.Text;
using Systore.Domain.Dtos;

namespace Systore.Tests.Common.Builders
{
    public class FilterDtoBuilder
    {
        private readonly FilterDto _instance;
        public FilterDtoBuilder()
        {
            _instance = new FilterDto()
            {
                Operation = Domain.Enums.Operation.Eq,
                PropertyName = "id",
                Value = ""
            };
        }

        public FilterDto Build() => _instance;
        
    }
}
