using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Systore.Services;
using Systore.Tests.Common.Factories;
using Systore.Domain.Enums;
using Systore.Domain.Abstractions;

namespace Systore.Tests.Unit.Services
{
    public class CalculateValuesClothingStoreServiceTest : IDisposable
    {
        private readonly ICalculateValuesClothingStoreService _calculateValuesClothingStoreService;
        public CalculateValuesClothingStoreServiceTest()
        {
            _calculateValuesClothingStoreService = new CalculateValuesClothingStoreService();
        }       

        [Theory]
        [MemberData(nameof(Data))]
        public void Should_Calculate_Values(
            BillReceiveSituation billReceiveSituation,
            decimal originalValue, 
            int daysAgo, 
            decimal interest,
            decimal finalValue,
            int expectedDaysDelay,
            decimal expectedInterest,
            decimal expectedFinalValue)
        {
            // Arrange
            var billReceive = BillReceiveFactory.CreateDefault();
            billReceive.Situation = billReceiveSituation;
            billReceive.OriginalValue = originalValue;
            billReceive.DaysDelay = daysAgo;
            billReceive.FinalValue = finalValue;
            billReceive.Interest = interest;            
            billReceive.DueDate = DateTime.UtcNow.Date.AddDays(daysAgo * -1);
            // Act
            _calculateValuesClothingStoreService.CalculateValues(billReceive);
            // Assert
            Assert.Equal(expectedDaysDelay, billReceive.DaysDelay);
            Assert.Equal(expectedInterest, billReceive.Interest);
            Assert.Equal(expectedFinalValue, billReceive.FinalValue);
        }

        public static TheoryData<BillReceiveSituation, decimal, int, decimal, decimal, int, decimal, decimal> Data =>
            new TheoryData<BillReceiveSituation, decimal, int, decimal, decimal, int, decimal, decimal>
                {
                    { BillReceiveSituation.Open, 100.0M, 2, 0.0M, 0.0M, 0, 0.0M, 100.0M }, //should calculate no paid 2 days, no interest
                    { BillReceiveSituation.Open, 100.0M, 4, 0.0M, 0.0M, 0, 0.0M, 100.0M }, //should calculate no paid 4 days, no interest
                    { BillReceiveSituation.Open, 100.0M, 6, 0.0M, 0.0M, 6, 1.4M, 101.4M }, //should calculate no paid 6 days, with interest
                    { BillReceiveSituation.Open, 100.0M, 11, 0.0M, 0.0M, 11, 2.57M, 102.57M }, //should calculate no paid 11 days, with interest
                    { BillReceiveSituation.Open, 248.0M, 46, 0.0M, 0.0M, 46, 26.62M, 274.62M }, //should calculate no paid 46 days, with interest, with diferent value
                    { BillReceiveSituation.Open, 100.0M, 57, 0.0M, 0.0M, 57, 13.3M, 113.3M }, //should calculate no paid 57 days, with interest
                    { BillReceiveSituation.Closed, 100M, 15, 3.5M, 103.5M, 15, 3.5M, 103.5M }, //should calculate paid 15 days, with interest
                    { BillReceiveSituation.Closed, 459.58M, 15, 16.09M, 475.67M, 15, 16.09M, 475.67M }, //should calculate paid 15 days, with interest, with diferente value
                    { BillReceiveSituation.Closed, 100M, 0, 0.0M, 100.0M, 0, 0.0M, 100.0M }, //should calculate paid, no interest
                    { BillReceiveSituation.Closed, 459.58M, 0, 0.0M, 459.58M, 0, 0.0M, 459.58M }, //should calculate paid 15 days, with interest, with diferente value
                                       
                };
      
        public void Dispose()
        {
         
        }
    }
}

