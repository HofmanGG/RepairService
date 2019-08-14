﻿using System.Collections.Generic;
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

        void RemoveCountry(Country country);

        Task RemoveCountryByIdAsync(int id);

        void RemoveCountries(IEnumerable<Country> countries);

        void UpdateUserInfo(Country country);
    }
}