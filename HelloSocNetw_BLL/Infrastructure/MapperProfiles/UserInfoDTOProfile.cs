using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_DAL.Entities;

namespace HelloSocNetw_BLL.Infrastructure.MapperProfiles
{
    public class UserInfoDTOProfile : Profile
    {
        public UserInfoDTOProfile()
        {
            CreateMap<UserInfoDTO, UserInfo>();
            CreateMap<UserInfo, UserInfoDTO>();
        }
    }
}