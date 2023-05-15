using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.DAL.Repositories;
using Web.Entity;
using Web.Entity.Resource;

namespace Web.BLL.Cource
{
    public interface ICourseBLL
    {
        Task<bool> Add(CourseResource entity);
        Task<CourseEntity> GetById(long Id);
        Task<bool> Delete(long Id);
        Task<IEnumerable<CourseEntity>> GetAll();
        Task<bool> Save(CourseResource course);
        Task<FilterResult<CourseResource>> GetList(CourseFillterResource filter);
    }
}
