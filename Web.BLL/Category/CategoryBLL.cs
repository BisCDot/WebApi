using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.DAL.Common;
using Web.DAL.Cource;
using Web.Entity;
using Web.Entity.Resource;

namespace Web.BLL.Category
{
    public class CategoryBLL : ICategoryBLL
    {
        private readonly ICategoryRepository _category;
        private readonly IMapper _mapper;
        private readonly IUnitofWork<MainDbContext> _unitofWork;

        public CategoryBLL(ICategoryRepository category,IMapper mapper,IUnitofWork<MainDbContext> unitofWork)
        {
            _category = category;
            _mapper = mapper;
            _unitofWork = unitofWork;
        }
        public async Task<bool> AddCategory(CategoryResource category)
        {
            var obj = _mapper.Map<CategoryResource, CategoryEntity>(category);
            if (obj != null)
            {
                var entity = new CategoryEntity()
                {
                    Name = obj.Name
                };
                await _category.AddAsync(entity);
                await _unitofWork.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<CategoryEntity>> GetAll()
        {
            return await _category.GetAllAsync();
        }
        public async Task<bool> SaveCategory(CategoryResource category)
        {   
            var obj = _mapper.Map<CategoryResource, CategoryEntity>(category);
            if (obj.Id > 0)
                _category.Update(obj);
            else
                await _category.AddAsync(obj);
            await _unitofWork.SaveChangesAsync();
            return true;
        }
        public async Task<bool> Delete(long id)
        {
            var obj = await _category.GetByIdAsync(id);
            if (obj != null)
            {
                _category.Remove(obj);
                await _unitofWork.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
