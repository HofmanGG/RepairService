using HelloSocNetw_BLL.EntitiesDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloSocNetw_BLL.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<AppRoleDTO>> GetRolesAsync();

        Task<bool> CreateRole(string roleName);

        Task<bool> RoleWithSuchIdExistsAsync(Guid id);

        Task<bool> RoleWithSuchNameExistsAsync(string roleName);

        Task<bool> UpdateRoleAsync(AppRoleDTO roleToUpdateDto);

        Task<bool> DeleteRoleAsync(string roleId);
    }
}
