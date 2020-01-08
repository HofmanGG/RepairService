using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Infrastructure;
using HelloSocNetw_BLL.Infrastructure.Exceptions;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Infrastructure.Exceptions;
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

        public async Task AddCountryAsync(CountryDTO countryDto)
        {
            var doesCountryWithSuchNameAlredyExist = await _uow.Countries
                .CountryExistsAsync(c => c.CountryName == countryDto.CountryName);

            if (doesCountryWithSuchNameAlredyExist)
                throw new ConflictException(nameof(Country), countryDto.CountryName);

            var country = _mpr.Map<Country>(countryDto);

            _uow.Countries.AddCountry(country);
            var rowsAffected = await _uow.SaveChangesAsync();
            if (rowsAffected != 1)
                throw new DBOperationException("Country adding went wrong");
        }

        public async Task DeleteCountryByCountryIdAsync(int countryId)
        {
            await _uow.Countries.DeleteCountryByIdAsync(countryId);
            var rowsAffected =  await _uow.SaveChangesAsync();
            if (rowsAffected != 1)
                throw new DBOperationException("Country deleting went wrong");
        }

        public async Task UpdateCountryAsync(CountryDTO newCountryInfoDto)
        {
            var countryToChange = await _uow.Countries.GetCountryByIdAsync(newCountryInfoDto.CountryId);
            if (countryToChange == null)
                throw new NotFoundException(nameof(Country), newCountryInfoDto.CountryId);

            var doesCountryWithSuchNameAlredyExist = await _uow.Countries
                .CountryExistsAsync(c => c.CountryName == newCountryInfoDto.CountryName);

            if (doesCountryWithSuchNameAlredyExist)
                throw new ConflictException(nameof(Country), newCountryInfoDto.CountryName);

            countryToChange.CountryName = newCountryInfoDto.CountryName;

            _uow.Countries.UpdateCountryAsync(countryToChange);
            var rowsAffected = await _uow.SaveChangesAsync();
            if (rowsAffected != 1)
                throw new DBOperationException("Country updating went wrong");
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
