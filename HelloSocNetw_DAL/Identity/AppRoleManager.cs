using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities.IdentityEntities;
using HelloSocNetw_DAL.Interfaces.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HelloSocNetw_DAL.Identity
{
    public class AppRoleManager : IRoleManager
    {
        private readonly RoleManager<AppRole> _roleManager;

        public AppRoleManager(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<AppRole>> GetRolesAsync()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IdentityResult> CreateRoleAsync(AppRole newRole)
        {
            return await _roleManager.CreateAsync(newRole);
        }

        public async Task<IdentityResult> DeleteRoleAsync(AppRole roleToDelete)
        {
            return await  _roleManager.DeleteAsync(roleToDelete);
        }

        public async  Task<AppRole> FindByIdAsync(string roleId)
        {
            return await  _roleManager.FindByIdAsync(roleId);
        }

        public async Task<bool> RoleExistsByIdAsync(Guid id)
        {
            return await _roleManager.Roles.AnyAsync(r => r.Id == id);
        }

        public async  Task<bool> RoleExistsByNameAsync(string roleName)
        {
            return await  _roleManager.RoleExistsAsync(roleName);
        }

        public async  Task<IdentityResult> UpdateRoleAsync(AppRole role)
        {
            var roleToUpdate = await _roleManager.FindByIdAsync(role.Id.ToString());

            roleToUpdate.Name = role.Name;

            return await  _roleManager.UpdateAsync(roleToUpdate);
        }
    }
}