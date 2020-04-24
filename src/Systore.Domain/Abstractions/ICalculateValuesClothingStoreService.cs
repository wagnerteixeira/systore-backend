using Systore.Domain.Entities;

namespace Systore.Domain.Abstractions
{
    public interface ICalculateValuesClothingStoreService
    {
        void CalculateValues(BillReceive billReceive);
    }
}
