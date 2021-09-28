using System.Linq;
using ThermalClub.Modules.AdminUsers.Models;
using ThermalClub.Modules.AdminUsers.Models.DTOs;
using ThermalClub.Modules.Core.Filters;

namespace ThermalClub.Modules.AdminUsers.Filters
{
    public class AdminUserFilter : BaseFilter<AdminUser, AdminUserFilterDto>
    {
        public AdminUserFilter(IQueryable<AdminUser> query, AdminUserFilterDto dto) : base(query, dto)
        {
        }

        internal void Name()
        {
            Query = Query.Where(w => w.Name.Contains(Dto.Name));
        }

        internal void Email()
        {
            Query = Query.Where(w => w.Email.Contains(Dto.Email));
        }

        internal void RoleId()
        {
            Query = Query.Where(w => w.AdminUsersAdminRoles.Any(r => r.AdminRoleId == Dto.RoleId));
        }

        internal void IsActive()
        {
            Query = Query.Where(w => w.IsActive == Dto.IsActive);
        }

        internal void FromLastLoginAt()
        {
            Query = Query.Where(w => w.LastLoginAt >= Dto.FromLastLoginAt);
        }

        internal void ToLastLoginAt()
        {
            Query = Query.Where(w => w.LastLoginAt <= Dto.ToLastLoginAt);
        }

        internal void FromCreatedAt()
        {
            Query = Query.Where(w => w.CreatedAt >= Dto.FromCreatedAt);
        }

        internal void ToCreatedAt()
        {
            Query = Query.Where(w => w.CreatedAt <= Dto.ToCreatedAt);
        }
    }
}