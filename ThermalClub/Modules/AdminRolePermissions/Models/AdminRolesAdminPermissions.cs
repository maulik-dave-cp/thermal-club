using Microsoft.EntityFrameworkCore;

namespace ThermalClub.Modules.AdminRolePermissions.Models
{
	public class AdminRolesAdminPermissions
	{
		public int AdminRoleId { get; set; }
		public AdminRole AdminRole { get; set; }

		public int AdminPermissionId { get; set; }
		public AdminPermission AdminPermission { get; set; }

		public static AdminRolesAdminPermissions Create(AdminRole adminRole, AdminPermission adminPermission)
		{
			return new AdminRolesAdminPermissions
			{
				AdminRoleId = adminRole.Id,
				AdminPermissionId = adminPermission.Id
			};
		}

		public static AdminRolesAdminPermissions Create(AdminRole adminRole, int adminPermissionId)
		{
			return new AdminRolesAdminPermissions
			{
				AdminRoleId = adminRole.Id,
				AdminPermissionId = adminPermissionId
			};
		}
	}
}