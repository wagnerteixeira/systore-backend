using Bogus;
using System;
using System.Collections.Generic;
using System.Text;
using Systore.Domain.Entities;
using Systore.Domain.Enums;

namespace Systore.Tests.Common.Factories
{
    public static class BillReceiveFactory
    {
        public static Faker<BillReceive> GetFaker()
        {
            return new Faker<BillReceive>()
                .RuleFor(c => c.Client, f => ClientFactory.CreateDefault())
                .RuleFor(c => c.Code, f => IntFactory.Positive())
                .RuleFor(c => c.Interest, f => DecimalFactory.Positive())
                .RuleFor(c => c.OriginalValue, f => DecimalFactory.Positive())
                .RuleFor(c => c.PayDate, f => f.Date.Recent(1))
                .RuleFor(c => c.PurchaseDate, f => f.Date.Recent(1))
                .RuleFor(c => c.Quota, IntFactory.Positive(100))
                .RuleFor(c => c.Situation, f => f.PickRandom<BillReceiveSituation>())
                .RuleFor(c => c.Vendor, f => f.Person.FirstName)
                .RuleFor(c => c.DueDate, f => f.Date.Soon(f.Random.Int(15, 60)));

        }

        public static BillReceive CreateDefault()
        {
            return GetFaker().Generate();
        }

        public static IEnumerable<BillReceive> GenerateLazy(int count)
        {
            return GetFaker().GenerateLazy(count);
        }

        public static BillReceive CreateNoPaid()
        {
            var billreceive = GetFaker().Generate();
            billreceive.Situation = BillReceiveSituation.Open;
            return billreceive;
        }

        public static BillReceive CreatePaidWithNoInterest()
        {
            var billreceive = GetFaker().Generate();
            billreceive.Situation = BillReceiveSituation.Closed;
            billreceive.Interest = 0;
            return billreceive;
        }

        public static BillReceive CreatePaidWithInterest()
        {
            var billreceive = GetFaker().Generate();
            billreceive.Situation = BillReceiveSituation.Closed;
            return billreceive;
        }

    }
}
