using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_DAL.Entities.IdentityEntities;

namespace HelloSocNetw_BLL.Infrastructure.MapperProfiles
{
    public class AppIdentityUserDTOProfile: Profile
    {
        public AppIdentityUserDTOProfile()
        {
            CreateMap<AppIdentityUserDTO, AppIdentityUser>();

            CreateMap<AppIdentityUser, AppIdentityUserDTO>();
        }
    }
}
