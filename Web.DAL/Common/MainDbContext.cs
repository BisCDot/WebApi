using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Web.Entity;

namespace Web.DAL.Common
{
    public class MainDbContext : DbContext
    {
        public virtual DbSet<CourceEntity> Courcess { get; set; }
        public virtual DbSet<CategoryEntity> Categories { get; set; }
        public MainDbContext(DbContextOptions<MainDbContext> options) : base (options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
