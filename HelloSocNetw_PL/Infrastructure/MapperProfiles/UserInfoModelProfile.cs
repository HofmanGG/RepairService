using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_PL.Models;

namespace HelloSocNetw_PL.Infrastructure.MapperProfiles
{
    public class UserInfoModelProfile: Profile
    {
        public UserInfoModelProfile()
        {
            CreateMap<UserInfoDTO, UserInfoModel> ()
                .ForMember(dest => dest.YearOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.Year))
                .ForMember(dest => dest.MonthOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.Month))
                .ForMember(dest => dest.DayOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.Day));
        }

    }
}
