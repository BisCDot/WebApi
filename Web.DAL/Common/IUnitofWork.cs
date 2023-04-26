using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.DAL.Common
{
    public interface IUnitofWork<TContext> where TContext : DbContext
    {
        Task<int> SaveChangesAsync();
    }
    public interface IDbFactory<TContext> : IDisposable where TContext : DbContext
    {
        TContext Init();
    }
    public class UnitOfWork<TContext> : IDisposable , IUnitofWork<TContext> where TContext : DbContext
    {
        private  TContext _context;

        public UnitOfWork(TContext context)
        {
            _context = context;
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
