using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Web.DAL.Common;
using Web.DAL.Repositories;
using Web.Entity;
using Web.Entity.Resource;

namespace Web.DAL.Cource
{
    public class CourceRepository : GenericRepository<CourseEntity,MainDbContext>,ICourceRepository
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public CourceRepository(MainDbContext context,IMapper mapper) : base(context,mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CourseEntity> GetList(int pageIndex, int pageSize)
        {
            return _context.Courcess.Where((k => k.Id > 10)).OrderByDescending(k => k.Id).Skip(pageIndex * pageSize - pageSize)
                .Take(pageSize).ToList();
        }
    }
}
