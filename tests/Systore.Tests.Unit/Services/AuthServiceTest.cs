
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading.Tasks;
using Systore.Data.Abstractions;
using Systore.Domain;
using Systore.Services;
using Systore.Tests.Common.Builders;
using Xunit;

namespace Systore.Tests.Unit.Services
{
    public class AuthServiceTest
    {
        // TODO test with lifetime, teste with no admin, test with no login resturn null
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IOptions<AppSettings> _appSettingsOptions;

        public AuthServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _appSettingsOptions = Options.Create(new AppSettingsBuilder().Build());

        }

        private void ResetMocks()
        {
            _userRepositoryMock.Reset();
        }

        [Fact]
        public void Should_Validate_Admin_Token()
        {
            #region arrange
            ResetMocks();
            var authService = new AuthService(_userRepositoryMock.Object, _appSettingsOptions);
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlVzZXJOYW1lIiwiYWRtaW4iOiJUcnVlIiwibmJmIjoxNTk4MDcyNTY4LCJleHAiOjE1OTgwNzI1NzAsImlhdCI6MTU5ODA3MjU2OH0.RSU4ABW-_g3PYTPjn51aiWM1rSrm4-Cwn7d5yGmWIyk";
            #endregion

            #region act
            var result = authService.ValidateToken(token, false);
            #endregion

            #region assert
            result.Should().NotBeNull();
            result.Valid.Should().BeTrue();
            result.Token.Should().Be(token);
            result.User.UserName.Should().Be("UserName");
            result.Relese.Should().BeFalse();
            #endregion
        }


        [Fact]
        public void Should_Validate_NotAdmin_Token()
        {
            #region arrange
            ResetMocks();
            var authService = new AuthService(_userRepositoryMock.Object, _appSettingsOptions);            
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IlVzZXJOYW1lIiwiYWRtaW4iOiJUcnVlIiwibmJmIjoxNTk4MDcyNTY4LCJleHAiOjE1OTgwNzI1NzAsImlhdCI6MTU5ODA3MjU2OH0.RSU4ABW-_g3PYTPjn51aiWM1rSrm4-Cwn7d5yGmWIyk";
            #endregion

            #region act
            var result = authService.ValidateToken(token, false);
            #endregion

            #region assert
            result.Should().NotBeNull();
            result.Valid.Should().BeTrue();
            result.Token.Should().Be(token);
            result.User.UserName.Should().Be("UserName");
            result.Relese.Should().BeFalse();
            #endregion
        }


        [Fact]
        public void Should_Not_Validate_Token()
        {
            #region arrange
            ResetMocks();
            var authService = new AuthService(_userRepositoryMock.Object, _appSettingsOptions);
            var token = "eeyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
            #endregion

            #region act
            var exception = Record.Exception(() => authService.ValidateToken(token));
            #endregion

            #region assert
            exception.Should().NotBeNull();
            exception.Should().BeOfType<ArgumentException>();            
            #endregion
        }

        [Fact]
        public async Task Should_Login_Admin_User()
        {
            #region arrange
            ResetMocks();

            var user = new UserBuilder()
                .WithAdmin(true)
                .Build();

            var loginRequest = new LoginRequestDtoBuilder()
                .WithPassword(user.Password)
                .WithUserName(user.UserName)
                .Build();
            
            _userRepositoryMock.Setup(s => s.GetUserByUsernameAndPassword(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(user);
            var authService = new AuthService(_userRepositoryMock.Object, _appSettingsOptions);
            #endregion

            #region act
            var result = await authService.Login(loginRequest);
            #endregion

            #region assert
            result.Should().NotBeNull();
            result.User.UserName.Should().Be(user.UserName);
            result.User.Admin.Should().Be(user.Admin);
            #endregion
        }
    }
}
