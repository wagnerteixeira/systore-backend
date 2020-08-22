using Bogus;
using System;
using System.Linq;
using Systore.Domain.Dtos;

namespace Systore.Tests.Common.Factories
{
    public class CreateBillReceivesDtoBuilder
    {
        private readonly CreateBillReceivesDto _instance;

        public CreateBillReceivesDtoBuilder(int quotas)
        {
            _instance = GetFaker(quotas).Generate();
        }

        private Faker<CreateBillReceivesDto> GetFaker(int quotas)
        {
            return new Faker<CreateBillReceivesDto>()
                .RuleFor(c => c.ClientId, 1)
                .RuleFor(c => c.PurchaseDate, DateTime.UtcNow)
                .RuleFor(c => c.Quotas, quotas)
                .RuleFor(c => c.Vendor, f => f.Person.FirstName)
                .RuleFor(c => c.OriginalValue, DecimalBuilder.Positive(1000))
                .RuleFor(c => c.BillReceives, new BillReceiveBuilder().BuildList(quotas).ToList());
        }

        public CreateBillReceivesDto Build() => _instance;

    }
}
