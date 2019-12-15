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

        Task<bool> AddCountryAsync(CountryDTO countryDto);

        Task<bool> UpdateCountryAsync(CountryDTO newCountryInfoDto);

        Task<bool> DeleteCountryByCountryIdAsync(int countryId);

        Task<bool> CountryWithSuchCountryIdExistsAsync(int countryId);

        Task<bool> CountryWithSuchNameExistsAsync(string countryName);
    }
}
