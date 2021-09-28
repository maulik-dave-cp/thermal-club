using System.Collections.Generic;
using ThermalClub.Modules.AdminRolePermissions.Data.Permissions;
using ThermalClub.Modules.AdminRolePermissions.Models.DTOs;
using ThermalClub.Modules.AdminRolePermissions.Services;
using ThermalClub.Modules.Core.Api;
using ThermalClub.Modules.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ThermalClub.Modules.AdminRolePermissions.Api
{
    [Route("api/admin/admin-permissions")]
    public class AdminPermissionsController : BaseApiController
    {
        private readonly IAdminPermissionService _adminPermissionService;

        public AdminPermissionsController(
            IAdminPermissionService adminPermissionService)
        {
            _adminPermissionService = adminPermissionService;
        }

        [HttpGet("all")]
        public IActionResult All()
        {
            return Result(_adminPermissionService.GetAdminPermissions());
        }

        [AuthorizeApiAdminUser(permissions: new[] { AdminPermissionPermission.List })]
        [HttpGet("")]
        public IActionResult Get([FromQuery]AdminPermissionFilterDto dto)
        {
            return Result(_adminPermissionService.List(dto));
        }

        [AuthorizeApiAdminUser(permissions: new[] { AdminPermissionPermission.Create })]
        [HttpPost("")]
        public IActionResult Post([FromBody]AdminPermissionDto dto)
        {
            return Result(_adminPermissionService.Create(dto));
        }

        [AuthorizeApiAdminUser(permissions: new[] { AdminPermissionPermission.Edit })]
        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            return Result(_adminPermissionService.ById(id));
        }

        [AuthorizeApiAdminUser(permissions: new[] { AdminPermissionPermission.Edit })]
        [HttpPut("{id:int}")]
        public IActionResult Put(int id, [FromBody]AdminPermissionDto dto)
        {
            return Result(_adminPermissionService.Edit(id, dto));
        }

        [AuthorizeApiAdminUser(permissions: new[] { AdminPermissionPermission.Delete })]
        [HttpPost("delete")]
        public IActionResult Delete([FromBody]IdsDto dto)
        {
            return Result(_adminPermissionService.Delete(dto.Ids));
        }

        [AuthorizeApiAdminUser(permissions: new[] { AdminPermissionPermission.Edit })]
        [HttpGet("sequence")]
        public IActionResult GetSequence()
        {
            return Result(_adminPermissionService.GetSequenceData());
        }

        [AuthorizeApiAdminUser(permissions: new[] { AdminPermissionPermission.Edit })]
        [HttpPost("save-sequence")]
        public void SaveSequence([FromBody] IList<AdminPermissionSequenceDto> data)
        {
            _adminPermissionService.SaveSequenceData(data);
        }
    }
}