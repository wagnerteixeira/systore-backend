using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Systore.Infra.Abstractions;
using System.Linq.Expressions;
using Systore.Data.Abstractions;
using Systore.Domain.Enums;
using Systore.Domain.Dtos;
using Systore.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Systore.Domain.Abstractions;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Systore.Data.Repositories
{
    public abstract class BaseRepository<TEntity> : IDisposable, IBaseRepository<TEntity> where TEntity : class
    {
        protected readonly IDbContext _context;
        protected readonly DbSet<TEntity> _entities;
        protected readonly IHeaderAuditRepository _headerAuditRepository;
        private bool _inTransaction;

        public bool IsConversion { get; set; }

        bool disposed = false;
        private readonly SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        protected BaseRepository(IDbContext context, IHeaderAuditRepository headerAuditRepository)
        {
            _context = context;
            _entities = _context.Instance.Set<TEntity>();
            _headerAuditRepository = headerAuditRepository;
            _inTransaction = false;
            IsConversion = false;
        }

        public virtual bool BeginTransaction()
        {
            if (_inTransaction)
                return false;
            try
            {
                _inTransaction = true;
                return true;
            }
            catch (Exception e)
            {
                throw;
            }

        }
        public virtual bool Commit()
        {
            return true;
        }

        public virtual void Rollback()
        {
            _inTransaction = false;
        }

        public virtual async Task<string> AddAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
            return await SaveChangesAsync();
        }
        public virtual async Task<TEntity> GetAsync(int id) => await _entities.FindAsync(id);
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _entities.ToListAsync();
        public virtual IQueryable<TEntity> GetAll() => _entities.Select(x => x);
        private IQueryable<TEntity> GetQueryWhere(IQueryable<TEntity> query, FilterPaginateDto filterPaginateDto)
        {
            if (filterPaginateDto.filters != null)
                return query.Where(QueryExpressionBuilder.GetExpression<TEntity>(filterPaginateDto.filters));
            else
                return query;
        }
        private IQueryable<TEntity> GetQueryPagination(IQueryable<TEntity> query, FilterPaginateDto filterPaginateDto)
        {
            return query
                .Skip(filterPaginateDto.Skip)
                .Take(filterPaginateDto.Limit); ;
        }
        // TODO: Test nullable sorts
        private IQueryable<TEntity> GetQueryOrderBy(IQueryable<TEntity> query, FilterPaginateDto filterPaginateDto)
        {            

            if (filterPaginateDto.Order == null)
                filterPaginateDto.Order = Order.Asc;

            var param = Expression.Parameter(typeof(TEntity), "t");
            var expression = Expression.Lambda<Func<TEntity, object>>(Expression.Property(param, filterPaginateDto.SortPropertyName), param);

            if (filterPaginateDto.Order == Order.Asc)
                return query.OrderBy(expression);
            else
                return query.OrderByDescending(expression);
        }

        public virtual async Task<List<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate) => await _entities.Where(predicate).ToListAsync();
        
        public virtual async Task<List<TEntity>> GetWhereAsync(FilterPaginateDto filterPaginateDto)
        {
            var query = _entities.Select(x => x);

            query = GetQueryWhere(query, filterPaginateDto);

            query = GetQueryPagination(query, filterPaginateDto);
            
            if (string.IsNullOrEmpty(filterPaginateDto.SortPropertyName))
            {
                return await query.ToListAsync();
            }
            else
            {
                return await GetQueryOrderBy(query, filterPaginateDto).ToListAsync();
            }            
        }
        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate) => await _entities.FirstOrDefaultAsync(predicate);

        public virtual async Task<int> CountAllAsync() => await _entities.CountAsync();
        public virtual async Task<int> CountWhereAsync(Expression<Func<TEntity, bool>> predicate) => await _entities.CountAsync(predicate);
        public virtual async Task<int> CountWhereAsync(IEnumerable<FilterDto> filters) => await _entities.CountAsync(QueryExpressionBuilder.GetExpression<TEntity>(filters));


        public virtual async Task<string> UpdateAsync(TEntity entity)
        {            
            await _context.Instance.Entry(entity).GetDatabaseValuesAsync();
            _context.Instance.Entry(entity).State = EntityState.Modified;
            return await SaveChangesAsync();
        }

        public virtual async Task<string> RemoveAsync(int id)
        {
            var entity = await _entities.FindAsync(id);
            _context.Instance.Remove(entity);
            return await SaveChangesAsync();
        }

        public virtual async Task<int> ExecuteCommandAsync(string command, params object[] parameters)
        {
            return await _context.Instance.Database.ExecuteSqlRawAsync(command, parameters);
        }

        private AuditOperation GetAuditOperation(EntityState entityState)
        {
            switch (entityState)
            {
                case EntityState.Added:
                    return AuditOperation.Add;
                case EntityState.Deleted:
                    return AuditOperation.Remove;
                case EntityState.Modified:
                    return AuditOperation.Update;
                default:
                    return AuditOperation.Add;
            }
        }

        private ListAuditEntry OnBeforeSaveChanges()
        {
            _context.Instance.ChangeTracker.DetectChanges();
            var auditEntries = new ListAuditEntry();
            foreach (var entry in _context.Instance.ChangeTracker.Entries())
            {

                if (entry.Entity is IAudit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                AuditEntry auditEntry = new AuditEntry();

                auditEntry.HeaderAudit.TableName = entry.Metadata.DisplayName();
                auditEntry.HeaderAudit.Date = DateTime.Now;
                auditEntry.HeaderAudit.UserName = "?";
                auditEntry.HeaderAudit.Operation = GetAuditOperation(entry.State);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        // value will be generated by the database, get the value after saving
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.HeaderAudit.ItemAudits.Add(new ItemAudit
                        {
                            FieldName = propertyName,
                            NewValue = property.CurrentValue.ToString(),
                        });
                        auditEntry.PrimaryKeys.Add(property.CurrentValue.ToString());
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.HeaderAudit.ItemAudits.Add(new ItemAudit
                            {
                                FieldName = propertyName,
                                NewValue = (property.CurrentValue ?? "").ToString(),
                            });
                            break;

                        case EntityState.Deleted:
                            auditEntry.HeaderAudit.ItemAudits.Add(new ItemAudit
                            {
                                FieldName = propertyName,
                                NewValue = (property.CurrentValue ?? "").ToString(),
                            });
                            break;

                        case EntityState.Modified:
                            if ((property.IsModified) && !string.IsNullOrWhiteSpace((property.CurrentValue ?? "").ToString()))
                            {
                                auditEntry.HeaderAudit.ItemAudits.Add(new ItemAudit
                                {
                                    FieldName = propertyName,
                                    NewValue = (property.CurrentValue ?? "").ToString(),
                                });
                            }
                            break;
                    }
                }
                auditEntries.AuditEntries.Add(auditEntry);
            }
            return auditEntries;
        }

        private Task OnAfterSaveChanges(ListAuditEntry auditEntries)
        {
            foreach (var auditEntry in auditEntries.AuditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.HeaderAudit.ItemAudits.Add(new ItemAudit
                        {
                            FieldName = prop.Metadata.Name,

                            NewValue = prop.CurrentValue.ToString()
                        });
                        auditEntry.PrimaryKeys.Add(prop.CurrentValue.ToString());
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace((prop.CurrentValue ?? "").ToString()))
                        {
                            auditEntry.HeaderAudit.ItemAudits.Add(new ItemAudit
                            {
                                FieldName = prop.Metadata.Name,
                                NewValue = prop.CurrentValue.ToString()
                            });
                        }
                    }

                }
                string pk = string.Join('|', auditEntry.PrimaryKeys);
                auditEntry.HeaderAudit.ItemAudits = auditEntry.HeaderAudit.ItemAudits.Select(c =>
                {
                    c.PrimaryKey = pk;
                    return c;
                }).ToList();
                _headerAuditRepository.AddAsync(auditEntry.HeaderAudit);
            }
            return Task.CompletedTask;
        }


        protected virtual async Task<string> SaveChangesAsync()
        {
            try
            {
                if (_headerAuditRepository != null)
                {
                    if (IsConversion)
                    {
                        await _context.Instance.SaveChangesAsync();
                    }
                    else
                    {
                        var listAuditEntry = OnBeforeSaveChanges();
                        await _context.Instance.SaveChangesAsync();
                        OnAfterSaveChanges(listAuditEntry);
                    }
                }
                else
                    await _context.Instance.SaveChangesAsync();
                return "";
            }
            catch (Exception e)
            {
                if (e.InnerException != null)
                {
                    if (e.InnerException.InnerException != null)
                        return e.InnerException.InnerException.Message;
                    else
                        return e.InnerException.Message;
                }
                else
                    return e.Message;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

    public class ListAuditEntry
    {
        public List<AuditEntry> AuditEntries { get; set; } = new List<AuditEntry>();
    }

    public class AuditEntry
    {
        public AuditEntry()
        {
            PrimaryKeys = new List<string>();
        }
        public HeaderAudit HeaderAudit { get; set; } = new HeaderAudit();
        public List<PropertyEntry> TemporaryProperties { get; set; } = new List<PropertyEntry>();

        public bool HasTemporaryProperties => TemporaryProperties.Any();
        public List<string> PrimaryKeys { get; set; }

    }
}
