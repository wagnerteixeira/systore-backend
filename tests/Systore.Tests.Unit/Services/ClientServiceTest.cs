using FluentAssertions;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Systore.Data.Abstractions;
using Systore.Domain.Entities;
using Systore.Services;
using Xunit;

namespace Systore.Tests.Unit.Services
{
    public class ClientServiceTest
    {
        [Fact]
        public async Task Should_ExistBillReceiveForClientReturnsTrue()
        {
            #region arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var billReceiveRepositoryMock = new Mock<IBillReceiveRepository>();

            Expression<Func<BillReceive, bool>> expression = obj => true;
            var returnValue = 1;
            billReceiveRepositoryMock.Setup(m => m.CountWhereAsync(It.IsAny<Expression<Func<BillReceive, bool>>>())).ReturnsAsync(returnValue);

            var service = new ClientService(clientRepositoryMock.Object, billReceiveRepositoryMock.Object);
            #endregion

            #region act
            var result = await service.ExistBillReceiveForClient(1);
            #endregion

            #region assert
            result.Should().Be(true);
            billReceiveRepositoryMock.Verify(m => m.CountWhereAsync(It.IsAny<Expression<Func<BillReceive, bool>>>()), Times.Exactly(1));
            #endregion
        }

        [Fact]
        public async Task Should_ExistBillReceiveForClientReturnsFalse()
        {
            #region arrange
            var clientRepositoryMock = new Mock<IClientRepository>();
            var billReceiveRepositoryMock = new Mock<IBillReceiveRepository>();

            Expression<Func<BillReceive, bool>> expression = obj => true;
            var returnValue = 0;
            billReceiveRepositoryMock.Setup(m => m.CountWhereAsync(It.IsAny<Expression<Func<BillReceive, bool>>>())).ReturnsAsync(returnValue);

            var service = new ClientService(clientRepositoryMock.Object, billReceiveRepositoryMock.Object);
            #endregion

            #region act
            var result = await service.ExistBillReceiveForClient(1);
            #endregion

            #region assert
            result.Should().Be(false);
            billReceiveRepositoryMock.Verify(m => m.CountWhereAsync(It.IsAny<Expression<Func<BillReceive, bool>>>()), Times.Exactly(1));
            #endregion
        }
    }

}
