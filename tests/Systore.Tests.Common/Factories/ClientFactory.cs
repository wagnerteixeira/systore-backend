using Bogus;
using Bogus.Extensions.Brazil;
using System;
using System.Collections.Generic;
using System.Text;
using Systore.Domain.Entities;
using Systore.Domain.Enums;

namespace Systore.Tests.Common.Factories
{
    public static class ClientFactory
    {
        private static Faker<Client> GetFaker()
        {
            return new Faker<Client>("pt_BR")
                .RuleFor(c=> c.Id, IntFactory.Positive())
                .RuleFor(c => c.Address, f => f.Address.StreetAddress())
                .RuleFor(c => c.AddressNumber, f => f.Address.BuildingNumber())
                .RuleFor(c => c.AdmissionDate, f => f.Date.Past(3))
                .RuleFor(c => c.City, f => f.Address.City())
                .RuleFor(c => c.CivilStatus, f => f.PickRandom<CivilStatus>())
                .RuleFor(c => c.Complement, f => f.Address.SecondaryAddress())
                .RuleFor(c => c.Cpf, f => f.Person.Cpf())
                .RuleFor(c => c.DateOfBirth, f => f.Date.Past(32))
                .RuleFor(c => c.FatherName, f => f.Person.FullName)
                .RuleFor(c => c.JobName, f => f.Company.CompanyName())
                .RuleFor(c => c.MotherName, f => f.Person.FullName)
                .RuleFor(c => c.Name, f => f.Person.FullName)
                .RuleFor(c => c.Neighborhood, f => f.Address.Direction())
                .RuleFor(c => c.Note, f => f.Random.AlphaNumeric(500))
                .RuleFor(c => c.Occupation, f => f.Random.AlphaNumeric(50))
                .RuleFor(c => c.Phone1, f => f.Person.Phone)
                .RuleFor(c => c.Phone2, f => f.Person.Phone)
                .RuleFor(c => c.PlaceOfBirth, f => f.Address.City())
                .RuleFor(c => c.PostalCode, f => f.Address.ZipCode())
                .RuleFor(c => c.RegistryDate, DateTime.Now)
                .RuleFor(c => c.Rg, f => f.Random.AlphaNumeric(20))
                .RuleFor(c => c.Sales, f => new List<Sale>())
                .RuleFor(c => c.Seller, f => f.Person.FirstName)
                .RuleFor(c => c.Spouse, f => f.Person.FullName)
                .RuleFor(c => c.State, f => f.Address.State());
        }

        public static Client CreateDefault()
        {
            return GetFaker().Generate();
        }
    }
}
