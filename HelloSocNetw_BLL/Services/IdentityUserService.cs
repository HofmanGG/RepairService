using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Infrastructure;
using HelloSocNetw_BLL.Infrastructure.Exceptions;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Infrastructure.Exceptions;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HelloSocNetw_BLL.Services
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IMapper _mpr;
        private readonly IJWTService _jwtService;

        public IdentityUserService(
            IUnitOfWork unitOfWork,
            UserManager<AppIdentityUser> userManager,
            RoleManager<AppRole> roleManager,
            IMapper mapper,
            IJWTService jwtService)
        {
            _uow = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
            _mpr = mapper;
            _jwtService = jwtService;
        }

        public async Task<bool> UserWithSuchEmailAlreadyExistsAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        public async Task CreateAccountAsync(UserInfoDTO userInfoDto, string email, string userName, string password)
        {
            var doesAccountWithSuchEmailExists = await _userManager.Users.AnyAsync(u => u.Email == email);
            if (doesAccountWithSuchEmailExists)
                throw new ConflictException(nameof(AppIdentityUser), email);

            var appIdentityUser = new AppIdentityUser() {Email = email, UserName = userName };
            var createOperationResult = await _userManager.CreateAsync(appIdentityUser, password);
            if (!createOperationResult.Succeeded)
                throw new DBOperationException("Account creation went wrong");

            var roleToAdd = "User";
            var addToRoleOperationResult = await _userManager.AddToRoleAsync(appIdentityUser, roleToAdd);
            if (!addToRoleOperationResult.Succeeded)
                throw new DBOperationException("Adding role to user went wrong");

            var userInfo = _mpr.Map<UserInfo>(userInfoDto); 
            userInfo.AppIdentityUser = appIdentityUser;

            _uow.UsersInfo.AddUserInfo(userInfo);
            var rowsAffected = await _uow.SaveChangesAsync();
            if (rowsAffected != 1)
                throw new DBOperationException("User info creatiog went wrong");
        }

        public async Task<AppIdentityUserDTO> Authenticate(string email, string password)
        {
            var userWithSuchEmail = await _userManager.FindByEmailAsync(email);
            if (userWithSuchEmail == null)
                return null;

            var userHasSuchPassword = await _userManager.CheckPasswordAsync(userWithSuchEmail, password);
            if (!userHasSuchPassword)
                return null;

            var userWithSuchEmailDto = _mpr.Map<AppIdentityUserDTO>(userWithSuchEmail);
            return userWithSuchEmailDto;
        }

        public async Task<AppIdentityUserDTO> FindByIdAsync(Guid appIdentityUserId)
        {
            var appIdentityUser = await _userManager.FindByIdAsync(appIdentityUserId.ToString());
            var appIdentityUserDto = _mpr.Map<AppIdentityUserDTO>(appIdentityUser);
            return appIdentityUserDto;
        }

        public async Task<UserInfoDTO> GetUserInfoWithTokenAsync(AppIdentityUserDTO appIdentityUserDto)
        {
            var userInfo = await _uow.UsersInfo.GetUserInfoByUserInfoIdAsync(appIdentityUserDto.UserInfoId);

            var userInfoDto = _mpr.Map<UserInfoDTO>(userInfo);
            var appIdentityUser = _mpr.Map<AppIdentityUser>(appIdentityUserDto);

            userInfoDto.Token = await _jwtService.GetJwtTokenAsync(appIdentityUser);
            return userInfoDto;
        }

        public async Task<UserInfoDTO> GetUserInfoByAppIdentityIdAsync(Guid appIdentityUserId)
        {
            var userInfo = await _uow.UsersInfo.GetUserInfoByAppIdentityIdAsync(appIdentityUserId);
            var userInfoDto = _mpr.Map<UserInfoDTO>(userInfo);

            return userInfoDto;
        }

        //не используется, заменен на UserInfoService.UpdateUserInfoAsync()
        public async Task UpdateUserInfoAsync(UserInfoDTO userInfoDto)
        {
            var userInfo = _mpr.Map<UserInfo>(userInfoDto);
            _uow.UsersInfo.UpdateUserInfo(userInfo);

            await _uow.SaveChangesAsync();
        }

        public async Task<string> GenerateEmailConfirmationTokenAsyncByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return code;
        }

        public async Task<bool> ConfirmEmailAsync(string email, string code)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var operationResult = await _userManager.ConfirmEmailAsync(user, code);
            return operationResult.Succeeded;
        }

        public async Task DeleteAccountAsync(AppIdentityUserDTO appIdentityUserDto)
        {
            var appIdentityUser = _mpr.Map<AppIdentityUser>(appIdentityUserDto);

            var operationResult = await _userManager.DeleteAsync(appIdentityUser);
            if (!operationResult.Succeeded)
                throw new DBOperationException("Account deleting went wrong");
        }

        public async Task DeleteAccountByAppIdentityUserIdAsync(Guid appIdentityUserId)
        {
            var appIdentityUser = await _userManager.FindByIdAsync(appIdentityUserId.ToString());
            if (appIdentityUser == null)
                throw new NotFoundException(nameof(AppIdentityUser), appIdentityUserId);

            var operationResult = await _userManager.DeleteAsync(appIdentityUser);
            if (!operationResult.Succeeded)
                throw new DBOperationException("Account deleting went wrong");
        }

        public async Task<bool> IsEmailConfirmedAsync(AppIdentityUserDTO appIdentityUserDto)
        {
            var appIdentityUser = _mpr.Map<AppIdentityUser>(appIdentityUserDto);

            return await _userManager.IsEmailConfirmedAsync(appIdentityUser);
        }

        public async Task<bool> IsLockedOutByEmailAsync(AppIdentityUserDTO appIdentityUserDto)
        {
            var appIdentityUser = _mpr.Map<AppIdentityUser>(appIdentityUserDto);

            var isLockedOut = await _userManager.IsLockedOutAsync(appIdentityUser);
            return isLockedOut;
        }
    }
}