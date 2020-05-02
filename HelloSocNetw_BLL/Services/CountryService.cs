using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Infrastructure;
using HelloSocNetw_BLL.Infrastructure.Exceptions;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using System.Collections.Generic;
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

        public async Task<CountryDTO> GetCountryByIdAsync(long countryId)
        {
            var country = await _uow.Countries.GetCountryByIdAsync(countryId);
            var countryDto = _mpr.Map<CountryDTO>(country);
            return countryDto;
        }

        public async Task AddCountryAsync(CountryDTO countryDto)
        {
            await ThrowIfCountryWithSuchNameExistsAsync(countryDto.CountryName);

            var country = _mpr.Map<Country>(countryDto);

            _uow.Countries.AddCountry(country);

            var rowsAffected = await _uow.SaveChangesAsync();
            if (rowsAffected != 1)
                throw new DBOperationException("Country adding went wrong");
        }

        public async Task DeleteCountryByIdAsync(long countryId)
        {
            await ThrowIfCountryWithSuchIdDoesNotExistAsync(countryId);

            await _uow.Countries.DeleteCountryByIdAsync(countryId);

            var rowsAffected =  await _uow.SaveChangesAsync();
            if (rowsAffected != 1)
                throw new DBOperationException("Country deleting went wrong");
        }

        private async Task ThrowIfCountryWithSuchIdDoesNotExistAsync(long countryId)
        {
            var countryExists = await _uow.Countries.CountryExistsByIdAsync(countryId);

            if (!countryExists)
                throw new NotFoundException(nameof(Country), countryId);
        }

        private async Task ThrowIfCountryWithSuchNameExistsAsync(string name)
        {
            var countryExists = await _uow.Countries
                .CountryExistsByNameAsync(name);

            if (countryExists)
                throw new ConflictException(nameof(Country), name);
        }

        public async Task UpdateCountryAsync(CountryDTO countryDto)
        {
            await ThrowIfCountryWithSuchIdDoesNotExistAsync(countryDto.Id);

            await ThrowIfCountryWithSuchNameExistsAsync(countryDto.CountryName);

            var country = _mpr.Map<Country>(countryDto);

            await _uow.Countries.UpdateCountryAsync(country);

            var rowsAffected = await _uow.SaveChangesAsync();
            if (rowsAffected != 1)
                throw new DBOperationException("Country updating went wrong");
        }
    }
}
