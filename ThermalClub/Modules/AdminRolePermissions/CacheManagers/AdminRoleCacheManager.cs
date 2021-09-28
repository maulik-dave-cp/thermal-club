using Z.EntityFramework.Plus;

namespace ThermalClub.Modules.AdminRolePermissions.CacheManagers
{
    public static class AdminRoleCacheManager
    {
        public static void ClearCache()
        {
            QueryCacheManager.ExpireTag(Name);
        }

        public static string Name { get; set; } = "List";
    }
}