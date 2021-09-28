using System.Collections.Generic;
using System.Linq;

namespace ThermalClub.Modules.AdminRolePermissions.Models.DTOs
{
    public class AdminPermissionDropDownDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string _displayName { get; set; }
        public int? Left { get; set; }
        public int? Right { get; set; }
        public int Depth { get; set; }
        public int? ParentId { get; set; }

        public string DisplayName
        {
            get { return string.Concat(Enumerable.Repeat("| - ", Depth)) + _displayName + " [" + Name + "]"; }
            set { _displayName = value; }
        }

        public List<string> Children { get; set; }
    }
}