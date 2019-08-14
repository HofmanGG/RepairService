using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DAL.Entities;
using DAL.Interfaces;
using DAL.Сontexts;
using Entities;
using Microsoft.EntityFrameworkCore;
namespace DAL.EFRepositories
{
    public class EfCountryRepository: ICountryRepository
    {
        private readonly SocNetwContext _context;

        public EfCountryRepository(DbContext dbContext) => _context = dbContext as SocNetwContext;

        public async Task<Country> GetCountryByIdAsync(int id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public async Task<int> GetCountOfCountriesAsync() => await _context.Countries.CountAsync();

        public void AddCountry(Country country) => _context.Countries.Add(country);

        public void AddCountries(IEnumerable<Country> countries) => _context.Countries.AddRange(countries);

        public void RemoveCountry(Country country) => _context.Countries.Remove(country);

        public async Task RemoveCountryByIdAsync(int id)
        {
            var country = await GetCountryByIdAsync(id);
            RemoveCountry(country);
        }

        public void RemoveCountries(IEnumerable<Country> country) => _context.Countries.RemoveRange(country);

        public void UpdateUserInfo(Country country)
        {
            _context.Countries.Attach(country);
            _context.Entry(country).State = EntityState.Modified;
        }
    }
}