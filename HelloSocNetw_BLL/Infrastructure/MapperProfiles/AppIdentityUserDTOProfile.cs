using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HelloSocNetw_BLL.Infrastructure.MapperProfiles
{
    public class AppIdentityUserDTOProfile: Profile
    {
        public AppIdentityUserDTOProfile()
        {
            CreateMap<AppIdentityUserDTO, AppIdentityUser>()
                .ForPath(dest => dest.UserInfo.UserInfoId, opt => opt.MapFrom(src => src.UserInfoId));

            CreateMap<AppIdentityUser, AppIdentityUserDTO>()
                .ForMember(dest => dest.UserInfoId, opt => opt.MapFrom(src => src.UserInfo.UserInfoId));
        }
    }
}
