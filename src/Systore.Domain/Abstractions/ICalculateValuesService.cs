using System;
using System.Collections.Generic;
using System.Text;
using Systore.Domain.Entities;

namespace Systore.Domain.Abstractions
{
    public interface ICalculateValuesService
    {
        void CalculateValues(BillReceive billReceive);
    }
}
