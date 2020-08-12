using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Systore.Data.Abstractions;
using Systore.Services;
using Systore.Tests.Common.Builders;
using Systore.Tests.Common.Factories;
using Xunit;

namespace Systore.Tests.Unit.Services
{

    public class TestableService : BaseService<object> 
    { 
        public TestableService(IBaseRepository<object> repository) : base(repository)
        {

        }
    }
    public class BaseServiceTest
    {
        [Fact]
        public async Task Should_AddAsync()
        {
            #region arrange
            var repositoryMock = new Mock<IBaseRepository<object>>();           
            repositoryMock.Setup(m => m.AddAsync(null)).ReturnsAsync("");

            var service = new TestableService(repositoryMock.Object);
            #endregion

            #region act
            var result = await service.AddAsync(null);
            #endregion

            #region assert
            result.Should().Be("");
            repositoryMock.Verify(m => m.AddAsync(null), Times.Exactly(1));
            #endregion
        }       

        [Fact]
        public async Task Should_GetAsync()
        {
            #region arrange
            var repositoryMock = new Mock<IBaseRepository<object>>();
            repositoryMock.Setup(m => m.GetAsync(0)).ReturnsAsync(0);

            var service = new TestableService(repositoryMock.Object);
            #endregion

            #region act
            var result = await service.GetAsync(0);
            #endregion

            #region assert
            result.Should().Be(0);
            repositoryMock.Verify(m => m.GetAsync(0), Times.Exactly(1));
            #endregion
        }

        [Fact]
        public void Should_GetAll()
        {
            #region arrange
            var repositoryMock = new Mock<IBaseRepository<object>>();
            var returnValue = Enumerable.Empty<object>().AsQueryable();
            repositoryMock.Setup(m => m.GetAll()).Returns(returnValue);

            var service = new TestableService(repositoryMock.Object);
            #endregion

            #region act
            var result = service.GetAll();
            #endregion

            #region assert
            result.Should().BeEquivalentTo(returnValue);
            repositoryMock.Verify(m => m.GetAll(), Times.Exactly(1));
            #endregion
        }

        [Fact]
        public async Task Should_GetWhereAsync_With_Expression()
        {
            #region arrange
            var repositoryMock = new Mock<IBaseRepository<object>>();
            var filterPaginate = new FilterPaginateDtoBuilder().Build();
            var returnValue = Enumerable.Empty<object>().ToList();
            repositoryMock.Setup(m => m.GetWhereAsync(filterPaginate)).ReturnsAsync(returnValue);

            var service = new TestableService(repositoryMock.Object);
            #endregion

            #region act
            var result = await service.GetWhereAsync(filterPaginate);
            #endregion

            #region assert
            result.Should().BeEquivalentTo(returnValue);
            repositoryMock.Verify(m => m.GetWhereAsync(filterPaginate), Times.Exactly(1));
            #endregion
        }

        [Fact]
        public async Task Should_GetWhereAsync_With_FilterPaginate()
        {
            #region arrange
            #endregion

            #region act
            #endregion

            #region assert
            #endregion
        }

        [Fact]
        public async Task Should_FirstOrDefaultAsync()
        {
            #region arrange
            #endregion

            #region act
            #endregion

            #region assert
            #endregion
        }

        [Fact]
        public async Task Should_CountAllAsync()
        {
            #region arrange
            #endregion

            #region act
            #endregion

            #region assert
            #endregion
        }

        [Fact]
        public async Task Should_CountWhereAsync_With_Expression()
        {
            #region arrange
            #endregion

            #region act
            #endregion

            #region assert
            #endregion
        }

        [Fact]
        public async Task Should_CountWhereAsync_With_Filter()
        {
            #region arrange
            #endregion

            #region act
            #endregion

            #region assert
            #endregion
        }

        [Fact]
        public async Task Should_UpdateAsync()
        {
            #region arrange
            #endregion

            #region act
            #endregion

            #region assert
            #endregion
        }

        [Fact]
        public async Task Should_RemoveAsync()
        {
            #region arrange
            #endregion

            #region act
            #endregion

            #region assert
            #endregion
        }
    }
}
