using ThermalClub.Modules.AdminUsers.Data.Permissions;
using ThermalClub.Modules.AdminUsers.Models.DTOs;
using ThermalClub.Modules.AdminUsers.Services;
using ThermalClub.Modules.Core.Api;
using ThermalClub.Modules.Core.Authorization;
using ThermalClub.Modules.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ThermalClub.Modules.AdminUsers.Api
{
    [Route("api/admin/admin-users")]
    [AuthorizeApiAdminUser]
    public class AdminUsersController : BaseApiController
    {
	    private readonly Auth _auth;
	    private readonly IAdminUserService _adminUserService;

        public AdminUsersController(
            Auth auth,
            IAdminUserService adminUserService)
        {
	        _auth = auth;
	        _adminUserService = adminUserService;
        }

        [HttpGet("")]
        [AuthorizeApiAdminUser(permissions: new[] { AdminUserPermission.List })]
        public IActionResult Get([FromQuery] AdminUserFilterDto dto)
        {
            return Result(_adminUserService.List(dto));
        }

        [HttpPost("")]
        [AuthorizeApiAdminUser(permissions: new[] { AdminUserPermission.Create })]
        public IActionResult Post([FromBody] AdminUserCreateDto dto)
        {
            return Result(_adminUserService.Create(dto));
        }

        [HttpGet("{id:int}")]
        [AuthorizeApiAdminUser(permissions: new[] { AdminUserPermission.Edit })]
        public IActionResult Get(int id)
        {
            return Result(_adminUserService.ById(id));
        }

        [HttpPut("{id:int}")]
        [AuthorizeApiAdminUser(permissions: new[] { AdminUserPermission.Edit })]
        public IActionResult Put(int id, [FromBody] AdminUserEditDto dto)
        {
            return Result(_adminUserService.Edit(id, dto));
        }

        [HttpPost("active")]
        [AuthorizeApiAdminUser(permissions: new[] { AdminUserPermission.Edit })]
        public IActionResult Active([FromBody] IdsDto dto)
        {
            return Result(_adminUserService.Active(dto.Ids));
        }

        [HttpPost("inactive")]
        [AuthorizeApiAdminUser(permissions: new[] { AdminUserPermission.Edit })]
        public IActionResult Inactive([FromBody] IdsDto dto)
        {
            var user = _auth.LoggedInUser();
            return Result(_adminUserService.Inactive(dto.Ids, user.Id));
        }

        [HttpPost("delete")]
        [AuthorizeApiAdminUser(permissions: new[] { AdminUserPermission.Delete })]
        public IActionResult Delete([FromBody] IdsDto dto)
        {
            var user = _auth.LoggedInUser();
            return Result(_adminUserService.Delete(dto.Ids, user.Id));
        }

        [HttpGet("all")]
        public IActionResult All()
        {
            return Result(_adminUserService.GetAdminUsers());
        }
    }
}