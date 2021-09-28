using System;
using System.Linq;
using ThermalClub.Modules.AdminRolePermissions.Data.Permissions;
using ThermalClub.Modules.AdminRolePermissions.Models;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.Data.Seed;
using ThermalClub.Modules.Core.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ThermalClub.Modules.AdminRolePermissions.Data.Seed
{
    public class AdminRoleSeed : BaseSeed
    {
        private readonly SqlContext _context;

        public AdminRoleSeed(SqlContext context) : base(context)
        {
            _context = context;

            OrderId = 10;
        }

        public override void Seed()
        {
            CreateRoles();
        }

        public void CreateRoles()
        {
            if (_context.Set<AdminRole>().Any(w => w.SystemName == "administrator")) 
                return;

            var adminRole = new AdminRole()
            {
                Name = "Administrator",
                SystemName = "administrator",
                CreatedAt = DateTime.Now
            };

            _context.Set<AdminRole>().Add(adminRole);
            _context.SaveChanges();
        }
    }
}