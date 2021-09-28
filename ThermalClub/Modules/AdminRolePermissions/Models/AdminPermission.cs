using System;
using System.Collections.Generic;
using ThermalClub.Modules.Core.Data;

namespace ThermalClub.Modules.AdminRolePermissions.Models
{
	public class AdminPermission : ITrackable, INestedSet
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string DisplayName { get; set; }
		public string Description { get; set; }

		public int? Left { get; set; }
		public int? Right { get; set; }

		public int Depth { get; set; }

		public int? ParentId { get; set; }

		public AdminPermission Parent { get; set; }
		public List<AdminPermission> Children { get; set; }

		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public List<AdminRolesAdminPermissions> AdminRolesAdminPermissions { get; set; }

		public static AdminPermission Create(string displayName, string name, int? parentId = null)
		{
			return new AdminPermission
			{
				DisplayName = displayName,
				Name = name,
				ParentId = parentId
			};
		}

		public static AdminPermission[] CreateInsertUpdateDelete(string displayName, string name, int parentId)
		{
			return new[]
			{
				Create("Create", name + ".create", parentId),
				Create("Edit", name + ".edit", parentId),
				Create("Delete", name + ".delete", parentId)
			};
		}
	}
}