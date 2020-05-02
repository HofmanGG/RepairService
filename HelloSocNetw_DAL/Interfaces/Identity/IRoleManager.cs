using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities.IdentityEntities;

namespace HelloSocNetw_DAL.Interfaces.Identity
{
    public interface IRoleManager
    {
        Task<IEnumerable<AppRole>> GetRolesAsync();
        Task<AppRole> FindByIdAsync(string roleId);

        Task<IdentityResult> CreateRoleAsync(AppRole newRole);

        Task<IdentityResult> UpdateRoleAsync(AppRole role);

        Task<IdentityResult> DeleteRoleAsync(AppRole roleToDelete);

        Task<bool> RoleExistsByIdAsync(Guid id);
        Task<bool> RoleExistsByNameAsync(string roleName);
    }
}