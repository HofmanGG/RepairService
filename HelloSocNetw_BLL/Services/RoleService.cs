using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HelloSocNetw_BLL.Services
{
    public class RoleService: IRoleService
    {
        private readonly IIdentityUnitOfWork _identityUow;
        private readonly IMapper _mpr;

        public RoleService(IIdentityUnitOfWork identityUnitOfWork, IMapper mapper)
        {
            _identityUow = identityUnitOfWork;
            _mpr = mapper;
        }

        public async Task<IEnumerable<AppRoleDTO>> GetRolesAsync()
        {
            var roles = await _identityUow.RoleManager.Roles.ToListAsync();
            var rolesDto = _mpr.Map<IEnumerable<AppRoleDTO>>(roles);
            return rolesDto;
        }

        public async Task<bool> CreateRole(string roleName)
        {
            var newRole = new AppRole() { Name = roleName };
            var operationResult = await _identityUow.RoleManager.CreateAsync(newRole);
            return operationResult.Succeeded;
        }

        public async Task<bool> RoleWithSuchNameExistsAsync(string roleName)
        {
            return await _identityUow.RoleManager.RoleExistsAsync(roleName);
        }

        public async Task<bool> RoleWithSuchIdExistsAsync(Guid id)
        {
            return await _identityUow.RoleManager.Roles.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> UpdateRoleAsync(AppRoleDTO roleToUpdateDto)
        {
            var roleToUpdate = await _identityUow.RoleManager.FindByIdAsync(roleToUpdateDto.Id.ToString());

            roleToUpdate.Name = roleToUpdateDto.Name;

            var operationResult = await _identityUow.RoleManager.UpdateAsync(roleToUpdate);
            return operationResult.Succeeded;
        }

        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            var roleToDelete = await _identityUow.RoleManager.FindByIdAsync(roleId);

            var operationResult = await _identityUow.RoleManager.DeleteAsync(roleToDelete);
            return operationResult.Succeeded;
        }
    }
}
