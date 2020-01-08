using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Infrastructure;
using HelloSocNetw_BLL.Infrastructure.Exceptions;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_DAL.Entities;
using HelloSocNetw_DAL.Infrastructure.Exceptions;
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

        public async Task CreateRole(string roleName)
        {
            var doesRoleWithSuchNameAlredyExist = await _identityUow.RoleManager.RoleExistsAsync(roleName);
            if (doesRoleWithSuchNameAlredyExist)
                throw new ConflictException(nameof(AppRole), roleName);

            var newRole = new AppRole() { Name = roleName };
            var operationResult = await _identityUow.RoleManager.CreateAsync(newRole);
            if (!operationResult.Succeeded)
                throw new DBOperationException("Role creating went wrong");
        }

        public async Task UpdateRoleAsync(AppRoleDTO roleToUpdateDto)
        {
            var roleToUpdate = await _identityUow.RoleManager.FindByIdAsync(roleToUpdateDto.Id.ToString());
            if (roleToUpdate == null)
                throw new NotFoundException(nameof(AppRole), roleToUpdateDto.Id);

            var doesRoleWithSuchNameAlredyExist = await _identityUow.RoleManager.RoleExistsAsync(roleToUpdateDto.Name);
            if (doesRoleWithSuchNameAlredyExist)
                throw new ConflictException(nameof(AppRole), roleToUpdateDto.Name);;

            roleToUpdate.Name = roleToUpdateDto.Name;

            var operationResult = await _identityUow.RoleManager.UpdateAsync(roleToUpdate);
            if (!operationResult.Succeeded)
                throw new DBOperationException("Role updating went wrong");
        }

        public async Task DeleteRoleAsync(string roleId)
        {
            var roleToDelete = await _identityUow.RoleManager.FindByIdAsync(roleId);
            if (roleToDelete == null)
                throw new NotFoundException(nameof(AppRole), roleId);

            var operationResult = await _identityUow.RoleManager.DeleteAsync(roleToDelete);
            if (!operationResult.Succeeded)
                throw new DBOperationException("Role deleting went wrong");
        }

        public async Task<bool> RoleWithSuchIdExistsAsync(Guid id)
        {
            return await _identityUow.RoleManager.Roles.AnyAsync(r => r.Id == id);
        }

        public async Task<bool> RoleWithSuchNameExistsAsync(string roleName)
        {
            return await _identityUow.RoleManager.RoleExistsAsync(roleName);
        }
    }
}
