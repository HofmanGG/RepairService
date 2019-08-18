using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_PL.Models;

namespace HelloSocNetw_PL.Infrastructure.MapperProfiles
{
    public class RegisterModelProfile: Profile
    {
        public RegisterModelProfile()
        {
            CreateMap<RegisterModel, UserInfoDTO>()
                .ForPath(dest => dest.DateOfBirth.Year, opt => opt.MapFrom(src => src.YearOfBirth))
                .ForPath(dest => dest.DateOfBirth.Month, opt => opt.MapFrom(src => src.MonthOfBirth))
                .ForPath(dest => dest.DateOfBirth.Day, opt => opt.MapFrom(src => src.DayOfBirth));
        }
    }
}