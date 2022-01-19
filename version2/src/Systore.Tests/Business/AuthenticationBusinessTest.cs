using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Systore.Business;
using Systore.Business.Models;
using Systore.CrossCutting.Models;
using Systore.Repositories.Interfaces;
using Systore.Tests.Builders;
using Xunit;

namespace Systore.Tests.authenticationBusiness;

public class AuthenticationBusinessTest
{
    private class AuthenticationFactory
    {
        public Mock<IUserRepository> UserRepository { get; }
        public Mock<IReleaseRepository> ReleaseRepository { get; }
        public Func<AuthenticationBusiness> Create { get; }

        public AuthenticationFactory()
        {
            var applicationConfig = Builder.ApplicationConfig with { };
            
            UserRepository = new();
            ReleaseRepository = new();
            Create = () => new(
                userRepository: UserRepository.Object,
                releaseRepository: ReleaseRepository.Object,
                applicationConfig: applicationConfig);
        }
    }

    [Fact]
    public async Task ShouldBeLogin()
    {
       //Arrange
       var factory = new AuthenticationFactory();
       factory.ReleaseRepository.Setup(m => m.VerifyRelease(It.IsAny<string>())).ReturnsAsync(true);
       factory.UserRepository.Setup(m => m.GetUserByUsernameAndPassword(It.IsAny<string>(), It.IsAny<string>()))
           .ReturnsAsync(new User()
           {
               Admin = true,
               Id = 1,
               Password = "password",
               UserName = "Username"
           });
       var authenticationBusiness = factory.Create();
       //Act
       var result = await authenticationBusiness.Login(new LoginRequestDto("Username", "password"));
       //Assert
       result.Should().NotBeNull();
       result.Relese.Should().BeTrue();
       result.Token.Should().NotBeEmpty();
       result.User.Should().NotBeNull();
       result.Valid.Should().BeTrue();
    }
    
    [Fact]
    public async Task ShouldNotBeLoginWithNotRelease()
    {
        //Arrange
        var factory = new AuthenticationFactory();
        factory.ReleaseRepository.Setup(m => m.VerifyRelease(It.IsAny<string>())).ReturnsAsync(false);
        var authenticationBusiness = factory.Create();
        //Act
        var result = await authenticationBusiness.Login(new LoginRequestDto("Username", "password"));
        //Assert
        result.Should().NotBeNull();
        result.Relese.Should().BeFalse();
        result.Token.Should().BeEmpty();
        result.User.Should().BeNull();
        result.Valid.Should().BeFalse();
    }
    
    [Fact]
    public async Task ShouldNotBeLoginWithNullUser()
    {
        //Arrange
        var factory = new AuthenticationFactory();
        factory.ReleaseRepository.Setup(m => m.VerifyRelease(It.IsAny<string>())).ReturnsAsync(true);
        factory.UserRepository.Setup(m => m.GetUserByUsernameAndPassword(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((User)null!);
        var authenticationBusiness = factory.Create();
        //Act
        var result = await authenticationBusiness.Login(new LoginRequestDto("Username", "password"));
        //Assert
        result.Should().NotBeNull();
        result.Relese.Should().BeTrue();
        result.Token.Should().BeEmpty();
        result.User.Should().BeNull();
        result.Valid.Should().BeFalse();
    }
}