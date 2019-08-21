using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_PL.Models;
using System;

namespace HelloSocNetw_PL.Infrastructure.MapperProfiles
{
    public class RegisterModelProfile: Profile
    {
        public RegisterModelProfile()
        {
            CreateMap<RegisterModel, UserInfoDTO>()
                .ForPath(dest => dest.DateOfBirth, opt => opt.MapFrom(src => new DateTime(src.YearOfBirth, src.MonthOfBirth, src.DayOfBirth)));
        }
    }
}