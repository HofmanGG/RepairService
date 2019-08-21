using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloSocNetw_BLL.Services
{
    public class CountryService: ICountryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CountryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CountryDTO>> GetCountriesAsync()
        {
            var countries = await _unitOfWork.Countries.GetCountriesAsync();
            var countriesDto = _mapper.Map<IEnumerable<CountryDTO>>(countries);
            return countriesDto;
        }
    }
}
