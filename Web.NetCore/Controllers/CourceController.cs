using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.BLL.Cource;
using Web.Entity.Resource;

namespace Web.NetCore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CourceController : Controller
    {
        private readonly ICourseBLL _courseBll;

        public CourceController(ICourseBLL courseBll)
        {
            _courseBll = courseBll;
        }
        [HttpPost]
        public async Task<object> Add([FromBody]CourseResource course)
        {
            return await _courseBll.Add(course);
        }
        [HttpPost]
        public object List(CourseFillterResource filter)
        {
            return _courseBll.GetList(filter);
        }
        [HttpGet]
        public async Task<object> GetAll()
        {
            return await _courseBll.GetAll();
        }
        [HttpDelete]
        public async Task<object> Delete(long Id)
        {
            return await _courseBll.Delete(Id);
        }
        [HttpPost]
        public async Task<object> Save(CourseResource course)
        {
            return await _courseBll.Save(course);
        }
        [HttpGet]
        public async Task<object> GetById(long id)
        {
            return await _courseBll.GetById(id);
        }
    }
}
