using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_PL.Infrastructure;
using HelloSocNetw_PL.Models;
using HelloSocNetw_PL.Models.RoleModels;
using HelloSocNetw_PL.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloSocNetw_PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateModel]
    [Authorize(Roles = "Admin")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleSvc;
        private readonly IMapper _mpr;
        private readonly ILogger _lgr;

        public RolesController(IRoleService roleService, IMapper mapper, ILogger<RolesController> logger)
        {
            _roleSvc = roleService;
            _mpr = mapper;
            _lgr = logger;
        }

        /// <summary>
        /// Creates new role
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /
        ///     {
        ///        "name": "SuperAdmin"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">If role is successfully created</response>
        /// <response code="400">If model is not valid</response>
        /// <response code="409">If role with such name already exists</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpPost]
        [ProducesResponseType(204), ProducesResponseType(400), ProducesResponseType(409)]
        public async Task<IActionResult> CreateRole(NewRoleModel newRole)
        {
            var roleWithSuchNameExists = await _roleSvc.RoleWithSuchNameExistsAsync(newRole.Name);
            if (roleWithSuchNameExists)
            {
                _lgr.LogInformation(LoggingEvents.InsertItemConflict, "CreateRole() CONFLICT");
                return Conflict();
            }

            var successfullyAdded = await _roleSvc.CreateRole(newRole.Name);
            if (successfullyAdded)
            {
                _lgr.LogInformation(LoggingEvents.InsertItem, "Role is created");
                return NoContent();
            }
            else
            {
                _lgr.LogInformation(LoggingEvents.InsertItemBadRequest, "CreateRole() BAD REQUEST");
                return BadRequest();
            }
        }

        /// <summary>
        /// Gets all roles
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Returnes all roles</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ResponseCache(Duration = 3600)]
        public async Task<ActionResult<IEnumerable<RoleModel>>> GetRoles()
        {
            _lgr.LogInformation(LoggingEvents.ListItems, "Getting All Roles");

            var rolesDto = await _roleSvc.GetRolesAsync();
            var roleModels = _mpr.Map<IEnumerable<RoleModel>>(rolesDto);
            return roleModels.ToList();
        }

        /// <summary>
        /// Updates role
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /{roleId}
        ///     {
        ///        "id": 5,
        ///        "name": "SuperAdmin"
        ///     }
        ///
        /// </remarks>
        /// <response code="204">if role is successfully updated</response>
        /// <response code="400">If model is not valid</response>
        /// <response code="404">If role with roleId doesnot exist</response>
        /// <response code="409">If role with such name already exists</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpPut("{roleId}")]
        [ProducesResponseType(204), ProducesResponseType(400), ProducesResponseType(404), ProducesResponseType(409)]
        public async Task<IActionResult> UpdateRole(Guid roleId, UpdateRoleModel roleModel)
        {
            if (roleId != roleModel.Id)
                return BadRequest();

            var roleWithSuchNameExists = await _roleSvc.RoleWithSuchNameExistsAsync(roleModel.Name);
            if (roleWithSuchNameExists)
            {
                _lgr.LogInformation(LoggingEvents.UpdateItemConflict, "UpdateRole({roleId}) CONFLICT", roleId);
                return Conflict();
            }

            var roleWithSuchIdExists = await _roleSvc.RoleWithSuchIdExistsAsync(roleId);
            if (!roleWithSuchIdExists)
            {
                _lgr.LogWarning(LoggingEvents.UpdateItemNotFound, "UpdateRole({roleId}) NOT FOUND", roleId);
                return NotFound();
            }

            var roleDto = _mpr.Map<AppRoleDTO>(roleModel);

            var successfullyUpdated = await _roleSvc.UpdateRoleAsync(roleDto);
            if (successfullyUpdated)
            {
                _lgr.LogWarning(LoggingEvents.UpdateItemBadRequest, "Role {roleId}) Updated", roleId);
                return NoContent();
            }
            else
            {
                _lgr.LogWarning(LoggingEvents.InsertItemBadRequest, "UpdateRole({roleId}) BAD REQUEST", roleId);
                return BadRequest();
            }
        }

        /// <summary>
        /// Deletes role
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /{roleId}
        ///     {
        ///     }
        ///
        /// </remarks>
        /// <response code="204">Returnes all countries</response>
        /// <response code="400">If role is not deleted</response>
        /// <response code="404">If role with such id doesnot exist</response>
        /// <response code="500">If an exception on server is thrown</response>
        [HttpDelete("{roleId}")]
        [ProducesResponseType(204), ProducesResponseType(400), ProducesResponseType(404)]
        public async Task<IActionResult> DeleteRole(Guid roleId)
        {
            var roleToDeleteExists = await _roleSvc.RoleWithSuchIdExistsAsync(roleId);
            if (!roleToDeleteExists)
            {
                _lgr.LogWarning(LoggingEvents.DeleteItemNotFound, "DeleteRole({roleId}) NOT FOUND", roleId);
                return NotFound();
            }

            var successfullyDeleted = await _roleSvc.DeleteRoleAsync(roleId.ToString());
            if (successfullyDeleted)
            {
                _lgr.LogInformation(LoggingEvents.DeleteItem, "Role {roleId} is deleted", roleId);
                return NoContent();
            }
            else
            {
                _lgr.LogWarning(LoggingEvents.DeleteItemBadRequest, "DeleteRole({roleId}) BAD REQUEST", roleId);
                return BadRequest();
            }
        }
    }
}

    