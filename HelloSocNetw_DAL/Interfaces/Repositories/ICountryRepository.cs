using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities;

namespace HelloSocNetw_DAL.Interfaces.Repositories
{
    public interface ICountryRepository
    {
        Task<Country> GetCountryByIdAsync(long countryId);
        Task<IEnumerable<Country>> GetCountriesAsync();
        Task<long> GetCountOfCountriesAsync();

        void AddCountry(Country countryToAdd);

        Task DeleteCountryByIdAsync(long countryId);

        Task UpdateCountryAsync(Country countryToUpdate);

        Task<bool> CountryExistsByIdAsync(long countryId);
        Task<bool> CountryExistsByNameAsync(string name);
    }
}