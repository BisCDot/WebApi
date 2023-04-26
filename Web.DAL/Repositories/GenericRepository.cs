using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Web.DAL.Common;

namespace Web.DAL.Repositories
{
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

    }
}
