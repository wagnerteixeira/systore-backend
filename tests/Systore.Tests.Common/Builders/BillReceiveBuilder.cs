using Bogus;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Systore.Domain.Entities;
using Systore.Domain.Enums;

namespace Systore.Tests.Common.Factories
{
    public class BillReceiveBuilder
    {
        private readonly BillReceive _instance;

        public BillReceiveBuilder()
        {
            _instance = GetFaker().Generate(); 
        }
        private Faker<BillReceive> GetFaker()
        {
            return new Faker<BillReceive>()
                .RuleFor(c => c.Client, f => new ClientBuilder().Build())
                .RuleFor(c => c.Code, f => IntBuilder.Positive())
                .RuleFor(c => c.Interest, f => DecimalBuilder.Positive())
                .RuleFor(c => c.OriginalValue, f => DecimalBuilder.Positive())
                .RuleFor(c => c.PayDate, f => f.Date.Recent(1))
                .RuleFor(c => c.PurchaseDate, f => f.Date.Recent(1))
                .RuleFor(c => c.Quota, IntBuilder.Positive(100))
                .RuleFor(c => c.Situation, f => f.PickRandom<BillReceiveSituation>())
                .RuleFor(c => c.Vendor, f => f.Person.FirstName)
                .RuleFor(c => c.DueDate, f => f.Date.Soon(f.Random.Int(15, 60)));

        }     
        
        public BillReceive Build() => _instance;
        

        public List<BillReceive> BuildList(int count) => GetFaker().GenerateLazy(count).ToList();
        


        public BillReceiveBuilder WithBillReceiveSituation(BillReceiveSituation billReceiveSituation)
        {
            _instance.Situation = billReceiveSituation;
            return this;
        }

        public BillReceiveBuilder WithInterest(decimal interest)
        {
            _instance.Interest = interest;
            return this;
        }       
    }
}
