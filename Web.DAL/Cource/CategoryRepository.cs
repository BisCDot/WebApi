using AutoMapper;
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
    public class CategoryRepository : GenericRepository<CategoryEntity,MainDbContext>, ICategoryRepository
    {
        private readonly MainDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(MainDbContext context,IMapper mapper) : base(context,mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
