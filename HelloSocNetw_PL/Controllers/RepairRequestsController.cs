using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_PL.Infrastructure;
using HelloSocNetw_PL.Models;
using HelloSocNetw_PL.Models.RepairRequestModels;
using HelloSocNetw_PL.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelloSocNetw_PL.Controllers
{
    [Route("/api/users/{userId}/repairrequests")]
    [ApiController]
    [ValidateModel]
    [Authorize(Roles = "Manager, Admin")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class RepairRequestsController : Controller
    {
        private readonly IRepairRequestsService _repReqSvc;
        private readonly IMapper _mpr;
        private readonly ILogger _lgr;

        public RepairRequestsController(IRepairRequestsService repairRequestsService, IMapper mapper, ILogger<RepairRequestsController> logger)
        {
            _repReqSvc = repairRequestsService;
            _mpr = mapper;
            _lgr = logger;
        }

        /// <summary>
        /// Returnes repair requests
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /{userId}
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returnes repair requests</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        public async Task<ActionResult<IEnumerable<RepairRequestModel>>> GetRepairRequestsByUserInfoIdAsync(int userId)
        {
            _lgr.LogInformation(LoggingEvents.ListItems, "Getting All Repair Requests of user {userId}", userId);

            if (!User.Identity.IsAuthenticated)
                return Forbid();
            
            IEnumerable<RepairRequestDTO> repairRequestsDto;
            if (User.IsInRole("Manager") || User.IsInRole("Admin"))
            {
                repairRequestsDto = await _repReqSvc.GetRepairRequestsByUserInfoIdAsync(userId);
            }
            else
            {
                var identityUserId = this.GetUserId();
                repairRequestsDto = await _repReqSvc.GetRepairRequestsByUserInfoIdAndAppIdentityUserIdAsync(userId, identityUserId);
            }

            var repairRequestsModels = _mpr.Map<IEnumerable<RepairRequestModel>>(repairRequestsDto);
            return repairRequestsModels.ToList();
        }

        /// <summary>
        /// Returnes count of repair requests
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET/ /count
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returnes count</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpGet("count")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<int>> GetCountOfRepairRequestsByUserInfoIdAsync(int userId)
        {
            _lgr.LogInformation(LoggingEvents.ListItems, "Getting Count of all Repair Requests of user {userId}", userId);

            var count = await _repReqSvc.GetCountOfRepairRequestsByUserInfoIdAsync(userId);
            return count;
        }

        /// <summary>
        /// Creates repair request for user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /
        ///     {
        ///        "productName": "IPhone 6",
        ///        "comment": "Broken glass",
        ///        "repairStatus": "In Progress",
        ///     }
        ///
        /// </remarks>
        /// <response code="204">If repair request is successfully created</response>
        /// <response code="400">If model is not valid</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpPost]
        [ProducesResponseType(204), ProducesResponseType(400)]
        public async Task<IActionResult> AddRepairRequestAsync(int userId, NewRepairRequestModel repairRequestModel)
        {
            var repairRequestDto = _mpr.Map<RepairRequestDTO>(repairRequestModel);
            repairRequestDto.UserInfoId = userId;

            var successfullyAdded = await _repReqSvc.AddRepairRequestAsync(repairRequestDto);
            if (successfullyAdded)
            {
                _lgr.LogInformation(LoggingEvents.InsertItem, "Repair Request successfully added for user {userId}", userId);
                return NoContent();
            }
            else
            {
                _lgr.LogWarning(LoggingEvents.InsertItemBadRequest, "AddRepairRequestAsync{userId} BAD REQUEST", userId);
                return BadRequest();
            }
        }

        /// <summary>
        /// Updates repair request
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /{repairRequestId}
        ///     {
        ///        "repairRequestId": 5,
        ///        "productName": "IPhone 6",
        ///        "comment": "Broken glass",
        ///        "repairStatus": "In Progress",
        ///     }
        ///
        /// </remarks>
        /// <response code="204">If repair request is successfully updated</response>
        /// <response code="400">If model is not valid</response>
        /// <response code="404">If repair request with repairRequestId is not found</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpPut("{repairRequestId}")]
        [ProducesResponseType(204), ProducesResponseType(400), ProducesResponseType(404)]
        public async Task<IActionResult> UpdateRepairRequestAsync(int userId, int repairRequestId, UpdateRepairRequestModel repairRequestModel)
        {
            if (repairRequestId != repairRequestModel.RepairRequestId)
                return BadRequest();

            var repReqToUpdate = await _repReqSvc.GetRepairRequestByRepairRequestIdAndUserInfoIdAsync(repairRequestId, userId);
            if (repReqToUpdate == null)
            {
                _lgr.LogWarning(LoggingEvents.UpdateItemNotFound, "UpdateRepairRequestAsync({userId}, {repairRequestId} NOT FOUND", userId, repairRequestId);
                return NotFound();
            }

            var repairRequestDto = _mpr.Map<RepairRequestDTO>(repairRequestModel);

            var successfullyUpdated = await _repReqSvc.UpdateRepairRequestAsync(repairRequestDto);
            if (successfullyUpdated)
            {
                _lgr.LogInformation(LoggingEvents.UpdateItem, "Repair Request {repairRequestId} successfully updated for user {userId}", repairRequestId, userId);
                return NoContent();
            }
            else
            {
                _lgr.LogWarning(LoggingEvents.UpdateItemBadRequest, "UpdateRepairRequestAsync({userId}, {repairRequestId} BAD REQUEST", repairRequestId, userId);
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes repair request 
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /{repairRequestId}
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <response code="204">If repair request is successfullt deleted</response>
        /// <response code="400">If repair request</response>
        /// <response code="404">If repair request is not deleted</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpDelete("{repairRequestId}")]
        [ProducesResponseType(204), ProducesResponseType(400), ProducesResponseType(404)]
        public async Task<IActionResult> DeleteRepairRequestByIdAsync(int userId, int repairRequestId)
        {
            var repairRequestExists = await _repReqSvc.RepairRequestWithSuchRepairRequestIdAndUserInfoIdExistsAsync(repairRequestId, userId);
            if (!repairRequestExists)
            {
                _lgr.LogWarning(LoggingEvents.DeleteItemNotFound, "DeleteRepairRequestByIdAsync({userId}, {repairRequestId}) NOT FOUND", userId, repairRequestId);
                return NotFound();
            }

            if (await _repReqSvc.DeleteRepairRequestByRepairRequestIdAsync(repairRequestId))
            {
                _lgr.LogInformation(LoggingEvents.DeleteItem, "Repair Request {repairRequestId} of user {userId} is successfully deleted", userId, repairRequestId);
                return NoContent();
            }
            else
            {
                _lgr.LogWarning(LoggingEvents.DeleteItemBadRequest, "DeleteRepairRequestByIdAsync({userId}, {repairRequestId}) BAD REQUEST", userId, repairRequestId);
                return BadRequest();
            }
        }
    }
}
