using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_PL.Infrastructure;
using HelloSocNetw_PL.Infrastructure.Interfaces;
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
    [Authorize(Roles = "Manager, Admin")]
    public class RepairRequestsController : ApiController
    {
        private readonly IRepairRequestsService _repReqSvc;
        private readonly ILogger _lgr;
        private readonly IMapper _mpr;
        private readonly ICurrentUserService _curUserSvc;

        public RepairRequestsController(
            IRepairRequestsService repairRequestsService, 
            ILogger<RepairRequestsController> logger,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _repReqSvc = repairRequestsService;
            _lgr = logger;
            _mpr = mapper;
            _curUserSvc = currentUserService;
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
            if (!User.Identity.IsAuthenticated)
                return Forbid();
            
            IEnumerable<RepairRequestDTO> repairRequestsDto;
            if (User.IsInRole("Manager") || User.IsInRole("Admin"))
            {
                repairRequestsDto = await _repReqSvc.GetRepairRequestsByUserInfoIdAsync(userId);
            }
            else
            {
                var identityUserId = _curUserSvc.UserId;
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

            await _repReqSvc.AddRepairRequestAsync(repairRequestDto);

            return NoContent();
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

            var repairRequestDto = _mpr.Map<RepairRequestDTO>(repairRequestModel);
            repairRequestDto.UserInfoId = userId;
            repairRequestDto.RepairRequestId = repairRequestId;

            await _repReqSvc.UpdateRepairRequestAsync(repairRequestDto);

            return NoContent();
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
            await _repReqSvc.DeleteRepairRequestByRepairRequestIdAsync(repairRequestId);

            return NoContent();
        }
    }
}
