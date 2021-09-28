using System.Linq;
using ThermalClub.Modules.AdminRolePermissions.Data.Permissions;
using ThermalClub.Modules.AdminRolePermissions.Models;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.Data.Seed;

namespace ThermalClub.Modules.AdminRolePermissions.Data.Seed
{
    public class AdminRolePermissionSeed : BaseSeed
    {
        public AdminRolePermissionSeed(SqlContext context) : base(context)
        {
            OrderId = 11;
        }

        public override void Seed()
        {
            CreateAdminPermissions();
        }

        private void CreateAdminPermissions()
        {
            if (Context.Set<AdminPermission>().Any(w => w.Name == AdminRolePermission.List))
                return;

            var listPermission = AdminPermission.Create("Admin Roles", AdminRolePermission.List);
            Context.Set<AdminPermission>().Add(listPermission);
            Context.SaveChanges();

            var insertUpdateDeletePermissions = AdminPermission.CreateInsertUpdateDelete("Admin Roles",
                AdminRolePermission.List, listPermission.Id);
            Context.Set<AdminPermission>().AddRange(insertUpdateDeletePermissions);
            Context.SaveChanges();

            UpdateAdministratorRoleWithPermissions(listPermission);
            UpdateAdministratorRoleWithPermissions(insertUpdateDeletePermissions);
        }
    }
}