using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Web.DAL.Common;
using Web.DAL.Repositories;
using Web.Entity;
using Web.Entity.Resource;

namespace Web.DAL.Cource
{
    public class CourceRepository : GenericRepository<CourceEntity,MainDbContext>,ICourceRepository
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public CourceRepository(MainDbContext context,IMapper mapper) : base(context,mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    }
}
