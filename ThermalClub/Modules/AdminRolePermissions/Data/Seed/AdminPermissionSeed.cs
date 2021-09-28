using ThermalClub.Modules.AdminRolePermissions.Data.Permissions;
using ThermalClub.Modules.AdminRolePermissions.Models;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.Data.Seed;
using System.Linq;

namespace ThermalClub.Modules.AdminRolePermissions.Data.Seed
{
    public class AdminPermissionSeed : BaseSeed
    {
        public AdminPermissionSeed(SqlContext context) : base(context)
        {
            OrderId = 2;
        }

        public override void Seed()
        {
            CreateAdminPermissions();
        }

        private void CreateAdminPermissions()
        {
            if (Context.Set<AdminPermission>().Any(w => w.Name == AdminPermissionPermission.List))
                return;

            var listPermission = AdminPermission.Create("Admin Permissions", AdminPermissionPermission.List);
            Context.Set<AdminPermission>().Add(listPermission);
            Context.SaveChanges();

            var insertUpdateDeletePermissions = AdminPermission.CreateInsertUpdateDelete("Admin Permissions",
                AdminPermissionPermission.List, listPermission.Id);
            Context.Set<AdminPermission>().AddRange(insertUpdateDeletePermissions);
            Context.SaveChanges();

            UpdateAdministratorRoleWithPermissions(listPermission);
            UpdateAdministratorRoleWithPermissions(insertUpdateDeletePermissions);
        }
    }
}