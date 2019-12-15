using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_DAL.Infrastructure.Exceptions;
using HelloSocNetw_PL.Infrastructure;
using HelloSocNetw_PL.Models;
using HelloSocNetw_PL.Models.UserInfoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static HelloSocNetw_PL.Infrastructure.ControllerExtension;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Manager, Admin")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class UsersController : ControllerBase
    {
        private readonly IUserInfoService _userInfoSvc;
        private readonly IMapper _mpr;
        private readonly ILogger _lgr;

        public UsersController(IUserInfoService userInfoService, IMapper mapper, ILogger<UsersController> logger)
        {
            _userInfoSvc = userInfoService;
            _mpr = mapper;
            _lgr = logger;
        }

        /// <summary>
        /// Returnes userInfo by userId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /{userId}
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returnes userInfo</response>
        /// <response code="404">If userInfo with such userId is not found</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpGet("{userInfoId}")]
        [ProducesResponseType(200), ProducesResponseType(404)]
        public async Task<ActionResult<UserInfoModel>> GetUser(int userInfoId)
        {
            _lgr.LogInformation(LoggingEvents.GetItem, "Getting userInfo {userInfoId}", userInfoId);
            var userInfoDto = await _userInfoSvc.GetUserInfoByUserInfoIdAsync(userInfoId);
            if (userInfoDto == null)
            {
                _lgr.LogWarning(LoggingEvents.GetItem, "GetUser({userInfoId}) NOT FOUND", userInfoId);
                return NotFound();
            }

            var userInfoModel = _mpr.Map<UserInfoModel>(userInfoDto);
            return userInfoModel;
        }

        /// <summary>
        /// Gets userInfoId by email
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /?email={email}
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returnes userInfoId</response>
        /// <response code="404">Returnes userInfo with such email is not found</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpGet("email")]
        [ProducesResponseType(200), ProducesResponseType(404)]
        public async Task<ActionResult<int>> GetUserInfoIdByEmail(string email)
        {
            _lgr.LogInformation(LoggingEvents.GetItem, "Getting userInfoId by email {email}", email);

            var id = await _userInfoSvc.GetUserInfoIdByEmailAsync(email);
            return id;
        }

        /// <summary>
        /// Gets count of all users
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /count
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returnes count of all users</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpGet("count")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<int>> GetCountOfUsers()
        {
            _lgr.LogInformation(LoggingEvents.GetItem, "Getting count of all users");
            return await _userInfoSvc.GetCountOfUsersInfoAsync();
        }

        /// <summary>
        /// Changes accounts info
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /count
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returnes total count of users</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpPut("{userInfoId}")]
        [AllowAnonymous]
        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(403), ProducesResponseType(404)]
        public async Task<ActionResult<UserInfoModel>> ChangeUserInfo(int userInfoId, UpdateUserInfoModel userInfoModel)
        {
            if (!User.Identity.IsAuthenticated)
                return Forbid();

            if (userInfoId != userInfoModel.UserInfoId)
                return BadRequest();

            var authorizedUserId = this.GetUserId();

            var userInfoToUpdate = await _userInfoSvc.GetUserInfoByAppIdentityIdAsync(authorizedUserId);
            if (userInfoToUpdate == null)
            {
                _lgr.LogWarning(LoggingEvents.UpdateItemNotFound, "ChangeUserInfo({userInfoId}) NOT FOUND", userInfoId);
                return NotFound();
            }

            var newUserInfoDto = _mpr.Map<UserInfoDTO>(userInfoModel);

            var successfullyUpdated = await _userInfoSvc.UpdateUserInfoAsync(newUserInfoDto);
            if (successfullyUpdated)
            {
                _lgr.LogInformation(LoggingEvents.UpdateItem, "User Info {userInfoId} is changed", userInfoId);

                var changedUserInfoDto = await _userInfoSvc.GetUserInfoByUserInfoIdAsync(userInfoId);
                var changedUserInfoModel = _mpr.Map<UserInfoModel>(changedUserInfoDto);

                return changedUserInfoModel;
            }
            else
            {
                _lgr.LogWarning(LoggingEvents.UpdateItemBadRequest, "ChangeUserInfo({userInfoId}) BAD REQUEST", userInfoId);
                return BadRequest();
            }
        }

        //не используется, заменен Accounts.DeleteAccount
        [HttpDelete("{userInfoId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(204), ProducesResponseType(400), ProducesResponseType(404)]
        public async Task<IActionResult> DeleteUser(int userInfoId)
        {
            var userInfoExists = await _userInfoSvc.UserInfoExistsAsync(userInfoId);
            if (!userInfoExists)
                return NotFound();

            if (await _userInfoSvc.DeleteUserInfoByUserIdAsync(userInfoId))
                return NoContent();
            else
                return BadRequest();
        }
    }   
}