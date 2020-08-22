
using Bogus;
using Systore.Dtos;

namespace Systore.Tests.Common.Builders
{
    public class LoginRequestDtoBuilder
    {
        private readonly LoginRequestDto _instance;

        public LoginRequestDtoBuilder()
        {
            _instance = GetFaker().Generate();
        }


        private Faker<LoginRequestDto> GetFaker()
        {
            return new Faker<LoginRequestDto>("pt_BR")
                .RuleFor(c => c.Password, f => f.Internet.Password())
                .RuleFor(c => c.UserName, f => f.Internet.UserName());
        }

        public LoginRequestDto Build() => _instance;
        

        public LoginRequestDtoBuilder WithUserName(string userName)
        {
            _instance.UserName = userName;
            return this;
        }

        public LoginRequestDtoBuilder WithPassword(string password)
        {
            _instance.Password = password;
            return this;
        }
    }
}
