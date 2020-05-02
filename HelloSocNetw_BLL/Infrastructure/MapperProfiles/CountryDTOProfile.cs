using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_DAL.Entities;

namespace HelloSocNetw_BLL.Infrastructure.MapperProfiles
{
    public class CountryDTOProfile : Profile
    {
        public CountryDTOProfile()
        {
            CreateMap<CountryDTO, Country>();
            CreateMap<Country, CountryDTO>();
        }
    }
}
