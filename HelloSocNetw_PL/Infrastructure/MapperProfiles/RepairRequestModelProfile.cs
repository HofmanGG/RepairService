using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Infrastructure.Enums;
using HelloSocNetw_PL.Infrastructure.Enums;
using HelloSocNetw_PL.Models;
using HelloSocNetw_PL.Models.RepairRequestModels;

namespace HelloSocNetw_PL.Infrastructure.MapperProfiles
{
    public class RepairRequestModelProfile : Profile
    {
        public RepairRequestModelProfile()
        {
            CreateMap<NewRepairRequestModel, RepairRequestDTO>()
                .ForMember(dest => dest.RepairStatus, opt => opt.MapFrom(src => (BLLEnums.BLLRepairStatusType)(int)src.RepairStatus));

            CreateMap<UpdateRepairRequestModel, RepairRequestDTO>()
                .ForMember(dest => dest.RepairStatus, opt => opt.MapFrom(src => (BLLEnums.BLLRepairStatusType)(int)src.RepairStatus));

            CreateMap<RepairRequestDTO, RepairRequestModel>()
                .ForMember(dest => dest.RepairStatus, opt => opt.MapFrom(src => (PLEnums.PLRepairStatusType)(int)src.RepairStatus))
                .ForMember(dest => dest.RequestDay, opt => opt.MapFrom(src => src.RequestTime.Day))
                .ForMember(dest => dest.RequestMonth, opt => opt.MapFrom(src => src.RequestTime.Month))
                .ForMember(dest => dest.RequestYear, opt => opt.MapFrom(src => src.RequestTime.Year));
        }
    }
}

