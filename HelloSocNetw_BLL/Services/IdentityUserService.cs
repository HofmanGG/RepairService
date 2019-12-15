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
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace HelloSocNetw_BLL.Services
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IIdentityUnitOfWork _identityUow;
        private readonly IMapper _mpr;
        private readonly IJWTService _jwtService;

        public IdentityUserService(IUnitOfWork unitOfWork, IIdentityUnitOfWork identityUnitOfWork, IMapper mapper, IJWTService jwtService)
        {
            _uow = unitOfWork;
            _identityUow = identityUnitOfWork;
            _mpr = mapper;
            _jwtService = jwtService;
        }

        public async Task<bool> UserWithSuchEmailAlreadyExistsAsync(string email)
        {
            return await _identityUow.UserManager.FindByEmailAsync(email) != null;
        }

        public async Task<bool> CreateAccountAsync(UserInfoDTO userInfoDto, string email, string userName, string password)
        {
            var appIdentityUser = new AppIdentityUser() {Email = email, UserName = userName };
                
            var createOperationResult = await _identityUow.UserManager.CreateAsync(appIdentityUser, password);
            if (!createOperationResult.Succeeded)
                return false;

            var roleToAdd = "User";
            var addToRoleOperationResult = await _identityUow.UserManager.AddToRoleAsync(appIdentityUser, roleToAdd);
            if (!addToRoleOperationResult.Succeeded)
                return false;

            var userInfo = _mpr.Map<UserInfo>(userInfoDto); 
            userInfo.AppIdentityUser = appIdentityUser;

            _uow.UsersInfo.AddUserInfo(userInfo);
            var rowsAffected = await _uow.SaveChangesAsync();
            return rowsAffected == 1;
        }

        public async Task<AppIdentityUserDTO> Authenticate(string email, string password)
        {
            var userWithSuchEmail = await _identityUow.UserManager.FindByEmailAsync(email);
            if (userWithSuchEmail == null)
                return null;

            var userHasSuchPassword = await _identityUow.UserManager.CheckPasswordAsync(userWithSuchEmail, password);
            if (!userHasSuchPassword)
                return null;

            var userWithSuchEmailDto = _mpr.Map<AppIdentityUserDTO>(userWithSuchEmail);
            return userWithSuchEmailDto;
        }

        public async Task<AppIdentityUserDTO> FindByIdAsync(Guid appIdentityUserId)
        {
            var appIdentityUser = await _identityUow.UserManager.FindByIdAsync(appIdentityUserId.ToString());
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

        public async Task UpdateUserInfoAsync(UserInfoDTO userInfoDto)
        {
            var userInfo = _mpr.Map<UserInfo>(userInfoDto);
            _uow.UsersInfo.UpdateUserInfo(userInfo);

            await _uow.SaveChangesAsync();
        }

        public async Task<string> GenerateEmailConfirmationTokenAsyncByEmail(string email)
        {
            var user = await _identityUow.UserManager.FindByEmailAsync(email);
            var code = await _identityUow.UserManager.GenerateEmailConfirmationTokenAsync(user);
            return code;
        }

        public async Task<bool> ConfirmEmailAsync(string email, string code)
        {
            var user = await _identityUow.UserManager.FindByEmailAsync(email);

            var operationResult = await _identityUow.UserManager.ConfirmEmailAsync(user, code);
            return operationResult.Succeeded;
        }

        public async Task<bool> DeleteAccountAsync(AppIdentityUserDTO appIdentityUserDto)
        {
            var appIdentityUser = _mpr.Map<AppIdentityUser>(appIdentityUserDto);

            var operationResult = await _identityUow.UserManager.DeleteAsync(appIdentityUser);
            return operationResult.Succeeded;
        }

        public async Task<bool> DeleteAccountByAppIdentityUserIdAsync(Guid appIdentityUserId)
        {
            var appIdentityUser = await _identityUow.UserManager.FindByIdAsync(appIdentityUserId.ToString());

            var operationResult = await _identityUow.UserManager.DeleteAsync(appIdentityUser);
            return operationResult.Succeeded;
        }

        public async Task<bool> IsEmailConfirmedAsync(AppIdentityUserDTO appIdentityUserDto)
        {
            var appIdentityUser = _mpr.Map<AppIdentityUser>(appIdentityUserDto);

            return await _identityUow.UserManager.IsEmailConfirmedAsync(appIdentityUser);
        }

        public async Task<bool> IsLockedOutByEmailAsync(AppIdentityUserDTO appIdentityUserDto)
        {
            var appIdentityUser = _mpr.Map<AppIdentityUser>(appIdentityUserDto);

            var isLockedOut = await _identityUow.UserManager.IsLockedOutAsync(appIdentityUser);
            return isLockedOut;
        }
    }
}