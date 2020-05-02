using System;
using System.Threading.Tasks;
using AutoMapper;
using BLL.ModelsDTO;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Infrastructure;
using HelloSocNetw_BLL.Infrastructure.Exceptions;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Entities.IdentityEntities;
using HelloSocNetw_DAL.Interfaces;
using HelloSocNetw_DAL.Interfaces.Identity;

namespace HelloSocNetw_BLL.Services
{
    public class IdentityUserService : IIdentityUserService
    {
        private readonly IUnitOfWork _uow;
        private readonly IUserManager _userManager;
        private readonly IMapper _mpr;
        private readonly IJWTService _jwtService;

        public IdentityUserService(
            IUnitOfWork unitOfWork,
            IUserManager userManager,
            IMapper mapper,
            IJWTService jwtService)
        {
            _uow = unitOfWork;
            _userManager = userManager;
            _mpr = mapper;
            _jwtService = jwtService;
        }

        public async Task<bool> UserWithSuchEmailAlreadyExistsAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }

        public async Task CreateAccountAsync(UserInfoDTO userInfoDto, string email, string userName, string password)
        {
            var accountWithSuchEmailExists = await _userManager.CheckIfUserExistsAsync(u => u.Email == email);
            if (accountWithSuchEmailExists)
                throw new ConflictException(nameof(AppIdentityUser), email);
            
            var identityUser = new AppIdentityUser {Email = email, UserName = userName };
            var createOperationResult = await _userManager.CreateAsync(identityUser, password);
            if (!createOperationResult.Succeeded)
                throw new DBOperationException("Account creation went wrong");

            var roleToAdd = "User";
            var addToRoleOperationResult = await _userManager.AddToRoleAsync(identityUser, roleToAdd);
            if (!addToRoleOperationResult.Succeeded)
                throw new DBOperationException("Adding role to user went wrong");

            var userInfo = _mpr.Map<UserInfo>(userInfoDto); 
            userInfo.AppIdentityUser = identityUser;

            _uow.UsersInfo.AddUserInfo(userInfo);
            var rowsAffected = await _uow.SaveChangesAsync();
            if (rowsAffected != 1)
                throw new DBOperationException("User info creating went wrong");
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

        public async Task<AppIdentityUserDTO> FindByIdAsync(Guid identityId)
        {
            var identityUser = await _userManager.FindByIdAsync(identityId.ToString());
            var identityUserDto = _mpr.Map<AppIdentityUserDTO>(identityUser);
            return identityUserDto;
        }

        public async Task<UserInfoDTO> GetUserInfoWithTokenAsync(AppIdentityUserDTO identityUserDto)
        {
            var userInfo = await _uow.UsersInfo.GetUserInfoByIdentityIdAsync(identityUserDto.Id);

            var userInfoDto = _mpr.Map<UserInfoDTO>(userInfo);
            var appIdentityUser = _mpr.Map<AppIdentityUser>(identityUserDto);

            userInfoDto.Token = await _jwtService.GetJwtTokenAsync(appIdentityUser);
            return userInfoDto;
        }

        public async Task<UserInfoDTO> GetUserInfoByIdentityIdAsync(Guid identityId)
        {
            var userInfo = await _uow.UsersInfo.GetUserInfoByIdentityIdAsync(identityId);
            var userInfoDto = _mpr.Map<UserInfoDTO>(userInfo);
            return userInfoDto;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsyncByEmail(string email)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
            return token;
        }

        public async Task<bool> ConfirmEmailAsync(string email, string code)
        {
            var identityUser = await _userManager.FindByEmailAsync(email);

            var operationResult = await _userManager.ConfirmEmailAsync(identityUser, code);
            return operationResult.Succeeded;
        }

        public async Task DeleteAccountAsync(AppIdentityUserDTO identityUserDto)
        {
            var identityUser = _mpr.Map<AppIdentityUser>(identityUserDto);

            var operationResult = await _userManager.DeleteAsync(identityUser);
            if (!operationResult.Succeeded)
                throw new DBOperationException("Account deleting went wrong");
        }

        public async Task DeleteAccountByIdentityIdAsync(Guid identityId)
        {
            var identityUser = await _userManager.FindByIdAsync(identityId.ToString());
            if (identityUser == null)
                throw new NotFoundException(nameof(AppIdentityUser), identityId);

            var operationResult = await _userManager.DeleteAsync(identityUser);
            if (!operationResult.Succeeded)
                throw new DBOperationException("Account deleting went wrong");
        }

        public async Task<bool> IsEmailConfirmedAsync(AppIdentityUserDTO identityUserDto)
        {
            var identityUser = _mpr.Map<AppIdentityUser>(identityUserDto);

            return await _userManager.IsEmailConfirmedAsync(identityUser);
        }

        public async Task<bool> IsLockedOutByEmailAsync(AppIdentityUserDTO identityUserDto)
        {
            var identityUser = _mpr.Map<AppIdentityUser>(identityUserDto);

            var isLockedOut = await _userManager.IsLockedOutAsync(identityUser);
            return isLockedOut;
        }
    }
}