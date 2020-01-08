using HelloSocNetw_BLL.EntitiesDTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace HelloSocNetw_BLL.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDTO>> GetCountriesAsync();

        Task<CountryDTO> GetCountryByCountryIdAsync(int countryId);

        Task AddCountryAsync(CountryDTO countryDto);

        Task UpdateCountryAsync(CountryDTO newCountryInfoDto);

        Task DeleteCountryByCountryIdAsync(int countryId);

        Task<bool> CountryWithSuchCountryIdExistsAsync(int countryId);

        Task<bool> CountryWithSuchNameExistsAsync(string countryName);
    }
}
