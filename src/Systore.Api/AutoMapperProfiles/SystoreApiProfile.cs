using AutoMapper;
using Systore.Domain.Dtos;
using Systore.Domain.Entities;

namespace Systore.Api.AutoMapperProfiles
{
    public class SystoreApiProfile : Profile
    {
        public SystoreApiProfile()
        {
            CreateMap<SaleDto, Sale>();
            CreateMap<Sale, SaleDto>();
            CreateMap<ItemSaleDto, ItemSale>();
            CreateMap<ItemSale, ItemSaleDto>();
        }
    }
}
