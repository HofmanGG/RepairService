using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_PL.Models;
using HelloSocNetw_PL.Models.CountryModels;

namespace HelloSocNetw_PL.Infrastructure.MapperProfiles
{
    public class CountryModelProfile: Profile
    {
        public CountryModelProfile()
        {
            CreateMap<CountryModel, CountryDTO>();
            CreateMap<NewCountryModel, CountryDTO>();
            CreateMap<UpdateCountryModel, CountryDTO>();

            CreateMap<CountryDTO, CountryModel>();
        }
    }
}
