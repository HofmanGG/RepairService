using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_PL.Infrastructure;
using HelloSocNetw_PL.Infrastructure.Interfaces;
using HelloSocNetw_PL.Models;
using HelloSocNetw_PL.Models.CountryModels;
using HelloSocNetw_PL.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloSocNetw_PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CountriesController : ApiController
    {
        private readonly ICountryService _countrySvc;
        private readonly ILogger _lgr;
        private readonly IMapper _mpr;
        private readonly ICurrentUserService _curUserSvc;

        public CountriesController(
            ICountryService countryService,
            ILogger<CountriesController> logger,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _countrySvc = countryService;
            _lgr = logger;
            _mpr = mapper;
            _curUserSvc = currentUserService;
        }

        /// <summary>
        /// Gets all countries
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returnes all countries</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpGet]
        [ResponseCache(Duration = 3600)]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<CountryModel>>> GetCountries()
        {
            var countriesDto = await _countrySvc.GetCountriesAsync();
            var countryModels = _mpr.Map<IEnumerable<CountryModel>>(countriesDto);
            return countryModels.ToList();
        }

        /// <summary>
        /// Adds new country
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /
        ///     {
        ///        "countryName": "Russia"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">If country successfully changed</response>
        /// <response code="400">If countryModel is not valid</response>
        /// <response code="409">If country with such name already exists</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpPost]
        [ProducesResponseType(204), ProducesResponseType(400), ProducesResponseType(409)]
        public async Task<IActionResult> AddCountry(NewCountryModel countryModel)
        {
            var countryDto = _mpr.Map<CountryDTO>(countryModel);
            await _countrySvc.AddCountryAsync(countryDto);
            
            return NoContent();
        }

        /// <summary>
        /// Changes country
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /{id}
        ///     {
        ///        "countryId": 5,
        ///        "countryName": "Russia"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">If country successfully changed</response>
        /// <response code="400">If countryModel is not valid or countryModel.CountryId = id</response>
        /// <response code="404">If country with such id doesnot exist</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpPut("{countryId}")]
        [ProducesResponseType(204), ProducesResponseType(400), ProducesResponseType(404), ProducesResponseType(409)]
        public async Task<IActionResult> UpdateCountry(int countryId, UpdateCountryModel countryModel)
        {
            if (countryId != countryModel.CountryId)
                return BadRequest();

            var newCountryInfo = _mpr.Map<CountryDTO>(countryModel);
            await _countrySvc.UpdateCountryAsync(newCountryInfo);

            return NoContent();
        }

        /// <summary>
        /// Deletes country by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /{id}
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <response code="204">If country successfully deleted</response>
        /// <response code="400">If country is not deleted</response>
        /// <response code="404">If country with such id doesnot exist</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpDelete("{countryId}")]
        [ProducesResponseType(204), ProducesResponseType(400), ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCountry(int countryId)
        {
            await _countrySvc.DeleteCountryByCountryIdAsync(countryId);

            return NoContent();
        }
    }
}