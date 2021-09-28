using System;
using System.Collections.Generic;
using ThermalClub.Modules.AdminRolePermissions.Models;
using ThermalClub.Modules.Core.Data;

namespace ThermalClub.Modules.AdminUsers.Models
{
    public class AdminUser : ITrackable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public bool IsActive { get; set; }
        public string ForgotPasswordToken { get; set; }
        public DateTime? LastLoginAt { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public List<AdminUsersAdminRoles> AdminUsersAdminRoles { get; set; }
    }
}