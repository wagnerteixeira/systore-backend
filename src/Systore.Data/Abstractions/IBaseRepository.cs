using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Linq;
using Systore.Domain.Dtos;

namespace Systore.Data.Abstractions
{
    public interface IBaseRepository<TEntity>
    {
        Task<string> AddAsync(TEntity entity);
        Task<TEntity> GetAsync(int id);
        IQueryable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAllAsync();
        Task<int> CountWhereAsync(Expression<Func<TEntity, bool>> predicate);
        Task<string> UpdateAsync(TEntity entity);
        Task<string> RemoveAsync(int id);
        Task<List<TEntity>> GetWhereAsync(FilterPaginateDto filterPaginateDto);
        Task<int> CountWhereAsync(IEnumerable<FilterDto> filters);
        bool BeginTransaction();
        bool Commit();
        void Rollback();
        void Dispose();
    }
}
