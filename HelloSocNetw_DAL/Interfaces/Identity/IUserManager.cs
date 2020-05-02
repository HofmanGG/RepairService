using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities.IdentityEntities;

namespace HelloSocNetw_DAL.Interfaces.Identity
{
    public interface IUserManager
    {
        Task<IdentityResult> AddToRoleAsync(AppIdentityUser appIdentityUser, string roleToAdd);

        Task<IdentityResult> ConfirmEmailAsync(AppIdentityUser user, string code);

        Task<IdentityResult> CreateAsync(AppIdentityUser appIdentityUser, string password);

        Task<IdentityResult> DeleteAsync(AppIdentityUser appIdentityUser);

        Task<AppIdentityUser> FindByEmailAsync(string email);

        Task<AppIdentityUser> FindByIdAsync(string userId);

        Task<string> GenerateEmailConfirmationTokenAsync(AppIdentityUser user);

        Task<bool> IsEmailConfirmedAsync(AppIdentityUser appIdentityUser);

        Task<bool> IsLockedOutAsync(AppIdentityUser appIdentityUser);

        Task<bool> CheckPasswordAsync(AppIdentityUser appIdentityUser, string password);

        Task<bool> CheckIfUserExistsAsync(Expression<Func<AppIdentityUser, bool>> predicate);

        Task<IEnumerable<string>> GetRolesAsync(AppIdentityUser appIdentityUser);
    }
}