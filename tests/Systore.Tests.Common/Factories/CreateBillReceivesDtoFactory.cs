using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Systore.Domain.Dtos;
using Systore.Domain.Entities;

namespace Systore.Tests.Common.Factories
{
    public class CreateBillReceivesDtoFactory
    {
        public static Faker<CreateBillReceivesDto> GetFaker(int quotas)
        {            
          /*  DateTime purchaseDate = new Faker().Date.Recent();

            decimal originalValue = DecimalFactory.Positive(1000.0M);

            List<BillReceive> billReceives = new List<BillReceive>();

            decimal originalValueRemaning = originalValue;

            for (int i = 0; i <_quotas; i++)
            {
                var billReceive = BillReceiveFactory.CreateNoPaid();
                billReceive.Interest = 0;
                billReceive.DaysDelay = 0;

                if (i == (_quotas -1))
                    billReceive.OriginalValue = originalValueRemaning;
                else
                    billReceive.OriginalValue = DecimalFactory.Positive(originalValue - 1 - _quotas);

                billReceive.FinalValue = billReceive.OriginalValue;
                originalValueRemaning -= billReceive.OriginalValue;

                billReceives.Add(billReceive);
            }*/

            return new Faker<CreateBillReceivesDto>()
                .RuleFor(c => c.ClientId, 1)
                .RuleFor(c => c.PurchaseDate, DateTime.UtcNow)
                .RuleFor(c => c.Quotas, quotas)
                .RuleFor(c => c.Vendor, f => f.Person.FirstName)
                .RuleFor(c => c.OriginalValue, DecimalFactory.Positive(1000))
                .RuleFor(c => c.BillReceives, BillReceiveFactory.GenerateLazy(quotas).ToList());
        }

        public static CreateBillReceivesDto CreateDefault(int quotas)
        {
            return GetFaker(quotas).Generate();          
        }

    }
}
