using Z.EntityFramework.Plus;

namespace ThermalClub.Modules.ErrorLogs.CacheManagers
{
    public static class ErrorLogCacheManager
    {
        public static void ClearCache()
        {
            QueryCacheManager.ExpireTag(Name);
        }

        public static string Name { get; set; } = "ErrorLog";
    }
}