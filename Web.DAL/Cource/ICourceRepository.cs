using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.DAL.Common;
using Web.DAL.Repositories;
using Web.Entity;

namespace Web.DAL.Cource
{
    public interface ICourceRepository : IGenericRepository<CourseEntity,MainDbContext>
    {
        
    }
}
