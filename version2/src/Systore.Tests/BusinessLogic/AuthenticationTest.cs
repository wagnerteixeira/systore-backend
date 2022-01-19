using System;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Systore.BusinessLogic;
using Systore.BusinessLogic.Models;
using Systore.CrossCutting.Models;
using Systore.Repositories.Interfaces;
using Systore.Tests.Builders;
using Xunit;

namespace Systore.Tests.BusinessLogic;

public class AuthenticationTest
{
    private class Setup
    {
        public Mock<IUserRepository> UserRepository { get; }
        public Mock<IReleaseRepository> ReleaseRepository { get; }
        public Func<Authentication> GetObject { get; }

        public Setup()
        {
            var applicationConfig = Builder.ApplicationConfig with { };
            
            UserRepository = new();
            ReleaseRepository = new();
            GetObject = () => new(
                userRepository: UserRepository.Object,
                releaseRepository: ReleaseRepository.Object,
                applicationConfig: applicationConfig);
        }
    }

    [Fact]
    public async Task ShouldBeLogin()
    {
       //Arrange
       var setup = new Setup();
       setup.ReleaseRepository.Setup(m => m.VerifyRelease(It.IsAny<string>())).ReturnsAsync(true);
       setup.UserRepository.Setup(m => m.GetUserByUsernameAndPassword(It.IsAny<string>(), It.IsAny<string>()))
           .ReturnsAsync(new User()
           {
               Admin = true,
               Id = 1,
               Password = "password",
               UserName = "Username"
           });
       var businessLogic = setup.GetObject();
       //Act
       var result = await businessLogic.Login(new LoginRequestDto("Username", "password"));
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
        var setup = new Setup();
        setup.ReleaseRepository.Setup(m => m.VerifyRelease(It.IsAny<string>())).ReturnsAsync(false);
        var businessLogic = setup.GetObject();
        //Act
        var result = await businessLogic.Login(new LoginRequestDto("Username", "password"));
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
        var setup = new Setup();
        setup.ReleaseRepository.Setup(m => m.VerifyRelease(It.IsAny<string>())).ReturnsAsync(true);
        setup.UserRepository.Setup(m => m.GetUserByUsernameAndPassword(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync((User)null!);
        var businessLogic = setup.GetObject();
        //Act
        var result = await businessLogic.Login(new LoginRequestDto("Username", "password"));
        //Assert
        result.Should().NotBeNull();
        result.Relese.Should().BeTrue();
        result.Token.Should().BeEmpty();
        result.User.Should().BeNull();
        result.Valid.Should().BeFalse();
    }
}