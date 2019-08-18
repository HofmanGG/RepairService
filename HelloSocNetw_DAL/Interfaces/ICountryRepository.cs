using System.Collections.Generic;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities;

namespace HelloSocNetw_DAL.Interfaces
{
    public interface ICountryRepository
    {
        Task<Country> GetCountryByIdAsync(int id);

        Task<int> GetCountOfCountriesAsync();

        void AddCountry(Country country);

        void AddCountries(IEnumerable<Country> countries);

        void DeleteCountry(Country country);

        Task DeleteCountryByIdAsync(int id);

        void DeleteCountries(IEnumerable<Country> countries);

        void UpdateUserInfo(Country country);
    }
}