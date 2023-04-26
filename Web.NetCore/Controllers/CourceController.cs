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
        private readonly ICourceBLL _courceBLL;

        public CourceController(ICourceBLL courceBLL)
        {
            _courceBLL = courceBLL;
        }
        [HttpPost]
        public async Task<object> Add([FromBody]CourceResource cource)
        {
            return await _courceBLL.Add(cource);
        }
        [HttpGet]
        public async Task<object> GetAll()
        {
            return await _courceBLL.GetAll();
        }
    }
}
