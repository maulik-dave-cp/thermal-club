using ThermalClub.Modules.AdminRolePermissions.Models;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.Data.Seed;
using ThermalClub.Modules.EmailTemplates.Data.Permissions;
using System.Linq;

namespace ThermalClub.Modules.EmailTemplates.Data.Seed
{
    public class EmailTemplatePermissionsSeed : BaseSeed
    {
        public EmailTemplatePermissionsSeed(SqlContext context) : base(context)
        {
            OrderId = 31;
        }

        public override void Seed()
        {
            if (Context.Set<AdminPermission>().Any(w => w.Name == EmailTemplatePermission.List))
                return;

            var listPermission = AdminPermission.Create("Email Templates", EmailTemplatePermission.List);
            Context.Set<AdminPermission>().Add(listPermission);
            Context.SaveChanges();

            var insertUpdateDeletePermissions = AdminPermission.CreateInsertUpdateDelete("Email Templates",
                EmailTemplatePermission.List, listPermission.Id);
            Context.Set<AdminPermission>().AddRange(insertUpdateDeletePermissions);
            Context.SaveChanges();

            UpdateAdministratorRoleWithPermissions(listPermission);
            UpdateAdministratorRoleWithPermissions(insertUpdateDeletePermissions);
        }
    }
}