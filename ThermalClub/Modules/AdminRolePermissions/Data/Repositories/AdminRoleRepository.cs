using System.Collections.Generic;
using System.Linq;
using ThermalClub.Modules.AdminRolePermissions.CacheManagers;
using ThermalClub.Modules.AdminRolePermissions.Models;
using ThermalClub.Modules.Core.Data;
using ThermalClub.Modules.Core.DTOs;
using Z.EntityFramework.Plus;

namespace ThermalClub.Modules.AdminRolePermissions.Data.Repositories
{
    public interface IAdminRoleRepository : IRepository<AdminRole>
    {
        IList<IdNameDto> Cached();
    }

    public class AdminRoleRepository : Repository<AdminRole>, IAdminRoleRepository
    {
        public AdminRoleRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public IList<IdNameDto> Cached()
        {
            return AsNoTracking
                .Select(s => new IdNameDto {Id = s.Id, Name = s.Name})
                .OrderBy(o => o.Name)
                .FromCache(AdminRoleCacheManager.Name)
                .ToList();
        }
    }
}