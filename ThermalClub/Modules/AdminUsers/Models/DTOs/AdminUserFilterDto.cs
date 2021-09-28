using System;
using ThermalClub.Modules.Core.Filters;

namespace ThermalClub.Modules.AdminUsers.Models.DTOs
{
    public class AdminUserFilterDto : BaseFilterDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public int? RoleId { get; set; }
        public DateTime? FromLastLoginAt { get; set; }
        public DateTime? ToLastLoginAt { get; set; }
    }
}