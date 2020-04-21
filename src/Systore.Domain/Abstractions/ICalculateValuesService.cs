using Systore.Domain.Entities;

namespace Systore.Domain.Abstractions
{
    public interface ICalculateValuesService
    {
        void CalculateValues(BillReceive billReceive);
    }
}
