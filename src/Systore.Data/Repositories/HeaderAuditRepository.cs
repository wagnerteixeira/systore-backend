using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Systore.Data.Abstractions;
using Systore.Domain;
using Systore.Domain.Dtos;
using Systore.Domain.Entities;
using Systore.Domain.Enums;
using Systore.Infra.Context;

namespace Systore.Data.Repositories
{
    public class HeaderAuditRepository : IHeaderAuditRepository
    {
        private readonly AuditContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DbSet<HeaderAudit> _entities;
        public HeaderAuditRepository(
            IOptions<AppSettings> options,
            IHttpContextAccessor httpContextAccessor,
            AuditContext context)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _entities = _context.Set<HeaderAudit>();
        }

        public async Task<string> AddAsync(HeaderAudit entity)
        {
            var clainUserName = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);
            if (clainUserName != null)
                entity.UserName = clainUserName.Value;
            else
                entity.UserName = "User not identified";
            await _entities.AddAsync(entity);
            return await SaveChangesAsync();
        }

        private string GetOperationString(AuditOperation auditOperation)
        {
            switch (auditOperation)
            {
                case AuditOperation.Add:
                    return "Cria��o";
                case AuditOperation.Remove:
                    return "Exclus�o";
                case AuditOperation.Update:
                    return "Altera��o";
                default:
                    return "";
            }
        }

        public async Task<List<AuditDto>> GetAuditsByDateAsync(DateTime initialDate, DateTime finalDate)
        {
            var ret = await _entities.
                    Include(c => c.ItemAudits).
                    Where(c => c.Date >= initialDate && c.Date <= finalDate)
                    .SelectMany(c =>
                        c.ItemAudits.Select(
                            i => new AuditDto()
                            {
                                Date = c.Date,
                                FieldName = i.FieldName,
                                Id = c.Id,
                                NewValue = i.NewValue,
                                PrimaryKey = i.PrimaryKey,
                                Operation = GetOperationString(c.Operation),
                                TableName = c.TableName,
                                UserName = c.UserName
                            }
                        )
                      ).ToListAsync();
            return ret;
        }

        protected virtual async Task<string> SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                return "";
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.InnerException != null)
                    {
                        Console.WriteLine(e.InnerException.InnerException.Message);
                        return e.InnerException.InnerException.Message;
                    }
                    else
                    {
                        Console.WriteLine(e.InnerException.Message);
                        return e.InnerException.Message;
                    }
                }
                else
                {
                    Console.WriteLine(e.Message);
                    return e.Message;
                }
            }
        }

    }
}
