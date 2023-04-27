using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity;
using Web.Entity.Resource;

namespace Web.BLL.Cource
{
    public interface ICourceBLL
    {
        Task<bool> Add(CourceResource entity);
        Task<CourceEntity> GetById(long Id);
        Task<bool> Delete(long Id);
        Task<IEnumerable<CourceEntity>> GetAll();
        Task<bool> Save(CourceResource cource);
    }
}
