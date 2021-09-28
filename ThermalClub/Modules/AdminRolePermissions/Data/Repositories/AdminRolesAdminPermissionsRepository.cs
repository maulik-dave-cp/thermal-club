using ThermalClub.Modules.AdminRolePermissions.Models;
using ThermalClub.Modules.Core.Data;

namespace ThermalClub.Modules.AdminRolePermissions.Data.Repositories
{
    public interface IAdminRolesAdminPermissionsRepository : IRepository<AdminRolesAdminPermissions>
    {
    }

    public class AdminRolesAdminPermissionsRepository : Repository<AdminRolesAdminPermissions>, IAdminRolesAdminPermissionsRepository
    {
        public AdminRolesAdminPermissionsRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}