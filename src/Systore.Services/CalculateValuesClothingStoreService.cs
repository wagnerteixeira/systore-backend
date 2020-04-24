using System;
using Systore.Domain.Abstractions;
using Systore.Domain.Entities;
using Systore.Domain.Enums;

namespace Systore.Services
{
    public class CalculateValuesClothingStoreService : ICalculateValuesClothingStoreService
    {
        private static decimal _interestTax = 0.0023333333333333333333333333M; //(0.07M / 30.0M)

        public void CalculateValues(BillReceive billReceive)
        {
            if (billReceive.Situation == BillReceiveSituation.Open)
            {
                var days = (DateTime.UtcNow.Date - billReceive.DueDate.Date).Days;
                if ((days > 5))
                {
                    billReceive.DaysDelay = days;
                    var interestPerDay = _interestTax * billReceive.DaysDelay;
                    billReceive.Interest = decimal.Round(billReceive.OriginalValue * interestPerDay, 2);
                    billReceive.FinalValue = billReceive.OriginalValue + billReceive.Interest;
                }
                else
                {
                    billReceive.Interest = 0;
                    billReceive.DaysDelay = 0;
                    billReceive.FinalValue = billReceive.OriginalValue;
                }
            }
        }
    }
}
