using AutoMapper;
using HelloSocNetw_BLL.EntitiesDTO;
using HelloSocNetw_BLL.Interfaces;
using HelloSocNetw_PL.Infrastructure;
using HelloSocNetw_PL.Infrastructure.Interfaces;
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
    [Authorize(Roles = "Admin")]
    public class RolesController : ApiController
    {
        private readonly IRoleService _roleSvc;
        private readonly ILogger _lgr;
        private readonly IMapper _mpr;
        private readonly ICurrentUserService _curUserSvc;

        public RolesController(
            IRoleService roleService, 
            ILogger<RolesController> logger,
            IMapper mapper,
            ICurrentUserService currentUserService)
        {
            _roleSvc = roleService;
            _lgr = logger;
            _mpr = mapper;
            _curUserSvc = currentUserService;
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
            var rolesDto = await _roleSvc.GetRolesAsync();
            var roleModels = _mpr.Map<IEnumerable<RoleModel>>(rolesDto);
            return roleModels.ToList();
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
            await _roleSvc.CreateRole(newRole.Name);

            return NoContent();
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

            var roleDto = _mpr.Map<AppRoleDTO>(roleModel);
            await _roleSvc.UpdateRoleAsync(roleDto);

            return NoContent();
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
            await _roleSvc.DeleteRoleAsync(roleId.ToString());

            return NoContent();
        }
    }           
}

    