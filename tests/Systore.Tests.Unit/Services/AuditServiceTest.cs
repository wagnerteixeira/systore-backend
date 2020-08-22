
using FluentAssertions;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Systore.Data.Abstractions;
using Systore.Domain.Dtos;
using Systore.Services;
using Systore.Tests.Common.Builders;
using Xunit;

namespace Systore.Tests.Unit.Services
{
    public class AuditServiceTest 
    {
        [Fact]
        public async Task Should_GetAuditsByDateAsyncSucess()
        {
            #region arrange         
            var headerAuditRepositoryMock = new Mock<IHeaderAuditRepository>();
            var returnList = new GenerictBuilder<AuditDto>().BuildListAuto(3).ToList();
            headerAuditRepositoryMock.Setup(s => s.GetAuditsByDateAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>())).ReturnsAsync(returnList);
            var auditService = new AuditService(headerAuditRepositoryMock.Object);
            #endregion
            #region act
            var result = await auditService.GetAuditsByDateAsync(DateTime.Now, DateTime.Now);
            #endregion
            #region assert
            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(returnList);
            headerAuditRepositoryMock.Verify(v => v.GetAuditsByDateAsync(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Exactly(1));
            #endregion
        }


    }
}
