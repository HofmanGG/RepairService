using System.Threading.Tasks;
using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_PL.Infrastructure;
using HelloSocNetw_PL.Interfaces;
using HelloSocNetw_PL.Models.UserInfoModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HelloSocNetw_PL.Controllers
{
    public class UsersController : ApiController
    {
        private readonly IUserInfoService _userInfoSvc;
        private readonly ILogger _lgr;
        private readonly IMapper _mpr;
        private readonly ICurrentUserService _curUserSvc;

        public UsersController(
            IUserInfoService userInfoService, 
            ILogger<UsersController> logger,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _userInfoSvc = userInfoService;
            _lgr = logger;
            _mpr = mapper;
            _curUserSvc = currentUserService;
        }

        /// <summary>
        /// Returns userInfo by userId
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /{userId}
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns userInfo</response>
        /// <response code="404">If userInfo with such userId is not found</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpGet("{userInfoId}")]
        [ProducesResponseType(200), ProducesResponseType(404)]
        public async Task<ActionResult<UserInfoModel>> GetUser(long userInfoId)
        {
            var userInfoDto = await _userInfoSvc.GetUserInfoByIdAsync(userInfoId);
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
        /// <response code="200">Returns userInfoId</response>
        /// <response code="404">Returns userInfo with such email is not found</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpGet("email")]
        [ProducesResponseType(200), ProducesResponseType(404)]
        public async Task<ActionResult<long>> GetUserInfoIdByEmail(string email)
        {
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
        /// <response code="200">Returns count of all users</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpGet("count")]
        [ProducesResponseType(200)]
        public async Task<ActionResult<long>> GetCountOfUsers()
        {
            var count =  await _userInfoSvc.GetCountOfUsersInfoAsync();
            return count;
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
        /// <response code="200">Returns total count of users</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpPut("{userInfoId}")]
        [AllowAnonymous]
        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(403), ProducesResponseType(404)]
        public async Task<ActionResult<UserInfoModel>> ChangeUserInfo(long userInfoId, UpdateUserInfoModel userInfoModel)
        {
            if (!User.Identity.IsAuthenticated)
                return Forbid();

            if (userInfoId != userInfoModel.Id)
                return BadRequest();

            var authorizedUserId = _curUserSvc.UserId;

            var newUserInfoDto = _mpr.Map<UserInfoDTO>(userInfoModel);
            newUserInfoDto.AppIdentityUserId = authorizedUserId;

            await _userInfoSvc.UpdateUserInfoAsync(newUserInfoDto);

            var changedUserInfoDto = await _userInfoSvc.GetUserInfoByIdAsync(userInfoId);
            var changedUserInfoModel = _mpr.Map<UserInfoModel>(changedUserInfoDto);

            return changedUserInfoModel;
        }
    }   
}