﻿using System;
using Microsoft.AspNetCore.Mvc;
using Systore.Domain.Entities;
using Systore.Domain.Enums;
using Systore.Domain.Dtos;
using Systore.Domain.Abstractions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Systore.Api.Controllers
{
    [Route("api/[controller]")]
    public class BillReceiveController : BaseController<BillReceive>
    {
        public BillReceiveController(IBillReceiveService Service, ILogger<BillReceiveController> logger)
            : base(Service, logger)
        {

        }

        [Authorize]
        [HttpGet("client/{clientId}")]
        public async Task<IActionResult> GetBillReceivesByClient(int clientId)
        {
            try
            {
                var result = await (_service as IBillReceiveService).GetBillReceivesByClient(clientId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return SendBadRequest(e);
            }
        }

        [Authorize]
        [HttpGet("client/{clientId}/paid")]                
        public async Task<IActionResult> GetPaidBillReceivesByClient(int clientId)
        {
            try
            {
                var result = await (_service as IBillReceiveService).GetPaidBillReceivesByClient(clientId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return SendBadRequest(e);
            }
        }

        [Authorize]
        [HttpGet("client/{clientId}/nopaid")]
        public async Task<IActionResult> GetNoPaidBillReceivesByClient(int clientId)
        {
            try
            {
                var result = await (_service as IBillReceiveService).GetNoPaidBillReceivesByClient(clientId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return SendBadRequest(e);
            }
        }

        [Authorize]
        [HttpGet("nextcode")]                
        public async Task<IActionResult> NextCode()
        {
            try
            {
                var result = await (_service as IBillReceiveService).NextCode();
                return Ok(result);
            }
            catch (Exception e)
            {
                return SendBadRequest(e);
            }
        }


        [Authorize]
        [HttpPost("createbillreceives")]
        public async Task<IActionResult> CreateBillReceives([FromBody]CreateBillReceivesDto createBillReceivesDto)
        {
            try
            {
                var result = await (_service as IBillReceiveService).CreateBillReceives(createBillReceivesDto);
                return Ok(result);
            }
            catch (NotSupportedException e)
            {
                return SendBadRequest(e.Message.Split('|'));
            }
            catch (Exception e)
            {
                return SendBadRequest(e);
            }
        }

        [Authorize]
        [HttpDelete("{code:int}")]                
        public override async Task<IActionResult> Delete([FromRoute]int Code)
        {
            if (await (_service as IBillReceiveService).CountWhereAsync(c => c.Code == Code) == 0)
                return SendBadRequest("Carnê não encontrado!");
            if (await (_service as IBillReceiveService).CountWhereAsync(c => c.Code == Code && c.Situation == BillReceiveSituation.Closed) > 0)
                return SendBadRequest("Carnê não pode ser excluído pois existe parcela paga!");

            await (_service as IBillReceiveService).RemoveBillReceivesByCode(Code);
            return Ok();
        }

    }


}