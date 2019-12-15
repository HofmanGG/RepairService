using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HelloSocNetw_BLL.Services
{
    public class CountryService: ICountryService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mpr;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _uow = unitOfWork;
            _mpr = mapper;
        }

        public async Task<IEnumerable<CountryDTO>> GetCountriesAsync()
        {
            var countries = await _uow.Countries.GetCountriesAsync();
            var countriesDto = _mpr.Map<IEnumerable<CountryDTO>>(countries);
            return countriesDto;
        }

        public async Task<CountryDTO> GetCountryByCountryIdAsync(int countryId)
        {
            var country = await _uow.Countries.GetCountryByIdAsync(countryId);
            if (country == null)
                return null;

            var countryDto = _mpr.Map<CountryDTO>(country);
            return countryDto;
        }

        public async Task<bool> AddCountryAsync(CountryDTO countryDto)
        {
            var country = _mpr.Map<Country>(countryDto);
            _uow.Countries.AddCountry(country);
            var rowsAffected = await _uow.SaveChangesAsync();
            return rowsAffected == 1;
        }

        public async Task<bool> DeleteCountryByCountryIdAsync(int countryId)
        {
            await _uow.Countries.DeleteCountryByIdAsync(countryId);
            var rowsAffected =  await _uow.SaveChangesAsync();
            return rowsAffected == 1;
        }

        public async Task<bool> UpdateCountryAsync(CountryDTO newCountryInfoDto)
        {
            var countryToChange = await _uow.Countries.GetCountryByIdAsync(newCountryInfoDto.CountryId);

            countryToChange.CountryName = newCountryInfoDto.CountryName;

            _uow.Countries.UpdateCountryAsync(countryToChange);
            var rowsAffected = await _uow.SaveChangesAsync();
            return rowsAffected == 1;
        }

        public async Task<bool> CountryWithSuchCountryIdExistsAsync(int countryId)
        {
            return await _uow.Countries.CountryExistsAsyncByCountryId(countryId);
        }

        public async Task<bool> CountryWithSuchNameExistsAsync(string countryName)
        {
            return await _uow.Countries.CountryExistsAsync(c => c.CountryName == countryName);
        }
    }
}
