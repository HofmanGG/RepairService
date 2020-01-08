using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Infrastructure;
using HelloSocNetw_DAL.Infrastructure.Exceptions;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HelloSocNetw_DAL.EFRepositories
{
    public class EfCountryRepository : ICountryRepository
    {
        private readonly SocNetwContext _context;

        public EfCountryRepository(DbContext dbContext)
        {
            _context = dbContext as SocNetwContext;
        }

        public async Task<Country> GetCountryByIdAsync(int countryId)
        {
            return await _context.Countries.FindAsync(countryId);
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<int> GetCountOfCountriesAsync()
        {
            return await _context.Countries.CountAsync();
        }

        public void AddCountry(Country country)
        {
            _context.Countries.Add(country);
        }

        public void AddCountries(IEnumerable<Country> countries)
        {
            _context.Countries.AddRange(countries);
        }

        public void DeleteCountry(Country country)
        {
            _context.Countries.Remove(country);
        }

        public async Task DeleteCountryByIdAsync(int countryId)
        {
            var countryToDelete = await _context.Countries.FindAsync(countryId);
            if (countryToDelete == null)
                throw new NotFoundException(nameof(Country), countryId);

            _context.Countries.Remove(countryToDelete);
        }

        public void DeleteCountries(IEnumerable<Country> country)
        {
            _context.Countries.RemoveRange(country);
        }

        public void UpdateCountryAsync(Country countryToUpdate) 
        {
            _context.Update(countryToUpdate);
        }

        public async Task<bool> CountryExistsAsyncByCountryId(int countryId)
        {
            return await _context.Countries.AnyAsync(u => u.CountryId == countryId);
        }

        public async Task<bool> CountryExistsAsync(Expression<Func<Country, bool>> where)
        {
            return await _context.Countries.AnyAsync(where);
        }
    } 
}