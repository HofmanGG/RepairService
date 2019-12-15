using System;
using System.Threading.Tasks;
using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_PL.Infrastructure;
using HelloSocNetw_PL.Models;
using HelloSocNetw_PL.Models.UserInfoModels;
using HelloSocNetw_PL.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static HelloSocNetw_PL.Infrastructure.ControllerExtension;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    [AllowAnonymous]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class AccountsController : ControllerBase
    {
        private readonly IIdentityUserService _identitySvc;
        private readonly IMapper _mpr;
        private readonly IEmailSender _emailSender;
        private readonly ILogger _lgr;

        public AccountsController(IIdentityUserService identityService, IMapper mapper, IEmailSender emailSender, ILogger<AccountsController> logger)
        {
            _identitySvc = identityService;
            _mpr = mapper;
            _emailSender = emailSender;
            _lgr = logger;
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
            var doesUserWithSuchEmailAlreadyExists = await _identitySvc.UserWithSuchEmailAlreadyExistsAsync(registerModel.Email);
            if (doesUserWithSuchEmailAlreadyExists)
            {
                _lgr.LogInformation("SignUp() CONFLICT, user with {email} already exists", registerModel.Email);
                return Conflict();
            }

            var userInfoDto = _mpr.Map<UserInfoDTO>(registerModel);

            var isAccountSuccessfulyCreated = await _identitySvc.CreateAccountAsync(userInfoDto, registerModel.Email, registerModel.Email, registerModel.Password);
            if (!isAccountSuccessfulyCreated)
            {
                _lgr.LogWarning("SignUp() BAR REQUEST");
                return BadRequest();
            } 

            _lgr.LogInformation(LoggingEvents.InsertItem, "Account {email} is successfully created", registerModel.Email);

            var code = await _identitySvc.GenerateEmailConfirmationTokenAsyncByEmail(registerModel.Email);
            var callbackUrl = Url.Action(
                "ConfirmEmail",
                "Accounts",
                new { registerModel.Email, code },
                protocol: HttpContext.Request.Scheme);

            _lgr.LogInformation("Sending Email Confirmation Message to {email}", registerModel.Email);

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
            {
                _lgr.LogInformation("SignIn() UNAUTHORIZED, Wrong Credentials");
                return Unauthorized();
            }

            var isEmailConfirmed = await _identitySvc.IsEmailConfirmedAsync(appIdentityUserDto);
            if (!isEmailConfirmed)
            {
                _lgr.LogInformation("SignIn() FORBIDDEN, email {email} is not confirmed", loginModel.Email);
                return Forbid();
            }

            var isUserLockedOut = await _identitySvc.IsLockedOutByEmailAsync(appIdentityUserDto);
            if (isUserLockedOut)
            {
                _lgr.LogInformation("SignIn() NOT FOUND, the user {email} is locked out", loginModel.Email);
                return NotFound();
            }
             
            _lgr.LogInformation("User {email} is successfully logged in", loginModel.Email);

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
                var authorizedUserId = this.GetUserId();

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
            var appIdentityUser = await _identitySvc.FindByIdAsync(appIdentityUserId);
            if (appIdentityUser == null)
            {
                _lgr.LogWarning(LoggingEvents.DeleteItemNotFound, "DeleteAccount({appIdentityUserId}) NOT FOUND", appIdentityUserId);
                return NotFound();
            }
            
            var isAccountSuccessfullyDeleted = await _identitySvc.DeleteAccountByAppIdentityUserIdAsync(appIdentityUserId);
            if (isAccountSuccessfullyDeleted)
            {
                _lgr.LogInformation(LoggingEvents.DeleteItem, "Account {appIdentityUserId} is successfully deleted", appIdentityUserId);
                return NoContent();
            }
            else
            {
                _lgr.LogWarning(LoggingEvents.DeleteItemBadRequest, "DeleteAccount({appIdentityUserId}) BAD REQUEST", appIdentityUserId);
                return BadRequest();
            }
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
            {
                _lgr.LogInformation("Account {email} is successfully confirmed", email);
                return Redirect("http://localhost:4200/login");
            }
            else
            {
                _lgr.LogWarning("ConfirmEmail({email}, {code}) BAD REQUEST", email, code);
                return BadRequest();
            }
        }
    }
}