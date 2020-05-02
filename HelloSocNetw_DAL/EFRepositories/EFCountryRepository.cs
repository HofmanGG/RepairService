using System.Collections.Generic;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace HelloSocNetw_DAL.EFRepositories
{
    public class EfCountryRepository : ICountryRepository
    {
        private readonly SocNetwContext _context;
        private readonly DbSet<Country> _countries;

        public EfCountryRepository(SocNetwContext dbContext)
        {
            _context = dbContext;
            _countries = _context.Countries;
        }

        public async Task<Country> GetCountryByIdAsync(long countryId)
        {
            var foundCountry =  await _countries.FindAsync(countryId);
            return foundCountry;
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            var countries =  await _countries.ToListAsync();
            return countries;
        }

        public async Task<long> GetCountOfCountriesAsync()
        {
            var count =  await _countries.CountAsync();
            return count;
        }

        public void AddCountry(Country country)
        {
            _countries.Add(country);
        }

        public async Task DeleteCountryByIdAsync(long countryId)
        {
            var countryToDelete = await _countries.FindAsync(countryId);
            _countries.Remove(countryToDelete);
        }

        public async Task UpdateCountryAsync(Country country)
        {
            var countryToUpdate = await _countries.FindAsync(country.Id);

            countryToUpdate.CountryName = country.CountryName;
        }

        public async Task<bool> CountryExistsByIdAsync(long countryId)
        {
            var countryExists = await _countries.AnyAsync(u => u.Id == countryId);
            return countryExists;
        }

        public async Task<bool> CountryExistsByNameAsync(string name)
        {
            var countryExists = await _countries.AnyAsync(c => c.CountryName == name);
            return countryExists;
        }
    } 
}