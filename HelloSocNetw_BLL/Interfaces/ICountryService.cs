using HelloSocNetw_BLL.EntitiesDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloSocNetw_BLL.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDTO>> GetCountriesAsync();
    }
}
