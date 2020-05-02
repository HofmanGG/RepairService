using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Entities.IdentityEntities;

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
