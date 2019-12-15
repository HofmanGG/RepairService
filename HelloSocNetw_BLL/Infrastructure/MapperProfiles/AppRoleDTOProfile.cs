using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_DAL.Entities;
using System;

namespace HelloSocNetw_BLL.Infrastructure.MapperProfiles
{
    public class AppRoleDTOProfile : Profile
    {
        public AppRoleDTOProfile()
        {
            CreateMap<AppRoleDTO, AppRole>();
            CreateMap<AppRole, AppRoleDTO>();
        }
    }
}
