using AutoMapper;
using Web.Entity;
using Web.Entity.Resource;

namespace Web.NetCore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CourceEntity, CourceResource>();
            CreateMap<CourceResource, CourceEntity>();
        }
    }
}
