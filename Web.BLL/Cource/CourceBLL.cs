using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.DAL.Common;
using Web.DAL.Cource;
using Web.Entity;
using Web.Entity.Resource;

namespace Web.BLL.Cource
{
    public class CourceBLL : ICourceBLL
    {
        private readonly IUnitofWork<MainDbContext> _unitofWork;
        private readonly IMapper _mapper;
        private readonly ICourceRepository _courceRepository;
        public CourceBLL(IUnitofWork<MainDbContext> unitofWork, ICourceRepository ModuleRepository, IMapper mapper) 
        {
            _unitofWork = unitofWork;
            _courceRepository = ModuleRepository;
            _mapper = mapper;
        }
       public async Task<bool> Add(CourceResource entity)
        {
            var obj = _mapper.Map<CourceResource, CourceEntity>(entity);
            if (obj != null)
            {
                var cource = new CourceEntity()
                {
                    Title = obj.Title,
                    Image = obj.Image,
                    Price = obj.Price,
                    Description = obj.Description,
                    CreateDatatime = DateTime.Now,
                    CategorysId = obj.CategorysId
                };
                await _courceRepository.AddAsync(cource);
                await _unitofWork.SaveChangesAsync();
                return true;
            }
            return false;
           
        }

        public async Task<CourceEntity> GetById(long Id)
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

        public async Task<IEnumerable<CourceEntity>> GetAll()
        {

            return await _courceRepository.GetAllAsync();
        }
        
        public async Task<bool> Save(CourceResource entity)
        {
            var obj = _mapper.Map<CourceResource, CourceEntity>(entity);
            if (obj.Id > 0)
                _courceRepository.Update(obj);
            else
                await _courceRepository.AddAsync(obj);

            await _unitofWork.SaveChangesAsync();
            return true;
        }
    }
}
