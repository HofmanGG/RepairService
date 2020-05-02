using HelloSocNetw_BLL.EntitiesDTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloSocNetw_BLL.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<AppRoleDTO>> GetRolesAsync();

        Task CreateRoleAsync(string roleName);

        Task UpdateRoleAsync(AppRoleDTO roleToUpdateDto);

        Task DeleteRoleAsync(Guid roleId);
    }
}
