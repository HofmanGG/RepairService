using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities;

namespace HelloSocNetw_DAL.Interfaces
{
    public interface ICountryRepository
    {
        Task<Country> GetCountryByIdAsync(int countryId);

        Task<IEnumerable<Country>> GetCountriesAsync();

        Task<int> GetCountOfCountriesAsync();

        void AddCountry(Country countryToAdd);

        void AddCountries(IEnumerable<Country> countriesToAdd);

        void DeleteCountry(Country countryToDelete);

        Task DeleteCountryByIdAsync(int countryId);

        void DeleteCountries(IEnumerable<Country> countriesToDelete);

        void UpdateCountryAsync(Country countryToUpdate);

        Task<bool> CountryExistsAsyncByCountryId(int countryId);

        Task<bool> CountryExistsAsync(Expression<Func<Country, bool>> where);
    }
}