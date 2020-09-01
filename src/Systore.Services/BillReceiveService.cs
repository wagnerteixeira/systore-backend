using System;
using Systore.Domain.Abstractions;
using Systore.Domain.Entities;
using Systore.Data.Abstractions;
using System.Threading.Tasks;
using System.Collections.Generic;
using Systore.Domain.Enums;
using Systore.Domain.Dtos;
using System.Linq;

namespace Systore.Services
{
    public class BillReceiveService : BaseService<BillReceive>, IBillReceiveService
    {
        private readonly ICalculateValuesService _calculateValuesService;
        public BillReceiveService(IBillReceiveRepository repository, ICalculateValuesService calculateValuesService) : base(repository)
        {
            _calculateValuesService = calculateValuesService;
        }

        public async Task<List<BillReceive>> GetBillReceivesByClient(int ClientId)
        {
            var billReceives = await (_repository as IBillReceiveRepository).GetBillReceivesByClient(ClientId);
            return billReceives.Select(c =>
            {
                _calculateValuesService.CalculateValues(c);
                return c;
            }).ToList();            
        }

        public async Task<List<BillReceive>> GetBillReceivesByClient(int ClientId)
        {
            var billReceives = await (_repository as IBillReceiveRepository)
                        .GetBillReceivesByClient(ClientId);
            billReceives.ForEach(b =>
            {
                b.NumberOfQuotas = billReceives.Where(c => c.Code == b.Code).Count();
            });

            return billReceives;
        }

        public Task<List<BillReceive>> GetPaidBillReceivesByClient(int ClientId) =>
          (_repository as IBillReceiveRepository)
            .GetPaidBillReceivesByClient(ClientId);
        public Task<List<BillReceive>> GetNoPaidBillReceivesByClient(int ClientId) =>
          (_repository as IBillReceiveRepository)
            .GetNoPaidBillReceivesByClient(ClientId);
        public Task<int> NextCode() =>
          (_repository as IBillReceiveRepository)
          .NextCode();

        public async Task<List<BillReceive>> CreateBillReceives(CreateBillReceivesDto createBillReceivesDto)
        {
            decimal sumOriginalValue = 0;
            string errors = "";
            if (createBillReceivesDto.PurchaseDate < new DateTime(1900, 01, 01))
                errors = $"A data da venda não pode ser inferior a 01/01/1900";

            foreach (var billReceive in createBillReceivesDto.BillReceives)
            {
                sumOriginalValue += billReceive.OriginalValue;

                if (billReceive.DueDate < createBillReceivesDto.PurchaseDate)
                {
                    if (errors != "")
                        errors += $"|A data do vencimento {billReceive.DueDate.ToString("dd/MM/yyyy")} da parcela {billReceive.Quota} é menor que a data da compra {createBillReceivesDto.PurchaseDate.ToString("dd/MM/yyyy")}";
                    else
                        errors = $"A data do vencimento {billReceive.DueDate.ToString("dd/MM/yyyy")} da parcela {billReceive.Quota} é menor que a data da compra {createBillReceivesDto.PurchaseDate.ToString("dd/MM/yyyy")}";
                }
            }

            if (sumOriginalValue != createBillReceivesDto.OriginalValue)
            {
                if (errors != "")
                    errors += $"|A soma das parcelas (R$ {sumOriginalValue}) difere do valor do título (R$ {createBillReceivesDto.OriginalValue})";
                else
                    errors = $"A soma das parcelas (R$ {sumOriginalValue}) difere do valor do título (R$ {createBillReceivesDto.OriginalValue})";
            }

            if (errors != "")
                throw new NotSupportedException(errors);

            int nextCode = await (_repository as IBillReceiveRepository).NextCode();

            foreach (var billReceive in createBillReceivesDto.BillReceives)
            {
                billReceive.ClientId = createBillReceivesDto.ClientId;
                billReceive.PurchaseDate = createBillReceivesDto.PurchaseDate;
                billReceive.Code = nextCode;
                billReceive.Vendor = createBillReceivesDto.Vendor;
                var ret = await (_repository as IBillReceiveRepository).AddAsync(billReceive);
                if (!string.IsNullOrWhiteSpace(ret))
                    throw new NotSupportedException(ret);
            }

            return createBillReceivesDto.BillReceives;
        }

        public Task RemoveBillReceivesByCode(int Code) => (_repository as IBillReceiveRepository).RemoveBillReceivesByCode(Code);



    }
}
