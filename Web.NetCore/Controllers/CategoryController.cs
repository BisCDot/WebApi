using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.BLL.Category;
using Web.Entity.Resource;

namespace Web.NetCore.Controllers
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryBLL _categoryBLL;

        public CategoryController(ICategoryBLL categoryBLL)
        {
            _categoryBLL = categoryBLL;
        }
        [HttpGet]
        public async Task<object> GetAll()
        {
            return await _categoryBLL.GetAll();
        }
        [HttpPost]
        public async Task<object> AddCategory(CategoryResource category)
        {
            return await _categoryBLL.AddCategory(category);
        }
        [HttpPost]
        public async Task<object> Save(CategoryResource category)
        {
            return await _categoryBLL.SaveCategory(category);
        }
        [HttpDelete]
        public async Task<object> Delete(long id)
        {
            return await _categoryBLL.Delete(id);
        }
        [HttpGet]
        public async Task<object> GetById(long id)
        {
            return await _categoryBLL.GetById(id);
        }
    }
}
