using System;
using System.Threading.Tasks;
using AutoBogus;
using AutoMapper;
using FluentAssertions;
using Moq;
using Xunit;
using Systore.Domain.Abstractions;
using Systore.Domain.Dtos;
using Systore.Services;
using Systore.Data.Abstractions;
using Systore.Domain.Entities;
using Systore.Tests.Common.Factories;
using System.Collections.Generic;
using System.Text;

namespace Systore.Tests.Unit.Services
{
    public class BillReceiveServiceTest : IDisposable
    {
        private readonly Mock<IBillReceiveRepository> _billReceiveRepositoryMock;
        private readonly IBillReceiveService _billReceiveService;
        private readonly Mock<IMapper> _mapperMock;

        public BillReceiveServiceTest()
        {
            _billReceiveRepositoryMock = new Mock<IBillReceiveRepository>();
            _billReceiveRepositoryMock.Setup(x => x.NextCode()).ReturnsAsync(1);
            _billReceiveRepositoryMock.Setup(x => x.AddAsync(It.IsAny<BillReceive>())).ReturnsAsync("");
            _billReceiveService = new BillReceiveService(_billReceiveRepositoryMock.Object, new CalculateValuesClothingStoreService());
            _mapperMock = new Mock<IMapper>();
        }

        public void Dispose()
        {
            _mapperMock.Reset();
            _billReceiveRepositoryMock.Reset();
        }

        [Fact]
        public async Task Should_Create_Valid_Bill_Receive()
        {
            // Arrange            _
            var billReceivesDto = CreateBillReceivesDtoFactory.CreateDefault(3);
            billReceivesDto.BillReceives[0].Situation = Domain.Enums.BillReceiveSituation.Open;
            billReceivesDto.BillReceives[0].Interest = 0;
            billReceivesDto.BillReceives[0].OriginalValue = 36.0M;
            billReceivesDto.BillReceives[0].FinalValue = 36.0M;
            billReceivesDto.BillReceives[0].DaysDelay = 0;

            billReceivesDto.BillReceives[1].Situation = Domain.Enums.BillReceiveSituation.Open;
            billReceivesDto.BillReceives[1].Interest = 0;
            billReceivesDto.BillReceives[1].OriginalValue = 25.0M;
            billReceivesDto.BillReceives[1].FinalValue = 25.0M;
            billReceivesDto.BillReceives[1].DaysDelay = 0;

            billReceivesDto.BillReceives[2].Situation = Domain.Enums.BillReceiveSituation.Open;
            billReceivesDto.BillReceives[2].Interest = 0;
            billReceivesDto.BillReceives[2].OriginalValue = 49.0M;
            billReceivesDto.BillReceives[2].FinalValue = 49.0M;
            billReceivesDto.BillReceives[2].DaysDelay = 0;

            billReceivesDto.OriginalValue = 110.0M;
            billReceivesDto.Quotas = 3;
            List<BillReceive> billReceives = null;

            // Act
            var exception = await Record.ExceptionAsync(async () => billReceives = await _billReceiveService.CreateBillReceives(billReceivesDto));

            // Assert 
            Assert.Null(exception);
            Assert.Equal(billReceivesDto.BillReceives, billReceives);
            _billReceiveRepositoryMock.Verify(v => v.NextCode(), Times.Exactly(1));
            _billReceiveRepositoryMock.Verify(v => v.AddAsync(It.IsAny<BillReceive>()), Times.Exactly(3));
        }

        [Fact]
        public async Task Should_Throw_Exception_By_Purchase_Less_Than_01_01_1900()
        {
            // Arrange
            var billReceivesDto = CreateBillReceivesDtoFactory.CreateDefault(3);
            billReceivesDto.BillReceives[0].Situation = Domain.Enums.BillReceiveSituation.Open;
            billReceivesDto.BillReceives[0].Interest = 0;
            billReceivesDto.BillReceives[0].OriginalValue = 36.0M;
            billReceivesDto.BillReceives[0].FinalValue = 36.0M;
            billReceivesDto.BillReceives[0].DaysDelay = 0;

            billReceivesDto.BillReceives[1].Situation = Domain.Enums.BillReceiveSituation.Open;
            billReceivesDto.BillReceives[1].Interest = 0;
            billReceivesDto.BillReceives[1].OriginalValue = 25.0M;
            billReceivesDto.BillReceives[1].FinalValue = 25.0M;
            billReceivesDto.BillReceives[1].DaysDelay = 0;

            billReceivesDto.BillReceives[2].Situation = Domain.Enums.BillReceiveSituation.Open;
            billReceivesDto.BillReceives[2].Interest = 0;
            billReceivesDto.BillReceives[2].OriginalValue = 49.0M;
            billReceivesDto.BillReceives[2].FinalValue = 49.0M;
            billReceivesDto.BillReceives[2].DaysDelay = 0;

            billReceivesDto.PurchaseDate = new DateTime(1900, 01, 01).AddDays(-1);

            billReceivesDto.OriginalValue = 110.0M;
            billReceivesDto.Quotas = 3;
            List<BillReceive> billReceives = null;

            // Act
            var exception = await Record.ExceptionAsync(async () => billReceives = await _billReceiveService.CreateBillReceives(billReceivesDto));

            // Assert 
            Assert.NotNull(exception);
            var expect = Encoding.Convert(Encoding.UTF8, Encoding.Default, Encoding.UTF8.GetBytes("A data da venda não pode ser inferior a 01/01/1900"));
            Assert.Contains(Encoding.Default.GetString(expect), exception.Message);
            _billReceiveRepositoryMock.Verify(v => v.NextCode(), Times.Exactly(1));
            _billReceiveRepositoryMock.Verify(v => v.AddAsync(It.IsAny<BillReceive>()), Times.Exactly(0));
        }

        [Fact]
        public async Task Should_Throw_Exception_By_DueDate_Less_Than_PurchaseDate()
        {
            // Arrange
            var billReceivesDto = CreateBillReceivesDtoFactory.CreateDefault(3);
            billReceivesDto.BillReceives[0].Situation = Domain.Enums.BillReceiveSituation.Open;
            billReceivesDto.BillReceives[0].Interest = 0;
            billReceivesDto.BillReceives[0].OriginalValue = 36.0M;
            billReceivesDto.BillReceives[0].FinalValue = 36.0M;
            billReceivesDto.BillReceives[0].DaysDelay = 0;
            billReceivesDto.BillReceives[0].DueDate = DateTime.UtcNow.AddDays(-1);

            billReceivesDto.BillReceives[1].Situation = Domain.Enums.BillReceiveSituation.Open;
            billReceivesDto.BillReceives[1].Interest = 0;
            billReceivesDto.BillReceives[1].OriginalValue = 25.0M;
            billReceivesDto.BillReceives[1].FinalValue = 25.0M;
            billReceivesDto.BillReceives[1].DaysDelay = 0;
            billReceivesDto.BillReceives[1].DueDate = DateTime.UtcNow.AddDays(-2);

            billReceivesDto.BillReceives[2].Situation = Domain.Enums.BillReceiveSituation.Open;
            billReceivesDto.BillReceives[2].Interest = 0;
            billReceivesDto.BillReceives[2].OriginalValue = 49.0M;
            billReceivesDto.BillReceives[2].FinalValue = 49.0M;
            billReceivesDto.BillReceives[2].DaysDelay = 0;
            billReceivesDto.BillReceives[2].DueDate = DateTime.UtcNow.AddDays(-3);

            billReceivesDto.OriginalValue = 110.0M;
            billReceivesDto.Quotas = 3;
            List<BillReceive> billReceives = null;

            // Act
            var exception = await Record.ExceptionAsync(async () => billReceives = await _billReceiveService.CreateBillReceives(billReceivesDto));

            // Assert 
            Assert.NotNull(exception);
            Assert.StartsWith("A data do vencimento ", exception.Message);
            Assert.Contains("|A data do vencimento", exception.Message);
            Assert.Contains("é menor que a data da compra", exception.Message);
            _billReceiveRepositoryMock.Verify(v => v.NextCode(), Times.Exactly(0));
            _billReceiveRepositoryMock.Verify(v => v.AddAsync(It.IsAny<BillReceive>()), Times.Exactly(0));
        }


        [Fact]
        public async Task Should_Throw_Exception_By_Sum_Of_OriginalValue_Different_Of_Total_OrignalValue()
        {
            // Arrange
            var billReceivesDto = CreateBillReceivesDtoFactory.CreateDefault(3);
            billReceivesDto.BillReceives[0].Situation = Domain.Enums.BillReceiveSituation.Open;
            billReceivesDto.BillReceives[0].Interest = 0;
            billReceivesDto.BillReceives[0].OriginalValue = 36.0M;
            billReceivesDto.BillReceives[0].FinalValue = 36.0M;
            billReceivesDto.BillReceives[0].DaysDelay = 0;

            billReceivesDto.BillReceives[1].Situation = Domain.Enums.BillReceiveSituation.Open;
            billReceivesDto.BillReceives[1].Interest = 0;
            billReceivesDto.BillReceives[1].OriginalValue = 25.0M;
            billReceivesDto.BillReceives[1].FinalValue = 25.0M;
            billReceivesDto.BillReceives[1].DaysDelay = 0;

            billReceivesDto.BillReceives[2].Situation = Domain.Enums.BillReceiveSituation.Open;
            billReceivesDto.BillReceives[2].Interest = 0;
            billReceivesDto.BillReceives[2].OriginalValue = 49.0M;
            billReceivesDto.BillReceives[2].FinalValue = 49.0M;
            billReceivesDto.BillReceives[2].DaysDelay = 0;

            billReceivesDto.OriginalValue = 250M;
            billReceivesDto.Quotas = 3;
            List<BillReceive> billReceives = null;

            // Act
            var exception = await Record.ExceptionAsync(async () => billReceives = await _billReceiveService.CreateBillReceives(billReceivesDto));

            // Assert 
            Assert.NotNull(exception);
            Assert.Contains("A soma das parcelas (R$", exception.Message);
            Assert.Contains("difere do valor do título (R$", exception.Message);
            _billReceiveRepositoryMock.Verify(v => v.NextCode(), Times.Exactly(0));
            _billReceiveRepositoryMock.Verify(v => v.AddAsync(It.IsAny<BillReceive>()), Times.Exactly(0));
        }
    }
}