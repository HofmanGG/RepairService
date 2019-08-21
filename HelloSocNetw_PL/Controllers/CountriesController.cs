using AutoMapper;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloSocNetw_PL.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public CountriesController(ICountryService countryService, IMapper mapper)
        {
            _countryService = countryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> CountriesAsync()
        {
            var countriesDto =  await _countryService.GetCountriesAsync();
            var countryModels = _mapper.Map<IEnumerable<CountryModel>>(countriesDto);
            return Ok(countryModels);
        }
    }
}