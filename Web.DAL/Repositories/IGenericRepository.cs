using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Web.Entity.Resource;

namespace Web.DAL.Repositories
{
    public interface IGenericRepository<TEntity,TContext> where TEntity : class
    {
        ValueTask<TEntity> GetByIdAsync(long id);
        TEntity Update(TEntity entity);
        TEntity GetById(long Id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
        FilterResult<TResource> Filter<TResource>(PagingParam<TResource> pagingParams, params Expression<Func<TResource, bool>>[] predicates)
            where TResource : class;

        Task<FilterResult<TEntity>> FilterWithIncludes(PagingParam<TEntity> pagingParams,
            Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, dynamic>>[] includes);

    }
}
