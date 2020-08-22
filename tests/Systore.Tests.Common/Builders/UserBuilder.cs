using Bogus;
using Systore.Domain.Entities;
using Systore.Tests.Common.Factories;

namespace Systore.Tests.Common.Builders
{
    public class UserBuilder
    {
        private readonly User _instance;

        public UserBuilder()
        {
            _instance = GetFaker().Generate();
        }


        private Faker<User> GetFaker()
        {
            return new Faker<User>("pt_BR")
                .RuleFor(c => c.Id, IntBuilder.Positive())
                .RuleFor(c => c.Admin, f => f.Random.Bool())
                .RuleFor(c => c.Password, f => f.Internet.Password())
                .RuleFor(c => c.UserName, f => f.Internet.UserName());                
        }

        public User Build() => _instance;

        public UserBuilder WithAdmin(bool admin)
        {
            _instance.Admin = admin;
            return this;
        }

        public UserBuilder WithUserName(string userName)
        {
            _instance.UserName = userName;
            return this;
        }

        public UserBuilder WithPassword(string password)
        {
            _instance.Password = password;
            return this;
        }

    }
}
