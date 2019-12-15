using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_PL.Infrastructure;
using HelloSocNetw_PL.Models;
using HelloSocNetw_PL.Models.CountryModels;
using HelloSocNetw_PL.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloSocNetw_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    [Authorize]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countrySvc;
        private readonly IMapper _mpr;
        private readonly ILogger _lgr;

        public CountriesController(ICountryService countryService, IMapper mapper, ILogger<CountriesController> logger)
        {
            _countrySvc = countryService;
            _mpr = mapper;
            _lgr = logger;
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
            _lgr.LogInformation(LoggingEvents.ListItems, "Getting All Countries");

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
            var countryWithSuchNameExists = await _countrySvc.CountryWithSuchNameExistsAsync(countryModel.CountryName);
            if (countryWithSuchNameExists)
            {
                _lgr.LogInformation(LoggingEvents.InsertItemConflict, "AddCountry() CONFLICT");

                return Conflict();
            }

            var countryDto = _mpr.Map<CountryDTO>(countryModel);

            var successfullyAdded = await _countrySvc.AddCountryAsync(countryDto);
            if (successfullyAdded)
            {
                _lgr.LogInformation(LoggingEvents.InsertItem, "Country is created");
                return NoContent();
            }
            else
            {
                _lgr.LogInformation(LoggingEvents.InsertItemBadRequest, "AddCountry() BAD REQUEST");
                return BadRequest();
            }
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

            var countryWithSuchNameExists = await _countrySvc.CountryWithSuchNameExistsAsync(countryModel.CountryName);
            if (countryWithSuchNameExists)
            {
                _lgr.LogInformation(LoggingEvents.UpdateItemConflict, "UpdateCountry({countryId}) CONFLICT", countryId);
                return Conflict();
            }

            var countryToUpdate = await _countrySvc.GetCountryByCountryIdAsync(countryModel.CountryId);
            if (countryToUpdate == null)
            {
                _lgr.LogWarning(LoggingEvents.UpdateItemNotFound, "UpdateCountry({countryId}) NOT FOUND", countryId);
                return NotFound();
            }

            var newCountryInfo = _mpr.Map<CountryDTO>(countryModel);

            var successfullyUpdated = await _countrySvc.UpdateCountryAsync(newCountryInfo);
            if (successfullyUpdated)
            {
                _lgr.LogWarning(LoggingEvents.UpdateItemBadRequest, "Country {countryId}) Updated", countryId);
                return NoContent();
            }
            else
            {
                _lgr.LogWarning(LoggingEvents.InsertItemBadRequest, "UpdateCountry({countryId}) BAD REQUEST", countryId);
                return BadRequest();
            }
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
            var countryToDeleteExists = await _countrySvc.CountryWithSuchCountryIdExistsAsync(countryId);
            if (!countryToDeleteExists)
            {
                 _lgr.LogWarning(LoggingEvents.DeleteItemNotFound, "DeleteCountry({countryId}) NOT FOUND", countryId);
                return NotFound();
            }

            if (await _countrySvc.DeleteCountryByCountryIdAsync(countryId))
            {
                _lgr.LogInformation(LoggingEvents.DeleteItem, "Country {countryId} is deleted", countryId);
                return NoContent();
            }
            else
            {
                _lgr.LogWarning(LoggingEvents.DeleteItemBadRequest, "DeleteCountry({countryId}) BAD REQUEST", countryId);
                return BadRequest();
            }
        }
    }
}