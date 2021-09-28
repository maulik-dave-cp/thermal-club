using Z.EntityFramework.Plus;

namespace ThermalClub.Modules.AdminUsers.CacheManagers
{
    public static class AdminUserCacheManager
    {
        public static void ClearCache()
        {
            QueryCacheManager.ExpireTag(Name);
        }

        public static string Name { get; set; } = "AdminUsers";
    }
}