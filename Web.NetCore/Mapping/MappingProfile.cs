﻿using AutoMapper;
using Web.Entity;
using Web.Entity.Resource;

namespace Web.NetCore.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CourseEntity, CourseResource>();
            CreateMap<CourseResource, CourseEntity>();
            CreateMap<CategoryEntity, CategoryResource>();
            CreateMap<CategoryResource,CategoryEntity>();
        }
    }
}
