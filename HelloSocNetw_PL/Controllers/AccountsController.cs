using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http.Results;
using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_PL.Models;
using HelloSocNetw_PL.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static HelloSocNetw_PL.Infrastructure.ControllerExtensions;

namespace PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController] 
    [AllowAnonymous]
    public class AccountsController : ControllerBase
    {
        private readonly IIdentityUserService _userService;
        private readonly IMapper _mapper;

        public AccountsController(IIdentityUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        // POST: api/accounts/signup
        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> SignUp(RegisterModel registerModel)
        {
            var regValidator = new RegisterModelValidator();
            if (regValidator.Validate(registerModel).IsValid)
            {
                if (!await _userService.UserWithSuchEmailAlreadyExistsAsync(registerModel.Email))
                {
                    var userInfoDto = _mapper.Map<UserInfoDTO>(registerModel);
                    await _userService.CreateAccountAsync(userInfoDto, registerModel.Email, registerModel.Password);
                    return Ok();
                }
                return Conflict();
            }
            return UnprocessableEntity();
        }

        // POST: api/accounts/signin
        [Route("signin")]
        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> SignIn(LoginModel loginModel)
        {
            var loginValidator = new LoginModelValidator();
            if (loginValidator.Validate(loginModel).IsValid)
            {
                var userInfoDto = await _userService.Authenticate(loginModel.Email, loginModel.Password);
                if(userInfoDto != null)
                {
                    var userInfoModel = _mapper.Map<UserInfoModel>(userInfoDto);
                    var token = await _userService.GetJwtTokenAsync(loginModel.Email);

                    userInfoModel.Token = token;

                    return Ok(userInfoModel);
                }
                return UnprocessableEntity();
            }
            return UnprocessableEntity();
        }

        [HttpPost]
        [Authorize]
        [Route("changeinfo")]
        [Produces("application/json")]
        public async Task<IActionResult> ChangeUserInfoAsync(UserInfoModel userInfoModel)
        {
            var userId = this.GetUserId();

            var userInfoDto = _mapper.Map<UserInfoDTO>(userInfoModel);
            userInfoDto.UserInfoId = userId;

            await _userService.UpdateUserInfoAsync(userInfoDto);
            var changedUserInfoDto = await _userService.GetUserInfoByIdAsync(userId);

            var changedUserInfoModel = _mapper.Map<UserInfoModel>(changedUserInfoDto);

            var email = await _userService.GetUserEmailByIdAsync(userId);
            var token = await _userService.GetJwtTokenAsync(email);
            changedUserInfoModel.Token = token;

            return Ok(changedUserInfoModel);
        }
    }
}