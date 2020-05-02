using HelloSocNetw_BLL.EntitiesDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloSocNetw_BLL.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDTO>> GetCountriesAsync();

        Task<CountryDTO> GetCountryByIdAsync(long countryId);

        Task AddCountryAsync(CountryDTO countryDto);

        Task UpdateCountryAsync(CountryDTO countryDto);

        Task DeleteCountryByIdAsync(long countryId);
    }
}
