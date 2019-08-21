using System.Collections.Generic;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HelloSocNetw_DAL.EFRepositories
{
    public class EfCountryRepository: ICountryRepository
    {
        private readonly SocNetwContext _context;

        public EfCountryRepository(DbContext dbContext) => _context = dbContext as SocNetwContext;

        public async Task<Country> GetCountryByIdAsync(int id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public async Task<IEnumerable<Country>> GetCountriesAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<int> GetCountOfCountriesAsync() => await _context.Countries.CountAsync();

        public void AddCountry(Country country) => _context.Countries.Add(country);

        public void AddCountries(IEnumerable<Country> countries) => _context.Countries.AddRange(countries);

        public void DeleteCountry(Country country) => _context.Countries.Remove(country);

        public async Task DeleteCountryByIdAsync(int id)
        {
            var country = await GetCountryByIdAsync(id);
            _context.Countries.Remove(country);
        }

        public void DeleteCountries(IEnumerable<Country> country) => _context.Countries.RemoveRange(country);

        public void UpdateUserInfo(Country country)
        {
            _context.Countries.Attach(country);
            _context.Entry(country).State = EntityState.Modified;
        }
    }
}