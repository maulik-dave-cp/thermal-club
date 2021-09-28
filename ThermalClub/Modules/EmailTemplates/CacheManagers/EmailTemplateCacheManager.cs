using Z.EntityFramework.Plus;

namespace ThermalClub.Modules.EmailTemplates.CacheManagers
{
    public static class EmailTemplateCacheManager
    {
        public static void ClearCache()
        {
            QueryCacheManager.ExpireTag(Name);
        }

        public static readonly string Name = "EmailTemplates";
    }
}