using Microsoft.AspNetCore.Mvc;
using Systore.Domain.Entities;
using Systore.Domain.Abstractions;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Systore.Domain.Dtos;
using AutoMapper;
using System.Linq;

namespace Systore.Api.Controllers
{
    [Route("api/[controller]")]
    public class SaleController : BaseController<Sale>
    {
        public SaleController(ISaleService Service, ILogger<SaleController> logger)
            : base(Service, logger)
        {
            
        }

        [Authorize]
        [HttpGet("GetSaleFullById/{id:int}")]
        public async Task<IActionResult> GetSaleFullById(int id)
        {            
            try
            {
                return Ok(await (_service as ISaleService).GetSaleFullById(id));
            }
            catch (Exception e)
            {
                return SendBadRequest(e);
            }
        }

        [Authorize]
        [HttpPost("post-dto")]
        // POST: api/Entity
        public async Task<IActionResult> Post([FromBody]SaleDto entity)
        {
            try
            {
                Sale sale = new Sale()
                {
                    ClientId = entity.ClientId,
                    FinalValue = entity.FinalValue,
                    Id = entity.Id,
                    SaleDate = entity.SaleDate,
                    Vendor = entity.Vendor
                };

                sale.ItemSale = entity.ItemSale.Select(c => new ItemSale()
                {
                    Id = c.Id,
                    Price = c.Price,
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    SaleId = c.SaleId,
                    TotalPrice = c.TotalPrice
                }).ToList();
                string ret = await _service.AddAsync(sale);
                var objectId = new { id = GetEntityId(sale) };
                if (string.IsNullOrWhiteSpace(ret))
                    return CreatedAtAction(nameof(Get), objectId, objectId);
                else
                    return SendBadRequest(ret.Split('|', StringSplitOptions.RemoveEmptyEntries));
            }
            catch (Exception e)
            {
                return SendBadRequest(e);
            }
        }

        [Authorize]
        [HttpPut("put-dto")]
        // PUT: api/Entity
        public async virtual Task<IActionResult> Put([FromBody]SaleDto entity)
        {
            try
            {                
                string ret = await (_service as ISaleService).UpdateAsync(entity);
                if (string.IsNullOrWhiteSpace(ret))
                    return Ok(entity);
                else
                    return SendBadRequest(ret.Split('|', StringSplitOptions.RemoveEmptyEntries));
            }
            catch (Exception e)
            {
                return SendBadRequest(e);
            }
        }


    }
}