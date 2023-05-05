using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Entity;
using Web.Entity.Resource;

namespace Web.BLL.Category
{
    public interface ICategoryBLL
    {
        Task<CategoryEntity> GetById(long id);
        Task<bool> AddCategory(CategoryResource category);
        Task<IEnumerable<CategoryEntity>> GetAll();
        Task<bool> SaveCategory(CategoryResource category);
        Task<bool> Delete(long id);
    }
}
