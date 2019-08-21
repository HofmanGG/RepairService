using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
