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

namespace PL.Controllers
{
    [Produces("application/json")]
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
        public async Task<IActionResult> SignUp([FromBody]RegisterModel registerModel)
        {
            var regValidator = new RegisterModelValidator();
            if (regValidator.Validate(registerModel).IsValid)
            {
                if (!await _userService.UserWithSuchEmailAlreadyExistsAsync(registerModel.Email))
                {
                    var userInfoDto = _mapper.Map<UserInfoDTO>(registerModel);
                    await _userService.CreateAccountAsync(userInfoDto, registerModel.Email, registerModel.Password);
                }
                return Conflict();
            }
            return UnprocessableEntity();
        }

        // POST: api/accounts/signin
        [Route("signin")]
        [HttpPost]
        public async Task<IActionResult> SignIn([FromBody]LoginModel loginModel)
        {
            var loginValidator = new LoginModelValidator();
            if (loginValidator.Validate(loginModel).IsValid)
            {
                if (await _userService.RightDataAsync(loginModel.Email, loginModel.Password))
                {
                    return Ok(await _userService.GetJwtTokenAsync(loginModel.Email));
                }
                return UnprocessableEntity();
            }
            return UnprocessableEntity();
        }
    }
}