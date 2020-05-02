using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_PL.Models;
using HelloSocNetw_PL.Models.RoleModels;

namespace HelloSocNetw_PL.Infrastructure.MapperProfiles
{
    public class RoleModelProfile : Profile
    {
        public RoleModelProfile()
        {
            CreateMap<RoleModel, AppRoleDTO>();
            CreateMap<NewRoleModel, AppRoleDTO>();
            CreateMap<UpdateRoleModel, AppRoleDTO>();

            CreateMap<AppRoleDTO, RoleModel>();
        }
    }
}
