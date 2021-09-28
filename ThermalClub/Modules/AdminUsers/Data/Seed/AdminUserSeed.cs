using ThermalClub.Modules.AdminRolePermissions.Models;
using ThermalClub.Modules.AdminUsers.Models;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.Encryption;
using System;
using System.Linq;
using ThermalClub.Modules.Core.Data.Seed;

namespace ThermalClub.Modules.AdminUsers.Data.Seed
{
    public class AdminUserSeed : BaseSeed
    {
        private readonly SqlContext _context;

        public AdminUserSeed(SqlContext context) : base(context)
        {
            _context = context;

            OrderId = 30;
        }

        public override void Seed()
        {
            BasicAdminUsers();

            _context.SaveChanges();
        }

        public void BasicAdminUsers()
        {
            if (_context.Set<AdminUser>().Any()) return;

            var salt = SecurityHelper.GenerateSalt();
            var adminUserKeyur = new AdminUser()
            {
                Name = "Keyur Ajmera",
                Email = "ajmerainfo@gmail.com",
                Password = SecurityHelper.GenerateHash("Ajm3r@2646", salt),
                Salt = salt,
                CreatedAt = DateTime.Now,
                IsActive = true
            };
            _context.Set<AdminUser>().Add(adminUserKeyur);

            salt = SecurityHelper.GenerateSalt();
            var adminUserAj = new AdminUser()
            {
                Name = "Aj Admin",
                Email = "admin@example.com",
                Password = SecurityHelper.GenerateHash("admin@123", salt),
                Salt = salt,
                CreatedAt = DateTime.Now,
                IsActive = true
            };
            _context.Set<AdminUser>().Add(adminUserAj);
            _context.SaveChanges();

            var administratorRole = _context.Set<AdminRole>().FirstOrDefault(w => w.SystemName == "administrator");

            var adminUsersAdminRoleForKeyur = new AdminUsersAdminRoles
            {
                AdminRoleId = administratorRole.Id,
                AdminUserId = adminUserKeyur.Id
            };
            var adminUsersAdminRoleForAj = new AdminUsersAdminRoles
            {
                AdminRoleId = administratorRole.Id,
                AdminUserId = adminUserAj.Id
            };

            _context.Set<AdminUsersAdminRoles>().AddRange(adminUsersAdminRoleForKeyur, adminUsersAdminRoleForAj);
            _context.SaveChanges();
        }
    }
}