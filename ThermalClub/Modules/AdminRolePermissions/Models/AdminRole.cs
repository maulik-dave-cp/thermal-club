using System;
using System.Collections.Generic;
using System.Linq;
using ThermalClub.Modules.Core.Data;

namespace ThermalClub.Modules.AdminRolePermissions.Models
{
	public class AdminRole : ITrackable
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string SystemName { get; set; }
		public string Description { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public List<AdminUsersAdminRoles> AdminUsersAdminRoles { get; set; }

		public List<AdminRolesAdminPermissions> AdminRolesAdminPermissionses { get; set; }

		public static AdminRolesAdminPermissions[] AddPermissionsToRole(AdminRole adminRole, AdminPermission[] adminPermissions)
		{
			return adminPermissions.Select(adminPermission => AdminRolesAdminPermissions.Create(adminRole, adminPermission)).ToArray();
		}

		public static AdminRolesAdminPermissions[] AddPermissionsToRole(AdminRole adminRole, int[] adminPermissions)
		{
			return adminPermissions.Select(adminPermission => AdminRolesAdminPermissions.Create(adminRole, adminPermission)).ToArray();
		}
	}
}
