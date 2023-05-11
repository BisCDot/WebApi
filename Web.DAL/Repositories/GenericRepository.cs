using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Web.DAL.Common;
using Web.Entity.Resource;

namespace Web.DAL.Repositories
{
    public class FilterResult<TEntity>
    {
        public int TotalRows { get; set; }
        public IEnumerable<TEntity> Data { get; set; }
    }
    public class GenericRepository<TEntity,TContext> : IGenericRepository<TEntity,TContext> where TEntity : class where TContext : DbContext
    {
        protected readonly TContext Context;
        private readonly DbSet<TEntity> _dbSet;
        private readonly IMapper _mapper;
        public GenericRepository(TContext context, IMapper mapper)
        {
            this.Context = context;
            _dbSet = context.Set<TEntity>();
            _mapper = mapper;
        }
        public async Task AddAsync(TEntity entity)
        {
            try
            {
                if (entity.GetType().GetProperty("CreatedDate") != null)
                    entity.GetType().GetProperty("CreatedDate").SetValue(entity, DateTime.Now, null);
                if (entity.GetType().GetProperty("LastModifiedDate") != null)
                    entity.GetType().GetProperty("LastModifiedDate").SetValue(entity, DateTime.Now, null);
            }
            catch (Exception)
            {

            }
            var a = await _dbSet.AddAsync(entity);
        }
        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    if (entity.GetType().GetProperty("CreatedDate") != null)
                        entity.GetType().GetProperty("CreatedDate").SetValue(entity, DateTime.Now, null);
                    if (entity.GetType().GetProperty("LastModifiedDate") != null)
                        entity.GetType().GetProperty("LastModifiedDate").SetValue(entity, DateTime.Now, null);
                }
            }
            catch (Exception)
            {

            }
            await _dbSet.AddRangeAsync(entities);
        }


        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expession)
        {
            return _dbSet.Where(expession);
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public TEntity GetById(long id)
        {
            var a = _dbSet.Find(id);
            return _dbSet.Find(id);
        }
        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);

            //TODO: Remove(TEntity entity). Kiểm tra lại việc áp dụng State
            //if (Context.Entry(entity).State == EntityState.Detached)
            //{
            //    _dbSet.Attach(entity);
            //}
            //_dbSet.Remove(entity);
        }
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
        }
        public ValueTask<TEntity> GetByIdAsync(long id)
        {
            return _dbSet.FindAsync(id);
        }
        public TEntity Update(TEntity entity)
        {
            try
            {
                if (entity.GetType().GetProperty("LastModifiedDate") != null)
                    entity.GetType().GetProperty("LastModifiedDate").SetValue(entity, DateTime.Now, null);
            }
            catch (Exception)
            {

            }
            _dbSet.Update(entity);
            return entity;
        }

        public FilterResult<TResource> Filter<TResource>(PagingParam<TResource> pagingParams,
            params Expression<Func<TResource, bool>>[] predicates) where TResource : class
        {
            var pageIndex = pagingParams.PageIndex;
            var pageSize = pagingParams.PageSize;
            bool isFilterPage = pageIndex > 0 &&  pageSize > 0;
            if (pagingParams == null)
            {
                throw new ArgumentException("pagingParams");
            }

            var result = new FilterResult<TResource>();
            IQueryable<TResource> query = Context.Set<TEntity>().ProjectTo<TResource>(_mapper.ConfigurationProvider);
            var pagingPredicates = pagingParams.GetPredicates();
            if (predicates != null)
            {
                pagingPredicates.AddRange(predicates.ToList());
                predicates = pagingPredicates.ToArray();
            }

            foreach (var item in predicates)
            {
                query = query.Where(item);
            }

            if (isFilterPage)
            {
                result.TotalRows = query.Count();
            }
            // Ordering
            if (!string.IsNullOrEmpty(pagingParams.SortExpression))
                query = query.OrderBy(pagingParams.SortExpression);
            else
            {
                if (typeof(TEntity).GetProperty("Id") != null)
                    query = query.OrderBy("Id desc");
                else if (isFilterPage)
                    throw new ArgumentNullException("SortExpression require");
            }
            if (isFilterPage)
                query = query.Skip((pageIndex - 1) * pageSize + pagingParams.Skip).Take(pageSize);
            result.Data = query.ToList();
            return result;
        }
    }
}
