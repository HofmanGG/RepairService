using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities.IdentityEntities;
using HelloSocNetw_DAL.Interfaces.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HelloSocNetw_DAL.Identity
{
    public class AppUserManager: IUserManager
    {
        private readonly UserManager<AppIdentityUser> _userManager;

        public AppUserManager(UserManager<AppIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddToRoleAsync(AppIdentityUser identityUser, string roleToAdd)
        {
            var operationResult = await _userManager.AddToRoleAsync(identityUser, roleToAdd);
            return operationResult;
        }

        public async Task<bool> CheckIfUserExistsAsync(Expression<Func<AppIdentityUser, bool>> predicate)
        {
            var doesUserExists = await _userManager.Users.AnyAsync(predicate);
            return doesUserExists;
        }

        public async Task<bool> CheckPasswordAsync(AppIdentityUser identityUser, string password)
        {
            var isPasswordRight = await _userManager.CheckPasswordAsync(identityUser, password);
            return isPasswordRight;
        }

        public async Task<IdentityResult> ConfirmEmailAsync(AppIdentityUser identityUser, string code)
        {
            var operationResult = await _userManager.ConfirmEmailAsync(identityUser, code);
            return operationResult;
        }

        public async Task<IdentityResult> CreateAsync(AppIdentityUser identityUser, string password)
        {
            var operationResult =  await _userManager.CreateAsync(identityUser, password);
            return operationResult;
        }

        public Task<IdentityResult> DeleteAsync(AppIdentityUser identityUser)
        {
            var operationResult =  _userManager.DeleteAsync(identityUser);
            return operationResult;
        }

        public async Task<AppIdentityUser> FindByEmailAsync(string email)
        {
            var userWithSuchEmail = await _userManager.FindByEmailAsync(email);
            return userWithSuchEmail;
        }

        public async Task<AppIdentityUser> FindByIdAsync(string userId)
        {
            var userWithSuchId =  await _userManager.FindByIdAsync(userId);
            return userWithSuchId;
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(AppIdentityUser identityUser)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
            return token;
        }

        public async Task<bool> IsEmailConfirmedAsync(AppIdentityUser identityUser)
        {
            var isEmailConfirmed = await _userManager.IsEmailConfirmedAsync(identityUser);
            return isEmailConfirmed;
        }

        public async Task<bool> IsLockedOutAsync(AppIdentityUser identityUser)
        {
            var isUserLockedOut = await _userManager.IsLockedOutAsync(identityUser);
            return isUserLockedOut;
        }

        public async Task<IEnumerable<string>> GetRolesAsync(AppIdentityUser identityUser)
        {
            var roles =  await _userManager.GetRolesAsync(identityUser);
            return roles;
        }
    }
}