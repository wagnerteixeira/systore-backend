using FluentAssertions;
using Moq;
using System.Threading.Tasks;
using Systore.Data.Abstractions;
using Systore.Domain.Entities;
using Systore.Services;
using Systore.Tests.Common.Builders;
using Xunit;

namespace Systore.Tests.Unit.Services
{
    public class UserServiceTest
    {
        [Fact]
        public async Task Should_AddAsync()
        {
            #region arrange
            var repositoryMock = new Mock<IUserRepository>();
            var entity = new GenerictBuilder<User>().BuildAuto();
            repositoryMock.Setup(m => m.AddAsync(entity)).ReturnsAsync("");

            var service = new UserService(repositoryMock.Object);
            #endregion

            #region act
            var result = await service.AddAsync(entity);
            #endregion

            #region assert
            result.Should().Be("");
            repositoryMock.Verify(m => m.AddAsync(entity), Times.Exactly(1));
            #endregion
        }
    }
}
