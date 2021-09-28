using ThermalClub.Modules.AdminRolePermissions.Models;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.Data.Seed;
using ThermalClub.Modules.ErrorLogs.Data.Permissions;
using System.Linq;

namespace ThermalClub.Modules.ErrorLogs.Data.Seed
{
    public class ErrorLogPermissionsSeed : BaseSeed
    {
        public ErrorLogPermissionsSeed(SqlContext context) : base(context)
        {
            OrderId = 61;
        }

        public override void Seed()
        {
            if (Context.Set<AdminPermission>().Any(w => w.Name == ErrorLogPermission.List))
                return;

            var listPermission = AdminPermission.Create("ErrorLog", ErrorLogPermission.List);
            Context.Set<AdminPermission>().Add(listPermission);
            Context.SaveChanges();

            UpdateAdministratorRoleWithPermissions(listPermission);
        }
    }
}