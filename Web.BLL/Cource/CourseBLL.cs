using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.DAL.Common;
using Web.DAL.Cource;
using Web.DAL.Repositories;
using Web.Entity;
using Web.Entity.Resource;

namespace Web.BLL.Cource
{
    public class CourseBLL : ICourseBLL
    {
        private readonly IUnitofWork<MainDbContext> _unitofWork;
        private readonly IMapper _mapper;
        private readonly ICourceRepository _courceRepository;
        public CourseBLL(IUnitofWork<MainDbContext> unitofWork, ICourceRepository ModuleRepository, IMapper mapper) 
        {
            _unitofWork = unitofWork;
            _courceRepository = ModuleRepository;
            _mapper = mapper;
        }
       public async Task<bool> Add(CourseResource entity)
        {
            var obj = _mapper.Map<CourseResource, CourseEntity>(entity);
            if (obj != null)
            {
                var cource = new CourseEntity()
                {
                    Title = obj.Title,
                    Image = obj.Image,
                    Price = obj.Price,
                    Description = obj.Description,
                    CategoryId = obj.CategoryId
                    
                };
                await _courceRepository.AddAsync(cource);
                await _unitofWork.SaveChangesAsync();
                return true;
            }
            return false;
           
        }

        public async Task<CourseEntity> GetById(long Id)
        {
            return await _courceRepository.GetByIdAsync(Id);
        }

        public async Task<bool> Delete(long Id)
        {
            var obj = await _courceRepository.GetByIdAsync(Id);
            if (obj != null)
            {
                _courceRepository.Remove(obj);
                await _unitofWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<FilterResult<CourseResource>> GetList(CourseFillterResource filter)
        {
            var result = await _courceRepository.FilterWithIncludes(new PagingParam<CourseEntity>
                {
                    MaxPrice = filter.MaxPrice,
                    MinPrice = filter.MinPrice,
                    PageIndex = filter.PageIndex,
                    PageSize = filter.PageSize,
                    Skip = filter.Skip,
                    SortExpression = filter.SortExpression,
                    NotSkip = filter.NotSkip,
                    CategoryId = filter.CategoryId
                },
                x =>  !string.IsNullOrWhiteSpace(filter.KeyWord) ? x.Title.Contains(filter.KeyWord.ToLower()) :  (filter.MaxPrice> 0 && filter.MinPrice>= 0) ? x.Price < filter.MaxPrice && x.Price> filter.MinPrice 
                    : filter.CategoryId > 0 ? x.CategoryId == filter.CategoryId : filter.Status > 0 ? x.Status == filter.Status : true,
                c => c.Category);
            var value = new FilterResult<CourseResource>()
            {
                TotalRows = result.TotalRows,
                Data = result.Data.Select(x => new CourseResource
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    Price = x.Price,
                    Image = x.Image,
                    CategoryId = x.CategoryId,
                    CreatedDate = x.CreatedDate,
                    CategoryName = x.Category.Name
                })
            };
            return value;
        }
        public async Task<IEnumerable<CourseEntity>> GetAll()
        {
            return await _courceRepository.GetAllAsync();
        }
        
        public async Task<bool> Save(CourseResource entity)
        {
           
            if (entity.Id > 0)
            {
                var old =  await _courceRepository.GetByIdAsync(entity.Id);
                var obj = _mapper.Map<CourseResource, CourseEntity>(entity, old);
                _courceRepository.Update(obj);
            }
            else
            {
                var obj = _mapper.Map<CourseResource, CourseEntity>(entity);
                                await _courceRepository.AddAsync(obj);
            }

            await _unitofWork.SaveChangesAsync();
            return true;
        }
    }
}
