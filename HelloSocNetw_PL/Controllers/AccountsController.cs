using System;
using System.Threading.Tasks;
using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_PL.Infrastructure;
using HelloSocNetw_PL.Infrastructure.Interfaces;
using HelloSocNetw_PL.Models;
using HelloSocNetw_PL.Models.UserInfoModels;
using HelloSocNetw_PL.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PL.Controllers
{
    [AllowAnonymous]
    public class AccountsController : ApiController
    {
        private readonly IIdentityUserService _identitySvc;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _lgr;
        private readonly IMapper _mpr;
        private readonly ICurrentUserService _curUserSvc;

        public AccountsController(
            IIdentityUserService identityService, 
            IEmailSender emailSender, 
            ILogger<AccountsController> logger, 
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _identitySvc = identityService;
            _emailSender = emailSender;
            _lgr = logger;
            _mpr = mapper;
            _curUserSvc = currentUserService;
        }

        /// <summary>
        /// Creates an account
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /signup
        ///     {
        ///        "email": "example@gmail.com",
        ///        "password": "p4ssW0Rd",
        ///        "confirmPassword": "p4ssW0Rd", 
        ///        "firstName": "Jason",
        ///        "lastName": "Smith",
        ///        "gender": "Male",
        ///        "countryId": 13,
        ///        "dayOfBirth": 11,
        ///        "monthOfBirth": 5,
        ///        "yearOfBirth": 2000
        ///     }
        ///
        /// </remarks>
        /// <response code="204">If an account successfully created</response>
        /// <response code="400">If registerModel is not valid</response>
        /// <response code="409">If such email already exists</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpPost("signup")]
        [ProducesResponseType(204), ProducesResponseType(400), ProducesResponseType(409)]
        public async Task<IActionResult> SignUp(RegisterModel registerModel)
        {
            var userInfoDto = _mpr.Map<UserInfoDTO>(registerModel);
            await _identitySvc.CreateAccountAsync(userInfoDto, registerModel.Email, registerModel.Email, registerModel.Password);

            var code = await _identitySvc.GenerateEmailConfirmationTokenAsyncByEmail(registerModel.Email);
            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "Accounts",
                new { registerModel.Email, code },
                protocol: HttpContext.Request.Scheme);

            await _emailSender.SendEmailAsync("samko.2000@ukr.net", "Confirm your account",
                $"Подтвердите регистрацию, перейдя по ссылке: <a href='{callbackUrl}'>link</a>");

            return NoContent();
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /signin
        ///     {
        ///        "email": "example@gmail.com",
        ///        "password": "p4ssW0Rd"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returns token and accounts info</response>
        /// <response code="400">If loginModel is not valid</response>
        /// <response code="401">If wrong password or email</response>
        /// <response code="403">If email is not confirmed</response>
        /// <response code="404">If account is bloked</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpPost("signin")]
        [ProducesResponseType(200), ProducesResponseType(400), ProducesResponseType(401), ProducesResponseType(403), ProducesResponseType(404)]
        public async Task<ActionResult<UserInfoModel>> SignIn(LoginModel loginModel)
        {
            var appIdentityUserDto = await _identitySvc.Authenticate(loginModel.Email, loginModel.Password);
            if (appIdentityUserDto == null)
                return Unauthorized();

            var isEmailConfirmed = await _identitySvc.IsEmailConfirmedAsync(appIdentityUserDto);
            if (!isEmailConfirmed)
                return Forbid();

            var isUserLockedOut = await _identitySvc.IsLockedOutByEmailAsync(appIdentityUserDto);
            if (isUserLockedOut)
                return NotFound();

            var userInfoDto = await _identitySvc.GetUserInfoWithTokenAsync(appIdentityUserDto);
            var userInfoModel = _mpr.Map<UserInfoModel>(userInfoDto);

            return userInfoModel;
        }

        //не используется, заменен на UserController.ChangeUserInfo
        /// <summary>
        /// Changes accounts info
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /changeinfo
        ///     {
        ///        "firstName": "Jason",
        ///        "lastName": "Smith",
        ///        "gender": "Male",
        ///        "countryId": 13,
        ///        "dayOfBirth": 11,
        ///        "monthOfBirth": 5,
        ///        "yearOfBirth": 2000
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returnes changed accounts info</response>
        /// <response code="400">If userInfoModel is not valid</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpPost("changeinfo")]
        [Authorize]
        [ProducesResponseType(200), ProducesResponseType(400)]
        public async Task<ActionResult<UserInfoModel>> ChangeUserInfo(UpdateUserInfoModel userInfoModel)
        {
            var authorizedUserId = _curUserSvc.UserId;

            var userInfoDto = _mpr.Map<UserInfoDTO>(userInfoModel);
            userInfoDto.AppIdentityUserId = authorizedUserId;
            await _identitySvc.UpdateUserInfoAsync(userInfoDto);

            var changedUserInfoDto = await _identitySvc.GetUserInfoByAppIdentityIdAsync(authorizedUserId);
            var changedUserInfoModel = _mpr.Map<UserInfoModel>(changedUserInfoDto);

            return changedUserInfoModel;
        }

        /// <summary>
        /// Deletes account
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /confirmemail
        ///     {
        ///        "email": "example@gmail.com",
        ///        "code": "alotofsymbols.............."
        ///     }
        ///
        /// </remarks>
        /// <response code="302">Redirectes to login page</response>
        /// <response code="500">If an exception on server is thrown</response>'
        /// 
        [HttpDelete("{appIdentityUserId}")]
        [Authorize(Roles = "Manager, Admin")]
        [ProducesResponseType(204), ProducesResponseType(400), ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAccount(Guid appIdentityUserId)
        {
            await _identitySvc.DeleteAccountByAppIdentityUserIdAsync(appIdentityUserId);

            return NoContent();
        }

        /// <summary>
        /// Confirms Email
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /confirmemail
        ///     {
        ///        "email": "example@gmail.com",
        ///        "code": "alotofsymbols.............."
        ///     }
        ///
        /// </remarks>
        /// <response code="302">Redirectes to login page</response>
        /// <response code="500">If an exception on server is thrown</response>'
        ///
        [HttpGet("confirmemail")]
        [ProducesResponseType(302), ProducesResponseType(400)]
        public async Task<IActionResult> ConfirmEmail(string email, string code)
        {
            var isEmailSuccessfullyConfirmed = await _identitySvc.ConfirmEmailAsync(email, code);
            if (isEmailSuccessfullyConfirmed)
                return Redirect("http://localhost:4200/login");
            else
                return BadRequest();
        }
    }
}