using System.Collections.Generic;

namespace ThermalClub.Modules.AdminRolePermissions.Models.DTOs
{
    public class AdminPermissionSequenceDto
    {
        public AdminPermissionSequenceItemDto Item { get; set; }
        public IList<AdminPermissionSequenceDto> Children { get; set; }
    }

    public class AdminPermissionSequenceItemDto
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int DisplayName { get; set; }
    }
}