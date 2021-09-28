using System.Linq;
using ThermalClub.Modules.AdminUsers.Models;
using ThermalClub.Modules.Core.Data;

namespace ThermalClub.Modules.AdminUsers.Data.Repositories
{
    public interface IAdminUserRepository : IRepository<AdminUser>
    {
        AdminUser ByEmailActiveUser(string email);
    }

    public class AdminUserRepository : Repository<AdminUser>, IAdminUserRepository
    {
        public AdminUserRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public AdminUser ByEmailActiveUser(string email)
        {
            return AsNoTracking.FirstOrDefault(t => t.Email == email && t.IsActive);
        }
    }
}