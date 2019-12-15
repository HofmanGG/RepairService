using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Infrastructure;
using static HelloSocNetw_BLL.Infrastructure.BLLEnums;
using static HelloSocNetw_DAL.Infrastructure.DALEnums;

namespace HelloSocNetw_BLL.Infrastructure.MapperProfiles
{
    public class RepairRequestDTOProfile: Profile
    {
        public RepairRequestDTOProfile()
        {
            CreateMap<RepairRequestDTO, RepairRequest>()
                .ForMember(dest => dest.RepairStatus, opt => opt.MapFrom(src => (DALRepairStatusType)(int)src.RepairStatus));

            CreateMap<RepairRequest, RepairRequestDTO>()
                .ForMember(dest => dest.RepairStatus, opt => opt.MapFrom(src => (BLLRepairStatusType)(int)src.RepairStatus));
        }
    }
}
