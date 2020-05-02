 using System;
 using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Infrastructure;
using HelloSocNetw_BLL.Infrastructure.Exceptions;
using HelloSocNetw_BLL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using HelloSocNetw_DAL.Entities.IdentityEntities;using HelloSocNetw_DAL.Interfaces.Identity;
 using Microsoft.AspNetCore.Identity;

 namespace HelloSocNetw_BLL.Services
{
    public class RoleService: IRoleService
    {
        private readonly IRoleManager _roleMgr;
        private readonly IMapper _mpr;

        public RoleService(IRoleManager roleManager, IMapper mapper)
        {
            _roleMgr = roleManager;
            _mpr = mapper;
        }

        public async Task<IEnumerable<AppRoleDTO>> GetRolesAsync()
        {
            var roles = await _roleMgr.GetRolesAsync();
            var rolesDto = _mpr.Map<IEnumerable<AppRoleDTO>>(roles);
            return rolesDto;
        }

        public async Task CreateRoleAsync(string roleName)
        {
            await ThrowIfRoleWithSuchNameExistsAsync(roleName);

            var newRole = new AppRole { Name = roleName };

            var operationResult = await _roleMgr.CreateRoleAsync(newRole);
            ThrowIfFailed(operationResult);
        }

        private async Task ThrowIfRoleWithSuchNameExistsAsync(string name)
        {
            var roleWithSuchNameExists = await _roleMgr.RoleExistsByNameAsync(name);
            if (roleWithSuchNameExists)
                throw new ConflictException(nameof(AppRole), name);
        }

        private async Task ThrowIfRoleWithIdDoesNotExistAsync(Guid id)
        {
            var roleExists = await _roleMgr.RoleExistsByIdAsync(id);
            if (!roleExists)
                throw new NotFoundException(nameof(AppRole), id);
        }

        public async Task UpdateRoleAsync(AppRoleDTO roleToUpdateDto)
        {
            await ThrowIfRoleWithIdDoesNotExistAsync(roleToUpdateDto.Id);

            await ThrowIfRoleWithSuchNameExistsAsync(roleToUpdateDto.Name);

            var role = _mpr.Map<AppRole>(roleToUpdateDto);

            var operationResult = await _roleMgr.UpdateRoleAsync(role);
            ThrowIfFailed(operationResult);
        }

        private void ThrowIfFailed(IdentityResult operationResult)
        {
            if (!operationResult.Succeeded)
            {
                var message = "";
                foreach (var error in operationResult.Errors)
                {
                    message += error.Description;
                }

                throw new DBOperationException(message);
            }
        }

        public async Task DeleteRoleAsync(Guid roleId)
        {
            await ThrowIfRoleWithIdDoesNotExistAsync(roleId);

            var roleToDelete = await _roleMgr.FindByIdAsync(roleId.ToString());

            var operationResult = await _roleMgr.DeleteRoleAsync(roleToDelete);
             ThrowIfFailed(operationResult);
        }
    }
}
