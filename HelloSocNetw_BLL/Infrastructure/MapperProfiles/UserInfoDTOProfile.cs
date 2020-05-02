using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_DAL.Entities;
using System.Linq;
using HelloSocNetw_BLL.Infrastructure.Enums;
using HelloSocNetw_DAL.Infrastructure.Enums;

namespace HelloSocNetw_BLL.Infrastructure.MapperProfiles
{
    public class UserInfoDTOProfile : Profile
    {
        public UserInfoDTOProfile()
        {
            CreateMap<UserInfoDTO, UserInfo>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (DALEnums.DALGenderType)(int)src.Gender));

            CreateMap<UserInfo, UserInfoDTO>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.CountryName))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.AppIdentityUser.UserRoles.Select(u => u.Role)))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (BLLEnums.BLLGenderType)(int)src.Gender));
        }
    }
}