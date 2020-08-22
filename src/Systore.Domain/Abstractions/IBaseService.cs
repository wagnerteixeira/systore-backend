﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Systore.Domain.Dtos;

namespace Systore.Domain.Abstractions
{
    public interface IBaseService<TEntity>
    {
        Task<string> AddAsync(TEntity entity);
        Task<TEntity> GetAsync(int id);
        IQueryable<TEntity> GetAll();
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetWhereAsync(FilterPaginateDto filterPaginateDto);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAllAsync();
        Task<int> CountWhereAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountWhereAsync(IEnumerable<FilterDto> filters);
        Task<string> UpdateAsync(TEntity entity);
        Task<string> RemoveAsync(int id);
    }
}
