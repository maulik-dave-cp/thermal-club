using System.Linq;
using ThermalClub.Modules.AdminRolePermissions.Models;
using ThermalClub.Modules.AdminUsers.Data.Permissions;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.Data.Seed;

namespace ThermalClub.Modules.AdminUsers.Data.Seed
{
    public class AjUserAdminPermissionsSeed : BaseSeed
    {
        public AjUserAdminPermissionsSeed(SqlContext context) : base(context)
        {
            OrderId = 31;
        }

        public override void Seed()
        {
            if (Context.Set<AdminPermission>().Any(w => w.Name == AdminUserPermission.List))
                return;

            var listPermission = AdminPermission.Create("Admin Users", AdminUserPermission.List);
            Context.Set<AdminPermission>().Add(listPermission);
            Context.SaveChanges();

            var insertUpdateDeletePermissions = AdminPermission.CreateInsertUpdateDelete("Admin Users",
                AdminUserPermission.List, listPermission.Id);
            Context.Set<AdminPermission>().AddRange(insertUpdateDeletePermissions);
            Context.SaveChanges();

            UpdateAdministratorRoleWithPermissions(listPermission);
            UpdateAdministratorRoleWithPermissions(insertUpdateDeletePermissions);
        }
    }
}