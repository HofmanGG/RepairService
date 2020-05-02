using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_BLL.Infrastructure;
using HelloSocNetw_PL.Models.UserInfoModels;
using System;
using HelloSocNetw_BLL.Infrastructure.Enums;
using HelloSocNetw_PL.Infrastructure.Enums;

namespace HelloSocNetw_PL.Infrastructure.MapperProfiles
{
    public class UserInfoModelProfile: Profile
    {
        public UserInfoModelProfile()
        {
            CreateMap<UserInfoDTO, UserInfoModel> ()
                .ForMember(dest => dest.YearOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.Year))
                .ForMember(dest => dest.MonthOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.Month))
                .ForMember(dest => dest.DayOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.Day))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (PLEnums.PLGenderType)(int)src.Gender));


            CreateMap<NewUserInfoModel, UserInfoDTO>()
                .ForPath(dest => dest.DateOfBirth, opt => opt.MapFrom(src => new DateTime(src.YearOfBirth, src.MonthOfBirth, src.DayOfBirth)))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (BLLEnums.BLLGenderType)(int)src.Gender));

            CreateMap<UpdateUserInfoModel, UserInfoDTO>()
                .ForPath(dest => dest.DateOfBirth, opt => opt.MapFrom(src => new DateTime(src.YearOfBirth, src.MonthOfBirth, src.DayOfBirth)))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => (BLLEnums.BLLGenderType)(int)src.Gender));
        }

    }
}
