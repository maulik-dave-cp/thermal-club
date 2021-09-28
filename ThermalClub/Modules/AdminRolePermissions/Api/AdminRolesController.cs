using ThermalClub.Modules.AdminRolePermissions.Data.Permissions;
using ThermalClub.Modules.AdminRolePermissions.Models.DTOs;
using ThermalClub.Modules.AdminRolePermissions.Services;
using ThermalClub.Modules.Core.Api;
using ThermalClub.Modules.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ThermalClub.Modules.AdminRolePermissions.Api
{
	[Route("api/admin/admin-roles")]
	[AuthorizeApiAdminUser]
	public class AdminRolesController : BaseApiController
	{
		private readonly IAdminRoleService _adminRoleService;

		public AdminRolesController(
			IAdminRoleService adminRoleService)
		{
			_adminRoleService = adminRoleService;
		}

		[HttpGet("all")]
		public IActionResult All()
		{
			return Result(_adminRoleService.GetRoles());
		}

		[HttpGet("")]
		[AuthorizeApiAdminUser(permissions: new[] { AdminRolePermission.List })]
		public IActionResult Get([FromQuery] AdminRoleFilterDto dto)
		{
			return Result(_adminRoleService.List(dto));
		}

		[HttpPost("")]
		[AuthorizeApiAdminUser(permissions: new[] { AdminRolePermission.Create })]
		public IActionResult Post([FromBody] AdminRoleCreateDto dto)
		{
			return Result(_adminRoleService.Create(dto));
		}

		[HttpGet("{id:int}")]
		[AuthorizeApiAdminUser(permissions: new[] { AdminRolePermission.Edit })]
		public IActionResult Get(int id)
		{
			return Result(_adminRoleService.ById(id));
		}

		[HttpPut("{id:int}")]
		[AuthorizeApiAdminUser(permissions: new[] { AdminRolePermission.Edit })]
		public IActionResult Put(int id, [FromBody] AdminRoleEditDto dto)
		{
			return Result(_adminRoleService.Edit(id, dto));
		}

		[HttpPost("delete")]
		[AuthorizeApiAdminUser(permissions: new[] { AdminRolePermission.Delete })]
		public IActionResult Delete([FromBody] IdsDto dto)
		{
			return Result(_adminRoleService.Delete(dto.Ids));
		}
	}
}