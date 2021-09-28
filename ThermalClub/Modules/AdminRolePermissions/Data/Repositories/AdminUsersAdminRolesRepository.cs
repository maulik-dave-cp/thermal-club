using ThermalClub.Modules.AdminRolePermissions.Models;
using ThermalClub.Modules.Core.Data;

namespace ThermalClub.Modules.AdminRolePermissions.Data.Repositories
{
    public interface IAdminUsersAdminRolesRepository : IRepository<AdminUsersAdminRoles>
    {
    }

    public class AdminUsersAdminRolesRepository : Repository<AdminUsersAdminRoles>, IAdminUsersAdminRolesRepository
    {
        public AdminUsersAdminRolesRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }
    }
}