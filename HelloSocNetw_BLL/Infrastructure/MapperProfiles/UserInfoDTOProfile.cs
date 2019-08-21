using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_DAL.Entities;

namespace HelloSocNetw_BLL.Infrastructure.MapperProfiles
{
    public class UserInfoDTOProfile : Profile
    {
        public UserInfoDTOProfile()
        {
            CreateMap<UserInfoDTO, UserInfo>()
                .ForMember(dest => dest.AppIdentityUserId, opt => opt.MapFrom(src => src.UserInfoId));
            
            CreateMap<UserInfo, UserInfoDTO>()
                .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country.CountryName));
        }
    }
}