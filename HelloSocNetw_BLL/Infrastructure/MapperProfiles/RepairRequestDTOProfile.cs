using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Infrastructure.Enums;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Infrastructure.Enums;

namespace HelloSocNetw_BLL.Infrastructure.MapperProfiles
{
    public class RepairRequestDTOProfile: Profile
    {
        public RepairRequestDTOProfile()
        {
            CreateMap<RepairRequestDTO, RepairRequest>()
                .ForMember(dest => dest.RepairStatus, opt => opt.MapFrom(src => (DALEnums.DALRepairStatusType)(int)src.RepairStatus));

            CreateMap<RepairRequest, RepairRequestDTO>()
                .ForMember(dest => dest.RepairStatus, opt => opt.MapFrom(src => (BLLEnums.BLLRepairStatusType)(int)src.RepairStatus))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.UserInfo.AppIdentityUser.Email));
        }
    }
}
